using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaReservation.Models.Migrations
{
    public partial class AddedIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "tblOrders",
            //    columns: table => new
            //    {
            //        OrderId = table.Column<Guid>(nullable: false),
            //        Name = table.Column<string>(maxLength: 20, nullable: false),
            //        Email = table.Column<string>(maxLength: 50, nullable: false),
            //        PhoneNumber = table.Column<string>(maxLength: 15, nullable: false),
            //        DeliveryAddress = table.Column<string>(maxLength: 50, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblOrders", x => x.OrderId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tblPizzas",
            //    columns: table => new
            //    {
            //        PizzaId = table.Column<Guid>(nullable: false),
            //        Name = table.Column<string>(maxLength: 30, nullable: false),
            //        ImageUrl = table.Column<string>(maxLength: 300, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblPizzas", x => x.PizzaId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tblSizes",
            //    columns: table => new
            //    {
            //        SizeId = table.Column<Guid>(nullable: false),
            //        Name = table.Column<string>(maxLength: 20, nullable: false),
            //        Diameter = table.Column<int>(nullable: false),
            //        Price = table.Column<decimal>(type: "decimal(6, 2)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblSizes", x => x.SizeId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tblToppings",
            //    columns: table => new
            //    {
            //        ToppingId = table.Column<Guid>(nullable: false),
            //        Name = table.Column<string>(maxLength: 20, nullable: false),
            //        Price = table.Column<decimal>(type: "decimal(6, 2)", nullable: false),
            //        Vegetarian = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblToppings", x => x.ToppingId);
            //    });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "tblPizzaOrders",
            //    columns: table => new
            //    {
            //        PizzaId = table.Column<Guid>(nullable: false),
            //        OrderId = table.Column<Guid>(nullable: false),
            //        SizeId = table.Column<Guid>(nullable: false),
            //        Price = table.Column<decimal>(type: "decimal(6, 2)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblPizzaOrders", x => new { x.PizzaId, x.OrderId, x.SizeId });
            //        table.ForeignKey(
            //            name: "FK_tblPizzaOrders_tblOrders_OrderId",
            //            column: x => x.OrderId,
            //            principalTable: "tblOrders",
            //            principalColumn: "OrderId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_tblPizzaOrders_tblPizzas_PizzaId",
            //            column: x => x.PizzaId,
            //            principalTable: "tblPizzas",
            //            principalColumn: "PizzaId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_tblPizzaOrders_tblSizes_SizeId",
            //            column: x => x.SizeId,
            //            principalTable: "tblSizes",
            //            principalColumn: "SizeId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tblPizzaSizes",
            //    columns: table => new
            //    {
            //        PizzaId = table.Column<Guid>(nullable: false),
            //        SizeId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblPizzaSizes", x => new { x.PizzaId, x.SizeId });
            //        table.ForeignKey(
            //            name: "FK_tblPizzaSizes_tblPizzas_PizzaId",
            //            column: x => x.PizzaId,
            //            principalTable: "tblPizzas",
            //            principalColumn: "PizzaId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_tblPizzaSizes_tblSizes_SizeId",
            //            column: x => x.SizeId,
            //            principalTable: "tblSizes",
            //            principalColumn: "SizeId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tblPizzaToppings",
            //    columns: table => new
            //    {
            //        PizzaId = table.Column<Guid>(nullable: false),
            //        ToppingId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblPizzaToppings", x => new { x.PizzaId, x.ToppingId });
            //        table.ForeignKey(
            //            name: "FK_tblPizzaToppings_tblPizzas_PizzaId",
            //            column: x => x.PizzaId,
            //            principalTable: "tblPizzas",
            //            principalColumn: "PizzaId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_tblPizzaToppings_tblToppings_ToppingId",
            //            column: x => x.ToppingId,
            //            principalTable: "tblToppings",
            //            principalColumn: "ToppingId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tblPizzaOrders_OrderId",
                table: "tblPizzaOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPizzaOrders_SizeId",
                table: "tblPizzaOrders",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPizzaSizes_SizeId",
                table: "tblPizzaSizes",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPizzaToppings_ToppingId",
                table: "tblPizzaToppings",
                column: "ToppingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "tblPizzaOrders");

            migrationBuilder.DropTable(
                name: "tblPizzaSizes");

            migrationBuilder.DropTable(
                name: "tblPizzaToppings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tblOrders");

            migrationBuilder.DropTable(
                name: "tblSizes");

            migrationBuilder.DropTable(
                name: "tblPizzas");

            migrationBuilder.DropTable(
                name: "tblToppings");
        }
    }
}
