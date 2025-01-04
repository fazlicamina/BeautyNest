using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeautyNest.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class fixesusermodela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41451580-acf7-422e-81b1-3bab4d8213a6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce3ec36e-33a8-468c-a9ae-cd264337bd08", "AQAAAAIAAYagAAAAEF3hnYwLRJ6kTSXzSI1cTRolAUx8AQa0wu/fHlXU9bNNeAswPNijA+6Gs4SjbiVJaQ==", "11395f2e-c1ad-43dc-808d-dfb976c8f2ae" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a9821a5d-6d8c-4b8b-8c68-cc6e52f1a529", 0, "92936bc5-1fd6-4005-b545-f89b563042d3", "aldinh@gmail.com", false, "Aldin", "Hodžić", false, null, "ALDINH@GMAIL.COM", "ALDINH", "AQAAAAIAAYagAAAAEAc9sSUSGzeCd0NfvKKiuC4bCSxHBrGVRLLSQ9wUQotQXOzcrvH5xIgRP1ycvTECYQ==", null, false, null, "3657b323-caf1-4cc8-8637-eb5f3900d465", false, "aldinh" },
                    { "b3b5c3e0-2f78-4f55-badb-d19d328c3240", 0, "7e91a48b-b0e9-49fc-b6b4-2d10558e9eae", "adnah@gmail.com", false, "Adna", "Halilović", false, null, "ADNAH@GMAIL.COM", "ADNAH", "AQAAAAIAAYagAAAAEALmLGUA1gXFzX/dvGbJWGThQrH+KtUi0a6PoTs28PrZ9zyxfEk2uA+1MlF+zdiFNA==", null, false, null, "5e04ec03-15f5-47dd-b0be-98f0d34afc17", false, "adnah" },
                    { "e2e78953-bb8d-4b62-bb1a-d5e5bcb0af03", 0, "40dbedd5-8b4e-45d4-9287-aef0cc2056ef", "anidasabic@gmail.com", false, "Anida", "Šabić", false, null, "ANIDASABIC@GMAIL.COM", "ANIDASABIC", "AQAAAAIAAYagAAAAEOUzuuRjBMd9hsiV+l4jz9VP3r3SxqvvGtUSDsGqyMDzHfLb3oXThYXmcBcjxynHDg==", null, false, null, "7395b745-9e48-4b78-afed-1161778954e6", false, "anidasabic" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "cbf297f5-0f16-4294-95cb-bd94ba401f0a", "a9821a5d-6d8c-4b8b-8c68-cc6e52f1a529" },
                    { "cbf297f5-0f16-4294-95cb-bd94ba401f0a", "b3b5c3e0-2f78-4f55-badb-d19d328c3240" },
                    { "cbf297f5-0f16-4294-95cb-bd94ba401f0a", "e2e78953-bb8d-4b62-bb1a-d5e5bcb0af03" }
                });

            migrationBuilder.Sql(@"
                UPDATE AspNetUsers
                SET FirstName = 'Amina', LastName = 'Fazlic'
                WHERE UserName = 'fazlicamina02'
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cbf297f5-0f16-4294-95cb-bd94ba401f0a", "a9821a5d-6d8c-4b8b-8c68-cc6e52f1a529" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cbf297f5-0f16-4294-95cb-bd94ba401f0a", "b3b5c3e0-2f78-4f55-badb-d19d328c3240" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cbf297f5-0f16-4294-95cb-bd94ba401f0a", "e2e78953-bb8d-4b62-bb1a-d5e5bcb0af03" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a9821a5d-6d8c-4b8b-8c68-cc6e52f1a529");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b3b5c3e0-2f78-4f55-badb-d19d328c3240");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2e78953-bb8d-4b62-bb1a-d5e5bcb0af03");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41451580-acf7-422e-81b1-3bab4d8213a6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01d838f4-f995-40a0-a24f-817f9a799249", "AQAAAAIAAYagAAAAEDCrcUaCY4cT5s9ShoLObXwTm0dbQ8GEJaqcX4j2BDishNjWwad2gJZJW0DpjL5UFw==", "af4868d6-5bff-45bc-8d3c-4df7e9a2c689" });
        }
    }
}
