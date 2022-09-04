using WhatToWatch.Entities.Conrete;
using WhatToWatch.Entities.Dtos.Movie;

namespace WhatToWatch.API.Test
{
    public class SampleData
    {
        #region MovieNoteAndVoteResponseDto
        public static List<MovieNoteAndVoteResponseDto> movieNoteAndVoteResponseDto = new List<MovieNoteAndVoteResponseDto>()
        {
            new MovieNoteAndVoteResponseDto 
            {
                Id=1,Page=1,Title="Başlık 1",Overview="Açıklam 1",VoteAverage=5,
                NoteAndVotes=new List<Entities.Dtos.MovieNoteAndVote.MovieNoteAndVoteDto>(){
                        new Entities.Dtos.MovieNoteAndVote.MovieNoteAndVoteDto()
                        {
                            Note="Note 1", Vote= 4
                        } 
                }
            },

        };

        #endregion

        #region MovieData
        public static List<Movie> movies = new List<Movie>()
        {
            new Movie()
            {
                Id=1,Page=1,Title="Test Title",Overview="Test Overview"
            },
             new Movie()
            {
                Id=2,Page=1,Title="Test Title 2",Overview="Test Overview 2"
            },
             new Movie()
            {
                Id=3,Page=1,Title="Test Title 3",Overview="Test Overview 3"
            }
        };
        #endregion

    }
}
