using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Data.Migrations
{
    public partial class UserCategoryMapTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCategoryMap",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsersUserID = table.Column<long>(nullable: true),
                    CategoriesId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCategoryMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCategoryMap_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCategoryMap_Users_UsersUserID",
                        column: x => x.UsersUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCategoryMap_CategoriesId",
                table: "UserCategoryMap",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCategoryMap_UsersUserID",
                table: "UserCategoryMap",
                column: "UsersUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCategoryMap");
        }
    }
}
