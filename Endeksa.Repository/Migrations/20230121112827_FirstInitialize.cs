using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Endeksa.Repository.Migrations
{
    public partial class FirstInitialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeometryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IlceAd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mevkii = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IlId = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParselId = table.Column<int>(type: "int", nullable: false),
                    ZeminKmdurum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZeminId = table.Column<int>(type: "int", nullable: false),
                    ParselNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nitelik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MahalleAd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GittigiParselListe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GittigiParselSebep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdaNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IlceId = table.Column<int>(type: "int", nullable: false),
                    IlAd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MahalleId = table.Column<int>(type: "int", nullable: false),
                    Pafta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roots", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roots");
        }
    }
}
