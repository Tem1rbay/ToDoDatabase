using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApplication.Migrations
{
    /// <inheritdoc />
    public partial class NewInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.EnsureSchema(
                name: "todpapp");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "tasks",
                newSchema: "todpapp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tasks",
                schema: "todpapp",
                table: "tasks",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tasks",
                schema: "todpapp",
                table: "tasks");

            migrationBuilder.RenameTable(
                name: "tasks",
                schema: "todpapp",
                newName: "Tasks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");
        }
    }
}
