using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoriesProject.API.Migrations
{
    /// <inheritdoc />
    public partial class AuthorRegister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorRegister",
                columns: table => new
                {
                    AuthorRegisterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorRegister", x => x.AuthorRegisterId);
                    table.ForeignKey(
                        name: "FK_Accountants_AuthorRegister",
                        column: x => x.AccountantId,
                        principalTable: "Accountants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StoryAccountGeneric",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfStory = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryAccountGeneric", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorRegister_AccountantId",
                table: "AuthorRegister",
                column: "AccountantId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorRegister");

            migrationBuilder.DropTable(
                name: "StoryAccountGeneric");
        }
    }
}
