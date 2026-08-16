namespace JwtMusicProjectCase.WebUi.Dtos.ListeningHistoryDtos
{
    public class UserListeningHistoryDto
    {
        public int ListeningHistoryId { get; set; }

        public int SongId { get; set; }
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public string CoverImageUrl { get; set; }

        public DateTime ListenDate { get; set; }
    }
}
