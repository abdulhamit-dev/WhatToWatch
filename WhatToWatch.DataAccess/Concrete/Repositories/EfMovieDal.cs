using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Core.DataAccess.Concrete;
using WhatToWatch.DataAccess.Abstract;
using WhatToWatch.DataAccess.Concrete.Contexts;
using WhatToWatch.Entities.Conrete;
using WhatToWatch.Entities.Dtos.Movie;
using WhatToWatch.Entities.Dtos.MovieNoteAndVote;

namespace WhatToWatch.DataAccess.Concrete.Repositories
{
    public class EfMovieDal : EfEntityRepositoryBase<Movie, WhatToWatchContext>, IMovieDal
    {
        public List<MovieNoteVoteDto> GetMovies(int movieId,int userId)
        {

            using WhatToWatchContext context = new();
            var result = from movie in context.Movies
                         join noteAndVote in context.MovieNoteAndVotes on movie.Id equals noteAndVote.MovieId
                         where movie.Id == movieId && noteAndVote.UserId == userId
                         select new MovieNoteVoteDto
                         {
                             Id = movieId,
                             UserId = userId,
                             Note = noteAndVote.Note,
                             OverView =movie.Overview,
                             Title=movie.Title,
                             Page=movie.Page,
                             Vote = noteAndVote.Vote,
                             VoteAverage =context.MovieNoteAndVotes.Where(x=>x.MovieId==movieId).Average(x=>x.Vote),
                            
                         };

            return result.ToList();
        }
    }
}
