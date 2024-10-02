using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace meditationApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedPreviewUrlMusic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviewUrl",
                table: "Musics",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviewUrl",
                table: "Musics");
        }
    }
}
