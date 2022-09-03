using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Business.Abstract;
using WhatToWatch.Core.Utilities.Result;
using WhatToWatch.DataAccess.Abstract;
using WhatToWatch.Entities.Conrete;
using WhatToWatch.Entities.Dtos.Movie;

namespace WhatToWatch.Business.Concrete
{
    public class MovieManager : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly IMovieDal _movieDal;
        public MovieManager(HttpClient httpClient, IMovieDal movieDal)
        {
            _httpClient = httpClient;
            _movieDal = movieDal;
        }
        public async Task<MovieResultDto> GetMovieData()
        {

            var response = await _httpClient.GetAsync(@"https://api.themoviedb.org/3/movie/popular?api_key=e1ba3fc6130fbb59a52bb7c9da642f32&language=en-US&page=3");
            string rv = response.Content.ReadAsStringAsync().Result;
            var returnData = JsonConvert.DeserializeObject<MovieResultDto>(rv, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });



            return returnData;
        }

        public async Task<int> SaveData()
        {
            var data = await GetMovieData();


            List<Movie> moList = new List<Movie>();
            for (int i = 0; i < 5; i++)
            {
                Movie m = new Movie();
                m.Page = i;
                m.Title = i.ToString();
                m.Overview = "açıklama" + i.ToString();
                moList.Add(m);
            }

            _movieDal.AddRange(moList);


            return 5;
        }
    }
}
