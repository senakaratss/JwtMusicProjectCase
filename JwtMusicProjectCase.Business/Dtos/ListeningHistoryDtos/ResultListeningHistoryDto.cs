using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Dtos.ListeningHistoryDtos
{
    public class ResultListeningHistoryDto
    {
        public int ListeningHistoryId { get; set; }

        public string UserId { get; set; }
        public string UserFullName{ get; set; }

        public int SongId { get; set; }
        public string SongName { get; set; }

        public DateTime ListenDate { get; set; }
    }
}
