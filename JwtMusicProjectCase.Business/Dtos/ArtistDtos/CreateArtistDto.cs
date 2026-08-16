using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Dtos.ArtistDtos
{
    public class CreateArtistDto
    {
        public string ArtistName { get; set; }
        public string ArtistImageUrl { get; set; }
        public string CoverImageUrl { get; set; }
        public string Bio { get; set; }
        public string Country { get; set; }
    }
}
