using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WhatToWatch.Business.Abstract;
using WhatToWatch.Business.Constant;
using WhatToWatch.Core.Caching;
using WhatToWatch.Core.Utilities.Result;
using WhatToWatch.DataAccess.Abstract;
using WhatToWatch.Entities.Conrete;
using WhatToWatch.Entities.Dtos.Movie;
using WhatToWatch.Entities.Dtos.MovieNoteAndVote;

namespace WhatToWatch.Business.Concrete
{
    public class MovieManager : IMovieService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IMovieDal _movieDal;
        private readonly string _apiKey;
        private readonly string _apiUrl;
        private IHttpContextAccessor _httpContextAccessor;
        private ICacheService _cacheService;
        public MovieManager(HttpClient httpClient, IMovieDal movieDal, IMapper mapper, IHttpContextAccessor httpContextAccessor, ICacheService cacheService)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            _apiKey = config.GetSection("ThemovieDbApiKey").Get<string>();
            _apiUrl = config.GetSection("ApiUrl").Get<string>();
            _httpClient = httpClient;
            _movieDal = movieDal;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _cacheService = cacheService;
        }

        //Daha önce aynı sayfa cache de ise cache den getiriyorum
        //Burada cache süresi veya ne kadar tutulacağı senaryoya göre değişeceği için şimdilik bu şekilde bıraktım.
        public IDataResult<List<Movie>> GetAll(int page)
        {
            if (_cacheService.Any($"MovieGetAll_{page}"))
            {
                var moviesdto = _cacheService.Get<List<Movie>>($"MovieGetAll_{page}");
                return new SuccessDataResult<List<Movie>>(moviesdto, MessagesReturn.GetAll);
            }

            var movies = _movieDal.GetAll(x => x.Page == page);
            if (movies.Count > 0)
                _cacheService.Add($"MovieGetAll_{page}", movies);

            return new SuccessDataResult<List<Movie>>(_movieDal.GetAll(x => x.Page == page), MessagesReturn.GetAll);
        }
        public IDataResult<MovieNoteAndVoteResponseDto> GetByIdDetail(int id)
        {
            var userId = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var result = _movieDal.GetMovies(id, Convert.ToInt32(userId));
            MovieNoteAndVoteResponseDto movieNoteAndVoteResponseDto = new ();
            movieNoteAndVoteResponseDto.Title= result.Select(x => x.Title).Distinct().FirstOrDefault();
            movieNoteAndVoteResponseDto.Overview= result.Select(x => x.OverView).Distinct().FirstOrDefault();
            movieNoteAndVoteResponseDto.VoteAverage=Math.Round( result.Select(x => x.VoteAverage).Distinct().FirstOrDefault(),2);
            foreach (var item in result)
            {
                MovieNoteAndVoteDto movieNoteAndVote = new()
                {
                    Vote = item.Vote,
                    Note = item.Note
                };
                movieNoteAndVoteResponseDto.NoteAndVotes.Add(movieNoteAndVote);
            }
            return new SuccessDataResult<MovieNoteAndVoteResponseDto>(movieNoteAndVoteResponseDto, MessagesReturn.GetAll);
        }

        public async Task<MovieResultDto> GetMovieData()
        {
            #region !!! Açıklama
            //themoviedb api de tüm filmleri çekme gibi bir metod yok yada ben bulamadım. 
            //onun yerine trende olanlar veya popüler olanların listesini sayfa sayfa çekmeyi tercih ettim.
            //servis ile de bu durumda 1 ile 50 arasında rastgele sayfa belirleyerek dataya çekme işlemini gerçekleştirdim.
            //normalde örneğin max 100 sayfalık veriyi tek seferde çekmeyi hayal ediyordum.Daha sonrada sayfalama yapsını kendim kuracaktım
            #endregion

            Random rnd = new Random();
            var page = rnd.Next(1, 50);

            var response = await _httpClient.GetAsync(@$"{_apiUrl}/movie/popular?api_key={_apiKey}&language=en-US&page={page}");
            string rv = response.Content.ReadAsStringAsync().Result;
            var returnData = JsonConvert.DeserializeObject<MovieResultDto>(rv, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return returnData;
        }
        public async Task<int> SaveData()
        {
            var data = await GetMovieData();
            List<Movie> moList = new List<Movie>();
            foreach (var movieDto in data.Results)
            {
                var movie = _mapper.Map<Movie>(movieDto);
                movie.Page = data.Page;
                moList.Add(movie);
            }
            _movieDal.AddRange(moList);

            return 1;
        }

        IDataResult<MovieDto> IMovieService.GetById(int id)
        {
            var result = _movieDal.Get(x => x.Id == id);
            
            if (result is null)
                return new ErrorDataResult<MovieDto>(null, MessagesReturn.NotFound);
            
            MovieDto resultDto = _mapper.Map<MovieDto>(result);
            
            return new SuccessDataResult<MovieDto>(resultDto, MessagesReturn.GetById);
        }
    }
}
