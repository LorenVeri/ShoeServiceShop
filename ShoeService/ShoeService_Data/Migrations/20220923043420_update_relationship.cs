using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeService_Data.Migrations
{
    public partial class update_relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RepositoryId",
                table: "Shoes",
                newName: "ShoeRepositoryId");

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceHasShoesRepositoryServiceId",
                table: "Shoes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceHasShoesRepositoryShoesRepositoryId",
                table: "Shoes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoesRepositoriesShoesRepositoryId",
                table: "Shoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceHasShoesRepositoriesServiceId",
                table: "ShoeRepositories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceHasShoesRepositoriesShoesRepositoryId",
                table: "ShoeRepositories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceHasShoesRepositoriesServiceId",
                table: "Services",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceHasShoesRepositoriesShoesRepositoryId",
                table: "Services",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderDetailOrderId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderDetailShoesId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_Shoes_ServiceHasShoesRepositoryServiceId_ServiceHasShoesRepositoryShoesRepositoryId",
                table: "Shoes",
                columns: new[] { "ServiceHasShoesRepositoryServiceId", "ServiceHasShoesRepositoryShoesRepositoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_ShoesRepositoriesShoesRepositoryId",
                table: "Shoes",
                column: "ShoesRepositoriesShoesRepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoeRepositories_ServiceHasShoesRepositoriesServiceId_ServiceHasShoesRepositoriesShoesRepositoryId",
                table: "ShoeRepositories",
                columns: new[] { "ServiceHasShoesRepositoriesServiceId", "ServiceHasShoesRepositoriesShoesRepositoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceHasShoesRepositoriesServiceId_ServiceHasShoesRepositoriesShoesRepositoryId",
                table: "Services",
                columns: new[] { "ServiceHasShoesRepositoriesServiceId", "ServiceHasShoesRepositoriesShoesRepositoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductsId",
                table: "ProductImages",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderDetailOrderId_OrderDetailShoesId",
                table: "Orders",
                columns: new[] { "OrderDetailOrderId", "OrderDetailShoesId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailService_OrderDetailsOrderId_OrderDetailsShoesId",
                table: "OrderDetailService",
                columns: new[] { "OrderDetailsOrderId", "OrderDetailsShoesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderDetails_OrderDetailOrderId_OrderDetailShoesId",
                table: "Orders",
                columns: new[] { "OrderDetailOrderId", "OrderDetailShoesId" },
                principalTable: "OrderDetails",
                principalColumns: new[] { "OrderId", "ShoesId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductsId",
                table: "ProductImages",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceHasShoesRepository_ServiceHasShoesRepositoriesServiceId_ServiceHasShoesRepositoriesShoesRepositoryId",
                table: "Services",
                columns: new[] { "ServiceHasShoesRepositoriesServiceId", "ServiceHasShoesRepositoriesShoesRepositoryId" },
                principalTable: "ServiceHasShoesRepository",
                principalColumns: new[] { "ServiceId", "ShoesRepositoryId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoeRepositories_ServiceHasShoesRepository_ServiceHasShoesRepositoriesServiceId_ServiceHasShoesRepositoriesShoesRepositoryId",
                table: "ShoeRepositories",
                columns: new[] { "ServiceHasShoesRepositoriesServiceId", "ServiceHasShoesRepositoriesShoesRepositoryId" },
                principalTable: "ServiceHasShoesRepository",
                principalColumns: new[] { "ServiceId", "ShoesRepositoryId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_ServiceHasShoesRepository_ServiceHasShoesRepositoryServiceId_ServiceHasShoesRepositoryShoesRepositoryId",
                table: "Shoes",
                columns: new[] { "ServiceHasShoesRepositoryServiceId", "ServiceHasShoesRepositoryShoesRepositoryId" },
                principalTable: "ServiceHasShoesRepository",
                principalColumns: new[] { "ServiceId", "ShoesRepositoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_ShoeRepositories_ShoesRepositoriesShoesRepositoryId",
                table: "Shoes",
                column: "ShoesRepositoriesShoesRepositoryId",
                principalTable: "ShoeRepositories",
                principalColumn: "ShoesRepositoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderDetails_OrderDetailOrderId_OrderDetailShoesId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductsId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceHasShoesRepository_ServiceHasShoesRepositoriesServiceId_ServiceHasShoesRepositoriesShoesRepositoryId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoeRepositories_ServiceHasShoesRepository_ServiceHasShoesRepositoriesServiceId_ServiceHasShoesRepositoriesShoesRepositoryId",
                table: "ShoeRepositories");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_ServiceHasShoesRepository_ServiceHasShoesRepositoryServiceId_ServiceHasShoesRepositoryShoesRepositoryId",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_ShoeRepositories_ShoesRepositoriesShoesRepositoryId",
                table: "Shoes");

            migrationBuilder.DropTable(
                name: "OrderDetailService");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_ServiceHasShoesRepositoryServiceId_ServiceHasShoesRepositoryShoesRepositoryId",
                table: "Shoes");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_ShoesRepositoriesShoesRepositoryId",
                table: "Shoes");

            migrationBuilder.DropIndex(
                name: "IX_ShoeRepositories_ServiceHasShoesRepositoriesServiceId_ServiceHasShoesRepositoriesShoesRepositoryId",
                table: "ShoeRepositories");

            migrationBuilder.DropIndex(
                name: "IX_Services_ServiceHasShoesRepositoriesServiceId_ServiceHasShoesRepositoriesShoesRepositoryId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductsId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderDetailOrderId_OrderDetailShoesId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ServiceHasShoesRepositoryServiceId",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "ServiceHasShoesRepositoryShoesRepositoryId",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "ShoesRepositoriesShoesRepositoryId",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "ServiceHasShoesRepositoriesServiceId",
                table: "ShoeRepositories");

            migrationBuilder.DropColumn(
                name: "ServiceHasShoesRepositoriesShoesRepositoryId",
                table: "ShoeRepositories");

            migrationBuilder.DropColumn(
                name: "ServiceHasShoesRepositoriesServiceId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceHasShoesRepositoriesShoesRepositoryId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "OrderDetailOrderId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderDetailShoesId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ShoeRepositoryId",
                table: "Shoes",
                newName: "RepositoryId");
        }
    }
}
