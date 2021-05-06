using Microsoft.EntityFrameworkCore.Migrations;

namespace Rule.WebAPI.Migrations
{
    public partial class Add_EntityType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EntityTypeId",
                table: "RuleEngines",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EntityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EntityTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Airport" },
                    { 2, "Country" },
                    { 3, "Aircraft" },
                    { 4, "Trips" },
                    { 5, "Person" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RuleEngines_EntityTypeId",
                table: "RuleEngines",
                column: "EntityTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RuleEngines_EntityTypes_EntityTypeId",
                table: "RuleEngines",
                column: "EntityTypeId",
                principalTable: "EntityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RuleEngines_EntityTypes_EntityTypeId",
                table: "RuleEngines");

            migrationBuilder.DropTable(
                name: "EntityTypes");

            migrationBuilder.DropIndex(
                name: "IX_RuleEngines_EntityTypeId",
                table: "RuleEngines");

            migrationBuilder.DropColumn(
                name: "EntityTypeId",
                table: "RuleEngines");
        }
    }
}
