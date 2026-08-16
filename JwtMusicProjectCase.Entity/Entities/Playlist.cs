using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Entity.Entities
{
    public class Playlist
    {
        public int PlaylistId { get; set; }
        public string PlaylistName { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public List<PlaylistSong> PlaylistSongs { get; set; }
    }
}
