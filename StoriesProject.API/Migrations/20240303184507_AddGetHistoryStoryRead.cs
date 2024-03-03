using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoriesProject.API.Migrations
{
    /// <inheritdoc />
    public partial class AddGetHistoryStoryRead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            @"drop procedure if exists GetHistoryStoryRead
            go
            CREATE PROCEDURE GetHistoryStoryRead
                @AccountantID uniqueidentifier
            AS
            BEGIN
                SELECT s.Id, s.Name, s.Code,
			            s.ImageLink, s.VideoLink, s.Description, s.TypeOfStory
                FROM Stories s
	            INNER JOIN StoryAccoutant sa ON s.Id = sa.StoryID
	            WHERE sa.AccoutantID = @AccountantID
            END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetHistoryStoryRead;");
        }
    }
}
