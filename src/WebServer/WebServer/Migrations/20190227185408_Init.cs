using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServer.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceInfos",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CostPerUnit = table.Column<decimal>(nullable: false),
                    Measurement = table.Column<string>(nullable: true),
                    IsDeprecated = table.Column<bool>(nullable: false),
                    ImgSrc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Fio = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    Units = table.Column<long>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    ServiceId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_ServiceInfos_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ServiceInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "e29300f5-1d97-443f-a694-2e0f9d5d7f19", "Клиент" },
                    { "e9dd62d8-02f6-4b7a-bf82-e1f705b59d45", "Администратор" },
                    { "0539bc82-8ff0-491b-a813-5154a40e72e3", "Управляющий" }
                });

            migrationBuilder.InsertData(
                table: "ServiceInfos",
                columns: new[] { "Id", "CostPerUnit", "ImgSrc", "IsDeprecated", "Measurement", "Name" },
                values: new object[,]
                {
                    { "32b5955c-eb1b-4d57-98bc-cf37c84e4b52", 1000m, "~/images/services/spa.png", false, "час.", "Спа" },
                    { "e3534844-1ad8-4c71-90da-4910ff475ff9", 2000m, "~/images/services/eight.png", false, "час", "Бильярд восьмёрка" },
                    { "7110e151-8e40-492e-a5a4-7af5cd912053", 2000m, "~/images/services/nine.png", false, "час", "Бильярд девятка" },
                    { "51507636-eeeb-4d40-871b-b19c31ca8a76", 2000m, "~/images/services/russian_billiards.png", false, "час", "Русский бильярд" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Fio", "Login", "Password", "RoleId" },
                values: new object[,]
                {
                    { "5233e7aa-968a-45c0-bac9-a1d87a85afa0", "Петров П.П", "petr", "11111", "e29300f5-1d97-443f-a694-2e0f9d5d7f19" },
                    { "ec7038b0-c212-4a94-b8be-32655826cf6e", "Иванов П.П", "iva", "44444", "e29300f5-1d97-443f-a694-2e0f9d5d7f19" },
                    { "a4d865fa-62d6-48ef-bd61-89de59a4be85", "Сидоров П.П", "sidor", "55555", "e29300f5-1d97-443f-a694-2e0f9d5d7f19" },
                    { "0c408d05-dc94-40b2-b257-098d1974ef65", "Смирнов П.П", "smr", "22222", "e9dd62d8-02f6-4b7a-bf82-e1f705b59d45" },
                    { "678492b0-d0be-4298-a9e6-38553162813a", "Симонов П.П", "simon", "33333", "0539bc82-8ff0-491b-a813-5154a40e72e3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ServiceId",
                table: "Orders",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ServiceInfos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
