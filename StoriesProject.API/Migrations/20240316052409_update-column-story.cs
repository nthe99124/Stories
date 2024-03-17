using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoriesProject.API.Migrations
{
    /// <inheritdoc />
    public partial class updatecolumnstory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "TargetObject",
                table: "Stories",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
            migrationBuilder.Sql(
                @"DROP PROCEDURE IF EXISTS GetAllStoryByTopic;
                go
                CREATE PROCEDURE GetAllStoryByTopic
                    @TopicID uniqueidentifier
                AS
                BEGIN
                    SELECT s.Id, s.Name, s.Code,
			                s.ImageLink, s.VideoLink, s.Description, s.ShortDescription, s.TypeOfStory
                    FROM Stories s
	                INNER JOIN TopicStory ts ON s.Id = ts.StoryID
	                WHERE ts.TopicId = @TopicID
                END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "TargetObject",
                table: "Stories");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllStoryByTopic;");
        }
    }
}
