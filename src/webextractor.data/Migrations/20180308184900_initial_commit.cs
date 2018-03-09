using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace webextractor.data.Migrations
{
    public partial class initial_commit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    domain = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Link",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    site_id = table.Column<Guid>(nullable: false),
                    url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Link", x => x.id);
                    table.ForeignKey(
                        name: "FK_Link_Site_site_id",
                        column: x => x.site_id,
                        principalTable: "Site",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expression",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    link_id = table.Column<Guid>(nullable: false),
                    order = table.Column<int>(nullable: false),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expression", x => x.id);
                    table.ForeignKey(
                        name: "FK_Expression_Link_link_id",
                        column: x => x.link_id,
                        principalTable: "Link",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expression_link_id",
                table: "Expression",
                column: "link_id");

            migrationBuilder.CreateIndex(
                name: "IX_Link_site_id",
                table: "Link",
                column: "site_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expression");

            migrationBuilder.DropTable(
                name: "Link");

            migrationBuilder.DropTable(
                name: "Site");
        }
    }
}
