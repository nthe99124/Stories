using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoriesProject.API.Migrations
{
    /// <inheritdoc />
    public partial class addcolumstory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RateScore",
                table: "Stories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RateScore",
                table: "Stories");
        }
    }
}
