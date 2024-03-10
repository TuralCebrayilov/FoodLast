using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food.Migrations
{
    /// <inheritdoc />
    public partial class AddColumToHomeSliderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuProducts_MenuCategories_MenuCategoryId",
                table: "MenuProducts");

            migrationBuilder.AlterColumn<int>(
                name: "MenuCategoryId",
                table: "MenuProducts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuProducts_MenuCategories_MenuCategoryId",
                table: "MenuProducts",
                column: "MenuCategoryId",
                principalTable: "MenuCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuProducts_MenuCategories_MenuCategoryId",
                table: "MenuProducts");

            migrationBuilder.AlterColumn<int>(
                name: "MenuCategoryId",
                table: "MenuProducts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuProducts_MenuCategories_MenuCategoryId",
                table: "MenuProducts",
                column: "MenuCategoryId",
                principalTable: "MenuCategories",
                principalColumn: "Id");
        }
    }
}
