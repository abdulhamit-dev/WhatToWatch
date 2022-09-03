using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using WhatToWatch.Business.Abstract;
using WhatToWatch.Entities.Dtos.MovieNoteAndVote;
using WhatToWatch.Entities.Dtos.User;

namespace WhatToWatch.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieNoteAndVoteController : ControllerBase
    {
        private readonly IMovieNoteAndVoteService _movieNoteAndVoteService;
        private IValidator<MovieNoteAndVoteAddDto> _validator;

        public MovieNoteAndVoteController(IMovieNoteAndVoteService movieNoteAndVoteService, IValidator<MovieNoteAndVoteAddDto> validator)
        {
            _movieNoteAndVoteService = movieNoteAndVoteService;
            _validator = validator;
        }

        [HttpPost("Add")]
        public IActionResult Add(MovieNoteAndVoteAddDto movieNoteAndVoteAddDto)
        {
            var validResult =  _validator.Validate(movieNoteAndVoteAddDto);

            if (!validResult.IsValid)
                return BadRequest(JsonConvert.SerializeObject(validResult.Errors.Select(x=>x.ErrorMessage).FirstOrDefault()));
        
            var result = _movieNoteAndVoteService.Add(movieNoteAndVoteAddDto);
            return Ok(result);
        }
    }
}
