using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Commerce_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedImagePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0357b5ea-c3d9-4a46-b083-ff27e6085dbf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35919898-396a-4044-874b-0747deee3e27");

            migrationBuilder.AddColumn<string>(
                name: "ProductImagePath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "87ed858c-86bc-4f7d-a91d-c4a9b2cee8a4", null, "Admin", "ADMIN" },
                    { "a92c3c18-bf27-4557-b36b-006a1711f665", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87ed858c-86bc-4f7d-a91d-c4a9b2cee8a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a92c3c18-bf27-4557-b36b-006a1711f665");

            migrationBuilder.DropColumn(
                name: "ProductImagePath",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0357b5ea-c3d9-4a46-b083-ff27e6085dbf", null, "Admin", "ADMIN" },
                    { "35919898-396a-4044-874b-0747deee3e27", null, "User", "USER" }
                });
        }
    }
}
