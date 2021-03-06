﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HelloEF.Migrations
{
    public partial class RemovingWalks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Walks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Walks",
                columns: table => new
                {
                    WalkId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Distance = table.Column<float>(nullable: false),
                    DogId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Walks", x => x.WalkId);
                    table.ForeignKey(
                        name: "FK_Walks_Dogs_DogId",
                        column: x => x.DogId,
                        principalTable: "Dogs",
                        principalColumn: "DogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Walks_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Walks_DogId",
                table: "Walks",
                column: "DogId");

            migrationBuilder.CreateIndex(
                name: "IX_Walks_OwnerId",
                table: "Walks",
                column: "OwnerId");
        }
    }
}
