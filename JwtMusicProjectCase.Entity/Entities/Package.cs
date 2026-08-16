using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Entity.Entities
{
    public class Package
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public int PackageLevel { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        public List<AppUser> Users { get; set; }
        public List<Song> Songs { get; set; }
    }
}
