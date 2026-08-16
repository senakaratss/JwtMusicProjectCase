namespace JwtMusicProjectCase.WebUi.Dtos.UserDtos
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
