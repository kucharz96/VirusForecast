using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VirusForecast.Migrations
{
    public partial class UpdateVirusCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Clinics_ClinicId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_VirusCases_Clinics_ClinicId",
                table: "VirusCases");

            migrationBuilder.DropForeignKey(
                name: "FK_VirusCases_Regions_RegionId",
                table: "VirusCases");

            migrationBuilder.DropForeignKey(
                name: "FK_VirusCases_WorkModes_WorkModeId",
                table: "VirusCases");

            migrationBuilder.AlterColumn<string>(
                name: "WorkModeId",
                table: "VirusCases",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "RegionId",
                table: "VirusCases",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "ClinicId",
                table: "VirusCases",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "VirusCases",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Clinics_ClinicId",
                table: "AspNetUsers",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VirusCases_Clinics_ClinicId",
                table: "VirusCases",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VirusCases_Regions_RegionId",
                table: "VirusCases",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VirusCases_WorkModes_WorkModeId",
                table: "VirusCases",
                column: "WorkModeId",
                principalTable: "WorkModes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Clinics_ClinicId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_VirusCases_Clinics_ClinicId",
                table: "VirusCases");

            migrationBuilder.DropForeignKey(
                name: "FK_VirusCases_Regions_RegionId",
                table: "VirusCases");

            migrationBuilder.DropForeignKey(
                name: "FK_VirusCases_WorkModes_WorkModeId",
                table: "VirusCases");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "VirusCases");

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkModeId",
                table: "VirusCases",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RegionId",
                table: "VirusCases",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ClinicId",
                table: "VirusCases",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Clinics_ClinicId",
                table: "AspNetUsers",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VirusCases_Clinics_ClinicId",
                table: "VirusCases",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VirusCases_Regions_RegionId",
                table: "VirusCases",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VirusCases_WorkModes_WorkModeId",
                table: "VirusCases",
                column: "WorkModeId",
                principalTable: "WorkModes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
