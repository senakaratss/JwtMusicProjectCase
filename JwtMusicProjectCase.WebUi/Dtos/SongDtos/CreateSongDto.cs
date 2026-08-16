namespace JwtMusicProjectCase.WebUi.Dtos.SongDtos
{
    public class CreateSongDto
    {
        public string SongName { get; set; }
        public string CoverImageUrl { get; set; }
        public IFormFile AudioFile { get; set; }
        public TimeSpan Duration { get; set; }
        public int ListenCount { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Lyrics { get; set; }

        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        public int PackageId { get; set; }
    }
}
