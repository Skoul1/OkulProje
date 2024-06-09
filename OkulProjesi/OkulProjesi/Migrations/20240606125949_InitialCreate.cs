using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OkulProjesi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblDersler",
                columns: table => new
                {
                    DersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersKodu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DersAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DersKredi = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDersler", x => x.DersId);
                });

            migrationBuilder.CreateTable(
                name: "tblOgrenciler",
                columns: table => new
                {
                    OgrenciId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgrenciAd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OgrenciSoyAd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OgrenciNumarasi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOgrenciler", x => x.OgrenciId);
                });

            migrationBuilder.CreateTable(
                name: "tblOgretmenler",
                columns: table => new
                {
                    OgretmenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgretmenAdı = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OgretmenSoyAdı = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OgretmenBolum = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOgretmenler", x => x.OgretmenId);
                });

            migrationBuilder.CreateTable(
                name: "OgrenciDersler",
                columns: table => new
                {
                    OgrenciId = table.Column<int>(type: "int", nullable: false),
                    DersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrenciDersler", x => new { x.OgrenciId, x.DersId });
                    table.ForeignKey(
                        name: "FK_OgrenciDersler_tblDersler_DersId",
                        column: x => x.DersId,
                        principalTable: "tblDersler",
                        principalColumn: "DersId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OgrenciDersler_tblOgrenciler_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "tblOgrenciler",
                        principalColumn: "OgrenciId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciDersler_DersId",
                table: "OgrenciDersler",
                column: "DersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OgrenciDersler");

            migrationBuilder.DropTable(
                name: "tblOgretmenler");

            migrationBuilder.DropTable(
                name: "tblDersler");

            migrationBuilder.DropTable(
                name: "tblOgrenciler");
        }
    }
}
