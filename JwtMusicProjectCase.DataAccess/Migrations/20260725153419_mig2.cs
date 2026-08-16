using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtMusicProjectCase.DataAccess.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Level",
                table: "Packages",
                newName: "PackageLevel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PackageLevel",
                table: "Packages",
                newName: "Level");
        }
    }
}
