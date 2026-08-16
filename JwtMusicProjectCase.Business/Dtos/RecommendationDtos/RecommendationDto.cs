using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Dtos.RecommendationDtos
{
    public class RecommendationDto
    {
        public int SongId { get; set; }
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public string CoverImageUrl { get; set; }
        public float Score { get; set; }
    }
}
