using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace molecules.infrastructure.data.Migrations
{
    /// <inheritdoc />
    public partial class AddCalcOrderItemDbEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalcOrderItem",
                schema: "moleculesapp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MoleculeName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Charge = table.Column<int>(type: "integer", nullable: false),
                    CalcType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    XYZ = table.Column<string>(type: "text", nullable: false),
                    CalcOrderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalcOrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalcOrderItem_CalcOrder_CalcOrderId",
                        column: x => x.CalcOrderId,
                        principalSchema: "moleculesapp",
                        principalTable: "CalcOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalcOrderItem_CalcOrderId",
                schema: "moleculesapp",
                table: "CalcOrderItem",
                column: "CalcOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalcOrderItem",
                schema: "moleculesapp");
        }
    }
}
