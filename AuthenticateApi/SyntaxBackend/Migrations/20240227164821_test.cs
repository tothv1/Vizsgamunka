using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyntaxBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "achievements",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    achievement_name = table.Column<string>(type: "char(255)", fixedLength: true, maxLength: 255, nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_hungarian_ci");

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    role_name = table.Column<string>(type: "char(32)", fixedLength: true, maxLength: 32, nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_hungarian_ci");

            migrationBuilder.CreateTable(
                name: "userstats",
                columns: table => new
                {
                    user_stat_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    kills = table.Column<int>(type: "int(11)", nullable: false),
                    deaths = table.Column<int>(type: "int(11)", nullable: false),
                    timesplayed = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.user_stat_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_hungarian_ci");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    username = table.Column<string>(type: "char(32)", fixedLength: true, maxLength: 32, nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "char(64)", fixedLength: true, maxLength: 64, nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    regdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    lastlogin = table.Column<DateTime>(type: "datetime", nullable: false),
                    roleid = table.Column<int>(type: "int(11)", nullable: false),
                    user_stats_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "User_fk0",
                        column: x => x.roleid,
                        principalTable: "roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "user_ibfk_1",
                        column: x => x.user_stats_id,
                        principalTable: "userstats",
                        principalColumn: "user_stat_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_hungarian_ci");

            migrationBuilder.CreateTable(
                name: "achievements_connect",
                columns: table => new
                {
                    achi_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userid = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.achi_id);
                    table.ForeignKey(
                        name: "achievements_connect_ibfk_1",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_hungarian_ci");

            migrationBuilder.CreateTable(
                name: "userachievements",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    achievement_id = table.Column<int>(type: "int(11)", nullable: false),
                    achi_connect_id = table.Column<int>(type: "int(11)", nullable: false),
                    achievement_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.user_id);
                    table.ForeignKey(
                        name: "userachievements_ibfk_1",
                        column: x => x.achi_connect_id,
                        principalTable: "achievements_connect",
                        principalColumn: "achi_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "userachievements_ibfk_2",
                        column: x => x.achievement_id,
                        principalTable: "achievements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_hungarian_ci");

            migrationBuilder.CreateIndex(
                name: "userid",
                table: "achievements_connect",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "User_fk0",
                table: "user",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "email",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "user_stats_id",
                table: "user",
                column: "user_stats_id");

            migrationBuilder.CreateIndex(
                name: "username",
                table: "user",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserAchievements_fk0",
                table: "userachievements",
                column: "achievement_id");

            migrationBuilder.CreateIndex(
                name: "achi_connect_id",
                table: "userachievements",
                column: "achi_connect_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userachievements");

            migrationBuilder.DropTable(
                name: "achievements_connect");

            migrationBuilder.DropTable(
                name: "achievements");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "userstats");
        }
    }
}
