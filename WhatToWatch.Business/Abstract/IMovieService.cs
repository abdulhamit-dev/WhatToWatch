using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Core.Utilities.Result;
using WhatToWatch.Entities.Conrete;
using WhatToWatch.Entities.Dtos.Movie;

namespace WhatToWatch.Business.Abstract
{
    public interface IMovieService
    {
        Task<MovieResultDto> GetMovieData();
        Task<int> SaveData();
        IDataResult<MovieNoteAndVoteResponseDto> GetById(int id);
        IDataResult<List<Movie>> GetAll(int page);

    }
}
