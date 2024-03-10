using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food.Migrations
{
    /// <inheritdoc />
    public partial class Drinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductPrice",
                table: "MenuProducts",
                newName: "ProductSPrice");

            migrationBuilder.AddColumn<double>(
                name: "ProductLPrice",
                table: "MenuProducts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ProductMPrice",
                table: "MenuProducts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Drinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinks", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drinks");

            migrationBuilder.DropColumn(
                name: "ProductLPrice",
                table: "MenuProducts");

            migrationBuilder.DropColumn(
                name: "ProductMPrice",
                table: "MenuProducts");

            migrationBuilder.RenameColumn(
                name: "ProductSPrice",
                table: "MenuProducts",
                newName: "ProductPrice");
        }
    }
}
