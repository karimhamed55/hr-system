using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IEEE.Migrations
{
    /// <inheritdoc />
    public partial class updatefix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Committees_CommitteeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CommitteeUser_AspNetUsers_UsersId",
                table: "CommitteeUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CommitteeUser_Committees_CommitteesId",
                table: "CommitteeUser");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CommitteeId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommitteeUser",
                table: "CommitteeUser");

            migrationBuilder.RenameTable(
                name: "CommitteeUser",
                newName: "UserCommittees");

            migrationBuilder.RenameIndex(
                name: "IX_CommitteeUser_UsersId",
                table: "UserCommittees",
                newName: "IX_UserCommittees_UsersId");

            migrationBuilder.AddColumn<int>(
                name: "ViceCommitteeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCommittees",
                table: "UserCommittees",
                columns: new[] { "CommitteesId", "UsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ViceCommitteeId",
                table: "AspNetUsers",
                column: "ViceCommitteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Committees_ViceCommitteeId",
                table: "AspNetUsers",
                column: "ViceCommitteeId",
                principalTable: "Committees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommittees_AspNetUsers_UsersId",
                table: "UserCommittees",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommittees_Committees_CommitteesId",
                table: "UserCommittees",
                column: "CommitteesId",
                principalTable: "Committees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Committees_ViceCommitteeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCommittees_AspNetUsers_UsersId",
                table: "UserCommittees");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCommittees_Committees_CommitteesId",
                table: "UserCommittees");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ViceCommitteeId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCommittees",
                table: "UserCommittees");

            migrationBuilder.DropColumn(
                name: "ViceCommitteeId",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "UserCommittees",
                newName: "CommitteeUser");

            migrationBuilder.RenameIndex(
                name: "IX_UserCommittees_UsersId",
                table: "CommitteeUser",
                newName: "IX_CommitteeUser_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommitteeUser",
                table: "CommitteeUser",
                columns: new[] { "CommitteesId", "UsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CommitteeId",
                table: "AspNetUsers",
                column: "CommitteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Committees_CommitteeId",
                table: "AspNetUsers",
                column: "CommitteeId",
                principalTable: "Committees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommitteeUser_AspNetUsers_UsersId",
                table: "CommitteeUser",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommitteeUser_Committees_CommitteesId",
                table: "CommitteeUser",
                column: "CommitteesId",
                principalTable: "Committees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
