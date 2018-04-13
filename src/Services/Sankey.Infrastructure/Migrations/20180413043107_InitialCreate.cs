using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Sankey.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Geo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Iso3166 = table.Column<string>(maxLength: 5, nullable: false),
                    NameEn = table.Column<string>(maxLength: 255, nullable: false),
                    NameFr = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Node",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NameEn = table.Column<string>(maxLength: 255, nullable: false),
                    NameFr = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Node", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GeoId = table.Column<int>(nullable: false),
                    SourceId = table.Column<int>(nullable: false),
                    Tag = table.Column<string>(maxLength: 255, nullable: false),
                    TargetId = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flow_Geo_GeoId",
                        column: x => x.GeoId,
                        principalTable: "Geo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flow_Node_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Node",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flow_Node_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Node",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flow_GeoId",
                table: "Flow",
                column: "GeoId");

            migrationBuilder.CreateIndex(
                name: "IX_Flow_SourceId",
                table: "Flow",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Flow_TargetId",
                table: "Flow",
                column: "TargetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flow");

            migrationBuilder.DropTable(
                name: "Geo");

            migrationBuilder.DropTable(
                name: "Node");
        }
    }
}
