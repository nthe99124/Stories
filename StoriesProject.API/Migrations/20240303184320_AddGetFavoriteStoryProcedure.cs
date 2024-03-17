using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoriesProject.API.Migrations
{
    /// <inheritdoc />
    public partial class AddGetFavoriteStoryProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            @"DROP PROCEDURE IF EXISTS GetFavoriteStory;
            go
            CREATE PROCEDURE GetFavoriteStory
                @AccountantID uniqueidentifier
            AS
            BEGIN
                SELECT s.Id, s.Name, s.Code,
			            s.ImageLink, s.VideoLink, s.Description, s.ShortDescription, s.TypeOfStory
                FROM Stories s
	            INNER JOIN FavoriteStories fs ON s.Id = fs.StoryID
	            WHERE fs.AccountantId = @AccountantID
            END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetFavoriteStory;");
        }
    }
}
