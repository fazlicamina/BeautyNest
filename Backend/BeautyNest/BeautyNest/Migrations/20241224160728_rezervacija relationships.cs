using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautyNest.Migrations
{
    /// <inheritdoc />
    public partial class rezervacijarelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "SalonId",
                table: "Rezervacije",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Rezervacije",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UslugaId",
                table: "Rezervacije",
                type: "int",
                nullable: false,
                defaultValue: 0);

          

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_SalonId",
                table: "Rezervacije",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_UserId",
                table: "Rezervacije",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_UslugaId",
                table: "Rezervacije",
                column: "UslugaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacije_Saloni_SalonId",
                table: "Rezervacije",
                column: "SalonId",
                principalTable: "Saloni",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacije_User_UserId",
                table: "Rezervacije",
                column: "UserId",
                 principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacije_Usluge_UslugaId",
                table: "Rezervacije",
                column: "UslugaId",
                principalTable: "Usluge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

     
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_Saloni_SalonId",
                table: "Rezervacije");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_User_UserId",
                table: "Rezervacije");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_Usluge_UslugaId",
                table: "Rezervacije");

           

            migrationBuilder.DropIndex(
                name: "IX_Rezervacije_SalonId",
                table: "Rezervacije");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacije_UserId",
                table: "Rezervacije");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacije_UslugaId",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "SalonId",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "UslugaId",
                table: "Rezervacije");

        
        }
    }
}
