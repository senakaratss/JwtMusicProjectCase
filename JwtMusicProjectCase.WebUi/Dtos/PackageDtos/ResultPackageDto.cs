namespace JwtMusicProjectCase.WebUi.Dtos.PackageDtos
{
    public class ResultPackageDto
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public int PackageLevel { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
