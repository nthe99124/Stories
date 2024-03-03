using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoriesProject.API.Migrations
{
    /// <inheritdoc />
    public partial class addtablemultistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Accountants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Accountants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "Gender",
                table: "Accountants",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "ImgAvatar",
                table: "Accountants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Introduce",
                table: "Accountants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Accountants",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Accountants");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Accountants");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Accountants");

            migrationBuilder.DropColumn(
                name: "ImgAvatar",
                table: "Accountants");

            migrationBuilder.DropColumn(
                name: "Introduce",
                table: "Accountants");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Accountants");
        }
    }
}
