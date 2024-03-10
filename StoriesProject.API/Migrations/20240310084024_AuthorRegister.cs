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
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountantType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    PaymentType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    BankName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BankNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BankAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
        }
    }
}
