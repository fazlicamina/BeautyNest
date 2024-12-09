using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Userfixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41451580-acf7-422e-81b1-3bab4d8213a6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ec43eae-5a40-4c89-874a-995e4628b8fd", "AQAAAAIAAYagAAAAELC9uRoIjug/SRPtvnslqIIkw0NgiPbRs+s7mqTaTdhhJLmXrcwJYxRUhr6c84r2rQ==", "2d54173c-8844-4920-b7c0-2aeff2214e99" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41451580-acf7-422e-81b1-3bab4d8213a6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4ffc09f7-af98-4d8f-aa72-8bcfee1e03bc", "AQAAAAIAAYagAAAAEGMK5vD0NV40JTxna/sxDqYrS59iENcp8tNFw9tNnzLQ80uZiCu81u5beL0mgjJouQ==", "d0f95f75-32d1-4c09-ad20-12af0d2a1d81" });
        }
    }
}
