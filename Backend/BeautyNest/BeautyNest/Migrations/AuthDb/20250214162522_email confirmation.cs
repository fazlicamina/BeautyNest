using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class emailconfirmation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b4c05f1-06b6-431d-9e3a-3d4272ea3e05",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e7f80b89-3089-45eb-9e7a-a45659f13861", true, "AQAAAAIAAYagAAAAEG4SE3BPJ7wVBe7Z/gBLMR9S2K2HVJbLpN6AKT+HW8sG/cZacTwasHJJMS0gNHFvzA==", "9d6fe49e-1fe2-4824-8a50-6eace8d154d4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "375bdc8f-0099-4009-8fe3-a69079ef38cb",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "36734ab6-dd11-466b-b441-a1e003001839", true, "AQAAAAIAAYagAAAAEMPadxkJt5Z+74S0Ih+Ky5aQ1zfXOKWrTC1sWpThHVo0Bpxl4fKm92HS/ogHeugkuw==", "172cffb8-f0ad-4d60-b383-a06212b4db19" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41451580-acf7-422e-81b1-3bab4d8213a6",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab518e03-afe9-408a-bd4a-24f6fa4ead99", true, "AQAAAAIAAYagAAAAEK9m9IhJu3ZVRtYRu5oQiSmVYuZy+Q8avb/sVZCBjTB9OAd+D5hga9qkoyG3FeMk7w==", "070a643b-70f6-49d5-8c2b-c53f49a8bd4c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9b9687c2-256b-43d1-99fb-829bc64bad18",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db1641aa-1ff5-42a2-9934-60fbc91d21cd", true, "AQAAAAIAAYagAAAAEOaUCz1H6fUY65Vea2t7HnGjEeinV2ygnlcnpSmtd9QusVMHaAdst0mRt7amxUceVw==", "433239dd-324d-4b4b-9154-d075cfb8d670" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a9821a5d-6d8c-4b8b-8c68-cc6e52f1a529",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb1e2d6b-3ef4-4a69-8a20-64a6dc1e3366", true, "AQAAAAIAAYagAAAAENnmS8Bv6jLyhvJ5YjEPwB2FCZVztttffH/YQoN63EKGCd7At7/U8hOReT3ISniNxA==", "cf77b74a-0a37-4cd9-82d2-ca32c06ffb2f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b3b5c3e0-2f78-4f55-badb-d19d328c3240",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6cf34f1-7f03-4bfb-a98d-c38da8cb6f84", true, "AQAAAAIAAYagAAAAEJ4TS1JMcEyjmcujWC23r0XjmwUJ/qInRuc6N5cy8FJ00wxZB4rQei9pPXyanqYh6g==", "bff9b3ba-4dac-413e-902a-7d50f30f6211" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2e78953-bb8d-4b62-bb1a-d5e5bcb0af03",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dee25ecb-b19b-4c99-8d5f-356c268a4737", true, "AQAAAAIAAYagAAAAEPDwihqKg33s0mk76KBCS6hMoxWClm0MsFkzEB121Qa2ajh/iJtz9XxsO3E+DFvI7Q==", "9afd23c4-9f2a-453a-ac29-97bd64f386ab" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b4c05f1-06b6-431d-9e3a-3d4272ea3e05",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25f68d6f-a661-43df-9297-aa9ed86e3567", false, "AQAAAAIAAYagAAAAEKXBmeGKLYJlIqJ7Ce1dGbAEsH624dXwAdXMjcFgcqUGOLnQltyy8uChe4jEr19xpQ==", "9536492f-10f2-47e8-b6c6-4b844de7edc1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "375bdc8f-0099-4009-8fe3-a69079ef38cb",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0106e17e-8064-4396-9471-bb00095d215e", false, "AQAAAAIAAYagAAAAEHV5TcIsh9SvFie+X5WHeTe7Ui40Yh03b5dvQ+ST1i7IGjGoRRudzSjH3ujBvBsaVA==", "af52556a-dc51-4fd8-a47a-ffab22d907a8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41451580-acf7-422e-81b1-3bab4d8213a6",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3787a2ca-b3be-4068-9bda-421e4b2c7399", false, "AQAAAAIAAYagAAAAEN51HNm4Ab5FqTnkEb6PHLiOWlY6MWJOSF1cDxT0PfF9PYwkU5tjdpMoT5Qp6nQc1A==", "983a7e1d-34d8-449e-9de8-f70d4d609750" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9b9687c2-256b-43d1-99fb-829bc64bad18",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8c5a8784-cdf4-4c00-be9b-ad23c98405a6", false, "AQAAAAIAAYagAAAAENfjI/3QOACmtlFmlB/nyWeG2LKdSrezkxsMZTIi+Vdwq1+Q4WVUcwAud2yYnl9hhQ==", "572d5402-73d6-4a72-adec-e276a29c9e9e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a9821a5d-6d8c-4b8b-8c68-cc6e52f1a529",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84dacd11-b89c-4540-9044-85a6bc393dc4", false, "AQAAAAIAAYagAAAAEIkU0Lv0476KwmUnXXVT013FUeRyYwY9kWiRD1v2Z2WvRXPh7ZJuiFSdJdJLYqveAw==", "cbee35d4-85fa-4ced-b0c8-e27a024c2563" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b3b5c3e0-2f78-4f55-badb-d19d328c3240",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4620228-5833-4450-90ce-9315bb21f9c9", false, "AQAAAAIAAYagAAAAEMBssM1HlwAAII0PJedF6+vJWU72fR/EqHPzviSIonnxv3WyvnG9XD7jaz8fu+L7XQ==", "4105ace3-2e21-4b0b-b632-cb904fa98f97" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2e78953-bb8d-4b62-bb1a-d5e5bcb0af03",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51511450-f0fd-463f-b293-41dae84419e9", false, "AQAAAAIAAYagAAAAEFPrP0MH38HRduHqolpGn3EtP/NjHZOAf6/ZRL0dy37BG8Ys1ibZBzE0zcUar+lfYQ==", "aee04862-df42-4aea-bbd7-f1f894e030d8" });
        }
    }
}
