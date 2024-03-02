using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TodoList",
                keyColumn: "Id",
                keyValue: new Guid("2d398b31-ec9a-4ea0-a9e3-dfe48baec4af"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TodoList",
                columns: new[] { "Id", "CardColor", "Category", "CreatedDate", "Description", "DueDate", "IsCompleted", "IsImportant", "Title", "UpdatedDate" },
                values: new object[] { new Guid("2d398b31-ec9a-4ea0-a9e3-dfe48baec4af"), 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New description", new DateTime(2024, 3, 1, 17, 50, 0, 895, DateTimeKind.Utc).AddTicks(4580), false, false, "New Title", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
