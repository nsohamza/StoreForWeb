using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Migrations
{
    /// <inheritdoc />
    public partial class AutoIncrement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                newName: "IX_Orders_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_userId",
                table: "Orders",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_userId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_userId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
