using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Memidlo.Web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class AddingNormalizedusernameforsuperadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "238d88bb-1ef5-47f3-bc34-0963518df061",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "159bba89-e180-41c3-bf97-64c7a6fd8730", "SUPERADMIN@SOMEMAIL.COM", "SUPERADMIN@SOMEMAIL.COM", "AQAAAAIAAYagAAAAECBzgeMM4BXXR9KHBlQwM8bL0y+saRNjy1BtYIZxK/OlD07eCqIEIo805w3IcMFJzw==", "b39320d7-4f98-4a5f-a26c-e9c042dfea47" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "238d88bb-1ef5-47f3-bc34-0963518df061",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a5cbfdc-87c8-4a6d-925b-6cfef0e378d5", null, null, "AQAAAAIAAYagAAAAEKwFVSnCBFh5HynyN8j0bnqjCMsreWajhUtaDwhr1TRTLjQUdJEDD+bWg1TOSAiDPQ==", "2dc8673d-2f9b-45cd-9db8-df81575782e3" });
        }
    }
}
