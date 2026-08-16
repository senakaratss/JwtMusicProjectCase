using JwtMusicProjectCase.WebUi.Dtos.PlaylistSongDtos;

namespace JwtMusicProjectCase.WebUi.Models
{
    public class PlaylistDetailViewModel
    {
        public int PlaylistId { get; set; }
        public string PlaylistName { get; set; }
        public List<ResultPlaylistSongDto> PlaylistSongs { get; set; }
    }
}
