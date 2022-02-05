using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecuritySystem.Infrastructre.Migrations
{
    public partial class AddClientTopic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Client",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Client_ClientId",
                table: "Client",
                column: "ClientId");

            migrationBuilder.CreateTable(
                name: "ClientTopics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Topic = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ClientId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Caption = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTopics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientTopics_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientTopics_ClientId",
                table: "ClientTopics",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientTopics");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Client_ClientId",
                table: "Client");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Client",
                type: "TEXT",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);
        }
    }
}
