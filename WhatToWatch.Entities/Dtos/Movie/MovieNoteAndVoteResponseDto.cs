using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Entities.Dtos.MovieNoteAndVote;

namespace WhatToWatch.Entities.Dtos.Movie
{
    public class MovieNoteAndVoteResponseDto
    {
        public int Id { get; set; }
        public int Page { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public double VoteAverage { get; set; }
        public List<MovieNoteAndVoteDto> NoteAndVotes { get; set; }=new List<MovieNoteAndVoteDto>();

    }

   
}
