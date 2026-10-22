using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoMuitosParaMuitos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_HABILIDADES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Dano = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_HABILIDADES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_PERSONAGENS_HABILIDADES",
                columns: table => new
                {
                    PersonagemId = table.Column<int>(type: "int", nullable: false),
                    HabilidadeId = table.Column<int>(type: "int", nullable: false),
                    PersonagemHabilidadeHabilidadeId = table.Column<int>(type: "int", nullable: true),
                    PersonagemHabilidadePersonagemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PERSONAGENS_HABILIDADES", x => new { x.PersonagemId, x.HabilidadeId });
                    table.ForeignKey(
                        name: "FK_TB_PERSONAGENS_HABILIDADES_TB_HABILIDADES_HabilidadeId",
                        column: x => x.HabilidadeId,
                        principalTable: "TB_HABILIDADES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_PERSONAGENS_HABILIDADES_TB_PERSONAGENS_HABILIDADES_PersonagemHabilidadePersonagemId_PersonagemHabilidadeHabilidadeId",
                        columns: x => new { x.PersonagemHabilidadePersonagemId, x.PersonagemHabilidadeHabilidadeId },
                        principalTable: "TB_PERSONAGENS_HABILIDADES",
                        principalColumns: new[] { "PersonagemId", "HabilidadeId" });
                    table.ForeignKey(
                        name: "FK_TB_PERSONAGENS_HABILIDADES_TB_PERSONAGENS_PersonagemId",
                        column: x => x.PersonagemId,
                        principalTable: "TB_PERSONAGENS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TB_HABILIDADES",
                columns: new[] { "Id", "Dano", "Nome" },
                values: new object[,]
                {
                    { 1, 39, "Adormecer" },
                    { 2, 41, "Congelar" },
                    { 3, 37, "Hipnotizar" }
                });

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 204, 248, 219, 177, 122, 63, 133, 94, 132, 7, 11, 113, 125, 11, 101, 195, 10, 185, 154, 165, 0, 100, 194, 25, 183, 191, 215, 95, 152, 220, 72, 186, 164, 37, 245, 61, 17, 169, 44, 1, 67, 63, 240, 224, 120, 6, 73, 24, 14, 175, 249, 201, 133, 193, 79, 181, 78, 191, 41, 128, 234, 127, 228, 161 }, new byte[] { 206, 32, 182, 173, 193, 158, 19, 175, 226, 187, 204, 227, 92, 162, 135, 189, 101, 221, 164, 148, 186, 71, 147, 178, 204, 135, 5, 96, 205, 78, 168, 168, 192, 23, 111, 165, 154, 72, 81, 148, 53, 213, 20, 151, 85, 178, 214, 49, 118, 42, 224, 233, 202, 199, 177, 139, 27, 163, 95, 237, 152, 249, 58, 44, 239, 121, 12, 130, 183, 132, 80, 58, 179, 26, 160, 197, 150, 10, 207, 170, 121, 19, 93, 241, 181, 34, 155, 161, 86, 238, 122, 14, 125, 64, 106, 137, 29, 199, 111, 207, 168, 39, 152, 77, 27, 163, 92, 94, 0, 104, 183, 53, 82, 140, 223, 227, 92, 157, 116, 229, 12, 219, 30, 129, 205, 37, 119, 251 } });

            migrationBuilder.InsertData(
                table: "TB_PERSONAGENS_HABILIDADES",
                columns: new[] { "HabilidadeId", "PersonagemId", "PersonagemHabilidadeHabilidadeId", "PersonagemHabilidadePersonagemId" },
                values: new object[,]
                {
                    { 1, 1, null, null },
                    { 2, 1, null, null },
                    { 2, 2, null, null },
                    { 2, 3, null, null },
                    { 3, 3, null, null },
                    { 3, 4, null, null },
                    { 1, 5, null, null },
                    { 2, 6, null, null },
                    { 3, 7, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_PERSONAGENS_HABILIDADES_HabilidadeId",
                table: "TB_PERSONAGENS_HABILIDADES",
                column: "HabilidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PERSONAGENS_HABILIDADES_PersonagemHabilidadePersonagemId_PersonagemHabilidadeHabilidadeId",
                table: "TB_PERSONAGENS_HABILIDADES",
                columns: new[] { "PersonagemHabilidadePersonagemId", "PersonagemHabilidadeHabilidadeId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_PERSONAGENS_HABILIDADES");

            migrationBuilder.DropTable(
                name: "TB_HABILIDADES");

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 181, 149, 23, 144, 28, 217, 28, 25, 80, 114, 3, 124, 231, 17, 46, 198, 165, 130, 187, 86, 213, 146, 218, 48, 128, 111, 84, 66, 84, 224, 132, 219, 150, 76, 23, 209, 243, 155, 100, 88, 38, 247, 117, 105, 60, 190, 120, 161, 50, 240, 29, 192, 203, 127, 81, 180, 149, 174, 242, 78, 20, 255, 43, 236 }, new byte[] { 44, 251, 121, 52, 195, 23, 5, 181, 204, 102, 215, 68, 186, 199, 24, 171, 16, 140, 198, 214, 252, 34, 140, 213, 4, 24, 27, 166, 91, 168, 201, 116, 159, 120, 2, 178, 1, 218, 206, 186, 32, 111, 66, 154, 142, 234, 217, 49, 87, 174, 77, 253, 74, 75, 186, 43, 22, 189, 117, 73, 197, 213, 112, 174, 100, 188, 185, 248, 20, 155, 53, 90, 31, 184, 188, 130, 233, 52, 246, 244, 134, 93, 165, 184, 137, 117, 174, 73, 17, 159, 168, 83, 133, 20, 200, 42, 60, 135, 143, 55, 232, 238, 168, 179, 191, 112, 153, 188, 119, 85, 33, 44, 86, 250, 56, 216, 183, 244, 17, 245, 142, 36, 137, 160, 205, 73, 161, 46 } });
        }
    }
}
