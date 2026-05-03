using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialsHubWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class finalUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionReport_Account",
                table: "TransactionReport");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "Notification");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_AccountId",
                table: "Notification",
                newName: "IX_Notification_AccountId");

            migrationBuilder.AlterColumn<string>(
                name: "ReportName",
                table: "TransactionReport",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorAccountId",
                table: "TransactionReport",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TransactionReport",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "TransactionReport",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ReviewedByAccountId",
                table: "TransactionReport",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TransactionReport",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "TransactionReport",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TransictionAccountId",
                table: "TransactionReport",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDate",
                table: "TransactionRecord",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TransictionAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullNameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransictionAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationFinancess",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RelatedReportId = table.Column<long>(type: "bigint", nullable: true),
                    TransictionAccountId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationFinancess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationFinancess_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationFinancess_TransactionReport_RelatedReportId",
                        column: x => x.RelatedReportId,
                        principalTable: "TransactionReport",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NotificationFinancess_TransictionAccounts_TransictionAccountId",
                        column: x => x.TransictionAccountId,
                        principalTable: "TransictionAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionReport_ReviewedByAccountId",
                table: "TransactionReport",
                column: "ReviewedByAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionReport_TransictionAccountId",
                table: "TransactionReport",
                column: "TransictionAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationFinancess_AccountId",
                table: "NotificationFinancess",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationFinancess_RelatedReportId",
                table: "NotificationFinancess",
                column: "RelatedReportId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationFinancess_TransictionAccountId",
                table: "NotificationFinancess",
                column: "TransictionAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionReport_Account",
                table: "TransactionReport",
                column: "CreatorAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionReport_Account_ReviewedByAccountId",
                table: "TransactionReport",
                column: "ReviewedByAccountId",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionReport_TransictionAccounts_TransictionAccountId",
                table: "TransactionReport",
                column: "TransictionAccountId",
                principalTable: "TransictionAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionReport_Account",
                table: "TransactionReport");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionReport_Account_ReviewedByAccountId",
                table: "TransactionReport");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionReport_TransictionAccounts_TransictionAccountId",
                table: "TransactionReport");

            migrationBuilder.DropTable(
                name: "NotificationFinancess");

            migrationBuilder.DropTable(
                name: "TransictionAccounts");

            migrationBuilder.DropIndex(
                name: "IX_TransactionReport_ReviewedByAccountId",
                table: "TransactionReport");

            migrationBuilder.DropIndex(
                name: "IX_TransactionReport_TransictionAccountId",
                table: "TransactionReport");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "TransactionReport");

            migrationBuilder.DropColumn(
                name: "ReviewedByAccountId",
                table: "TransactionReport");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TransactionReport");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "TransactionReport");

            migrationBuilder.DropColumn(
                name: "TransictionAccountId",
                table: "TransactionReport");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "Notifications");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_AccountId",
                table: "Notifications",
                newName: "IX_Notifications_AccountId");

            migrationBuilder.AlterColumn<string>(
                name: "ReportName",
                table: "TransactionReport",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorAccountId",
                table: "TransactionReport",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TransactionReport",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "TransactionDate",
                table: "TransactionRecord",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionReport_Account",
                table: "TransactionReport",
                column: "CreatorAccountId",
                principalTable: "Account",
                principalColumn: "Id");
        }
    }
}
