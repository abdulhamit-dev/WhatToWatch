using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatToWatch.Entities.Dtos.Movie
{
    // themoviedb den dönen json nesneni
    // diğer alanların hepsini almadım ihtiyaç olmadığından
    // https://json2csharp.com/ adresinden dönen json nesnesini hızlıca convert ettim.
    public class MovieDto
    {
        
        //public bool adult { get; set; }
        //public string backdrop_path { get; set; }
        //public List<int> genre_ids { get; set; }
        //public int id { get; set; }
        //public string original_language { get; set; }
        //public string original_title { get; set; }
        public string Overview { get; set; }
        //public double popularity { get; set; }
        //public string poster_path { get; set; }
        //public string release_date { get; set; }
        public string Title { get; set; }
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
