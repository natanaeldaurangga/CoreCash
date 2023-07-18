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
                    { new Guid("425ff2f0-abac-4af7-a8d1-5d5d89e61fdf"), 11005, 11, "RECEIVABLE", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5170), null, new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5170) },
                    { new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 11001, 11, "CASH", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5168), null, new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5168) },
                    { new Guid("dbf530c7-9e1c-42ce-96c9-fa39cfd08a47"), 21001, 21, "PAYABLE", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5172), null, new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5172) }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_at", "deleted_at", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("37a31523-fdc3-4221-bbfb-27e4e7a7cda9"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(4264), null, "ROLE_USER", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(4264) },
                    { new Guid("b7f354e8-da85-4275-ab42-a26455086564"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(4224), null, "ROLE_ADMIN", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(4226) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "deleted_at", "email", "full_name", "password_hash", "password_salt", "profile_picture", "refresh_token", "reset_password_token", "reset_token_expires", "role_id", "token_expires", "updated_at", "verification_token", "verified_at" },
                values: new object[,]
                {
                    { new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5140), null, "user2@example.com", "User Dua", new byte[] { 181, 0, 171, 184, 96, 109, 22, 2, 183, 99, 239, 52, 233, 50, 255, 166, 150, 139, 21, 250, 201, 13, 248, 253, 134, 225, 66, 238, 187, 220, 3, 61 }, new byte[] { 146, 129, 146, 181, 248, 168, 244, 238, 136, 170, 253, 107, 232, 118, 150, 91, 69, 177, 31, 49, 4, 239, 33, 31, 206, 205, 95, 207, 209, 65, 65, 113, 173, 71, 53, 245, 93, 255, 177, 144, 228, 240, 137, 181, 173, 27, 30, 27, 8, 181, 80, 182, 127, 200, 235, 200, 21, 178, 2, 148, 243, 104, 80, 245 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("37a31523-fdc3-4221-bbfb-27e4e7a7cda9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5140), null, new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5140) },
                    { new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5137), null, "user1@example.com", "User Satu", new byte[] { 181, 0, 171, 184, 96, 109, 22, 2, 183, 99, 239, 52, 233, 50, 255, 166, 150, 139, 21, 250, 201, 13, 248, 253, 134, 225, 66, 238, 187, 220, 3, 61 }, new byte[] { 146, 129, 146, 181, 248, 168, 244, 238, 136, 170, 253, 107, 232, 118, 150, 91, 69, 177, 31, 49, 4, 239, 33, 31, 206, 205, 95, 207, 209, 65, 65, 113, 173, 71, 53, 245, 93, 255, 177, 144, 228, 240, 137, 181, 173, 27, 30, 27, 8, 181, 80, 182, 127, 200, 235, 200, 21, 178, 2, 148, 243, 104, 80, 245 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("37a31523-fdc3-4221-bbfb-27e4e7a7cda9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5138), null, new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5137) },
                    { new Guid("9e12144b-12ea-4f86-b8ef-45d08a0bea40"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5134), null, "admin2@example.com", "Admin Dua", new byte[] { 181, 0, 171, 184, 96, 109, 22, 2, 183, 99, 239, 52, 233, 50, 255, 166, 150, 139, 21, 250, 201, 13, 248, 253, 134, 225, 66, 238, 187, 220, 3, 61 }, new byte[] { 146, 129, 146, 181, 248, 168, 244, 238, 136, 170, 253, 107, 232, 118, 150, 91, 69, 177, 31, 49, 4, 239, 33, 31, 206, 205, 95, 207, 209, 65, 65, 113, 173, 71, 53, 245, 93, 255, 177, 144, 228, 240, 137, 181, 173, 27, 30, 27, 8, 181, 80, 182, 127, 200, 235, 200, 21, 178, 2, 148, 243, 104, 80, 245 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b7f354e8-da85-4275-ab42-a26455086564"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5135), null, new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5134) },
                    { new Guid("ffcd63e2-ed1b-418f-9c38-8616bed15682"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5131), null, "admin1@example.com", "Admin Satu", new byte[] { 181, 0, 171, 184, 96, 109, 22, 2, 183, 99, 239, 52, 233, 50, 255, 166, 150, 139, 21, 250, 201, 13, 248, 253, 134, 225, 66, 238, 187, 220, 3, 61 }, new byte[] { 146, 129, 146, 181, 248, 168, 244, 238, 136, 170, 253, 107, 232, 118, 150, 91, 69, 177, 31, 49, 4, 239, 33, 31, 206, 205, 95, 207, 209, 65, 65, 113, 173, 71, 53, 245, 93, 255, 177, 144, 228, 240, 137, 181, 173, 27, 30, 27, 8, 181, 80, 182, 127, 200, 235, 200, 21, 178, 2, 148, 243, 104, 80, 245 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b7f354e8-da85-4275-ab42-a26455086564"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5131), null, new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5131) }
                });

            migrationBuilder.InsertData(
                table: "contacts",
                columns: new[] { "id", "address", "created_at", "deleted_at", "email", "name", "phone_number", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("4648fc23-7399-4b36-9e9f-a753eb5e2b58"), "39494 Kartens Terrace", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5472), null, "sblanchard4@vinaora.com", "Sal Blanchard", "4904310249", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5472), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("610e03d6-4392-4ed3-b044-3626752e5491"), "4649 Swallow Trail", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5475), null, "tshew6@tripadvisor.com", "Tiphany Shew", "1457940882", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5476), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("75a1f206-bdb1-4eb3-a353-e03b4bb55fe6"), "620 Lakeland Center", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5464), null, "rhavock0@gov.uk", "Randell Havock", "6765109874", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5464), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("855e5a8c-e8c1-4142-a081-c5b9ae066f31"), "10 Calypso Center", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5477), null, "candrelli7@intel.com", "Ceil Andrelli", "1401949061", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5477), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("8d854e22-585d-4143-9f7f-76781c276287"), "34 Eastwood Avenue", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5467), null, "smahood1@wufoo.com", "Sarine Mahood", "6977095403", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5467), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("993f9e59-b487-4d02-8c09-51902664022a"), "11 Old Shore Terrace", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5481), null, "msivorn8@icq.com", "Margaretta Sivorn", "5311644464", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5481), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("a493f93c-df25-497a-84ec-9795d8e50d62"), "54 Redwing Drive", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5484), null, "jorneblowa@youtube.com", "Jennee Orneblow", "9316403125", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5484), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("abe9e7a2-8bdb-483f-bc39-7a8cb694f3b6"), "615 Jenifer Alley", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5474), null, "ldunster5@marketwatch.com", "Laurette Dunster", "7678939672", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5474), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("c5115ef0-2948-47a7-b15c-dac7f19e568e"), "96 Summerview Drive", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5471), null, "klanphere3@upenn.edu", "Kev Lanphere", "7268834724", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5471), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("de51f0a1-ee44-4811-ac74-20fa40bf6525"), "17 Onsgard Avenue", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5469), null, "bmcging2@ihg.com", "Benjie McGing", "5025308916", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5469), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("ee894440-a464-4792-94f1-6ce5199c95f8"), "4330 Doe Crossing Junction", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5482), null, "vwoodsford9@issuu.com", "Vasilis Woodsford", "8364764344", new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5483), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") }
                });

            migrationBuilder.InsertData(
                table: "records",
                columns: new[] { "id", "created_at", "deleted_at", "description", "record_group", "recorded_at", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("05997ea9-e9cf-4561-8d2e-8606dd4f8e26"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5217), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5217), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5218), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("09b68054-4f6d-432c-8fdc-f00d85637b58"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5386), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5385), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5386), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("0becc9eb-0cea-4ffb-88aa-073a06a966b9"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5232), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5231), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5232), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("0d883426-315e-4e15-aad9-feb1f74f5b09"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5248), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5247), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5248), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("0f9a6955-29a1-4cd3-be47-341e86ff7511"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5290), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5290), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5290), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("105838c7-1dc6-492c-8135-4a2ff0ccc7bc"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5360), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5359), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5360), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("17181f5d-86d0-46f9-8603-56ed3b4dbbcb"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5245), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5244), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5245), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("1ba03d01-2868-40d0-bb1e-fd4468de0315"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5208), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5195), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5209), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("1c935a31-e1de-43f0-90c6-7343d78ef95c"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5303), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5303), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5304), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("248f9b45-3302-4f96-bacb-60f22fada637"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5220), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5220), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5221), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("2554b5fe-f503-4016-aa4f-fe7125fc66e6"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5365), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5365), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5366), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("2679402f-fd57-4c77-956c-3605b8fd3619"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5296), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5296), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5296), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("3745b292-a40b-4c62-83eb-aec0937d3d5a"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5376), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5375), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5376), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("41b7afbe-3434-46ce-a548-e7735ef469d5"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5234), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5234), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5235), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("45949344-3820-4539-b222-bf7db999143b"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5282), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5282), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5282), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("552ff3c9-44c8-4df6-97af-6c592c55c39b"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5430), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5430), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5430), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("68483cfa-c908-4e15-b526-ec9e86a56734"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5355), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5354), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5355), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("7549fb3e-b255-41e3-8541-1b7fffe49284"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5378), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5378), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5379), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("7c004c6d-c807-417b-be42-06c68c34029d"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5287), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5287), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5287), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("84ff4573-4de8-4aa5-815b-6397960eaf90"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5363), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5362), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5363), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("8a6597b6-25e7-4b98-a9e4-d2ac171f40b3"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5346), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5346), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5346), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("9c02dcdd-7b39-451c-8e2d-46c53a9197f6"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5224), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5223), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5224), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("9c068935-2175-4cc1-8119-54c7b4bf92aa"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5293), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5292), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5293), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("9d2e1ea6-d5d2-4b51-8644-778f50950ed4"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5242), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5242), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5242), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("9e1f6caa-79b5-46ce-ad39-2e2bef71db62"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5309), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5309), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5309), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("a071a964-e913-4431-930e-31e9df9ece13"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5301), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5300), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5301), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("a0e6a579-26dc-480b-8d82-dfc4d69e93aa"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5368), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5368), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5368), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("a810b1ec-41a6-4d48-be82-2ac26a3d7e8f"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5389), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5388), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5389), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("aec52098-7e43-4034-90ff-d1135defa603"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5352), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5351), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5352), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("afc35b8c-e0c2-4cc9-89c1-2494c88f5ee5"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5399), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5398), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5399), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("bc218e8d-1c4b-4ec1-8a02-47316de988e8"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5433), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5432), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5433), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") }
                });

            migrationBuilder.InsertData(
                table: "records",
                columns: new[] { "id", "created_at", "deleted_at", "description", "record_group", "recorded_at", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("bdb73925-8d8b-4c17-bdab-4f188db108e6"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5306), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5306), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5306), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("ca5a20ee-cdc2-4242-943c-b6d3d72ebd7c"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5381), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5381), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5381), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("d1bc76c9-d836-4e4e-9554-ecebc585b6cf"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5237), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5237), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5237), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("d6110d05-2874-4434-aa7a-45c340e2fbe6"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5427), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5426), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5427), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("ddb4c070-25da-44e1-a605-225b94626e34"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5394), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5394), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5395), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("e248d2d2-5f8f-4e3e-ae38-6a407ce97912"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5349), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5349), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5349), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("e536426d-5cb2-4a80-9fc1-81ddd0578023"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5229), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5228), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5229), new Guid("78f6a4bc-eaaf-4c9d-b35d-6d60a81ab4c6") },
                    { new Guid("f9c3176a-cec6-4b9a-a1b0-c4fd36a46902"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5373), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5372), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5373), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") },
                    { new Guid("fbb886e7-b049-4fd1-8d14-ccbc7fbd15f8"), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5391), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 17, 11, 57, 57, 179, DateTimeKind.Local).AddTicks(5391), new DateTime(2023, 7, 17, 4, 57, 57, 179, DateTimeKind.Utc).AddTicks(5392), new Guid("39591bae-0bd0-4df6-ac8a-fc5be514d7ea") }
                });

            migrationBuilder.InsertData(
                table: "ledgers",
                columns: new[] { "id", "account_id", "balance", "entry", "record_id" },
                values: new object[,]
                {
                    { new Guid("050c4a91-63b4-4352-8422-15763eb95a0f"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 180000m, 1, new Guid("68483cfa-c908-4e15-b526-ec9e86a56734") },
                    { new Guid("137c47fa-54f3-4314-8c12-16d84f0d2c1c"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 170000m, 0, new Guid("e536426d-5cb2-4a80-9fc1-81ddd0578023") },
                    { new Guid("2048e6d9-472b-4c9b-add4-3036b49f7252"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 190000m, 0, new Guid("248f9b45-3302-4f96-bacb-60f22fada637") },
                    { new Guid("22658655-b351-42a1-8023-6b0907527832"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 30000m, 0, new Guid("bdb73925-8d8b-4c17-bdab-4f188db108e6") },
                    { new Guid("24554c95-05a6-4939-b1e7-e5b05e26f7f2"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 120000m, 1, new Guid("3745b292-a40b-4c62-83eb-aec0937d3d5a") },
                    { new Guid("24ac8bd0-dbe1-4067-97be-8e569f6af847"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 20000m, 1, new Guid("bc218e8d-1c4b-4ec1-8a02-47316de988e8") },
                    { new Guid("2c089f41-b48e-46f8-be54-403994bcfbc9"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 180000m, 1, new Guid("9c02dcdd-7b39-451c-8e2d-46c53a9197f6") },
                    { new Guid("2eb12eaa-6dfc-4aae-8259-f0cf6ccdab52"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 50000m, 0, new Guid("afc35b8c-e0c2-4cc9-89c1-2494c88f5ee5") },
                    { new Guid("3f028057-7b19-4d66-9aef-52ad2dcfe208"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 130000m, 0, new Guid("f9c3176a-cec6-4b9a-a1b0-c4fd36a46902") },
                    { new Guid("3fc294d3-54db-4d26-af27-309505eb3586"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 140000m, 1, new Guid("a0e6a579-26dc-480b-8d82-dfc4d69e93aa") },
                    { new Guid("406d8187-32ee-4ac3-8206-bee7d22baa82"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 60000m, 1, new Guid("ddb4c070-25da-44e1-a605-225b94626e34") },
                    { new Guid("46efdf1d-7f16-45d0-b2f9-12e87abe7334"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 70000m, 0, new Guid("9c068935-2175-4cc1-8119-54c7b4bf92aa") },
                    { new Guid("4ad09e43-952c-4b27-91a8-4a7d8112d87a"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 80000m, 1, new Guid("0f9a6955-29a1-4cd3-be47-341e86ff7511") },
                    { new Guid("4b94af2c-1c1c-4393-8634-0683e62d3a9f"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 40000m, 1, new Guid("d6110d05-2874-4434-aa7a-45c340e2fbe6") },
                    { new Guid("512771fa-7b58-4c67-9f16-c2ea14cc48fc"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 100000m, 1, new Guid("45949344-3820-4539-b222-bf7db999143b") },
                    { new Guid("5c2217ab-cc28-4ec1-bbcf-df2b414222e6"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 170000m, 0, new Guid("105838c7-1dc6-492c-8135-4a2ff0ccc7bc") },
                    { new Guid("66b25734-28b1-4020-b456-2821b65184e2"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 90000m, 0, new Guid("09b68054-4f6d-432c-8fdc-f00d85637b58") },
                    { new Guid("6dbfd246-61b0-4ad6-a857-45a35e7768c0"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 130000m, 0, new Guid("9d2e1ea6-d5d2-4b51-8644-778f50950ed4") },
                    { new Guid("73dea449-3d26-4823-98f0-ee618796c850"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 200000m, 1, new Guid("e248d2d2-5f8f-4e3e-ae38-6a407ce97912") },
                    { new Guid("7452a1d2-b27b-4c8c-97f2-f343ad95e423"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 40000m, 1, new Guid("1c935a31-e1de-43f0-90c6-7343d78ef95c") },
                    { new Guid("7712f25f-2e99-4f27-b8fa-034ebf250c9e"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 60000m, 1, new Guid("2679402f-fd57-4c77-956c-3605b8fd3619") },
                    { new Guid("7898f6a5-9cb7-4120-909f-edc43d923736"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 210000m, 0, new Guid("1ba03d01-2868-40d0-bb1e-fd4468de0315") },
                    { new Guid("8a7ef6f2-b719-4c31-9a38-2e68ce69635f"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 200000m, 1, new Guid("05997ea9-e9cf-4561-8d2e-8606dd4f8e26") },
                    { new Guid("8b3a14b0-d89e-49fb-9115-e6351a978ba8"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 120000m, 1, new Guid("17181f5d-86d0-46f9-8603-56ed3b4dbbcb") },
                    { new Guid("8f8db193-57c3-48ca-a9fb-b8f542ae9575"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 100000m, 1, new Guid("ca5a20ee-cdc2-4242-943c-b6d3d72ebd7c") },
                    { new Guid("a25dfcfc-6d26-4c6f-90b3-b962f1bac31d"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 30000m, 0, new Guid("552ff3c9-44c8-4df6-97af-6c592c55c39b") },
                    { new Guid("a9e5d0d8-e7d9-45c4-8286-1c9595d7fab7"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 160000m, 1, new Guid("84ff4573-4de8-4aa5-815b-6397960eaf90") },
                    { new Guid("c43ccd71-87b3-4913-a19e-3fb5ae96206d"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 110000m, 0, new Guid("7549fb3e-b255-41e3-8541-1b7fffe49284") },
                    { new Guid("c7c8415e-915f-413f-8af1-72d641d3df87"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 20000m, 1, new Guid("9e1f6caa-79b5-46ce-ad39-2e2bef71db62") },
                    { new Guid("cb9cf7e5-d028-44c1-bd92-f6e7ec5e367b"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 50000m, 0, new Guid("a071a964-e913-4431-930e-31e9df9ece13") },
                    { new Guid("d07bb753-5370-44c7-bfe4-bca05bf03792"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 70000m, 0, new Guid("fbb886e7-b049-4fd1-8d14-ccbc7fbd15f8") },
                    { new Guid("d250d355-a5ef-4c70-b8fe-fa2eadbf3d66"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 110000m, 0, new Guid("0d883426-315e-4e15-aad9-feb1f74f5b09") },
                    { new Guid("db2d10e6-965a-4d19-b1a8-c45762d93a51"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 210000m, 0, new Guid("8a6597b6-25e7-4b98-a9e4-d2ac171f40b3") },
                    { new Guid("de9c3292-3187-44fc-8be4-01ae9f145cdb"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 90000m, 0, new Guid("7c004c6d-c807-417b-be42-06c68c34029d") },
                    { new Guid("e188d9d8-fa00-4895-a60d-233af3c3db2d"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 80000m, 1, new Guid("a810b1ec-41a6-4d48-be82-2ac26a3d7e8f") },
                    { new Guid("e61140df-2aa3-4731-9a49-62e358a43472"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 160000m, 1, new Guid("0becc9eb-0cea-4ffb-88aa-073a06a966b9") },
                    { new Guid("e6ed17e6-fea4-4e9c-861f-5b7b96bdd9f2"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 190000m, 0, new Guid("aec52098-7e43-4034-90ff-d1135defa603") },
                    { new Guid("f21bd7b1-2a97-4596-98eb-b2b97eef0334"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 150000m, 0, new Guid("2554b5fe-f503-4016-aa4f-fe7125fc66e6") },
                    { new Guid("fe337e08-014e-4239-a80c-9c669ef9229c"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 150000m, 0, new Guid("41b7afbe-3434-46ce-a548-e7735ef469d5") },
                    { new Guid("ff4ae36b-2267-4413-801c-ce5454053b1d"), new Guid("9cfa7436-7931-4da8-aebc-b4a05b479ab9"), 140000m, 1, new Guid("d1bc76c9-d836-4e4e-9554-ecebc585b6cf") }
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
