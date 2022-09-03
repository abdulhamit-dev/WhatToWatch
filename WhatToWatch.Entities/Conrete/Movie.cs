using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Core.Entities;

namespace WhatToWatch.Entities.Conrete
{
    public class Movie:IEntity
    {
        public int Id { get; set; }
        public int Page { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }

    }
}
