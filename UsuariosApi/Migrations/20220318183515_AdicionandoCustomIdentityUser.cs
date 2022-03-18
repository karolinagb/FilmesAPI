using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosApi.Migrations
{
    public partial class AdicionandoCustomIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "AspNetUsers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "9ec36618-2069-4669-b7be-156011b26e96");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "07333727-aa8e-4cd2-882f-79e6554cd83e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea0291e0-a0e5-4d6b-ba45-8bfa4b91b0d8", "AQAAAAEAACcQAAAAEOW8O+2iWqg1UWu5wOnjxhlfJGC+1j5UzXWFoEwrCIHKWmsE7EkuUxzT4t/Nn2I/2w==", "6db10f6c-2b46-44a9-bc34-fc3803235a4a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "e3525f2e-dc13-4e0a-99d8-5e2e6ee49fe6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "5e07cd2d-46a9-4d42-b988-e93c945a7a81");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "264b1436-8a4d-4311-8d30-7db069f0cd89", "AQAAAAEAACcQAAAAEDOS7AQ+7YylWndYD9BHdoa8Mjm79iip3icr4SHogzq6tG679XXI/J7W2DjQt1hMXA==", "6fae1180-34f2-465d-947f-5258fb117d46" });
        }
    }
}
