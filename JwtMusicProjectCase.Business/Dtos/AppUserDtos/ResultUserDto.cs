using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Dtos.AppUserDtos
{
    public class ResultUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public int PackageId { get; set; }
        public string PackageName { get; set; }

        public string RoleName { get; set; }
    }
}
