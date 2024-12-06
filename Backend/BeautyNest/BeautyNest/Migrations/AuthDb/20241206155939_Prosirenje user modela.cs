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
                values: new object[] { "c381c2eb-d72f-4979-b4fb-4ccb64124f5e", "AQAAAAIAAYagAAAAECnG1RAwRAItG3Rws3Kk/igyIX/5K1k5DZSXJYah5gDezsPMryTosBhpK8UdUdOw6A==", "2d552bf6-e119-42eb-8d40-94ef80fa7f22" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41451580-acf7-422e-81b1-3bab4d8213a6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "177d6d03-d438-49dc-ac3f-38a07a60febc", "AQAAAAIAAYagAAAAEB4EGY75KJWDJIQbZX0fT1hTrOnXD62dwCj/YX2yQ5KrhurkE5mjpA+s3A8b8KilKg==", "6f45e1a3-4576-4235-ad5d-6251425380dc" });
        }
    }
}
