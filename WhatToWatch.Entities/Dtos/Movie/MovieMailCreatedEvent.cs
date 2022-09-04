using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatToWatch.Entities.Dtos.Movie
{
    public class MovieMailCreatedEvent
    {
        public string UserName { get; set; }
        public int MovieId { get; set; }
        public string Mail { get; set; }
    }
}
