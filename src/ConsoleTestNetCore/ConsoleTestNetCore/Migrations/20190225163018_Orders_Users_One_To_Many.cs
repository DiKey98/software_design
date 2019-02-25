using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleTestNetCore.Migrations
{
    public partial class Orders_Users_One_To_Many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "ServiceInfos",
            //    keyColumn: "Id",
            //    keyValue: "1e75571d-a33c-4a56-9c33-e8c047fcacca");

            //migrationBuilder.DeleteData(
            //    table: "ServiceInfos",
            //    keyColumn: "Id",
            //    keyValue: "d2a9100e-843d-40f2-873d-38793fa644d3");

            //migrationBuilder.DeleteData(
            //    table: "ServiceInfos",
            //    keyColumn: "Id",
            //    keyValue: "e870c5a2-e990-4429-8940-c128a5a65b32");

            //migrationBuilder.DeleteData(
            //    table: "ServiceInfos",
            //    keyColumn: "Id",
            //    keyValue: "fe326a82-cdf5-436e-aac8-b117bcef0d67");

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: "04b24114-b976-428d-bd8e-b89abd21046e");

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: "2957934c-ac2b-4036-9c4a-c3b196e02dc6");

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: "35216458-5801-404e-a4f6-98d376562cea");

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: "aee0a7ca-cdb9-4dcc-8cfb-ca5e7b510481");

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: "e187d3ed-8912-4042-a380-012d90bf81d5");

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: "6f9c37b6-5ba0-4582-a1bc-708642c1079f");

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: "a6615a24-570b-4bf1-980f-e69ab6957add");

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: "ffe5ca75-81ed-432c-85b7-20a8a3d7245b");

            //migrationBuilder.InsertData(
            //    table: "Roles",
            //    columns: new[] { "Id", "Name" },
            //    values: new object[,]
            //    {
            //        { "dd9945dd-8456-448b-948e-7a9a8244947e", "Клиент" },
            //        { "3f5cf399-4607-40a0-aac3-6dbe4369ac2b", "Администратор" },
            //        { "29be7ddc-2802-4460-a2d8-b580f97b9e66", "Управляющий" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "ServiceInfos",
            //    columns: new[] { "Id", "CostPerUnit", "Measurement", "Name" },
            //    values: new object[,]
            //    {
            //        { "60c2742c-6d75-4647-94e5-c7f894a8e6b3", 1000m, "час.", "Спа" },
            //        { "d853c763-7a2e-4b55-ad40-365085845320", 2000m, "час", "Бильярд восьмёрка" },
            //        { "54613e14-a8b5-4b08-8ddb-355927fb040b", 2000m, "час", "Бильярд девятка" },
            //        { "896e1a06-73b0-4e48-a266-ada59e03d050", 2000m, "час", "Русский бильярд" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Users",
            //    columns: new[] { "Id", "Fio", "Login", "Password", "RoleId" },
            //    values: new object[,]
            //    {
            //        { "d72700b6-87ac-456a-903c-41bd15e7cba7", "Петров П.П", "petr", "11111", "dd9945dd-8456-448b-948e-7a9a8244947e" },
            //        { "b16e00c3-feb5-4a1a-ae25-6b936ce871a5", "Иванов П.П", "iva", "44444", "dd9945dd-8456-448b-948e-7a9a8244947e" },
            //        { "cc75fca7-0e93-48d4-8948-7483da1f96e5", "Сидоров П.П", "sidor", "55555", "dd9945dd-8456-448b-948e-7a9a8244947e" },
            //        { "cca3e71c-2c38-453a-8322-19c2df1ce6ee", "Смирнов П.П", "smr", "22222", "3f5cf399-4607-40a0-aac3-6dbe4369ac2b" },
            //        { "53345b66-5006-490e-b8f1-bb582ca6363f", "Симонов П.П", "simon", "33333", "29be7ddc-2802-4460-a2d8-b580f97b9e66" }
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "ServiceInfos",
            //    keyColumn: "Id",
            //    keyValue: "54613e14-a8b5-4b08-8ddb-355927fb040b");

            //migrationBuilder.DeleteData(
            //    table: "ServiceInfos",
            //    keyColumn: "Id",
            //    keyValue: "60c2742c-6d75-4647-94e5-c7f894a8e6b3");

            //migrationBuilder.DeleteData(
            //    table: "ServiceInfos",
            //    keyColumn: "Id",
            //    keyValue: "896e1a06-73b0-4e48-a266-ada59e03d050");

            //migrationBuilder.DeleteData(
            //    table: "ServiceInfos",
            //    keyColumn: "Id",
            //    keyValue: "d853c763-7a2e-4b55-ad40-365085845320");

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: "53345b66-5006-490e-b8f1-bb582ca6363f");

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: "b16e00c3-feb5-4a1a-ae25-6b936ce871a5");

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: "cc75fca7-0e93-48d4-8948-7483da1f96e5");

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: "cca3e71c-2c38-453a-8322-19c2df1ce6ee");

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "Id",
            //    keyValue: "d72700b6-87ac-456a-903c-41bd15e7cba7");

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: "29be7ddc-2802-4460-a2d8-b580f97b9e66");

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: "3f5cf399-4607-40a0-aac3-6dbe4369ac2b");

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: "dd9945dd-8456-448b-948e-7a9a8244947e");

            //migrationBuilder.InsertData(
            //    table: "Roles",
            //    columns: new[] { "Id", "Name" },
            //    values: new object[,]
            //    {
            //        { "6f9c37b6-5ba0-4582-a1bc-708642c1079f", "Клиент" },
            //        { "a6615a24-570b-4bf1-980f-e69ab6957add", "Администратор" },
            //        { "ffe5ca75-81ed-432c-85b7-20a8a3d7245b", "Управляющий" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "ServiceInfos",
            //    columns: new[] { "Id", "CostPerUnit", "Measurement", "Name" },
            //    values: new object[,]
            //    {
            //        { "1e75571d-a33c-4a56-9c33-e8c047fcacca", 1000m, "час.", "Спа" },
            //        { "d2a9100e-843d-40f2-873d-38793fa644d3", 2000m, "час", "Бильярд восьмёрка" },
            //        { "fe326a82-cdf5-436e-aac8-b117bcef0d67", 2000m, "час", "Бильярд девятка" },
            //        { "e870c5a2-e990-4429-8940-c128a5a65b32", 2000m, "час", "Русский бильярд" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Users",
            //    columns: new[] { "Id", "Fio", "Login", "Password", "RoleId" },
            //    values: new object[,]
            //    {
            //        { "04b24114-b976-428d-bd8e-b89abd21046e", "Петров П.П", "petr", "11111", "6f9c37b6-5ba0-4582-a1bc-708642c1079f" },
            //        { "2957934c-ac2b-4036-9c4a-c3b196e02dc6", "Иванов П.П", "iva", "44444", "6f9c37b6-5ba0-4582-a1bc-708642c1079f" },
            //        { "e187d3ed-8912-4042-a380-012d90bf81d5", "Сидоров П.П", "sidor", "55555", "6f9c37b6-5ba0-4582-a1bc-708642c1079f" },
            //        { "aee0a7ca-cdb9-4dcc-8cfb-ca5e7b510481", "Смирнов П.П", "smr", "22222", "a6615a24-570b-4bf1-980f-e69ab6957add" },
            //        { "35216458-5801-404e-a4f6-98d376562cea", "Симонов П.П", "simon", "33333", "ffe5ca75-81ed-432c-85b7-20a8a3d7245b" }
            //    });
        }
    }
}
