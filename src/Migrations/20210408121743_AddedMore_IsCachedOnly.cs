using Microsoft.EntityFrameworkCore.Migrations;

namespace GloboDiet.Migrations
{
    public partial class AddedMore_IsCachedOnly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCachedOnly",
                table: "Meals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "BrandnameId",
                table: "MealElements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCachedOnly",
                table: "MealElements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_MealElements_BrandnameId",
                table: "MealElements",
                column: "BrandnameId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealElements_Brandnames_BrandnameId",
                table: "MealElements",
                column: "BrandnameId",
                principalTable: "Brandnames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealElements_Brandnames_BrandnameId",
                table: "MealElements");

            migrationBuilder.DropIndex(
                name: "IX_MealElements_BrandnameId",
                table: "MealElements");

            migrationBuilder.DropColumn(
                name: "IsCachedOnly",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "BrandnameId",
                table: "MealElements");

            migrationBuilder.DropColumn(
                name: "IsCachedOnly",
                table: "MealElements");
        }
    }
}
