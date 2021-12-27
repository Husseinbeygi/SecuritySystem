using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecuritySystem.Infrastructre.Migrations
{
    public partial class AddedCameraname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CameraName",
                table: "IPCameras",
                type: "TEXT",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CameraName",
                table: "IPCameras");
        }
    }
}
