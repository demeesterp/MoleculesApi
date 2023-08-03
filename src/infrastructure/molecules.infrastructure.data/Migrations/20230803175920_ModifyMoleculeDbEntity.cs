using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace molecules.infrastructure.data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyMoleculeDbEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Molecule_OrderItemId",
                schema: "moleculesapp",
                table: "Molecule");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                schema: "moleculesapp",
                table: "Molecule");

            migrationBuilder.AddColumn<string>(
                name: "BasisSet",
                schema: "moleculesapp",
                table: "Molecule",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderName",
                schema: "moleculesapp",
                table: "Molecule",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Molecule_MoleculeName_OrderName_BasisSet",
                schema: "moleculesapp",
                table: "Molecule",
                columns: new[] { "MoleculeName", "OrderName", "BasisSet" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Molecule_MoleculeName_OrderName_BasisSet",
                schema: "moleculesapp",
                table: "Molecule");

            migrationBuilder.DropColumn(
                name: "BasisSet",
                schema: "moleculesapp",
                table: "Molecule");

            migrationBuilder.DropColumn(
                name: "OrderName",
                schema: "moleculesapp",
                table: "Molecule");

            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                schema: "moleculesapp",
                table: "Molecule",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Molecule_OrderItemId",
                schema: "moleculesapp",
                table: "Molecule",
                column: "OrderItemId",
                unique: true);
        }
    }
}
