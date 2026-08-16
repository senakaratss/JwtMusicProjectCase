using JwtMusicProjectCase.WebUi.Dtos.SongDtos;

namespace JwtMusicProjectCase.WebUi.Models
{
    public class GenreDetailViewModel
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }

        public List<ResultSongDto> Songs { get; set; }
    }
}
