using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoriesProject.API.Migrations
{
    /// <inheritdoc />
    public partial class addtableStoryAccoutant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoryAccoutant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccoutantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoryID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReaded = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StoryIDNavigationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccoutantIDNavigationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryAccoutant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryAccoutant_Accountants_AccoutantIDNavigationId",
                        column: x => x.AccoutantIDNavigationId,
                        principalTable: "Accountants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoryAccoutant_Stories_StoryIDNavigationId",
                        column: x => x.StoryIDNavigationId,
                        principalTable: "Stories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoryAccoutant_AccoutantIDNavigationId",
                table: "StoryAccoutant",
                column: "AccoutantIDNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryAccoutant_StoryIDNavigationId",
                table: "StoryAccoutant",
                column: "StoryIDNavigationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoryAccoutant");
        }
    }
}
