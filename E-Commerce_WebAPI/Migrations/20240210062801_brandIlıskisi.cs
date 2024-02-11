using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Commerce_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class brandIlıskisi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87ed858c-86bc-4f7d-a91d-c4a9b2cee8a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a92c3c18-bf27-4557-b36b-006a1711f665");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Models",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d32ee82-6d8e-44c1-a8e6-fc8b86d23e5d", null, "Admin", "ADMIN" },
                    { "d9f29d74-7e03-4376-94dd-ccb8fb47fefa", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Models_BrandId",
                table: "Models",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId");
                
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_BrandId",
                table: "Models");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d32ee82-6d8e-44c1-a8e6-fc8b86d23e5d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9f29d74-7e03-4376-94dd-ccb8fb47fefa");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Models");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "87ed858c-86bc-4f7d-a91d-c4a9b2cee8a4", null, "Admin", "ADMIN" },
                    { "a92c3c18-bf27-4557-b36b-006a1711f665", null, "User", "USER" }
                });
        }
    }
}
