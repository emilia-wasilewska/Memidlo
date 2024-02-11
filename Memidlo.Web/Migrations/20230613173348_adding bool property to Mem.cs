using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Memidlo.Web.Migrations
{
    /// <inheritdoc />
    public partial class addingboolpropertytoMem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Main",
                table: "Mems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Main",
                table: "Mems");
        }
    }
}
