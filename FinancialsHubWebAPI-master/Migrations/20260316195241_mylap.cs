using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialsHubWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class mylap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "TransactionReport",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TransactionReport",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionReport_CategoryId",
                table: "TransactionReport",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionReport_CreatorAccountId",
                table: "TransactionReport",
                column: "CreatorAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRecord_CategoryId",
                table: "TransactionRecord",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRecord_TransactionReportId",
                table: "TransactionRecord",
                column: "TransactionReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_StatusId",
                table: "Category",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Status",
                table: "Category",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRecord_Category",
                table: "TransactionRecord",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRecord_TransactionReport",
                table: "TransactionRecord",
                column: "TransactionReportId",
                principalTable: "TransactionReport",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionReport_Account",
                table: "TransactionReport",
                column: "CreatorAccountId",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionReport_Category",
                table: "TransactionReport",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Status",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRecord_Category",
                table: "TransactionRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRecord_TransactionReport",
                table: "TransactionRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionReport_Account",
                table: "TransactionReport");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionReport_Category",
                table: "TransactionReport");

            migrationBuilder.DropIndex(
                name: "IX_TransactionReport_CategoryId",
                table: "TransactionReport");

            migrationBuilder.DropIndex(
                name: "IX_TransactionReport_CreatorAccountId",
                table: "TransactionReport");

            migrationBuilder.DropIndex(
                name: "IX_TransactionRecord_CategoryId",
                table: "TransactionRecord");

            migrationBuilder.DropIndex(
                name: "IX_TransactionRecord_TransactionReportId",
                table: "TransactionRecord");

            migrationBuilder.DropIndex(
                name: "IX_Category_StatusId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "TransactionReport");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TransactionReport");
        }
    }
}
