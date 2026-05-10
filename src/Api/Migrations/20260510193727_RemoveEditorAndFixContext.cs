using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEditorAndFixContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id_category = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id_category);
                });

            migrationBuilder.CreateTable(
                name: "MoodPrimaries",
                columns: table => new
                {
                    id_mood_primary = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoodPrimaries", x => x.id_mood_primary);
                });

            migrationBuilder.CreateTable(
                name: "PageStocks",
                columns: table => new
                {
                    id_page_stock = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    slug_url = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    html_content = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    is_system_page = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageStocks", x => x.id_page_stock);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id_role = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id_role);
                });

            migrationBuilder.CreateTable(
                name: "MoodDetails",
                columns: table => new
                {
                    id_mood_detail = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    id_mood_primary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoodDetails", x => x.id_mood_detail);
                    table.ForeignKey(
                        name: "FK_MoodDetails_MoodPrimaries_id_mood_primary",
                        column: x => x.id_mood_primary,
                        principalTable: "MoodPrimaries",
                        principalColumn: "id_mood_primary",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    id_menu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    label = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sort_order = table.Column<int>(type: "int", nullable: false),
                    id_page_stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.id_menu);
                    table.ForeignKey(
                        name: "FK_Menus_PageStocks_id_page_stock",
                        column: x => x.id_page_stock,
                        principalTable: "PageStocks",
                        principalColumn: "id_page_stock",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    statut = table.Column<int>(type: "int", nullable: false),
                    firstname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    lastname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    id_role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id_user);
                    table.ForeignKey(
                        name: "FK_Users_Roles_id_role",
                        column: x => x.id_role,
                        principalTable: "Roles",
                        principalColumn: "id_role",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    id_article = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdById = table.Column<int>(type: "int", nullable: false),
                    lastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    id_category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.id_article);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_id_category",
                        column: x => x.id_category,
                        principalTable: "Categories",
                        principalColumn: "id_category",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Articles_Users_createdById",
                        column: x => x.createdById,
                        principalTable: "Users",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    id_profile = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    avatar = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    last_connection = table.Column<DateTime>(type: "datetime2", nullable: true),
                    gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    id_user = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.id_profile);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_id_user",
                        column: x => x.id_user,
                        principalTable: "Users",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    id_session = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    token = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    date_ = table.Column<DateTime>(type: "datetime2", nullable: true),
                    adress_ip = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    expire_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    id_user = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.id_session);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_id_user",
                        column: x => x.id_user,
                        principalTable: "Users",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrackerLogs",
                columns: table => new
                {
                    id_tracker_log = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date_ = table.Column<DateTime>(type: "datetime2", nullable: true),
                    comment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    id_profile = table.Column<int>(type: "int", nullable: false),
                    id_mood_detail = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackerLogs", x => x.id_tracker_log);
                    table.ForeignKey(
                        name: "FK_TrackerLogs_MoodDetails_id_mood_detail",
                        column: x => x.id_mood_detail,
                        principalTable: "MoodDetails",
                        principalColumn: "id_mood_detail",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrackerLogs_Profiles_id_profile",
                        column: x => x.id_profile,
                        principalTable: "Profiles",
                        principalColumn: "id_profile",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_createdById",
                table: "Articles",
                column: "createdById");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_id_category",
                table: "Articles",
                column: "id_category");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_id_page_stock",
                table: "Menus",
                column: "id_page_stock");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_label",
                table: "Menus",
                column: "label",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoodDetails_id_mood_primary",
                table: "MoodDetails",
                column: "id_mood_primary");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_id_user",
                table: "Profiles",
                column: "id_user",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_id_user",
                table: "Sessions",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_TrackerLogs_id_mood_detail",
                table: "TrackerLogs",
                column: "id_mood_detail");

            migrationBuilder.CreateIndex(
                name: "IX_TrackerLogs_id_profile",
                table: "TrackerLogs",
                column: "id_profile");

            migrationBuilder.CreateIndex(
                name: "IX_Users_email",
                table: "Users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_id_role",
                table: "Users",
                column: "id_role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "TrackerLogs");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "PageStocks");

            migrationBuilder.DropTable(
                name: "MoodDetails");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "MoodPrimaries");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
