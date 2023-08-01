using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mercury.Server.Migrations
{
    /// <inheritdoc />
    public partial class personupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_AspNetUsers_AppUserId",
                schema: "Mercury",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                schema: "Mercury",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                schema: "Mercury",
                table: "Person",
                newName: "AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Person_AppUserId",
                schema: "Mercury",
                table: "Person",
                newName: "IX_Person_AppUserID");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserID",
                schema: "Mercury",
                table: "Person",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_AspNetUsers_AppUserID",
                schema: "Mercury",
                table: "Person",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_AspNetUsers_AppUserID",
                schema: "Mercury",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "AppUserID",
                schema: "Mercury",
                table: "Person",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Person_AppUserID",
                schema: "Mercury",
                table: "Person",
                newName: "IX_Person_AppUserId");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                schema: "Mercury",
                table: "Person",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUserID",
                schema: "Mercury",
                table: "Person",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_AspNetUsers_AppUserId",
                schema: "Mercury",
                table: "Person",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
