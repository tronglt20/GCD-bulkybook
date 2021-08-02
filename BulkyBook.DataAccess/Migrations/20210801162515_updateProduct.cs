using Microsoft.EntityFrameworkCore.Migrations;

namespace BulkyBook.DataAccess.Migrations
{
    public partial class updateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CoverType_CoverTypeId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "CoverType");

            migrationBuilder.DropIndex(
                name: "IX_Products_CoverTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CoverTypeId",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "r1",
                column: "ConcurrencyStamp",
                value: "d4ba6777-6e49-46eb-b238-29d8be2d0721");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "r2",
                column: "ConcurrencyStamp",
                value: "7479624a-b1e4-4288-a71c-bba9bcec3f3d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9999",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "09469f49-a0b1-4118-8593-5ce24689c95e", "AQAAAAEAACcQAAAAEPHcfpTkbzxlzCTI3NSHCH5b4wkkC3wpk7cY7v/Q9y7znNIS/vBzmpNsopDfwMa29g==", "6e6bc393-8bd4-4f45-bef0-1defb404fb7f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoverTypeId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CoverType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverType", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "r1",
                column: "ConcurrencyStamp",
                value: "a38a9e0f-38a3-4b0a-9bff-370ea3c8401b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "r2",
                column: "ConcurrencyStamp",
                value: "5a17ade2-4894-4e71-b812-472d8a396df4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9999",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "867a9186-2ebc-4e4b-8dd2-8aa591978dd7", "AQAAAAEAACcQAAAAEK7hbIjlw7hOdm0+/yT2Ze/jAaRUetl+siQ0HFoxNFwRk0YyCGHItglOFProqOxO0w==", "cd1c69ba-144f-4ea0-8ecd-df954ddfa81f" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CoverTypeId",
                table: "Products",
                column: "CoverTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CoverType_CoverTypeId",
                table: "Products",
                column: "CoverTypeId",
                principalTable: "CoverType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
