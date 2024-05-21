using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIService.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bots",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BotDatas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BotId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BotDatas_Bots_BotId",
                        column: x => x.BotId,
                        principalTable: "Bots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bots",
                columns: new[] { "Id", "Description", "Model", "Name", "TenantId" },
                values: new object[] { 1L, null, "gpt-4", "Main", 1L });

            migrationBuilder.InsertData(
                table: "BotDatas",
                columns: new[] { "Id", "BotId", "Content", "Name", "Role" },
                values: new object[] { 1L, 1L, "My name is Ali", "Initial", "system" });

            migrationBuilder.CreateIndex(
                name: "IX_BotDatas_BotId",
                table: "BotDatas",
                column: "BotId");

            migrationBuilder.CreateIndex(
                name: "IX_BotDatas_Role",
                table: "BotDatas",
                column: "Role");

            migrationBuilder.CreateIndex(
                name: "IX_Bots_TenantId_Name",
                table: "Bots",
                columns: new[] { "TenantId", "Name" },
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BotDatas");

            migrationBuilder.DropTable(
                name: "Bots");
        }
    }
}
