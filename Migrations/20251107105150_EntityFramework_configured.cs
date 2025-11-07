using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberBoardAPI.Migrations
{
    /// <inheritdoc />
    public partial class EntityFramework_configured : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Agents_AgentId",
                table: "Missions");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Agents_TargetAgentId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Missions_AgentId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Missions");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Agents",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "AgencyId",
                table: "Agents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartingDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgentMission",
                columns: table => new
                {
                    AgentsAssignedId = table.Column<int>(type: "int", nullable: false),
                    MissionsAssignedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentMission", x => new { x.AgentsAssignedId, x.MissionsAssignedId });
                    table.ForeignKey(
                        name: "FK_AgentMission_Agents_AgentsAssignedId",
                        column: x => x.AgentsAssignedId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgentMission_Missions_MissionsAssignedId",
                        column: x => x.MissionsAssignedId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_AgencyId",
                table: "Agents",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentMission_MissionsAssignedId",
                table: "AgentMission",
                column: "MissionsAssignedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Agencies_AgencyId",
                table: "Agents",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Agents_TargetAgentId",
                table: "Notifications",
                column: "TargetAgentId",
                principalTable: "Agents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Agencies_AgencyId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Agents_TargetAgentId",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "Agencies");

            migrationBuilder.DropTable(
                name: "AgentMission");

            migrationBuilder.DropIndex(
                name: "IX_Agents_AgencyId",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "AgencyId",
                table: "Agents");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Agents",
                newName: "Username");

            migrationBuilder.AddColumn<int>(
                name: "AgentId",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Missions_AgentId",
                table: "Missions",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Agents_AgentId",
                table: "Missions",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Agents_TargetAgentId",
                table: "Notifications",
                column: "TargetAgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
