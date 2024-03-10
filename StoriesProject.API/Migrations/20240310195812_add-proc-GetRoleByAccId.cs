using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoriesProject.API.Migrations
{
    /// <inheritdoc />
    public partial class addprocGetRoleByAccId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            @"DROP PROCEDURE IF EXISTS GetRoleByAccId;
            go
            CREATE PROCEDURE [GetRoleByAccId]
                @AccID uniqueidentifier
            AS
            BEGIN
                SELECT 
					r.Id,
					r.RoleName,
					r.RoleDescription
                FROM Roles r
	            INNER JOIN RoleAccountant ra ON r.Id = ra.RoleId
	            WHERE ra.AccountId = @AccID
            END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetRoleByAccId;");
        }
    }
}
