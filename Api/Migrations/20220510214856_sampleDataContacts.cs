using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class sampleDataContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ContactId", "Created", "Updated" },
                values: new object[] { 12345, new DateTime(2022, 5, 10, 21, 48, 56, 372, DateTimeKind.Utc).AddTicks(1815), new DateTime(2022, 5, 10, 21, 48, 56, 372, DateTimeKind.Utc).AddTicks(1815) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ContactId", "Created", "Updated" },
                values: new object[] { 12345, new DateTime(2022, 5, 10, 21, 48, 56, 372, DateTimeKind.Utc).AddTicks(1817), new DateTime(2022, 5, 10, 21, 48, 56, 372, DateTimeKind.Utc).AddTicks(1818) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ContactId", "Created", "Updated" },
                values: new object[] { 12345, new DateTime(2022, 5, 10, 21, 48, 56, 372, DateTimeKind.Utc).AddTicks(1818), new DateTime(2022, 5, 10, 21, 48, 56, 372, DateTimeKind.Utc).AddTicks(1819) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ContactId", "Created", "Updated" },
                values: new object[] { null, new DateTime(2022, 5, 2, 20, 12, 44, 728, DateTimeKind.Utc).AddTicks(2985), new DateTime(2022, 5, 2, 20, 12, 44, 728, DateTimeKind.Utc).AddTicks(2986) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ContactId", "Created", "Updated" },
                values: new object[] { null, new DateTime(2022, 5, 2, 20, 12, 44, 728, DateTimeKind.Utc).AddTicks(2987), new DateTime(2022, 5, 2, 20, 12, 44, 728, DateTimeKind.Utc).AddTicks(2987) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ContactId", "Created", "Updated" },
                values: new object[] { null, new DateTime(2022, 5, 2, 20, 12, 44, 728, DateTimeKind.Utc).AddTicks(2988), new DateTime(2022, 5, 2, 20, 12, 44, 728, DateTimeKind.Utc).AddTicks(2988) });
        }
    }
}
