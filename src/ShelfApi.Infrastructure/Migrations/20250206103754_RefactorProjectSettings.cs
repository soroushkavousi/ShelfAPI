using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShelfApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorProjectSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "ProjectSettings",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "ProjectSettings",
                type: "text",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");
            
            migrationBuilder.Sql(@"
                UPDATE ""ProjectSettings"" SET ""Key"" = 'StartupData' WHERE ""Id"" = 1;
                UPDATE ""ProjectSettings"" SET ""Key"" = 'FinancialSettings' WHERE ""Id"" = 2;
            "); 
            
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectSettings",
                table: "ProjectSettings");
            
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectSettings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectSettings",
                table: "ProjectSettings",
                column: "Key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectSettings",
                table: "ProjectSettings");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "ProjectSettings");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ProjectSettings",
                newName: "Data");

            migrationBuilder.AddColumn<byte>(
                name: "Id",
                table: "ProjectSettings",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectSettings",
                table: "ProjectSettings",
                column: "Id");
        }
    }
}
