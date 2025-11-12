using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefatoracaoEnterpriseClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientEnterprise");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientEnterprise",
                columns: table => new
                {
                    ClientsId = table.Column<long>(type: "bigint", nullable: false),
                    EnterprisesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientEnterprise", x => new { x.ClientsId, x.EnterprisesId });
                    table.ForeignKey(
                        name: "FK_ClientEnterprise_Clients_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientEnterprise_Enterprises_EnterprisesId",
                        column: x => x.EnterprisesId,
                        principalTable: "Enterprises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientEnterprise_EnterprisesId",
                table: "ClientEnterprise",
                column: "EnterprisesId");
        }
    }
}
