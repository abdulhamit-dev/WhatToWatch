using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Business.Abstract;
using WhatToWatch.Business.Constant;
using WhatToWatch.Core.Utilities.Result;
using WhatToWatch.DataAccess.Abstract;
using WhatToWatch.Entities.Conrete;
using WhatToWatch.Entities.Dtos.Movie;
using WhatToWatch.Entities.Dtos.MovieNoteAndVote;

namespace WhatToWatch.Business.Concrete
{
    public class MovieNoteAndVoteManager : IMovieNoteAndVoteService
    {
        private readonly IMovieNoteAndVoteDal _movieNoteAndVoteDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MovieNoteAndVoteManager(IMovieNoteAndVoteDal movieNoteAndVoteDal, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _movieNoteAndVoteDal = movieNoteAndVoteDal;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public IResult Add(MovieNoteAndVoteAddDto movieNoteAndVoteAddDto)
        {
            //token içerisindeki claimden id bilgisini çekiyorum.
            var userId = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var noteAndVote = _mapper.Map<MovieNoteAndVote>(movieNoteAndVoteAddDto);
            noteAndVote.UserId = Convert.ToInt32(userId);
            _movieNoteAndVoteDal.Add(noteAndVote);
            return new SuccessResult(MessagesReturn.Add);
        }
    }


}
