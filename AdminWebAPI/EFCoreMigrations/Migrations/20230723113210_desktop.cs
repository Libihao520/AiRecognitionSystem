using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreMigrations.Migrations
{
    public partial class desktop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeskTops",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    yf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fz = table.Column<int>(type: "int", nullable: false),
                    sf = table.Column<int>(type: "int", nullable: false),
                    df = table.Column<int>(type: "int", nullable: false),
                    hj = table.Column<int>(type: "int", nullable: false),
                    cr = table.Column<int>(type: "int", nullable: false),
                    sy = table.Column<int>(type: "int", nullable: false),
                    jldc = table.Column<int>(type: "int", nullable: false),
                    ck = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateUserId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyUserId = table.Column<long>(type: "bigint", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeskTops", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeskTops");
        }
    }
}
