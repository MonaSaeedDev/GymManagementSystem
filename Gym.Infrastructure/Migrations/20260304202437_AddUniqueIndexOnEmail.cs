using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexOnEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Trainers_TraineerId",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "TraineerId",
                table: "Sessions",
                newName: "TrainerId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_TraineerId",
                table: "Sessions",
                newName: "IX_Sessions_TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_Email",
                table: "Members",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Trainers_TrainerId",
                table: "Sessions",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Trainers_TrainerId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Members_Email",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "TrainerId",
                table: "Sessions",
                newName: "TraineerId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_TrainerId",
                table: "Sessions",
                newName: "IX_Sessions_TraineerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Trainers_TraineerId",
                table: "Sessions",
                column: "TraineerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
