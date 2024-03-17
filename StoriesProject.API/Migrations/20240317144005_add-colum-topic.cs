using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoriesProject.API.Migrations
{
    /// <inheritdoc />
    public partial class addcolumtopic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Topic",
                type: "nvarchar(max)",
                nullable: true);
            migrationBuilder.Sql(
            @"DROP PROCEDURE IF EXISTS GetListStoryForAdmin;
            go
            CREATE PROCEDURE [dbo].[GetListStoryForAdmin]
				-- Add the parameters for the stored procedure here
				@Status smallint
            AS
            BEGIN
                SELECT s.Id, 
					s.Name as Name, 
					a.Name as AuthorName, 
					s.Price, 
					s.ShortDescription as Description,
					STUFF((SELECT ', ' + t.Name
              FROM TopicStory ts
              JOIN Topic t ON ts.TopicId = t.Id
              WHERE ts.StoryId = s.Id
              FOR XML PATH('')), 1, 2, '') AS TopicName,
					s.CreatedDate
			FROM Stories s
			left join TopicStory ts on s.Id = ts.StoryId
			left join Topic t on ts.TopicId = t.Id
			left join Accountants a on a.Id = s.CreatedBy
            END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Topic");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetListStoryForAdmin;");
        }
    }
}
