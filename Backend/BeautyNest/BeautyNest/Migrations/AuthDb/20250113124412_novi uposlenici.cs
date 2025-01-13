using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeautyNest.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class noviuposlenici : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41451580-acf7-422e-81b1-3bab4d8213a6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3787a2ca-b3be-4068-9bda-421e4b2c7399", "AQAAAAIAAYagAAAAEN51HNm4Ab5FqTnkEb6PHLiOWlY6MWJOSF1cDxT0PfF9PYwkU5tjdpMoT5Qp6nQc1A==", "983a7e1d-34d8-449e-9de8-f70d4d609750" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a9821a5d-6d8c-4b8b-8c68-cc6e52f1a529",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84dacd11-b89c-4540-9044-85a6bc393dc4", "AQAAAAIAAYagAAAAEIkU0Lv0476KwmUnXXVT013FUeRyYwY9kWiRD1v2Z2WvRXPh7ZJuiFSdJdJLYqveAw==", "cbee35d4-85fa-4ced-b0c8-e27a024c2563" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b3b5c3e0-2f78-4f55-badb-d19d328c3240",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4620228-5833-4450-90ce-9315bb21f9c9", "AQAAAAIAAYagAAAAEMBssM1HlwAAII0PJedF6+vJWU72fR/EqHPzviSIonnxv3WyvnG9XD7jaz8fu+L7XQ==", "4105ace3-2e21-4b0b-b632-cb904fa98f97" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2e78953-bb8d-4b62-bb1a-d5e5bcb0af03",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51511450-f0fd-463f-b293-41dae84419e9", "AQAAAAIAAYagAAAAEFPrP0MH38HRduHqolpGn3EtP/NjHZOAf6/ZRL0dy37BG8Ys1ibZBzE0zcUar+lfYQ==", "aee04862-df42-4aea-bbd7-f1f894e030d8" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SalonId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0b4c05f1-06b6-431d-9e3a-3d4272ea3e05", 0, "25f68d6f-a661-43df-9297-aa9ed86e3567", "samra@gmail.com", false, "Samra", "Fazlić", false, null, "SAMRA@GMAIL.COM", "SAMRA", "AQAAAAIAAYagAAAAEKXBmeGKLYJlIqJ7Ce1dGbAEsH624dXwAdXMjcFgcqUGOLnQltyy8uChe4jEr19xpQ==", null, false, null, 4, "9536492f-10f2-47e8-b6c6-4b844de7edc1", false, "samra" },
                    { "375bdc8f-0099-4009-8fe3-a69079ef38cb", 0, "0106e17e-8064-4396-9471-bb00095d215e", "sara@gmail.com", false, "Sara", "Hodžić", false, null, "SARA@GMAIL.COM", "SARA", "AQAAAAIAAYagAAAAEHV5TcIsh9SvFie+X5WHeTe7Ui40Yh03b5dvQ+ST1i7IGjGoRRudzSjH3ujBvBsaVA==", null, false, null, 2, "af52556a-dc51-4fd8-a47a-ffab22d907a8", false, "sara" },
                    { "9b9687c2-256b-43d1-99fb-829bc64bad18", 0, "8c5a8784-cdf4-4c00-be9b-ad23c98405a6", "azra@gmail.com", false, "Azra", "Hodžić", false, null, "AZRA@GMAIL.COM", "AZRA", "AQAAAAIAAYagAAAAENfjI/3QOACmtlFmlB/nyWeG2LKdSrezkxsMZTIi+Vdwq1+Q4WVUcwAud2yYnl9hhQ==", null, false, null, 3, "572d5402-73d6-4a72-adec-e276a29c9e9e", false, "azra" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "cbf297f5-0f16-4294-95cb-bd94ba401f0a", "0b4c05f1-06b6-431d-9e3a-3d4272ea3e05" },
                    { "cbf297f5-0f16-4294-95cb-bd94ba401f0a", "375bdc8f-0099-4009-8fe3-a69079ef38cb" },
                    { "cbf297f5-0f16-4294-95cb-bd94ba401f0a", "9b9687c2-256b-43d1-99fb-829bc64bad18" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cbf297f5-0f16-4294-95cb-bd94ba401f0a", "0b4c05f1-06b6-431d-9e3a-3d4272ea3e05" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cbf297f5-0f16-4294-95cb-bd94ba401f0a", "375bdc8f-0099-4009-8fe3-a69079ef38cb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cbf297f5-0f16-4294-95cb-bd94ba401f0a", "9b9687c2-256b-43d1-99fb-829bc64bad18" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b4c05f1-06b6-431d-9e3a-3d4272ea3e05");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "375bdc8f-0099-4009-8fe3-a69079ef38cb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9b9687c2-256b-43d1-99fb-829bc64bad18");


            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41451580-acf7-422e-81b1-3bab4d8213a6",
                columns: new[] { "ConcurrencyStamp", "OmiljeniSaloniIds", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cfa6087b-f8bc-4b73-9338-441e47f7c3c4", null, "AQAAAAIAAYagAAAAENlQMA92gzeBB1HdQFzU/Bn8RVnYQSn5M1dNvlLROYx9N6QYYwKJdTRWnt+ykPtYHA==", "6d7b3816-34e6-48af-913b-ac910870e550" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a9821a5d-6d8c-4b8b-8c68-cc6e52f1a529",
                columns: new[] { "ConcurrencyStamp", "OmiljeniSaloniIds", "PasswordHash", "SecurityStamp" },
                values: new object[] { "545b5288-ecb7-4133-89b7-a9f954e321ce", null, "AQAAAAIAAYagAAAAENzA5+seZd99drWd30iAfUI04RmeSyNIPs7MF8c/TRfC10U5LBBKfteD1My3qstfvw==", "bd7216b9-4b7d-4a03-9ee2-3764bec40330" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b3b5c3e0-2f78-4f55-badb-d19d328c3240",
                columns: new[] { "ConcurrencyStamp", "OmiljeniSaloniIds", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a74d4dfb-ae9d-49a0-a3c7-4306fc714e05", null, "AQAAAAIAAYagAAAAEO53WpsofaD4q6jc4VwWVjl1zmLm/OdirvAFLXA+ObITeu2BrlKmBPIviZWuK0AIag==", "ae8ca808-1a5a-4a57-aba0-5dc2bfc2c792" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2e78953-bb8d-4b62-bb1a-d5e5bcb0af03",
                columns: new[] { "ConcurrencyStamp", "OmiljeniSaloniIds", "PasswordHash", "SecurityStamp" },
                values: new object[] { "523930c6-bfc7-4cf6-af23-ecb0f542baae", null, "AQAAAAIAAYagAAAAEJjXEq3Gh5u/BCxlGICtzYlBcPWlKiUDCIRsFpa6CrHWe24pHON9Bwy1u/PqX9QW1w==", "eb5ba04b-3c2c-411e-a6f2-38ae66f18a2a" });
        }
    }
}
