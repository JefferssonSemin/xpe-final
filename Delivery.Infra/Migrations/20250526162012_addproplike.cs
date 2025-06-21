using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Addproplike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Likes",
                table: "feed",
                newName: "likes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "likes",
                table: "feed",
                newName: "Likes");
        }
    }
}
