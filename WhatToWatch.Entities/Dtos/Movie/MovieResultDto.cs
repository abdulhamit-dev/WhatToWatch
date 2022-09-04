using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatToWatch.Entities.Dtos.Movie
{
    // themoviedb den dönen json nesneni
    // diğer alanların hepsini almadım ihtiyaç olmadığından
    public class MovieDto
    {
        public int Id { get; set; }
        public string Overview { get; set; }
        public string Title { get; set; }
        public int Page { get; set; }
        //public bool adult { get; set; }
        //public string backdrop_path { get; set; }
        //public List<int> genre_ids { get; set; }
        //public string original_language { get; set; }
        //public string original_title { get; set; }
        //public double popularity { get; set; }
        //public string poster_path { get; set; }
        //public string release_date { get; set; }
        //public bool video { get; set; }
        //public double vote_average { get; set; }
        //public int vote_count { get; set; }
    }

    public class MovieResultDto
    {
        public int Page { get; set; }
        public List<MovieDto> Results { get; set; }
    }
}
