using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Core.Entities;

namespace WhatToWatch.Entities.Conrete
{
    public class MovieNoteAndVote:IEntity
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public string Note { get; set; }
        public int Vote { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
