using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoriesProject.Migrations
{
    /// <inheritdoc />
    public partial class updatetablelog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoinLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoinTransacted = table.Column<int>(type: "int", nullable: false),
                    MoneyConvert = table.Column<decimal>(type: "money", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accountants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    Coin = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accountants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accountants_CoinLog",
                        column: x => x.Id,
                        principalTable: "CoinLog",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "money", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    Discounts = table.Column<decimal>(type: "money", nullable: false),
                    Taxes = table.Column<decimal>(type: "money", nullable: false),
                    CoinTransacted = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Accountants",
                        column: x => x.CreatedBy,
                        principalTable: "Accountants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleAccountant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAccountant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleAccountant_Accountants",
                        column: x => x.AccountId,
                        principalTable: "Accountants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleAccountant_Accountants1",
                        column: x => x.CreatedBy,
                        principalTable: "Accountants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleAccountant_Roles",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfStory = table.Column<int>(type: "int", nullable: false),
                    ModifiedDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stories_Accountants",
                        column: x => x.CreatedBy,
                        principalTable: "Accountants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stories_Accountants1",
                        column: x => x.ModifiedBy,
                        principalTable: "Accountants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TopicStory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicStory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicStory_Stories",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TopicStory_Topic",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedBy",
                table: "Orders",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccountant_AccountId",
                table: "RoleAccountant",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccountant_CreatedBy",
                table: "RoleAccountant",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccountant_RoleId",
                table: "RoleAccountant",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_CreatedBy",
                table: "Stories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_ModifiedBy",
                table: "Stories",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TopicStory_StoryId",
                table: "TopicStory",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicStory_TopicId",
                table: "TopicStory",
                column: "TopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "RoleAccountant");

            migrationBuilder.DropTable(
                name: "TopicStory");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Stories");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "Accountants");

            migrationBuilder.DropTable(
                name: "CoinLog");
        }
    }
}
