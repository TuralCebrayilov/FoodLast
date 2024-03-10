using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food.Migrations
{
    /// <inheritdoc />
    public partial class AddColumsToAboutTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image4",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image5",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image6",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image7",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image8",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "Image3",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "Image4",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "Image5",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "Image6",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "Image7",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "Image8",
                table: "Abouts");
        }
    }
}
