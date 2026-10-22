using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoUmparaUm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Derrotas",
                table: "TB_PERSONAGENS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Disputas",
                table: "TB_PERSONAGENS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Vitorias",
                table: "TB_PERSONAGENS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonagemId",
                table: "TB_ARMAS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 1,
                column: "PersonagemId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 2,
                column: "PersonagemId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 3,
                column: "PersonagemId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 4,
                column: "PersonagemId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 5,
                column: "PersonagemId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 6,
                column: "PersonagemId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 7,
                column: "PersonagemId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 181, 149, 23, 144, 28, 217, 28, 25, 80, 114, 3, 124, 231, 17, 46, 198, 165, 130, 187, 86, 213, 146, 218, 48, 128, 111, 84, 66, 84, 224, 132, 219, 150, 76, 23, 209, 243, 155, 100, 88, 38, 247, 117, 105, 60, 190, 120, 161, 50, 240, 29, 192, 203, 127, 81, 180, 149, 174, 242, 78, 20, 255, 43, 236 }, new byte[] { 44, 251, 121, 52, 195, 23, 5, 181, 204, 102, 215, 68, 186, 199, 24, 171, 16, 140, 198, 214, 252, 34, 140, 213, 4, 24, 27, 166, 91, 168, 201, 116, 159, 120, 2, 178, 1, 218, 206, 186, 32, 111, 66, 154, 142, 234, 217, 49, 87, 174, 77, 253, 74, 75, 186, 43, 22, 189, 117, 73, 197, 213, 112, 174, 100, 188, 185, 248, 20, 155, 53, 90, 31, 184, 188, 130, 233, 52, 246, 244, 134, 93, 165, 184, 137, 117, 174, 73, 17, 159, 168, 83, 133, 20, 200, 42, 60, 135, 143, 55, 232, 238, 168, 179, 191, 112, 153, 188, 119, 85, 33, 44, 86, 250, 56, 216, 183, 244, 17, 245, 142, 36, 137, 160, 205, 73, 161, 46 } });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ARMAS_PersonagemId",
                table: "TB_ARMAS",
                column: "PersonagemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ARMAS_TB_PERSONAGENS_PersonagemId",
                table: "TB_ARMAS",
                column: "PersonagemId",
                principalTable: "TB_PERSONAGENS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_ARMAS_TB_PERSONAGENS_PersonagemId",
                table: "TB_ARMAS");

            migrationBuilder.DropIndex(
                name: "IX_TB_ARMAS_PersonagemId",
                table: "TB_ARMAS");

            migrationBuilder.DropColumn(
                name: "Derrotas",
                table: "TB_PERSONAGENS");

            migrationBuilder.DropColumn(
                name: "Disputas",
                table: "TB_PERSONAGENS");

            migrationBuilder.DropColumn(
                name: "Vitorias",
                table: "TB_PERSONAGENS");

            migrationBuilder.DropColumn(
                name: "PersonagemId",
                table: "TB_ARMAS");

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 173, 251, 180, 0, 16, 136, 233, 209, 189, 213, 240, 222, 79, 44, 53, 67, 200, 30, 235, 142, 79, 109, 204, 185, 59, 56, 83, 252, 59, 56, 77, 132, 212, 218, 170, 248, 63, 214, 248, 79, 106, 89, 196, 157, 174, 102, 138, 22, 37, 94, 218, 82, 35, 233, 167, 107, 249, 27, 127, 128, 21, 77, 250, 127 }, new byte[] { 105, 218, 86, 109, 133, 70, 152, 142, 31, 57, 206, 89, 119, 17, 15, 215, 227, 226, 49, 129, 123, 19, 149, 105, 94, 113, 211, 227, 241, 91, 141, 100, 188, 247, 227, 167, 178, 192, 167, 214, 9, 17, 22, 88, 107, 143, 244, 132, 121, 60, 96, 108, 75, 203, 52, 202, 218, 59, 129, 54, 91, 206, 210, 49, 40, 116, 175, 74, 109, 234, 151, 40, 90, 14, 191, 44, 34, 140, 59, 152, 210, 7, 213, 123, 19, 33, 169, 35, 9, 163, 197, 197, 138, 194, 240, 232, 181, 6, 79, 126, 26, 135, 132, 60, 236, 36, 222, 243, 211, 105, 138, 153, 240, 178, 79, 28, 247, 191, 19, 118, 50, 194, 164, 170, 102, 98, 42, 66 } });
        }
    }
}
