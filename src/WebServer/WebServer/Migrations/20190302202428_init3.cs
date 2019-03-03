using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServer.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ServiceInfos",
                columns: new[] { "Id", "CostPerUnit", "ImgSrc", "IsDeprecated", "Measurement", "Name" },
                values: new object[,]
                {
                    { "482cf7e2-9173-4b82-8256-198d0d94a15a", 1000m, "~/images/services/spa.png", false, "час.", "Спа" },
                    { "bea244f6-846b-43da-bb15-3bdd8bbc551c", 2000m, "~/images/services/eight.png", false, "час", "Бильярд восьмёрка" },
                    { "24eba2e4-b3d1-44e1-8ec4-6d3f702bea43", 2000m, "~/images/services/nine.png", false, "час", "Бильярд девятка" },
                    { "8da23433-2dc0-4f99-9a05-c76dc01bad4b", 2000m, "~/images/services/russian_billiards.png", false, "час", "Русский бильярд" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ServiceInfos",
                columns: new[] { "Id", "CostPerUnit", "ImgSrc", "IsDeprecated", "Measurement", "Name" },
                values: new object[,]
                {
                    { "cb693424-a8d2-44c6-adf7-3df9eb36626f", 1000m, "~/images/services/spa.png", false, "час.", "Спа" },
                    { "a9d98270-6f82-4c51-96b3-5a1edb742973", 2000m, "~/images/services/eight.png", false, "час", "Бильярд восьмёрка" },
                    { "35e2c37b-aeb5-4759-aa1a-b60a7cbafb31", 2000m, "~/images/services/nine.png", false, "час", "Бильярд девятка" },
                    { "c96987c0-2582-4e3c-b5d4-125a4031b387", 2000m, "~/images/services/russian_billiards.png", false, "час", "Русский бильярд" }
                });     
        }
    }
}
