using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoDisputas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_DISPUTAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dt_Disputa = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Atacanteld = table.Column<int>(type: "int", nullable: false),
                    OponenteId = table.Column<int>(type: "int", nullable: false),
                    Tx_Narracao = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_DISPUTAS", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 99, 232, 42, 228, 147, 32, 98, 255, 247, 73, 192, 180, 73, 3, 158, 146, 170, 200, 167, 63, 9, 203, 242, 197, 112, 17, 1, 11, 100, 237, 86, 125, 51, 243, 144, 73, 137, 212, 91, 117, 37, 168, 184, 215, 189, 144, 222, 56, 185, 142, 206, 62, 25, 74, 112, 142, 114, 75, 21, 106, 171, 157, 148, 166 }, new byte[] { 111, 38, 26, 6, 203, 185, 186, 44, 6, 84, 98, 69, 158, 175, 33, 253, 32, 126, 186, 61, 152, 55, 218, 225, 88, 34, 84, 190, 182, 111, 152, 255, 228, 15, 32, 52, 124, 110, 220, 240, 31, 67, 89, 167, 133, 247, 92, 66, 74, 69, 189, 101, 40, 185, 112, 179, 98, 42, 161, 100, 204, 226, 251, 125, 226, 83, 91, 41, 186, 166, 105, 21, 162, 121, 200, 170, 166, 169, 21, 242, 26, 165, 82, 119, 230, 218, 12, 193, 1, 131, 230, 108, 187, 109, 46, 10, 37, 161, 99, 210, 32, 80, 78, 127, 154, 129, 20, 246, 114, 115, 250, 193, 243, 187, 151, 225, 63, 84, 217, 39, 125, 204, 76, 198, 18, 236, 9, 79 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_DISPUTAS");

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 204, 248, 219, 177, 122, 63, 133, 94, 132, 7, 11, 113, 125, 11, 101, 195, 10, 185, 154, 165, 0, 100, 194, 25, 183, 191, 215, 95, 152, 220, 72, 186, 164, 37, 245, 61, 17, 169, 44, 1, 67, 63, 240, 224, 120, 6, 73, 24, 14, 175, 249, 201, 133, 193, 79, 181, 78, 191, 41, 128, 234, 127, 228, 161 }, new byte[] { 206, 32, 182, 173, 193, 158, 19, 175, 226, 187, 204, 227, 92, 162, 135, 189, 101, 221, 164, 148, 186, 71, 147, 178, 204, 135, 5, 96, 205, 78, 168, 168, 192, 23, 111, 165, 154, 72, 81, 148, 53, 213, 20, 151, 85, 178, 214, 49, 118, 42, 224, 233, 202, 199, 177, 139, 27, 163, 95, 237, 152, 249, 58, 44, 239, 121, 12, 130, 183, 132, 80, 58, 179, 26, 160, 197, 150, 10, 207, 170, 121, 19, 93, 241, 181, 34, 155, 161, 86, 238, 122, 14, 125, 64, 106, 137, 29, 199, 111, 207, 168, 39, 152, 77, 27, 163, 92, 94, 0, 104, 183, 53, 82, 140, 223, 227, 92, 157, 116, 229, 12, 219, 30, 129, 205, 37, 119, 251 } });
        }
    }
}
