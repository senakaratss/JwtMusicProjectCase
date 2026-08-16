namespace JwtMusicProjectCase.WebUi.Dtos.PlaylistSongDtos
{
    public class ResultPlaylistSongDto
    {
        public int SongId { get; set; }
        public string SongName { get; set; }
        public string CoverImageUrl { get; set; }
        public string AudioUrl { get; set; }
        public TimeSpan Duration { get; set; }

        public string ArtistName { get; set; }
    }
}
