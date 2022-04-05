using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    TagsJson = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Created", "TagsJson", "Title", "Updated" },
                values: new object[] { 1, "Content1", new DateTime(2022, 4, 3, 20, 30, 7, 391, DateTimeKind.Utc).AddTicks(7902), "[]", "Title1", new DateTime(2022, 4, 3, 20, 30, 7, 391, DateTimeKind.Utc).AddTicks(7903) });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Created", "TagsJson", "Title", "Updated" },
                values: new object[] { 2, "Content2", new DateTime(2022, 4, 3, 20, 30, 7, 391, DateTimeKind.Utc).AddTicks(7904), "[]", "Title2", new DateTime(2022, 4, 3, 20, 30, 7, 391, DateTimeKind.Utc).AddTicks(7905) });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Created", "TagsJson", "Title", "Updated" },
                values: new object[] { 3, "Content3", new DateTime(2022, 4, 3, 20, 30, 7, 391, DateTimeKind.Utc).AddTicks(7905), "[]", "Title3", new DateTime(2022, 4, 3, 20, 30, 7, 391, DateTimeKind.Utc).AddTicks(7906) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
