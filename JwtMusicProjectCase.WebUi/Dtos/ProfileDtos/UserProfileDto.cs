namespace JwtMusicProjectCase.WebUi.Dtos.ProfileDtos
{
    public class UserProfileDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string? ImageUrl { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int PackageId { get; set; }
        public string PackageName { get; set; }
    }
}
