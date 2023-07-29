using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace molecules.infrastructure.data.Migrations
{
    /// <inheritdoc />
    public partial class AddMoleculeDbEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Molecule",
                schema: "moleculesapp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderItemId = table.Column<int>(type: "integer", nullable: false),
                    MoleculeName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Molecule = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Molecule", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Molecule_OrderItemId",
                schema: "moleculesapp",
                table: "Molecule",
                column: "OrderItemId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Molecule",
                schema: "moleculesapp");
        }
    }
}
