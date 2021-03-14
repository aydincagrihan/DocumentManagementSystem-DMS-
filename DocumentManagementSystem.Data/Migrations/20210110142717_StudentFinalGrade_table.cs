using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentManagementSystem.Data.Migrations
{
    public partial class StudentFinalGrade_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentFinalGrade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReportsId = table.Column<int>(nullable: false),
                    DuzenBicim = table.Column<decimal>(nullable: false),
                    Alintilama = table.Column<decimal>(nullable: false),
                    YazimveDilkalite = table.Column<decimal>(nullable: false),
                    RaporBoyut = table.Column<decimal>(nullable: false),
                    ProjeTanimi = table.Column<decimal>(nullable: false),
                    GereksinimAnaliz = table.Column<decimal>(nullable: false),
                    GercekciKisitlama = table.Column<decimal>(nullable: false),
                    AhlakiKonu = table.Column<decimal>(nullable: false),
                    SistemTasarim = table.Column<decimal>(nullable: false),
                    Uygulama = table.Column<decimal>(nullable: false),
                    Standartlar = table.Column<decimal>(nullable: false),
                    Testler = table.Column<decimal>(nullable: false),
                    SistemKullanimKılavuz = table.Column<decimal>(nullable: false),
                    CozumAciklama = table.Column<decimal>(nullable: false),
                    Kaynak = table.Column<decimal>(nullable: false),
                    Ekler = table.Column<decimal>(nullable: false),
                    ToplamRaporPuan = table.Column<decimal>(nullable: false),
                    Toplantisikligi = table.Column<decimal>(nullable: false),
                    IlerlemeRapor = table.Column<decimal>(nullable: false),
                    ProjeGelistirme = table.Column<decimal>(nullable: false),
                    ToplamDanismanPuan = table.Column<decimal>(nullable: false),
                    DisiplinliGelistirme = table.Column<decimal>(nullable: false),
                    YeniFikir = table.Column<decimal>(nullable: false),
                    UygulamaKalitesi = table.Column<decimal>(nullable: false),
                    UygulamaAraclari = table.Column<decimal>(nullable: false),
                    ProjeCozum = table.Column<decimal>(nullable: false),
                    ToplamKalitePuan = table.Column<decimal>(nullable: false),
                    Duzen = table.Column<decimal>(nullable: false),
                    ZamanKullanim = table.Column<decimal>(nullable: false),
                    SlaytKalite = table.Column<decimal>(nullable: false),
                    IletisimYetenek = table.Column<decimal>(nullable: false),
                    SoruCevap = table.Column<decimal>(nullable: false),
                    ToplamSunumPuan = table.Column<decimal>(nullable: false),
                    GenelToplamRaporPuan = table.Column<decimal>(nullable: false),
                    GenelBolunmusToplamPuan = table.Column<decimal>(nullable: false),
                    GenelOzgunluk = table.Column<decimal>(nullable: false),
                    GenelSonPuan = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentFinalGrade", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentFinalGrade");
        }
    }
}
