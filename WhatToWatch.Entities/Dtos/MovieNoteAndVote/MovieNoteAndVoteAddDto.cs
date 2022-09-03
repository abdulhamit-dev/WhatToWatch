using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatToWatch.Entities.Dtos.MovieNoteAndVote
{
    public class MovieNoteAndVoteAddDto
    {
        public int MovieId { get; set; }
        public string Note { get; set; }
        public int Vote { get; set; }
    }
}
