using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIStory.Migrations
{
  public partial class Initial : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Categories",
          columns: table => new
          {
            CategoryId = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(maxLength: 50, nullable: false),
            RegistrationDate = table.Column<DateTime>(nullable: false),
            UpdateDate = table.Column<DateTime>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Categories", x => x.CategoryId);
          });

      migrationBuilder.CreateTable(
          name: "Users",
          columns: table => new
          {
            UserId = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(maxLength: 50, nullable: false),
            Email = table.Column<string>(maxLength: 50, nullable: false),
            Phone = table.Column<string>(maxLength: 12, nullable: false),
            SocialSecurity = table.Column<string>(maxLength: 11, nullable: false),
            IdentityDocument = table.Column<string>(maxLength: 10, nullable: true),
            Genre = table.Column<int>(nullable: false),
            RegistrationDate = table.Column<DateTime>(nullable: false),
            UpdateDate = table.Column<DateTime>(nullable: false),
            Login = table.Column<string>(maxLength: 20, nullable: false),
            Password = table.Column<string>(maxLength: 20, nullable: false),
            Street = table.Column<string>(maxLength: 50, nullable: false),
            Number = table.Column<string>(maxLength: 8, nullable: false),
            Complement = table.Column<string>(maxLength: 50, nullable: true),
            Neighborhood = table.Column<string>(maxLength: 50, nullable: false),
            City = table.Column<string>(maxLength: 50, nullable: false),
            Uf = table.Column<string>(maxLength: 2, nullable: false),
            TypeAddressUser = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Users", x => x.UserId);
          });

      migrationBuilder.CreateTable(
          name: "Products",
          columns: table => new
          {
            ProductId = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(maxLength: 50, nullable: false),
            Description = table.Column<string>(maxLength: 150, nullable: false),
            Price = table.Column<decimal>(nullable: false),
            ImageUrl = table.Column<string>(maxLength: 150, nullable: true),
            Stock = table.Column<int>(nullable: false),
            Active = table.Column<bool>(nullable: false),
            RegistrationDate = table.Column<DateTime>(nullable: false),
            UpdateDate = table.Column<DateTime>(nullable: false),
            CategoryId = table.Column<int>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Products", x => x.ProductId);
            table.ForeignKey(
                      name: "FK_Products_Categories_CategoryId",
                      column: x => x.CategoryId,
                      principalTable: "Categories",
                      principalColumn: "CategoryId",
                      onDelete: ReferentialAction.Restrict);
          });

      migrationBuilder.CreateTable(
          name: "BuyProducts",
          columns: table => new
          {
            BuyProductId = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Quantity = table.Column<int>(nullable: false),
            Code = table.Column<string>(nullable: false),
            UnitaryValue = table.Column<decimal>(nullable: false),
            Total = table.Column<decimal>(nullable: false),
            RegistrationDate = table.Column<DateTime>(nullable: false),
            UpdateDate = table.Column<DateTime>(nullable: false),
            UserId = table.Column<int>(nullable: false),
            ProductId = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_BuyProducts", x => x.BuyProductId);
            table.ForeignKey(
                      name: "FK_BuyProducts_Products_ProductId",
                      column: x => x.ProductId,
                      principalTable: "Products",
                      principalColumn: "ProductId",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_BuyProducts_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "UserId",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Manufacturers",
          columns: table => new
          {
            ManufacturerId = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            CompanyName = table.Column<string>(maxLength: 50, nullable: false),
            Email = table.Column<string>(maxLength: 50, nullable: false),
            Phone = table.Column<string>(maxLength: 12, nullable: false),
            FederalRegistration = table.Column<string>(maxLength: 14, nullable: false),
            StateRegistration = table.Column<string>(maxLength: 15, nullable: true),
            RegistrationDate = table.Column<DateTime>(nullable: false),
            UpdateDate = table.Column<DateTime>(nullable: false),
            Street = table.Column<string>(maxLength: 50, nullable: false),
            Number = table.Column<string>(maxLength: 8, nullable: false),
            Complement = table.Column<string>(maxLength: 50, nullable: true),
            Neighborhood = table.Column<string>(maxLength: 50, nullable: false),
            City = table.Column<string>(maxLength: 50, nullable: false),
            Uf = table.Column<string>(maxLength: 2, nullable: false),
            ProductId = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Manufacturers", x => x.ManufacturerId);
            table.ForeignKey(
                      name: "FK_Manufacturers_Products_ProductId",
                      column: x => x.ProductId,
                      principalTable: "Products",
                      principalColumn: "ProductId",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_BuyProducts_ProductId",
          table: "BuyProducts",
          column: "ProductId");

      migrationBuilder.CreateIndex(
          name: "IX_BuyProducts_UserId",
          table: "BuyProducts",
          column: "UserId");

      migrationBuilder.CreateIndex(
          name: "IX_Manufacturers_ProductId",
          table: "Manufacturers",
          column: "ProductId");

      migrationBuilder.CreateIndex(
          name: "IX_Products_CategoryId",
          table: "Products",
          column: "CategoryId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "BuyProducts");

      migrationBuilder.DropTable(
          name: "Manufacturers");

      migrationBuilder.DropTable(
          name: "Users");

      migrationBuilder.DropTable(
          name: "Products");

      migrationBuilder.DropTable(
          name: "Categories");
    }
  }
}
