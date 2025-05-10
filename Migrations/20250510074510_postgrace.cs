using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HostelFinder.Migrations
{
    /// <inheritdoc />
    public partial class postgrace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hostels",
                columns: table => new
                {
                    HostelId = table.Column<string>(type: "text", nullable: false),
                    HostelName = table.Column<string>(type: "text", nullable: false),
                    ManagerName = table.Column<string>(type: "text", nullable: false),
                    MobileNumber = table.Column<string>(type: "text", nullable: false),
                    ASA = table.Column<int>(type: "integer", nullable: false),
                    ASN = table.Column<int>(type: "integer", nullable: false),
                    ADA = table.Column<int>(type: "integer", nullable: false),
                    ADN = table.Column<int>(type: "integer", nullable: false),
                    NSA = table.Column<int>(type: "integer", nullable: false),
                    NSN = table.Column<int>(type: "integer", nullable: false),
                    NDA = table.Column<int>(type: "integer", nullable: false),
                    NDN = table.Column<int>(type: "integer", nullable: false),
                    ASAPRICE = table.Column<int>(type: "integer", nullable: false),
                    ASNPRICE = table.Column<int>(type: "integer", nullable: false),
                    ADAPRICE = table.Column<int>(type: "integer", nullable: false),
                    ADNPRICE = table.Column<int>(type: "integer", nullable: false),
                    NSAPRICE = table.Column<int>(type: "integer", nullable: false),
                    NSNPRICE = table.Column<int>(type: "integer", nullable: false),
                    NDAPRICE = table.Column<int>(type: "integer", nullable: false),
                    NDNPRICE = table.Column<int>(type: "integer", nullable: false),
                    AvailableASA = table.Column<int>(type: "integer", nullable: false),
                    AvailableASN = table.Column<int>(type: "integer", nullable: false),
                    AvailableADA = table.Column<int>(type: "integer", nullable: false),
                    AvailableADN = table.Column<int>(type: "integer", nullable: false),
                    AvailableNSA = table.Column<int>(type: "integer", nullable: false),
                    AvailableNSN = table.Column<int>(type: "integer", nullable: false),
                    AvailableNDA = table.Column<int>(type: "integer", nullable: false),
                    AvailableNDN = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hostels", x => x.HostelId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    MobileNumber = table.Column<string>(type: "text", nullable: false),
                    StudentName = table.Column<string>(type: "text", nullable: false),
                    GuardianName = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    District = table.Column<string>(type: "text", nullable: false),
                    Tehsil = table.Column<string>(type: "text", nullable: false),
                    Pincode = table.Column<string>(type: "text", nullable: false),
                    PreparationFor = table.Column<string>(type: "text", nullable: false),
                    Class = table.Column<string>(type: "text", nullable: false),
                    CoachingInstitute = table.Column<string>(type: "text", nullable: false),
                    Medium = table.Column<string>(type: "text", nullable: false),
                    HostelId = table.Column<string>(type: "text", nullable: true),
                    Money = table.Column<string>(type: "text", nullable: true),
                    SeatPreferenceCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.MobileNumber);
                    table.ForeignKey(
                        name: "FK_Students_Hostels_HostelId",
                        column: x => x.HostelId,
                        principalTable: "Hostels",
                        principalColumn: "HostelId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_HostelId",
                table: "Students",
                column: "HostelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Hostels");
        }
    }
}
