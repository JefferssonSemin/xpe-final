using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Changehasonetomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_feed_user_id",
                table: "feed");

            migrationBuilder.CreateIndex(
                name: "IX_feed_user_id",
                table: "feed",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_feed_user_id",
                table: "feed");

            migrationBuilder.CreateIndex(
                name: "IX_feed_user_id",
                table: "feed",
                column: "user_id",
                unique: true);
        }
    }
}
