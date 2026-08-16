using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Dtos.GenreDtos
{
    public class UpdateGenreDto
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
    }
}
