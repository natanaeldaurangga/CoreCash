using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreCashApi.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    account_code = table.Column<int>(type: "int", nullable: false),
                    account_group = table.Column<int>(type: "int", nullable: false),
                    account_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    password_hash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    password_salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    profile_picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    refresh_token = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    verification_token = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    verified_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    token_expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    reset_password_token = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    reset_token_expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "contacts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contacts", x => x.id);
                    table.ForeignKey(
                        name: "FK_contacts_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "records",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    record_group = table.Column<int>(type: "int", nullable: false),
                    recorded_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_records", x => x.id);
                    table.ForeignKey(
                        name: "FK_records_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ledgers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    record_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    entry = table.Column<int>(type: "int", nullable: false),
                    balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ledgers", x => x.id);
                    table.ForeignKey(
                        name: "FK_ledgers_accounts_account_id",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ledgers_records_record_id",
                        column: x => x.record_id,
                        principalTable: "records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payable_ledgers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    record_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    record_group = table.Column<int>(type: "int", nullable: false),
                    creditor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payable_ledgers", x => x.id);
                    table.ForeignKey(
                        name: "FK_payable_ledgers_contacts_creditor_id",
                        column: x => x.creditor_id,
                        principalTable: "contacts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_payable_ledgers_records_record_id",
                        column: x => x.record_id,
                        principalTable: "records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "receivable_ledgers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    record_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    debtor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receivable_ledgers", x => x.id);
                    table.ForeignKey(
                        name: "FK_receivable_ledgers_contacts_debtor_id",
                        column: x => x.debtor_id,
                        principalTable: "contacts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_receivable_ledgers_records_record_id",
                        column: x => x.record_id,
                        principalTable: "records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "id", "account_code", "account_group", "account_name", "created_at", "deleted_at", "updated_at" },
                values: new object[,]
                {
                    { new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 11001, 11, "CASH", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8742), null, new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8743) },
                    { new Guid("a4edc04b-36b8-464b-87a0-35e4e006641a"), 11005, 11, "RECEIVABLE", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8745), null, new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8745) },
                    { new Guid("ae6ce077-681d-495c-8ba1-8f2ce9a5ae96"), 21001, 21, "PAYABLE", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8747), null, new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8747) }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_at", "deleted_at", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("27ffa0bf-e12f-4cf7-b07c-e56e1baab903"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(7622), null, "ROLE_ADMIN", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(7626) },
                    { new Guid("d283c3ea-6efd-4489-bfca-4ff8fe86f43c"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(7658), null, "ROLE_USER", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(7658) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "deleted_at", "email", "full_name", "password_hash", "password_salt", "profile_picture", "refresh_token", "reset_password_token", "reset_token_expires", "role_id", "token_expires", "updated_at", "verification_token", "verified_at" },
                values: new object[,]
                {
                    { new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8706), null, "user1@example.com", "User Satu", new byte[] { 125, 151, 97, 153, 143, 10, 83, 219, 23, 109, 100, 157, 119, 19, 28, 28, 253, 137, 235, 210, 115, 7, 166, 52, 9, 12, 51, 194, 114, 137, 73, 49 }, new byte[] { 156, 100, 99, 124, 119, 111, 254, 192, 9, 182, 132, 207, 242, 51, 82, 180, 240, 192, 232, 155, 6, 50, 153, 210, 150, 130, 108, 121, 69, 49, 126, 144, 251, 59, 200, 82, 216, 209, 199, 140, 156, 182, 92, 133, 104, 59, 25, 145, 194, 129, 179, 185, 69, 225, 221, 214, 70, 80, 133, 92, 38, 205, 128, 235 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d283c3ea-6efd-4489-bfca-4ff8fe86f43c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8706), null, new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8706) },
                    { new Guid("0c73946d-fce9-458c-9531-3e65c28eeb51"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8699), null, "admin1@example.com", "Admin Satu", new byte[] { 125, 151, 97, 153, 143, 10, 83, 219, 23, 109, 100, 157, 119, 19, 28, 28, 253, 137, 235, 210, 115, 7, 166, 52, 9, 12, 51, 194, 114, 137, 73, 49 }, new byte[] { 156, 100, 99, 124, 119, 111, 254, 192, 9, 182, 132, 207, 242, 51, 82, 180, 240, 192, 232, 155, 6, 50, 153, 210, 150, 130, 108, 121, 69, 49, 126, 144, 251, 59, 200, 82, 216, 209, 199, 140, 156, 182, 92, 133, 104, 59, 25, 145, 194, 129, 179, 185, 69, 225, 221, 214, 70, 80, 133, 92, 38, 205, 128, 235 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("27ffa0bf-e12f-4cf7-b07c-e56e1baab903"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8699), null, new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8698) },
                    { new Guid("18dfba9a-b8bc-4a21-a7ce-31694edbffd7"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8703), null, "admin2@example.com", "Admin Dua", new byte[] { 125, 151, 97, 153, 143, 10, 83, 219, 23, 109, 100, 157, 119, 19, 28, 28, 253, 137, 235, 210, 115, 7, 166, 52, 9, 12, 51, 194, 114, 137, 73, 49 }, new byte[] { 156, 100, 99, 124, 119, 111, 254, 192, 9, 182, 132, 207, 242, 51, 82, 180, 240, 192, 232, 155, 6, 50, 153, 210, 150, 130, 108, 121, 69, 49, 126, 144, 251, 59, 200, 82, 216, 209, 199, 140, 156, 182, 92, 133, 104, 59, 25, 145, 194, 129, 179, 185, 69, 225, 221, 214, 70, 80, 133, 92, 38, 205, 128, 235 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("27ffa0bf-e12f-4cf7-b07c-e56e1baab903"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8704), null, new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8703) },
                    { new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8709), null, "user2@example.com", "User Dua", new byte[] { 125, 151, 97, 153, 143, 10, 83, 219, 23, 109, 100, 157, 119, 19, 28, 28, 253, 137, 235, 210, 115, 7, 166, 52, 9, 12, 51, 194, 114, 137, 73, 49 }, new byte[] { 156, 100, 99, 124, 119, 111, 254, 192, 9, 182, 132, 207, 242, 51, 82, 180, 240, 192, 232, 155, 6, 50, 153, 210, 150, 130, 108, 121, 69, 49, 126, 144, 251, 59, 200, 82, 216, 209, 199, 140, 156, 182, 92, 133, 104, 59, 25, 145, 194, 129, 179, 185, 69, 225, 221, 214, 70, 80, 133, 92, 38, 205, 128, 235 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d283c3ea-6efd-4489-bfca-4ff8fe86f43c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8709), null, new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8708) }
                });

            migrationBuilder.InsertData(
                table: "contacts",
                columns: new[] { "id", "address", "created_at", "deleted_at", "email", "name", "phone_number", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("46a48b63-8044-428f-bc17-382519594da2"), "96 Summerview Drive", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9068), null, "klanphere3@upenn.edu", "Kev Lanphere", "7268834724", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9068), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("57447951-c13c-49d1-a7bc-3f18be463d98"), "34 Eastwood Avenue", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9060), null, "smahood1@wufoo.com", "Sarine Mahood", "6977095403", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9060), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("6c9f14dc-a781-44a4-8fc5-3d5fa85817b3"), "11 Old Shore Terrace", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9076), null, "msivorn8@icq.com", "Margaretta Sivorn", "5311644464", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9076), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("723812cd-b0f9-49ba-86df-7086741aa425"), "10 Calypso Center", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9074), null, "candrelli7@intel.com", "Ceil Andrelli", "1401949061", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9075), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("7acd1227-f609-4623-85bf-c5b82d3d3023"), "4330 Doe Crossing Junction", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9078), null, "vwoodsford9@issuu.com", "Vasilis Woodsford", "8364764344", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9078), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("87c39553-250c-4cb0-887c-12687ee62907"), "39494 Kartens Terrace", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9069), null, "sblanchard4@vinaora.com", "Sal Blanchard", "4904310249", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9070), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("9ea2e36f-dd2d-43b5-b3f1-2e2da4e24881"), "54 Redwing Drive", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9082), null, "jorneblowa@youtube.com", "Jennee Orneblow", "9316403125", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9082), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("a36252cb-da6c-43ef-ad5c-e0bbb8b07cb7"), "4649 Swallow Trail", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9073), null, "tshew6@tripadvisor.com", "Tiphany Shew", "1457940882", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9073), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("f0473eb7-3c2d-4a8b-bb82-54f78401147f"), "615 Jenifer Alley", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9071), null, "ldunster5@marketwatch.com", "Laurette Dunster", "7678939672", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9071), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("f2cff3b1-bae6-4614-b273-fd149fb194cb"), "17 Onsgard Avenue", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9066), null, "bmcging2@ihg.com", "Benjie McGing", "5025308916", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9066), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("f2f4d7eb-3d7a-4caf-8ac3-a3505e7453b8"), "620 Lakeland Center", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9057), null, "rhavock0@gov.uk", "Randell Havock", "6765109874", new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9057), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") }
                });

            migrationBuilder.InsertData(
                table: "records",
                columns: new[] { "id", "created_at", "deleted_at", "description", "record_group", "recorded_at", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("063746cc-0553-4b67-a1db-74fb28aa29f7"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8782), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8771), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8782), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("09a5b2fe-e049-455c-873c-6c716fc518d8"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8874), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8874), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8874), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("152da5a4-bc3b-4852-87c1-05938bdfb4ef"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8889), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8889), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8889), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("1bd8a77b-4d58-4e03-89a4-dcad72961b83"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8860), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8859), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8860), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("27d438ed-1657-4d27-ad3b-b9a65730229b"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8886), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8886), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8887), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("2e9dcc44-3483-46fb-8b1a-9fc2b6cb558f"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8961), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8961), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8962), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("3292fabb-06a4-40e9-b514-942b86704cab"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8953), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8952), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8953), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("3cac0ea0-74cb-4a16-8adf-c1c00b24322c"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8944), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8943), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8944), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("427fd471-ab1e-44bc-8976-bac56da138f1"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8869), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8868), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8869), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("469f56a6-0f10-41f5-9f87-0daf0cc2fd3c"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9016), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(9015), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9016), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("4a4d2585-5c0e-4889-aef2-77e28f18fa0f"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9013), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(9012), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9013), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("568d70cd-8ab0-4e57-adf1-e186a4e4ae0a"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8814), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8813), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8814), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("574c51d9-bb72-4946-8497-291ecd82e684"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8981), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8981), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8982), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("5ba28aa9-228d-490d-9a89-ca70e006191e"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9025), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(9024), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9025), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("5f46a0ba-549e-4f6e-b9b5-438082d77898"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8857), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8856), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8857), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("6b18309a-f58a-450e-8be8-b1f321998bb1"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8817), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8817), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8817), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("7cc5b8dd-220c-424c-9d75-1b4f34d16bf8"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8871), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8871), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8872), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("80c550fb-ef17-42b9-8c65-61eac91584c5"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8798), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8798), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8798), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("898cd1a4-099c-4e84-9389-8ea343fa41b5"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8941), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8941), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8941), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("95170234-6b86-415b-9cac-8c8afb37c970"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8938), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8938), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8938), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("9528a09f-db5b-402b-a1d0-6329f34a1bd4"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8811), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8810), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8811), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("9827c1a6-d338-4997-b26b-abf984ea8287"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8884), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8883), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8884), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("9e677aa7-657d-4111-9830-8fa7c43de843"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8958), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8958), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8959), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("b4adc28d-196e-4418-a143-5147f1bbb90f"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9027), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(9027), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9028), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("b517f50d-938a-44f1-8660-a66e16575ab7"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9009), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(9009), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9010), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("b6a144a9-dbdd-4189-bf26-c546b0827923"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8804), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8804), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8805), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("c8da30e9-278c-4076-bbaf-eeb569a38912"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8947), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8946), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8947), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("cae09a4d-ecdd-4e36-a544-ea68054c4b5d"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8931), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8930), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8931), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("ccb6b25a-1eb8-40b4-958e-74a9d86fcf3f"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8795), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8794), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8795), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("cd1e380a-58ed-450b-98d8-04b68f49fed6"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8853), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8853), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8854), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("d0d7ec82-3b27-4ea0-99a6-ee9eeb08c5f4"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8877), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8876), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8877), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") }
                });

            migrationBuilder.InsertData(
                table: "records",
                columns: new[] { "id", "created_at", "deleted_at", "description", "record_group", "recorded_at", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("d5de233a-bcf8-4f5b-b1f1-f9b6c2249aff"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8820), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8819), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8820), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("dc2cfb75-8df9-4647-a0ba-2761a3b4209d"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8863), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8862), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8863), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("de0a8c63-db77-488a-909a-63ec24f6a5ac"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8973), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8972), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8973), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("e1e6a4fc-8669-496a-9a3d-adb49a508df6"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8967), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8967), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8967), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("ecb991d1-f44c-4956-98fd-83cf177475c0"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8801), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8801), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8802), new Guid("04f0f524-930e-430c-82d9-888fd05b6ee2") },
                    { new Guid("f0ef2aba-d85e-4734-a799-4af28f79fa39"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8975), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8975), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8976), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("f2e4c66d-3b7c-4beb-ae28-e9be82969820"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8970), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8970), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8970), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("f5f7c33c-846e-403b-bd45-b826324687c6"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8956), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(8955), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(8956), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") },
                    { new Guid("f7cec09c-15e0-4de1-b9b5-35f9be926e7c"), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9022), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 22, 20, 53, 11, 20, DateTimeKind.Local).AddTicks(9021), new DateTime(2023, 8, 22, 13, 53, 11, 20, DateTimeKind.Utc).AddTicks(9022), new Guid("1e97b9af-6eb7-4180-a9c5-bb0f6cb179ed") }
                });

            migrationBuilder.InsertData(
                table: "ledgers",
                columns: new[] { "id", "account_id", "balance", "entry", "record_id" },
                values: new object[,]
                {
                    { new Guid("042d51dd-b206-4912-8b28-b026baae0740"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 120000m, 1, new Guid("cd1e380a-58ed-450b-98d8-04b68f49fed6") },
                    { new Guid("04e2f944-df31-4e57-95fc-bae497c66874"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 20000m, 1, new Guid("152da5a4-bc3b-4852-87c1-05938bdfb4ef") },
                    { new Guid("062295a8-e350-416e-9ab6-9a842c4c7ebe"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 110000m, 0, new Guid("5f46a0ba-549e-4f6e-b9b5-438082d77898") },
                    { new Guid("0be63f99-1783-4e65-b4f7-d15b72247452"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 100000m, 1, new Guid("de0a8c63-db77-488a-909a-63ec24f6a5ac") },
                    { new Guid("0c216276-25af-4689-b044-d34975752d4e"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 40000m, 1, new Guid("f7cec09c-15e0-4de1-b9b5-35f9be926e7c") },
                    { new Guid("22e1bf5c-03ee-44e8-af8b-ef5cfb3d541a"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 140000m, 1, new Guid("9e677aa7-657d-4111-9830-8fa7c43de843") },
                    { new Guid("26be0c07-e225-4776-ba42-3617fe8037d3"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 130000m, 0, new Guid("d5de233a-bcf8-4f5b-b1f1-f9b6c2249aff") },
                    { new Guid("277b451e-6fbe-4c4b-aa0c-b2ffa9ba2945"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 90000m, 0, new Guid("f0ef2aba-d85e-4734-a799-4af28f79fa39") },
                    { new Guid("2f478d02-37a9-483a-9942-6c7456a315b7"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 190000m, 0, new Guid("80c550fb-ef17-42b9-8c65-61eac91584c5") },
                    { new Guid("32cca770-a351-4b1b-a860-f1791c7889b2"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 150000m, 0, new Guid("568d70cd-8ab0-4e57-adf1-e186a4e4ae0a") },
                    { new Guid("39948f94-de30-4d12-ad73-8f11c7309bba"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 120000m, 1, new Guid("e1e6a4fc-8669-496a-9a3d-adb49a508df6") },
                    { new Guid("3c1b685a-d126-4100-b5db-e720e3255cd4"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 160000m, 1, new Guid("9528a09f-db5b-402b-a1d0-6329f34a1bd4") },
                    { new Guid("3f78eb3f-40c5-46f7-98de-faa41b7cb34b"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 80000m, 1, new Guid("574c51d9-bb72-4946-8497-291ecd82e684") },
                    { new Guid("4434e69e-3ae8-4823-bbde-afba3659b9d4"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 210000m, 0, new Guid("063746cc-0553-4b67-a1db-74fb28aa29f7") },
                    { new Guid("47966076-7eb6-48c5-bb43-52a2825dfad2"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 130000m, 0, new Guid("2e9dcc44-3483-46fb-8b1a-9fc2b6cb558f") },
                    { new Guid("4cb037d4-5674-4b8a-a03f-54ee453fe50b"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 170000m, 0, new Guid("b6a144a9-dbdd-4189-bf26-c546b0827923") },
                    { new Guid("4d2d10a3-5a25-49f0-9416-239150e49171"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 160000m, 1, new Guid("3292fabb-06a4-40e9-b514-942b86704cab") },
                    { new Guid("57bd55ef-025b-40d0-a9bc-7dd886c8825e"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 140000m, 1, new Guid("6b18309a-f58a-450e-8be8-b1f321998bb1") },
                    { new Guid("67897dfe-b3d7-4397-9d3e-08dbe6737a4c"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 100000m, 1, new Guid("1bd8a77b-4d58-4e03-89a4-dcad72961b83") },
                    { new Guid("7093a7dd-c7b7-464e-9a9a-1777dd1d5e2c"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 50000m, 0, new Guid("d0d7ec82-3b27-4ea0-99a6-ee9eeb08c5f4") },
                    { new Guid("847ce75c-a26f-4965-af92-603c2bbf98da"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 110000m, 0, new Guid("f2e4c66d-3b7c-4beb-ae28-e9be82969820") },
                    { new Guid("8679de4d-2480-4602-8677-a85aacd10835"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 200000m, 1, new Guid("95170234-6b86-415b-9cac-8c8afb37c970") },
                    { new Guid("8a4c5996-7e46-47cb-9c4a-4b9e5ca1e064"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 180000m, 1, new Guid("ecb991d1-f44c-4956-98fd-83cf177475c0") },
                    { new Guid("8a5bec26-5113-4435-a96c-a64a9622149e"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 190000m, 0, new Guid("898cd1a4-099c-4e84-9389-8ea343fa41b5") },
                    { new Guid("8a985981-784c-4756-9aaa-ad9df2afe2d4"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 70000m, 0, new Guid("7cc5b8dd-220c-424c-9d75-1b4f34d16bf8") },
                    { new Guid("8cf972cf-c0a7-4f63-b23f-e2e6b9f521c8"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 70000m, 0, new Guid("b517f50d-938a-44f1-8660-a66e16575ab7") },
                    { new Guid("99bb4a65-5a4f-491d-ace8-685e15e9b40f"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 30000m, 0, new Guid("27d438ed-1657-4d27-ad3b-b9a65730229b") },
                    { new Guid("9ba11fa9-0664-49b3-8e80-dd79d7527887"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 150000m, 0, new Guid("f5f7c33c-846e-403b-bd45-b826324687c6") },
                    { new Guid("a10b6c85-7fcf-4bdd-97a8-1acf91de20e8"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 80000m, 1, new Guid("427fd471-ab1e-44bc-8976-bac56da138f1") },
                    { new Guid("a864b1ef-97d9-4e07-b16c-e82d78dfe5f8"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 210000m, 0, new Guid("cae09a4d-ecdd-4e36-a544-ea68054c4b5d") },
                    { new Guid("abe20050-6bba-47dc-a05a-c7b8429123fb"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 40000m, 1, new Guid("9827c1a6-d338-4997-b26b-abf984ea8287") },
                    { new Guid("ac10ed00-d646-4bce-9437-b0eee496e17d"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 60000m, 1, new Guid("4a4d2585-5c0e-4889-aef2-77e28f18fa0f") },
                    { new Guid("b9e21c4a-8bc6-482b-ac12-b866f844bf4b"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 90000m, 0, new Guid("dc2cfb75-8df9-4647-a0ba-2761a3b4209d") },
                    { new Guid("c0a60beb-b007-408b-8954-6db87bb41c63"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 180000m, 1, new Guid("3cac0ea0-74cb-4a16-8adf-c1c00b24322c") },
                    { new Guid("da5fc32b-ce25-4276-aa69-80d77975c435"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 60000m, 1, new Guid("09a5b2fe-e049-455c-873c-6c716fc518d8") },
                    { new Guid("de5c4f40-b907-4b99-bf3f-71bc3020373e"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 30000m, 0, new Guid("5ba28aa9-228d-490d-9a89-ca70e006191e") },
                    { new Guid("e362022b-b735-48b9-a415-a42bd370c4b1"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 20000m, 1, new Guid("b4adc28d-196e-4418-a143-5147f1bbb90f") },
                    { new Guid("e8be08dd-5e1e-4f58-98c8-06a0eae00440"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 170000m, 0, new Guid("c8da30e9-278c-4076-bbaf-eeb569a38912") },
                    { new Guid("ed01bf91-5df2-43a0-bb4d-9bcca087d657"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 50000m, 0, new Guid("469f56a6-0f10-41f5-9f87-0daf0cc2fd3c") },
                    { new Guid("ee01b0a9-3aba-4071-abb4-5d3d3bab9da4"), new Guid("85e41921-df7f-40e0-a84b-6f833b2d7dc3"), 200000m, 1, new Guid("ccb6b25a-1eb8-40b4-958e-74a9d86fcf3f") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_contacts_name",
                table: "contacts",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_contacts_user_id",
                table: "contacts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ledgers_account_id",
                table: "ledgers",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_ledgers_record_id_account_id",
                table: "ledgers",
                columns: new[] { "record_id", "account_id" });

            migrationBuilder.CreateIndex(
                name: "IX_payable_ledgers_creditor_id",
                table: "payable_ledgers",
                column: "creditor_id");

            migrationBuilder.CreateIndex(
                name: "IX_payable_ledgers_record_id",
                table: "payable_ledgers",
                column: "record_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payable_ledgers_record_id_creditor_id",
                table: "payable_ledgers",
                columns: new[] { "record_id", "creditor_id" });

            migrationBuilder.CreateIndex(
                name: "IX_receivable_ledgers_debtor_id",
                table: "receivable_ledgers",
                column: "debtor_id");

            migrationBuilder.CreateIndex(
                name: "IX_receivable_ledgers_record_id",
                table: "receivable_ledgers",
                column: "record_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_receivable_ledgers_record_id_debtor_id",
                table: "receivable_ledgers",
                columns: new[] { "record_id", "debtor_id" });

            migrationBuilder.CreateIndex(
                name: "IX_records_user_id",
                table: "records",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ledgers");

            migrationBuilder.DropTable(
                name: "payable_ledgers");

            migrationBuilder.DropTable(
                name: "receivable_ledgers");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "contacts");

            migrationBuilder.DropTable(
                name: "records");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
