namespace JwtMusicProjectCase.WebUi.Dtos.RecommendationDtos
{
    public class RecommendationDto
    {
        public int SongId { get; set; }
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public string CoverImageUrl { get; set; }
        public float Score { get; set; }
    }
}
