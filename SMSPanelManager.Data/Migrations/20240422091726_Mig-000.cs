using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SMSPanelManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class Mig000 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<byte>(type: "TINYINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "OrderedProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderedProduct = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    PrintableSMSId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrinteableSMs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SMSText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: true),
                    SMSId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPrinted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrinteableSMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrinteableSMs_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "ContactId");
                });

            migrationBuilder.CreateTable(
                name: "SMSs",
                columns: table => new
                {
                    SMSId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Receiver = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceivedSmsId = table.Column<int>(type: "int", nullable: false),
                    PrintableSMSId = table.Column<int>(type: "int", nullable: true),
                    ReceivedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSs", x => x.SMSId);
                    table.ForeignKey(
                        name: "FK_SMSs_PrinteableSMs_PrintableSMSId",
                        column: x => x.PrintableSMSId,
                        principalTable: "PrinteableSMs",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ProductName" },
                values: new object[,]
                {
                    { 1, "تره" },
                    { 2, "جعفری" },
                    { 3, "گشنیز" },
                    { 4, "اسفناج" },
                    { 5, "شوید" },
                    { 6, "شنبلیله" },
                    { 7, "سیر" },
                    { 8, "باقالی" },
                    { 9, "لوبیا" },
                    { 10, "بادمجان" },
                    { 11, "سبزی" },
                    { 12, "بامیه" },
                    { 13, "مخلوط" },
                    { 14, "نخود" },
                    { 15, "ذرت" },
                    { 16, "رب" },
                    { 17, "خیار" },
                    { 18, "زیتون" },
                    { 19, "پیاز" },
                    { 20, "نعنا" },
                    { 21, "ترخون" },
                    { 22, "مرزه" },
                    { 23, "تیغ" },
                    { 24, "شور" },
                    { 25, "ترشی" },
                    { 26, "فلفل" },
                    { 27, "سس" },
                    { 28, "برگ" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "UserName", "UserPassword" },
                values: new object[] { (byte)1, "<Awnm#|$|%!@ll*$>", "!!<Q!0!@9f|j|f&#$>!!" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderedProducts_PrintableSMSId",
                table: "OrderedProducts",
                column: "PrintableSMSId");

            migrationBuilder.CreateIndex(
                name: "IX_PrinteableSMs_ContactId",
                table: "PrinteableSMs",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_PrinteableSMs_SMSId",
                table: "PrinteableSMs",
                column: "SMSId");

            migrationBuilder.CreateIndex(
                name: "IX_SMSs_PrintableSMSId",
                table: "SMSs",
                column: "PrintableSMSId",
                unique: true,
                filter: "[PrintableSMSId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedProducts_PrinteableSMs_PrintableSMSId",
                table: "OrderedProducts",
                column: "PrintableSMSId",
                principalTable: "PrinteableSMs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrinteableSMs_SMSs_SMSId",
                table: "PrinteableSMs",
                column: "SMSId",
                principalTable: "SMSs",
                principalColumn: "SMSId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SMSs_PrinteableSMs_PrintableSMSId",
                table: "SMSs");

            migrationBuilder.DropTable(
                name: "OrderedProducts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PrinteableSMs");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "SMSs");
        }
    }
}
