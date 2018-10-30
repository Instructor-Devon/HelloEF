using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HelloEF.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Owners",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Owners",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Owners",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Owners",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Owners");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Owners",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Owners",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
