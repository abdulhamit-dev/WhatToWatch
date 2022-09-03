using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Core.DataAccess.Concrete;
using WhatToWatch.DataAccess.Abstract;
using WhatToWatch.DataAccess.Concrete.Contexts;
using WhatToWatch.Entities.Conrete;

namespace WhatToWatch.DataAccess.Concrete.Repositories
{
    public class EfMovieNoteAndVoteDal : EfEntityRepositoryBase<MovieNoteAndVote, WhatToWatchContext>, IMovieNoteAndVoteDal
    {
    }
}
