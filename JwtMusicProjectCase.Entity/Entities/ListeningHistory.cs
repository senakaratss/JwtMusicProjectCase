using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Entity.Entities
{
    public class ListeningHistory
    {
        public int ListeningHistoryId { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int SongId{ get; set; }
        public Song Song { get; set; }

        public DateTime ListenDate{ get; set; }
    }
}
