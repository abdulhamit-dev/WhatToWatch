using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatToWatch.Entities.Conrete
{
    public class MovieNoteAndScore
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public string Not { get; set; }
        public int Score { get; set; }
    }
}
