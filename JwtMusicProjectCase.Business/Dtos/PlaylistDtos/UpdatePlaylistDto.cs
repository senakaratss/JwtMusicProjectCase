using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Dtos.PlaylistDtos
{
    public class UpdatePlaylistDto
    {
        public int PlaylistId { get; set; }
        public string PlaylistName { get; set; }
    }
}
