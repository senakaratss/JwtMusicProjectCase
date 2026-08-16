using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Dtos.ListeningHistoryDtos
{
    public class ListeningHistoryByUserDto
    {
        public int ListeningHistoryId { get; set; }

        public int SongId { get; set; }
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public string CoverImageUrl { get; set; }

        public DateTime ListenDate { get; set; }
    }
}
