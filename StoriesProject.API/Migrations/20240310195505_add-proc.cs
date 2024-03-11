using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoriesProject.API.Migrations
{
    /// <inheritdoc />
    public partial class addproc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            @"DROP PROCEDURE IF EXISTS GetStoryDetailById;
            go
            CREATE PROCEDURE GetStoryDetailById
				-- Add the parameters for the stored procedure here
				@StoryId uniqueidentifier
			AS
			DECLARE @TotalStory INT;
			BEGIN
				-- lấy tổng truyện đã có
				SELECT @TotalStory = Count(*) FROM Stories WHERE Id = @StoryId;

				SELECT 
					s.Name as StoryName,
					a.Name as AuthorName,
					@TotalStory as TotalStory,
					s.Description,
					s.Content,
					s.ImageLink,
					s.Price,
					s.RateScore,
					s.TypeOfStory,
					s.VideoLink,
					s.ViewAccess
				FROM 
					Stories s 
					left join Accountants a on s.CreatedBy = a.Id
				WHERE s.Id = @StoryId;

				SELECT 
					c.Content,
					c.Name,
					c.Id
				FROM Chapters c where StoryId = @StoryId;

				-- lấy danh sách topic
				SELECT 
					t.Name,
					t.Id
				FROM 
					Topic t
					inner join TopicStory ts on t.Id = ts.TopicId
					inner join Stories s on ts.StoryId = s.Id
				where StoryId = @StoryId;
			END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetStoryDetailById;");
        }
    }
}
