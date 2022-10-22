using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeService_Data.Migrations
{
    public partial class update_Table_Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsLocked",
                table: "Storage",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsLocked",
                table: "Shoes",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsLocked",
                table: "Service",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsLocked",
                table: "Product",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsLocked",
                table: "Order",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsLocked",
                table: "MemberShip",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsLocked",
                table: "Customer",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Customer",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Storage",
                newName: "IsLocked");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Shoes",
                newName: "IsLocked");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Service",
                newName: "IsLocked");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Product",
                newName: "IsLocked");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Order",
                newName: "IsLocked");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "MemberShip",
                newName: "IsLocked");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Customer",
                newName: "IsLocked");
        }
    }
}
