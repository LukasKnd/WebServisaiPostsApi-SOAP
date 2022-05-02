using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class addContactId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "Posts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 5, 2, 20, 12, 44, 728, DateTimeKind.Utc).AddTicks(2985), new DateTime(2022, 5, 2, 20, 12, 44, 728, DateTimeKind.Utc).AddTicks(2986) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 5, 2, 20, 12, 44, 728, DateTimeKind.Utc).AddTicks(2987), new DateTime(2022, 5, 2, 20, 12, 44, 728, DateTimeKind.Utc).AddTicks(2987) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 5, 2, 20, 12, 44, 728, DateTimeKind.Utc).AddTicks(2988), new DateTime(2022, 5, 2, 20, 12, 44, 728, DateTimeKind.Utc).AddTicks(2988) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Posts");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 4, 3, 20, 30, 7, 391, DateTimeKind.Utc).AddTicks(7902), new DateTime(2022, 4, 3, 20, 30, 7, 391, DateTimeKind.Utc).AddTicks(7903) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 4, 3, 20, 30, 7, 391, DateTimeKind.Utc).AddTicks(7904), new DateTime(2022, 4, 3, 20, 30, 7, 391, DateTimeKind.Utc).AddTicks(7905) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2022, 4, 3, 20, 30, 7, 391, DateTimeKind.Utc).AddTicks(7905), new DateTime(2022, 4, 3, 20, 30, 7, 391, DateTimeKind.Utc).AddTicks(7906) });
        }
    }
}
