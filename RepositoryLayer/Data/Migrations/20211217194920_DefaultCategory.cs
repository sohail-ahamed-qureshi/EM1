using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Data.Migrations
{
    public partial class DefaultCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "Categories",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Categories",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Categories",
                newName: "isDeleted");
        }
    }
}
