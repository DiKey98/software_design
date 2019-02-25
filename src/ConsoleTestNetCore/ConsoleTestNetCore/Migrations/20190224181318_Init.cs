using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleTestNetCore.Migrations
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
                    Measurement = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Fio = table.Column<string>(nullable: true),
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
                    { "552993ba-055a-4749-ac17-6e77c3b62a74", "Клиент" },
                    { "fe9bd704-cbaf-41b1-b691-fb5d8c7985f2", "Администратор" },
                    { "416073d0-3a17-4cbc-9035-5f3e0a7d0586", "Управляющий" }
                });

            migrationBuilder.InsertData(
                table: "ServiceInfos",
                columns: new[] { "Id", "CostPerUnit", "Measurement", "Name" },
                values: new object[,]
                {
                    { "fdf04ac2-c523-4e25-884c-e4e7b112a2ab", 1000m, "час.", "Спа" },
                    { "1b8fed43-9237-4998-b25d-063b23f00672", 2000m, "час", "Бильярд восьмёрка" },
                    { "19334466-5e9e-4f5d-bccd-710364f42ee0", 2000m, "час", "Бильярд девятка" },
                    { "95984817-6bbd-440c-aece-78b272512990", 2000m, "час", "Русский бильярд" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Fio", "Login", "Password", "RoleId" },
                values: new object[,]
                {
                    { "b136278e-d453-4485-babf-9db66eb4ebcd", "Петров П.П", "petr", "11111", "552993ba-055a-4749-ac17-6e77c3b62a74" },
                    { "c5374844-9455-481f-8a40-f067686a4703", "Иванов П.П", "iva", "44444", "552993ba-055a-4749-ac17-6e77c3b62a74" },
                    { "c245085f-30b5-4a40-bc2c-99ee812348c5", "Сидоров П.П", "sidor", "55555", "552993ba-055a-4749-ac17-6e77c3b62a74" },
                    { "bd727a6a-ed86-4cfe-a5fb-37cad1313466", "Смирнов П.П", "smr", "22222", "fe9bd704-cbaf-41b1-b691-fb5d8c7985f2" },
                    { "1d1b0758-62d7-493f-818b-294ed87d2f90", "Симонов П.П", "simon", "33333", "416073d0-3a17-4cbc-9035-5f3e0a7d0586" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ServiceId",
                table: "Orders",
                column: "ServiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId",
                unique: true);

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
