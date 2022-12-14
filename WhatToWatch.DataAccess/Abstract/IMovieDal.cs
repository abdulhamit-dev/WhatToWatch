using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Core.DataAccess.Abstract;
using WhatToWatch.Entities.Conrete;
using WhatToWatch.Entities.Dtos.Movie;
using WhatToWatch.Entities.Dtos.MovieNoteAndVote;

namespace WhatToWatch.DataAccess.Abstract
{
    public interface IMovieDal : IEntityRepository<Movie>
    {
        List<MovieNoteVoteDto> GetMovies(int id,int userId);
    }
}
