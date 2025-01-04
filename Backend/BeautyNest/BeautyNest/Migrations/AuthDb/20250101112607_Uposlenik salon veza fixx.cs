using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Uposleniksalonvezafixx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalonId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41451580-acf7-422e-81b1-3bab4d8213a6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SalonId", "SecurityStamp" },
                values: new object[] { "fb5c8fda-26ed-441b-89a2-ffc08e71926f", "AQAAAAIAAYagAAAAEByZi6/y89KcuRooBKF7G8tZAmUir4zB28v/yuE4H4qB6eOMr0FSWieCGh/kCngPVg==", null, "24f1140f-4f31-404f-9cca-5fdfcc2b0ec4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a9821a5d-6d8c-4b8b-8c68-cc6e52f1a529",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SalonId", "SecurityStamp" },
                values: new object[] { "9904dce6-ecce-49d8-95d1-360fef6bd7bd", "AQAAAAIAAYagAAAAEOmX4+AcjKXrbWyFHwvDcAhyc/0LAmK/dE7/6w8L1LRBfAe0dCx6uNruxxGYRg13Fw==", 1, "20fd109d-fefc-40b4-b3af-40316ab48ecb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b3b5c3e0-2f78-4f55-badb-d19d328c3240",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SalonId", "SecurityStamp" },
                values: new object[] { "9dabec18-3e9c-4670-a579-9482cf9443a8", "AQAAAAIAAYagAAAAEMZR3uKlgKggb0XXRGXYEjCJ4H9y+lkQTj6heiCGyL5eo8mj3/Ht+07iEHexsvBXkQ==", 1, "ed41aba1-4436-4ae3-ba13-02a17d29fa7f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2e78953-bb8d-4b62-bb1a-d5e5bcb0af03",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SalonId", "SecurityStamp" },
                values: new object[] { "6c342e25-50ad-4297-951e-2b30245b7c13", "AQAAAAIAAYagAAAAEJtWilUi9fJ11kmmWkHsuw8o8RGkKQkaHosjI426+THwybUTNnzGPfr6ehr+9OGa3w==", 1, "6dd3522f-ede6-46ee-a33e-fb44e6827cbd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalonId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41451580-acf7-422e-81b1-3bab4d8213a6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6de39297-272f-4665-a7eb-5dec9e88a1a5", "AQAAAAIAAYagAAAAEB+GOunEFF1Vauq5oWUc6mQWcL0iL+vwhdYque2Qa+y45aeQL7w0vSt4TZJJQQsUjQ==", "2991dd55-ad6d-46cf-9f80-30dfca829a2a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a9821a5d-6d8c-4b8b-8c68-cc6e52f1a529",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20248747-2410-4948-bc55-df8acafe0415", "AQAAAAIAAYagAAAAEJArT8ufhzTn7lGF6GJVaHtAxg6MYpDhVDXLBnJQVIhUl2amXAFxThzNH+tKb/sKJQ==", "04f47a58-613e-47ac-a1a1-ed0a1aedb76c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b3b5c3e0-2f78-4f55-badb-d19d328c3240",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c67f182-5197-487c-b178-32a9995d321e", "AQAAAAIAAYagAAAAEL4fe1mTpBmKb9qyT6ZOBY5a5y5vZ4VCROpxO6qVCJ5w2AS/74xDCHZa8FXxCngo8w==", "c6aad850-ae2d-4011-be56-ff1a04e1cfe9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2e78953-bb8d-4b62-bb1a-d5e5bcb0af03",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d09de8b3-e84f-4a70-9063-f221cec97d90", "AQAAAAIAAYagAAAAEG/MsIIs1mKFLwy1Gb3/2q7TX4BwBM7Z2Rlq0fbkWOL++vrqFw0NXj4VWRLDAZSpzQ==", "d0266697-661a-433d-b286-dae1c6137db9" });
        }
    }
}
