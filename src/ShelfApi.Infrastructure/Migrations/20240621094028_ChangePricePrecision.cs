using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShelfApi.Infrastructure.Migrations;

/// <inheritdoc />
public partial class ChangePricePrecision : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "Price",
            table: "Products",
            type: "numeric(12,2)",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(10,0)");

        migrationBuilder.AlterColumn<decimal>(
            name: "TaxPrice",
            table: "Orders",
            type: "numeric(12,2)",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(10,0)");

        migrationBuilder.AlterColumn<decimal>(
            name: "NetPrice",
            table: "Orders",
            type: "numeric(12,2)",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(10,0)");

        migrationBuilder.AlterColumn<decimal>(
            name: "ListPrice",
            table: "Orders",
            type: "numeric(12,2)",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(10,0)");

        migrationBuilder.AlterColumn<decimal>(
            name: "TotalPrice",
            table: "OrderLines",
            type: "numeric(12,2)",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(10,0)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "Price",
            table: "Products",
            type: "numeric(10,0)",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(12,2)");

        migrationBuilder.AlterColumn<decimal>(
            name: "TaxPrice",
            table: "Orders",
            type: "numeric(10,0)",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(12,2)");

        migrationBuilder.AlterColumn<decimal>(
            name: "NetPrice",
            table: "Orders",
            type: "numeric(10,0)",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(12,2)");

        migrationBuilder.AlterColumn<decimal>(
            name: "ListPrice",
            table: "Orders",
            type: "numeric(10,0)",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(12,2)");

        migrationBuilder.AlterColumn<decimal>(
            name: "TotalPrice",
            table: "OrderLines",
            type: "numeric(10,0)",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(12,2)");
    }
}
