using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Dtos.AppUserDtos
{
    public class UpdateProfileDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string? ImageUrl { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
