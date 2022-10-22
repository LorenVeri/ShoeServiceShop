using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeService_Data.Migrations
{
    public partial class update_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServiceHasRepository_ServiceHasRepositoryRepositoryId_ServiceHasRepositoryServiceId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_RepositoryHasShoes_RepositoryHasShoesRepositoryId_RepositoryHasShoesShoesId",
                table: "Shoes");

            migrationBuilder.DropTable(
                name: "Repository");

            migrationBuilder.DropTable(
                name: "RepositoryHasShoes");

            migrationBuilder.DropTable(
                name: "ServiceHasRepository");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_RepositoryHasShoesRepositoryId_RepositoryHasShoesShoesId",
                table: "Shoes");

            migrationBuilder.DropIndex(
                name: "IX_Service_ServiceHasRepositoryRepositoryId_ServiceHasRepositoryServiceId",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "RepositoryHasShoesShoesId",
                table: "Shoes",
                newName: "StorageHasShoesStorageId");

            migrationBuilder.RenameColumn(
                name: "RepositoryHasShoesRepositoryId",
                table: "Shoes",
                newName: "StorageHasShoesShoesId");

            migrationBuilder.RenameColumn(
                name: "ServiceHasRepositoryServiceId",
                table: "Service",
                newName: "ServiceHasStorageStorageId");

            migrationBuilder.RenameColumn(
                name: "ServiceHasRepositoryRepositoryId",
                table: "Service",
                newName: "ServiceHasStorageServiceId");

            migrationBuilder.CreateTable(
                name: "ServiceHasStorage",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    StorageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceHasStorage", x => new { x.StorageId, x.ServiceId });
                });

            migrationBuilder.CreateTable(
                name: "StorageHasShoes",
                columns: table => new
                {
                    StorageId = table.Column<int>(type: "int", nullable: false),
                    ShoesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageHasShoes", x => new { x.StorageId, x.ShoesId });
                });

            migrationBuilder.CreateTable(
                name: "Storage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ServiceHasStorageStorageId = table.Column<int>(type: "int", nullable: true),
                    ServiceHasStorageServiceId = table.Column<int>(type: "int", nullable: true),
                    StorageHasShoesStorageId = table.Column<int>(type: "int", nullable: true),
                    StorageHasShoesShoesId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Storage_ServiceHasStorage_ServiceHasStorageStorageId_ServiceHasStorageServiceId",
                        columns: x => new { x.ServiceHasStorageStorageId, x.ServiceHasStorageServiceId },
                        principalTable: "ServiceHasStorage",
                        principalColumns: new[] { "StorageId", "ServiceId" });
                    table.ForeignKey(
                        name: "FK_Storage_StorageHasShoes_StorageHasShoesStorageId_StorageHasShoesShoesId",
                        columns: x => new { x.StorageHasShoesStorageId, x.StorageHasShoesShoesId },
                        principalTable: "StorageHasShoes",
                        principalColumns: new[] { "StorageId", "ShoesId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_StorageHasShoesStorageId_StorageHasShoesShoesId",
                table: "Shoes",
                columns: new[] { "StorageHasShoesStorageId", "StorageHasShoesShoesId" });

            migrationBuilder.CreateIndex(
                name: "IX_Service_ServiceHasStorageStorageId_ServiceHasStorageServiceId",
                table: "Service",
                columns: new[] { "ServiceHasStorageStorageId", "ServiceHasStorageServiceId" });

            migrationBuilder.CreateIndex(
                name: "IX_Storage_ServiceHasStorageStorageId_ServiceHasStorageServiceId",
                table: "Storage",
                columns: new[] { "ServiceHasStorageStorageId", "ServiceHasStorageServiceId" });

            migrationBuilder.CreateIndex(
                name: "IX_Storage_StorageHasShoesStorageId_StorageHasShoesShoesId",
                table: "Storage",
                columns: new[] { "StorageHasShoesStorageId", "StorageHasShoesShoesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServiceHasStorage_ServiceHasStorageStorageId_ServiceHasStorageServiceId",
                table: "Service",
                columns: new[] { "ServiceHasStorageStorageId", "ServiceHasStorageServiceId" },
                principalTable: "ServiceHasStorage",
                principalColumns: new[] { "StorageId", "ServiceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_StorageHasShoes_StorageHasShoesStorageId_StorageHasShoesShoesId",
                table: "Shoes",
                columns: new[] { "StorageHasShoesStorageId", "StorageHasShoesShoesId" },
                principalTable: "StorageHasShoes",
                principalColumns: new[] { "StorageId", "ShoesId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServiceHasStorage_ServiceHasStorageStorageId_ServiceHasStorageServiceId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_StorageHasShoes_StorageHasShoesStorageId_StorageHasShoesShoesId",
                table: "Shoes");

            migrationBuilder.DropTable(
                name: "Storage");

            migrationBuilder.DropTable(
                name: "ServiceHasStorage");

            migrationBuilder.DropTable(
                name: "StorageHasShoes");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_StorageHasShoesStorageId_StorageHasShoesShoesId",
                table: "Shoes");

            migrationBuilder.DropIndex(
                name: "IX_Service_ServiceHasStorageStorageId_ServiceHasStorageServiceId",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "StorageHasShoesStorageId",
                table: "Shoes",
                newName: "RepositoryHasShoesShoesId");

            migrationBuilder.RenameColumn(
                name: "StorageHasShoesShoesId",
                table: "Shoes",
                newName: "RepositoryHasShoesRepositoryId");

            migrationBuilder.RenameColumn(
                name: "ServiceHasStorageStorageId",
                table: "Service",
                newName: "ServiceHasRepositoryServiceId");

            migrationBuilder.RenameColumn(
                name: "ServiceHasStorageServiceId",
                table: "Service",
                newName: "ServiceHasRepositoryRepositoryId");

            migrationBuilder.CreateTable(
                name: "RepositoryHasShoes",
                columns: table => new
                {
                    RepositoryId = table.Column<int>(type: "int", nullable: false),
                    ShoesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepositoryHasShoes", x => new { x.RepositoryId, x.ShoesId });
                });

            migrationBuilder.CreateTable(
                name: "ServiceHasRepository",
                columns: table => new
                {
                    RepositoryId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceHasRepository", x => new { x.RepositoryId, x.ServiceId });
                });

            migrationBuilder.CreateTable(
                name: "Repository",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepositoryHasShoesRepositoryId = table.Column<int>(type: "int", nullable: true),
                    ServiceHasRepositoryRepositoryId = table.Column<int>(type: "int", nullable: true),
                    RepositoryHasShoesShoesId = table.Column<int>(type: "int", nullable: true),
                    ServiceHasRepositoryServiceId = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repository", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repository_RepositoryHasShoes_RepositoryHasShoesRepositoryId_RepositoryHasShoesShoesId",
                        columns: x => new { x.RepositoryHasShoesRepositoryId, x.RepositoryHasShoesShoesId },
                        principalTable: "RepositoryHasShoes",
                        principalColumns: new[] { "RepositoryId", "ShoesId" });
                    table.ForeignKey(
                        name: "FK_Repository_ServiceHasRepository_ServiceHasRepositoryRepositoryId_ServiceHasRepositoryServiceId",
                        columns: x => new { x.ServiceHasRepositoryRepositoryId, x.ServiceHasRepositoryServiceId },
                        principalTable: "ServiceHasRepository",
                        principalColumns: new[] { "RepositoryId", "ServiceId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_RepositoryHasShoesRepositoryId_RepositoryHasShoesShoesId",
                table: "Shoes",
                columns: new[] { "RepositoryHasShoesRepositoryId", "RepositoryHasShoesShoesId" });

            migrationBuilder.CreateIndex(
                name: "IX_Service_ServiceHasRepositoryRepositoryId_ServiceHasRepositoryServiceId",
                table: "Service",
                columns: new[] { "ServiceHasRepositoryRepositoryId", "ServiceHasRepositoryServiceId" });

            migrationBuilder.CreateIndex(
                name: "IX_Repository_RepositoryHasShoesRepositoryId_RepositoryHasShoesShoesId",
                table: "Repository",
                columns: new[] { "RepositoryHasShoesRepositoryId", "RepositoryHasShoesShoesId" });

            migrationBuilder.CreateIndex(
                name: "IX_Repository_ServiceHasRepositoryRepositoryId_ServiceHasRepositoryServiceId",
                table: "Repository",
                columns: new[] { "ServiceHasRepositoryRepositoryId", "ServiceHasRepositoryServiceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServiceHasRepository_ServiceHasRepositoryRepositoryId_ServiceHasRepositoryServiceId",
                table: "Service",
                columns: new[] { "ServiceHasRepositoryRepositoryId", "ServiceHasRepositoryServiceId" },
                principalTable: "ServiceHasRepository",
                principalColumns: new[] { "RepositoryId", "ServiceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_RepositoryHasShoes_RepositoryHasShoesRepositoryId_RepositoryHasShoesShoesId",
                table: "Shoes",
                columns: new[] { "RepositoryHasShoesRepositoryId", "RepositoryHasShoesShoesId" },
                principalTable: "RepositoryHasShoes",
                principalColumns: new[] { "RepositoryId", "ShoesId" });
        }
    }
}
