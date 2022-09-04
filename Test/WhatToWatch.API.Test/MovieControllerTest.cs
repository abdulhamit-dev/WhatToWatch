using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WhatToWatch.API.Controllers;
using WhatToWatch.Business.Abstract;
using WhatToWatch.Core.Utilities.Result;
using WhatToWatch.Entities.Conrete;
using WhatToWatch.Entities.Dtos.Movie;
using WhatToWatch.Entities.Dtos.MovieNoteAndVote;

namespace WhatToWatch.API.Test
{
    public class MovieControllerTest
    {
        private readonly MovieController _movieController;
        private readonly Mock<IMovieService> _mockMovieService;
        private readonly Mock<IRabbitMqService> _mockRabbitMqService;
        private readonly Mock<IMovieNoteAndVoteService> _mockMovieNoteAndVoteService;
        private readonly Mock<IValidator<MovieNoteAndVoteAddDto>> _mockValidatorService;

        private IDataResult<MovieNoteAndVoteResponseDto> _movieNoteAndVoteResultData;
        private IDataResult<List<Movie>> _movieResultData;
        public MovieControllerTest()
        {
            _mockMovieService = new Mock<IMovieService>();
            _mockRabbitMqService = new Mock<IRabbitMqService>();
            _mockMovieNoteAndVoteService = new Mock<IMovieNoteAndVoteService>();
            _mockValidatorService = new Mock<IValidator<MovieNoteAndVoteAddDto>>();

            _movieNoteAndVoteResultData = new SuccessDataResult<MovieNoteAndVoteResponseDto>(SampleData.movieNoteAndVoteResponseDto[0]);
            _movieResultData = new SuccessDataResult<List<Movie>>(SampleData.movies);

            _movieController = new MovieController(_mockMovieService.Object, _mockRabbitMqService.Object, _mockMovieNoteAndVoteService.Object, _mockValidatorService.Object);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_ResultOK(int id)
        {
            _mockMovieService.Setup(x => x.GetByIdDetail(id)).Returns(_movieNoteAndVoteResultData);
            var result = _movieController.GetById(id);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(id, _movieNoteAndVoteResultData.Data.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(2)]
        public void GetById_BadRequest(int id)
        {
            _mockMovieService.Setup(x => x.GetByIdDetail(id)).Returns(new ErrorDataResult<MovieNoteAndVoteResponseDto>());
            var result = _movieController.GetById(id);
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Theory]
        [InlineData(1)]
        public void GetAll_ResultOK(int page)
        {
            _mockMovieService.Setup(x => x.GetAll(page)).Returns(_movieResultData);
            var result = _movieController.GetAll(page);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var movieResult = Assert.IsAssignableFrom<IDataResult<List<Movie>>>(okResult.Value);
            Assert.Equal<int>(3, movieResult.Data.Count);
        }
    }
}
