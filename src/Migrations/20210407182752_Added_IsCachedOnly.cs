using Microsoft.EntityFrameworkCore.Migrations;

namespace GloboDiet.Migrations
{
    public partial class Added_IsCachedOnly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCachedOnly",
                table: "Interviews",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCachedOnly",
                table: "Interviews");
        }
    }
}
