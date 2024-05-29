using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShelfApi.Infrastructure.Migrations;

/// <inheritdoc />
public partial class ChangeConfigsToMainSettings : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Configs");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "UserTokens",
            type: "datetime2(2)",
            precision: 2,
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "UserTokens",
            type: "datetime2(2)",
            precision: 2,
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "Users",
            type: "datetime2(2)",
            precision: 2,
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Users",
            type: "datetime2(2)",
            precision: 2,
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "UserRoles",
            type: "datetime2(2)",
            precision: 2,
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "UserRoles",
            type: "datetime2(2)",
            precision: 2,
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "UserLogins",
            type: "datetime2(2)",
            precision: 2,
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "UserLogins",
            type: "datetime2(2)",
            precision: 2,
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "UserClaims",
            type: "datetime2(2)",
            precision: 2,
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "UserClaims",
            type: "datetime2(2)",
            precision: 2,
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "Roles",
            type: "datetime2(2)",
            precision: 2,
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Roles",
            type: "datetime2(2)",
            precision: 2,
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValueSql: "SYSUTCDATETIME()");

        //////////////////////////////////////

        migrationBuilder.AddColumn<byte>(
            name: "NewId",
            table: "Roles",
            type: "tinyint",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.Sql("UPDATE Roles SET NewId = Id");

        migrationBuilder.DropForeignKey(
            name: "FK_UserRoles_Roles_RoleId",
            table: "UserRoles");

        migrationBuilder.DropForeignKey(
            name: "FK_RoleClaims_Roles_RoleId",
            table: "RoleClaims");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Roles",
            table: "Roles");

        migrationBuilder.DropColumn(
            name: "Id",
            table: "Roles");

        migrationBuilder.RenameColumn(
            name: "NewId",
            table: "Roles",
            newName: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Roles",
            table: "Roles",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_UserRoles_Roles_RoleId",
            table: "UserRoles",
            column: "RoleId",
            principalTable: "Roles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_RoleClaims_Roles_RoleId",
            table: "RoleClaims",
            column: "RoleId",
            principalTable: "Roles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        /////////////////////////////////

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "RoleClaims",
            type: "datetime2(2)",
            precision: 2,
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "RoleClaims",
            type: "datetime2(2)",
            precision: 2,
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "Products",
            type: "datetime2(2)",
            precision: 2,
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Products",
            type: "datetime2(2)",
            precision: 2,
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "Orders",
            type: "datetime2(2)",
            precision: 2,
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Orders",
            type: "datetime2(2)",
            precision: 2,
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "OrderLines",
            type: "datetime2(2)",
            precision: 2,
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "OrderLines",
            type: "datetime2(2)",
            precision: 2,
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.CreateTable(
            name: "MainSettings",
            columns: table => new
            {
                Category = table.Column<byte>(type: "tinyint", nullable: false),
                Data = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "{}"),
                CreatedAt = table.Column<DateTime>(type: "datetime2(2)", precision: 2, nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                ModifiedAt = table.Column<DateTime>(type: "datetime2(2)", precision: 2, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MainSettings", x => x.Category);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "MainSettings");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "UserTokens",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "UserTokens",
            type: "datetime2",
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "Users",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Users",
            type: "datetime2",
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "UserRoles",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "UserRoles",
            type: "datetime2",
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "UserLogins",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "UserLogins",
            type: "datetime2",
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "UserClaims",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "UserClaims",
            type: "datetime2",
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "Roles",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Roles",
            type: "datetime2",
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.AlterColumn<byte>(
            name: "Id",
            table: "Roles",
            type: "tinyint",
            nullable: false,
            oldClrType: typeof(byte),
            oldType: "tinyint")
            .Annotation("SqlServer:Identity", "1, 1");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "RoleClaims",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "RoleClaims",
            type: "datetime2",
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "Products",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Products",
            type: "datetime2",
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "Orders",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Orders",
            type: "datetime2",
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "ModifiedAt",
            table: "OrderLines",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "OrderLines",
            type: "datetime2",
            nullable: false,
            defaultValueSql: "SYSUTCDATETIME()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2(2)",
            oldPrecision: 2,
            oldDefaultValueSql: "SYSUTCDATETIME()");

        migrationBuilder.CreateTable(
            name: "Configs",
            columns: table => new
            {
                Id = table.Column<byte>(type: "tinyint", nullable: false),
                EnvironmentName = table.Column<string>(type: "varchar(60)", nullable: false),
                Category = table.Column<string>(type: "varchar(60)", nullable: false),
                Value = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "{}"),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Configs", x => x.Id);
            });
    }
}