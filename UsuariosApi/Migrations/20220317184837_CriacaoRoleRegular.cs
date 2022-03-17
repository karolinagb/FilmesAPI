using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosApi.Migrations
{
    public partial class CriacaoRoleRegular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "5e07cd2d-46a9-4d42-b988-e93c945a7a81");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99997, "e3525f2e-dc13-4e0a-99d8-5e2e6ee49fe6", "regular", "REGULAR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "264b1436-8a4d-4311-8d30-7db069f0cd89", "AQAAAAEAACcQAAAAEDOS7AQ+7YylWndYD9BHdoa8Mjm79iip3icr4SHogzq6tG679XXI/J7W2DjQt1hMXA==", "6fae1180-34f2-465d-947f-5258fb117d46" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "5da321ad-aa4d-4bb4-8eda-40fc59fbd15f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de8f78cc-f613-40ab-81e2-c58acc22ae77", "AQAAAAEAACcQAAAAEMIFkhzOJq54SrRAeAwB1NVkGLvI3hxggw5lRRGlWL8CQb0joF5x1Ds34GhbFIX0Hg==", "e2b0d16e-7acc-4151-8ec1-168d564b619a" });
        }
    }
}
