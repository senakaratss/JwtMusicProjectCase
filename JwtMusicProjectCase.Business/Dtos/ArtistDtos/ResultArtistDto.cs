using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Dtos.ArtistDtos
{
    public class ResultArtistDto
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string ArtistImageUrl { get; set; }
        public string CoverImageUrl { get; set; }
        public string Bio { get; set; }
        public string Country { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsVerified { get; set; }
    }
}
