using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeService_Data.Migrations
{
    public partial class update_Db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetailService");

            migrationBuilder.AddColumn<int>(
                name: "ServiceHasShoesServiceId",
                table: "Shoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceHasShoesShoesId",
                table: "Shoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderDetailOrderId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderDetailShoesId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceHasShoesServiceId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceHasShoesShoesId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ServiceHasShoes",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ShoesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceHasShoes", x => new { x.ServiceId, x.ShoesId });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_ServiceHasShoesServiceId_ServiceHasShoesShoesId",
                table: "Shoes",
                columns: new[] { "ServiceHasShoesServiceId", "ServiceHasShoesShoesId" });

            migrationBuilder.CreateIndex(
                name: "IX_Services_OrderDetailOrderId_OrderDetailShoesId",
                table: "Services",
                columns: new[] { "OrderDetailOrderId", "OrderDetailShoesId" });

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceHasShoesServiceId_ServiceHasShoesShoesId",
                table: "Services",
                columns: new[] { "ServiceHasShoesServiceId", "ServiceHasShoesShoesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Services_OrderDetails_OrderDetailOrderId_OrderDetailShoesId",
                table: "Services",
                columns: new[] { "OrderDetailOrderId", "OrderDetailShoesId" },
                principalTable: "OrderDetails",
                principalColumns: new[] { "OrderId", "ShoesId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceHasShoes_ServiceHasShoesServiceId_ServiceHasShoesShoesId",
                table: "Services",
                columns: new[] { "ServiceHasShoesServiceId", "ServiceHasShoesShoesId" },
                principalTable: "ServiceHasShoes",
                principalColumns: new[] { "ServiceId", "ShoesId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_ServiceHasShoes_ServiceHasShoesServiceId_ServiceHasShoesShoesId",
                table: "Shoes",
                columns: new[] { "ServiceHasShoesServiceId", "ServiceHasShoesShoesId" },
                principalTable: "ServiceHasShoes",
                principalColumns: new[] { "ServiceId", "ShoesId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_OrderDetails_OrderDetailOrderId_OrderDetailShoesId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceHasShoes_ServiceHasShoesServiceId_ServiceHasShoesShoesId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_ServiceHasShoes_ServiceHasShoesServiceId_ServiceHasShoesShoesId",
                table: "Shoes");

            migrationBuilder.DropTable(
                name: "ServiceHasShoes");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_ServiceHasShoesServiceId_ServiceHasShoesShoesId",
                table: "Shoes");

            migrationBuilder.DropIndex(
                name: "IX_Services_OrderDetailOrderId_OrderDetailShoesId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ServiceHasShoesServiceId_ServiceHasShoesShoesId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceHasShoesServiceId",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "ServiceHasShoesShoesId",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "OrderDetailOrderId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "OrderDetailShoesId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceHasShoesServiceId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceHasShoesShoesId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderDetails");

            migrationBuilder.CreateTable(
                name: "OrderDetailService",
                columns: table => new
                {
                    ServicesId = table.Column<int>(type: "int", nullable: false),
                    OrderDetailsOrderId = table.Column<int>(type: "int", nullable: false),
                    OrderDetailsShoesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailService", x => new { x.ServicesId, x.OrderDetailsOrderId, x.OrderDetailsShoesId });
                    table.ForeignKey(
                        name: "FK_OrderDetailService_OrderDetails_OrderDetailsOrderId_OrderDetailsShoesId",
                        columns: x => new { x.OrderDetailsOrderId, x.OrderDetailsShoesId },
                        principalTable: "OrderDetails",
                        principalColumns: new[] { "OrderId", "ShoesId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetailService_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailService_OrderDetailsOrderId_OrderDetailsShoesId",
                table: "OrderDetailService",
                columns: new[] { "OrderDetailsOrderId", "OrderDetailsShoesId" });
        }
    }
}
