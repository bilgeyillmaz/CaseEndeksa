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
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    geometryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    coordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ilceAd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mevkii = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ilId = table.Column<int>(type: "int", nullable: false),
                    durum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    parselId = table.Column<int>(type: "int", nullable: false),
                    zeminKmdurum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    zeminId = table.Column<int>(type: "int", nullable: false),
                    parselNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nitelik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mahalleAd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gittigiParselListe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gittigiParselSebep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    alan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adaNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ilceId = table.Column<int>(type: "int", nullable: false),
                    ilAd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mahalleId = table.Column<int>(type: "int", nullable: false),
                    pafta = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
