using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceApi.Migrations
{
    public partial class EditProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quanlity",
                table: "Products",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "Acailable",
                table: "PaymentMethods",
                newName: "Available");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Products",
                newName: "Quanlity");

            migrationBuilder.RenameColumn(
                name: "Available",
                table: "PaymentMethods",
                newName: "Acailable");
        }
    }
}
