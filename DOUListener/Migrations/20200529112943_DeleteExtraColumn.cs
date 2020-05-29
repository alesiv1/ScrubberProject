using Microsoft.EntityFrameworkCore.Migrations;

namespace DOUListener.Migrations
{
    public partial class DeleteExtraColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Likes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Likes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
