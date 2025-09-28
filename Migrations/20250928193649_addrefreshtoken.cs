using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IEEE.Migrations
{
    /// <inheritdoc />
    public partial class addrefreshtoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subsections_Articles_ArticleId",
                table: "Subsections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subsections",
                table: "Subsections");

            migrationBuilder.RenameTable(
                name: "Subsections",
                newName: "Subsection");

            migrationBuilder.RenameIndex(
                name: "IX_Subsections_ArticleId",
                table: "Subsection",
                newName: "IX_Subsection_ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subsection",
                table: "Subsection",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId1",
                table: "RefreshToken",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Subsection_Articles_ArticleId",
                table: "Subsection",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subsection_Articles_ArticleId",
                table: "Subsection");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subsection",
                table: "Subsection");

            migrationBuilder.RenameTable(
                name: "Subsection",
                newName: "Subsections");

            migrationBuilder.RenameIndex(
                name: "IX_Subsection_ArticleId",
                table: "Subsections",
                newName: "IX_Subsections_ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subsections",
                table: "Subsections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subsections_Articles_ArticleId",
                table: "Subsections",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
