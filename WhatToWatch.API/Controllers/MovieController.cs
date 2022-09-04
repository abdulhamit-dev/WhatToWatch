using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using WhatToWatch.Business.Abstract;
using WhatToWatch.Entities.Dtos.Movie;
using WhatToWatch.Entities.Dtos.MovieNoteAndVote;

namespace WhatToWatch.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {

        private readonly IMovieService  _movieService;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly IMovieNoteAndVoteService _movieNoteAndVoteService;
        //private HttpContextAccessor _httpContextAccessor;
        private IValidator<MovieNoteAndVoteAddDto> _validator;

        public MovieController(IMovieService movieService, IRabbitMqService rabbitMqService, IMovieNoteAndVoteService movieNoteAndVoteService, IValidator<MovieNoteAndVoteAddDto> validator)
        {
            _movieService = movieService;
            _rabbitMqService = rabbitMqService;
            _movieNoteAndVoteService = movieNoteAndVoteService;
            _validator = validator;
            //_httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll(int page)
        {
            var result = _movieService.GetAll(page);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("GetById/{id:int}")]
        public IActionResult GetById(int id)    
        {
            var result = _movieService.GetByIdDetail(id);

            var resulttest = _movieService.GetById(id);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }
       

        [HttpPost("AddNoteAndVote")]
        public IActionResult AddNoteAndVote(MovieNoteAndVoteAddDto movieNoteAndVoteAddDto)
        {
            var validResult = _validator.Validate(movieNoteAndVoteAddDto);

            if (!validResult.IsValid)
                return BadRequest(JsonConvert.SerializeObject(validResult.Errors.Select(x => x.ErrorMessage).FirstOrDefault()));

            var result = _movieNoteAndVoteService.Add(movieNoteAndVoteAddDto);
            return Ok(result);
        }

        [HttpPost("RecommendedMovieSendMail")]
        public IActionResult RecommendedMovieSendMail(int MovieId, string mail)
        {
            var name = User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            _rabbitMqService.Publish(new MovieMailCreatedEvent() { Mail = mail, MovieId = MovieId,UserName=name! });
            return Ok();
        }
    }
}
