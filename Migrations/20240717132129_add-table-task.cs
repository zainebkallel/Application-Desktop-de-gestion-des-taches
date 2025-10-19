using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfApp1.Migrations
{
    /// <inheritdoc />
    public partial class addtabletask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TASK",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    T_TITLE = table.Column<string>(type: "TEXT", nullable: false),
                    T_DESCRIPTION = table.Column<string>(type: "TEXT", nullable: false),
                    T_CATEGORY = table.Column<int>(type: "TEXT", nullable: false),
                    T_STATUS = table.Column<int>(type: "TEXT", nullable: false),
                    T_START_DATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    T_END_DATE = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TASK", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TASK");
        }
    }
}
