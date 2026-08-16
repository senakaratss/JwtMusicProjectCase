using JwtMusicProjectCase.Entity.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Dtos.SongDtos
{
    public class CreateSongDto
    {
        public string SongName { get; set; }
        public string CoverImageUrl { get; set; }
        public string? AudioUrl { get; set; }
        public IFormFile AudioFile { get; set; }
        public TimeSpan Duration { get; set; }
        public int ListenCount { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Lyrics { get; set; }

        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        public int PackageId { get; set; }
    }
}
