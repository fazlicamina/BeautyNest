using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Prosirenjeusermodela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41451580-acf7-422e-81b1-3bab4d8213a6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2867632-9a9f-42f0-85f3-b98e58920ac2", "AQAAAAIAAYagAAAAEEqtngRVXmupzAPu/Hs158qO7P6hjq9Uyfh9rB5FY844u4w2pOjEW6n542anRoFFMg==", "27f6b7b7-4b2f-4930-8d5f-1389ee91380d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41451580-acf7-422e-81b1-3bab4d8213a6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f5eb4792-09d4-4c3c-a940-1dcbfe930742", "AQAAAAIAAYagAAAAEJX85S7ZEG1m1+oIAPboqGPuv7pgw/h7+4ygfRLUxrotY70C3xCJbozdQ3zfjpzm/g==", "d3530fca-c135-4762-b525-760fe5692f91" });
        }
    }
}
