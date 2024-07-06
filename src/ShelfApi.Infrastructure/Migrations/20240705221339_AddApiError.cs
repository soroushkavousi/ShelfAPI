using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShelfApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddApiError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiErrors",
                columns: table => new
                {
                    Code = table.Column<short>(type: "smallint", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    Message = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'"),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiErrors", x => x.Code);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiErrors");
        }
    }
}
