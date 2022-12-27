using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberSecurityProject.Data.Migrations
{
    public partial class AssignAdminToRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [security].[UserRoles] (UserId, RoleId) SELECT '693fd349-02b4-42c1-8699-e789129c06ff', Id FROM [security].[Roles]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[UserRoles] WHERE UserId = '693fd349-02b4-42c1-8699-e789129c06ff'");
        }
    }
}
