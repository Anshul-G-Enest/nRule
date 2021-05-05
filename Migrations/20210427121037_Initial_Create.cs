using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rule.WebAPI.Migrations
{
    public partial class Initial_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NRules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    IsMale = table.Column<bool>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatementConnectors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatementConnectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RuleEngines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyName = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    SecondValue = table.Column<string>(nullable: true),
                    ConnectorId = table.Column<int>(nullable: false),
                    OperationId = table.Column<int>(nullable: false),
                    NRuleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleEngines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RuleEngines_StatementConnectors_ConnectorId",
                        column: x => x.ConnectorId,
                        principalTable: "StatementConnectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RuleEngines_NRules_NRuleId",
                        column: x => x.NRuleId,
                        principalTable: "NRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RuleEngines_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Operations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "EqualTo" },
                    { 17, "In" },
                    { 16, "IsNotNullNorWhiteSpace" },
                    { 15, "IsNotEmpty" },
                    { 14, "IsNotNull" },
                    { 13, "IsNullOrWhiteSpace" },
                    { 12, "IsEmpty" },
                    { 11, "IsNull" },
                    { 10, "Between" },
                    { 8, "LessThan" },
                    { 7, "GreaterThanOrEqualTo" },
                    { 6, "GreaterThan" },
                    { 5, "NotEqualTo" },
                    { 4, "EndsWith" },
                    { 3, "StartsWith" },
                    { 2, "Contains" },
                    { 9, "LessThanOrEqualTo" }
                });

            migrationBuilder.InsertData(
                table: "StatementConnectors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "And" },
                    { 2, "Or" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RuleEngines_ConnectorId",
                table: "RuleEngines",
                column: "ConnectorId");

            migrationBuilder.CreateIndex(
                name: "IX_RuleEngines_NRuleId",
                table: "RuleEngines",
                column: "NRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_RuleEngines_OperationId",
                table: "RuleEngines",
                column: "OperationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "RuleEngines");

            migrationBuilder.DropTable(
                name: "StatementConnectors");

            migrationBuilder.DropTable(
                name: "NRules");

            migrationBuilder.DropTable(
                name: "Operations");
        }
    }
}
