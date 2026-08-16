using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Dtos.SongDtos
{
    public class ResultSongDto
    {
        public int SongId { get; set; }
        public string SongName { get; set; }
        public string CoverImageUrl { get; set; }
        public string AudioUrl { get; set; }
        public TimeSpan Duration { get; set; }
        public int ListenCount { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Lyrics { get; set; }

        public int ArtistId { get; set; }
        public string ArtistName { get; set; }

        public int GenreId { get; set; }
        public string GenreName { get; set; }

        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public int PackageLevel { get; set; }
    }
}
