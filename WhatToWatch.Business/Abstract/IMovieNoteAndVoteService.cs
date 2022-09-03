using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Core.Utilities.Result;
using WhatToWatch.Entities.Conrete;
using WhatToWatch.Entities.Dtos.MovieNoteAndVote;

namespace WhatToWatch.Business.Abstract
{
    public interface IMovieNoteAndVoteService
    {
        IResult Add(MovieNoteAndVoteAddDto movieNoteAndVoteAddDto);
    }
}
