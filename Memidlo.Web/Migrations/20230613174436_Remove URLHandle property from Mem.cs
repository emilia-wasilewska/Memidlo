using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Memidlo.Web.Migrations
{
    /// <inheritdoc />
    public partial class RemoveURLHandlepropertyfromMem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlHandle",
                table: "Mems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlHandle",
                table: "Mems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
