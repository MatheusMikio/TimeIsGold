using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class SchedulingEnterpriseId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EnterpriseId",
                table: "Schedulings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Schedulings_EnterpriseId",
                table: "Schedulings",
                column: "EnterpriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedulings_Enterprises_EnterpriseId",
                table: "Schedulings",
                column: "EnterpriseId",
                principalTable: "Enterprises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedulings_Enterprises_EnterpriseId",
                table: "Schedulings");

            migrationBuilder.DropIndex(
                name: "IX_Schedulings_EnterpriseId",
                table: "Schedulings");

            migrationBuilder.DropColumn(
                name: "EnterpriseId",
                table: "Schedulings");
        }
    }
}
