using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoriesProject.API.Migrations
{
    /// <inheritdoc />
    public partial class addtableChapterContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChapterContent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChapterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SortOrder = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImgLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedByNavigationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChapterContent_Accountants_CreatedByNavigationId",
                        column: x => x.CreatedByNavigationId,
                        principalTable: "Accountants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChapterContent_Chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChapterContent_ChapterId",
                table: "ChapterContent",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterContent_CreatedByNavigationId",
                table: "ChapterContent",
                column: "CreatedByNavigationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChapterContent");
        }
    }
}
