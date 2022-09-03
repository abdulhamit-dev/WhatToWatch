using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Entities.Dtos.MovieNoteAndVote;

namespace WhatToWatch.Entities.Dtos.Movie
{
    public class MovieNoteVoteDto
    {
        public int Id { get; set; }
        public int Page { get; set; }
        public string Title { get; set; }
        public string OverView { get; set; }
        public string Note { get; set; }
        public int Vote { get; set; }
        public int UserId { get; set; }
        public double VoteAverage { get; set; }
    }
}
