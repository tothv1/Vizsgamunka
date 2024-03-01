using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "blacklisted_tokens",
                columns: table => new
                {
                    token_id = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    token = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    blacklisted_status_expires = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.token_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_hungarian_ci");

            migrationBuilder.CreateTable(
                name: "confirmation_keys",
                columns: table => new
                {
                    keyid = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userid = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    confirmation_key = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    expiration_time = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.keyid);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_hungarian_ci");

            migrationBuilder.CreateTable(
                name: "logged_in_users",
                columns: table => new
                {
                    userid = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    token = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.userid);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    roleid = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    role_name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.roleid);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_hungarian_ci");

            migrationBuilder.CreateTable(
                name: "temp_roles",
                columns: table => new
                {
                    temp_role_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    role_name = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.temp_role_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_hungarian_ci");

            migrationBuilder.CreateTable(
                name: "registered_users",
                columns: table => new
                {
                    userid = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: false, collation: "utf8_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    username = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, collation: "utf8_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    fullname = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: false, collation: "utf8_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    email = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, collation: "utf8_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    hash = table.Column<string>(type: "text", nullable: false, collation: "utf8_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    is_logged_in = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    regdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    roleid = table.Column<int>(type: "int(11)", nullable: false, defaultValueSql: "'2'"),
                    confirmation_keyid = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.userid);
                    table.ForeignKey(
                        name: "registered_users_ibfk_1",
                        column: x => x.confirmation_keyid,
                        principalTable: "confirmation_keys",
                        principalColumn: "keyid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "registered_users_ibfk_2",
                        column: x => x.roleid,
                        principalTable: "roles",
                        principalColumn: "roleid");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_hungarian_ci");

            migrationBuilder.CreateTable(
                name: "registry",
                columns: table => new
                {
                    temp_userid = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    temp_username = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, collation: "utf8_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    temp_fullname = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: false, collation: "utf8_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    temp_email = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, collation: "utf8_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    temp_hash = table.Column<string>(type: "text", nullable: false, collation: "utf8_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    temp_regdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    temp_roleid = table.Column<int>(type: "int(11)", nullable: true),
                    temp_user_expire = table.Column<DateTime>(type: "datetime", nullable: false),
                    temp_confirmation_key = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.temp_userid);
                    table.ForeignKey(
                        name: "registry_ibfk_1",
                        column: x => x.temp_roleid,
                        principalTable: "temp_roles",
                        principalColumn: "temp_role_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_hungarian_ci");

            migrationBuilder.CreateIndex(
                name: "token",
                table: "blacklisted_tokens",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "userid",
                table: "confirmation_keys",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "confirmation_keyid",
                table: "registered_users",
                column: "confirmation_keyid");

            migrationBuilder.CreateIndex(
                name: "email",
                table: "registered_users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "roleid",
                table: "registered_users",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "username",
                table: "registered_users",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "temp_email",
                table: "registry",
                column: "temp_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "temp_roleid",
                table: "registry",
                column: "temp_roleid");

            migrationBuilder.CreateIndex(
                name: "temp_username",
                table: "registry",
                column: "temp_username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "role_name",
                table: "roles",
                column: "role_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blacklisted_tokens");

            migrationBuilder.DropTable(
                name: "logged_in_users");

            migrationBuilder.DropTable(
                name: "registered_users");

            migrationBuilder.DropTable(
                name: "registry");

            migrationBuilder.DropTable(
                name: "confirmation_keys");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "temp_roles");
        }
    }
}
