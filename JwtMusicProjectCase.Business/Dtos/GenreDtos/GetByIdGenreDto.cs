using JwtMusicProjectCase.Business.Dtos.SongDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Dtos.GenreDtos
{
    public class GetByIdGenreDto
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public List<ResultSongDto> Songs { get; set; }
    }
}
