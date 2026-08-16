using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Entity.Entities
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string?  ImageUrl{ get; set; }

        public int PackageId { get; set; }
        public Package Package { get; set; }

        public List<Playlist> Playlists { get; set; }
    }
}
