using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Entities.Conrete;
using WhatToWatch.Entities.Dtos.MovieNoteAndVote;

namespace WhatToWatch.Business.ValidationRules
{
    public class MovieNoteAndVoteValidator : AbstractValidator<MovieNoteAndVoteAddDto>
    {
        public MovieNoteAndVoteValidator()
        {
            RuleFor(x=>x.Vote).InclusiveBetween(1,10).NotNull();
        }
    }
}
