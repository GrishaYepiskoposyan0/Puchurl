using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Puchurl.Migrations
{
    /// <inheritdoc />
    public partial class AddHitCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "HitCount",
                table: "ShortUrls",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HitCount",
                table: "ShortUrls");
        }
    }
}
