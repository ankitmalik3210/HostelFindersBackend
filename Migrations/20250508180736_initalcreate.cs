using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HostelFinder.Migrations
{
    /// <inheritdoc />
    public partial class initalcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hostels",
                columns: table => new
                {
                    HostelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HostelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ASA = table.Column<int>(type: "int", nullable: false),
                    ASN = table.Column<int>(type: "int", nullable: false),
                    ADA = table.Column<int>(type: "int", nullable: false),
                    ADN = table.Column<int>(type: "int", nullable: false),
                    NSA = table.Column<int>(type: "int", nullable: false),
                    NSN = table.Column<int>(type: "int", nullable: false),
                    NDA = table.Column<int>(type: "int", nullable: false),
                    NDN = table.Column<int>(type: "int", nullable: false),
                    AvailableASA = table.Column<int>(type: "int", nullable: false),
                    AvailableASN = table.Column<int>(type: "int", nullable: false),
                    AvailableADA = table.Column<int>(type: "int", nullable: false),
                    AvailableADN = table.Column<int>(type: "int", nullable: false),
                    AvailableNSA = table.Column<int>(type: "int", nullable: false),
                    AvailableNSN = table.Column<int>(type: "int", nullable: false),
                    AvailableNDA = table.Column<int>(type: "int", nullable: false),
                    AvailableNDN = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hostels", x => x.HostelId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    MobileNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuardianName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tehsil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pincode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreparationFor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoachingInstitute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medium = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HostelId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SeatPreferenceCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
