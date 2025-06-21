using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Addlike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "feed",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "feed");
        }
    }
}
