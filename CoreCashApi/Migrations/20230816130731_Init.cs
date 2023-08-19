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
                    { new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 11001, 11, "CASH", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1314), null, new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1314) },
                    { new Guid("82ae83bf-e4c8-4689-8d4d-71468b3aef6c"), 11005, 11, "RECEIVABLE", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1316), null, new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1316) },
                    { new Guid("fafe0f2e-9f47-4d27-9708-9325283b8293"), 21001, 21, "PAYABLE", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1318), null, new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1318) }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_at", "deleted_at", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("7768ffb9-1ee9-45fd-8bb6-6854b5954f0e"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(243), null, "ROLE_USER", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(243) },
                    { new Guid("8583bf86-599d-4032-890a-b7a95a9b8375"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(208), null, "ROLE_ADMIN", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(212) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "deleted_at", "email", "full_name", "password_hash", "password_salt", "profile_picture", "refresh_token", "reset_password_token", "reset_token_expires", "role_id", "token_expires", "updated_at", "verification_token", "verified_at" },
                values: new object[,]
                {
                    { new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1279), null, "user1@example.com", "User Satu", new byte[] { 159, 131, 127, 62, 148, 90, 191, 24, 40, 49, 141, 76, 41, 145, 120, 72, 211, 99, 177, 165, 33, 191, 104, 119, 6, 125, 192, 108, 147, 44, 148, 69 }, new byte[] { 47, 0, 77, 102, 4, 241, 233, 197, 216, 51, 120, 4, 160, 241, 57, 40, 216, 100, 139, 52, 237, 87, 212, 8, 69, 119, 6, 123, 204, 112, 84, 237, 49, 113, 66, 164, 113, 45, 145, 247, 203, 67, 124, 139, 73, 127, 205, 24, 248, 14, 233, 222, 42, 247, 137, 183, 137, 38, 218, 163, 233, 225, 204, 164 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7768ffb9-1ee9-45fd-8bb6-6854b5954f0e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1280), null, new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1279) },
                    { new Guid("7e746144-7175-40b1-93aa-4e75dc290847"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1282), null, "user2@example.com", "User Dua", new byte[] { 159, 131, 127, 62, 148, 90, 191, 24, 40, 49, 141, 76, 41, 145, 120, 72, 211, 99, 177, 165, 33, 191, 104, 119, 6, 125, 192, 108, 147, 44, 148, 69 }, new byte[] { 47, 0, 77, 102, 4, 241, 233, 197, 216, 51, 120, 4, 160, 241, 57, 40, 216, 100, 139, 52, 237, 87, 212, 8, 69, 119, 6, 123, 204, 112, 84, 237, 49, 113, 66, 164, 113, 45, 145, 247, 203, 67, 124, 139, 73, 127, 205, 24, 248, 14, 233, 222, 42, 247, 137, 183, 137, 38, 218, 163, 233, 225, 204, 164 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7768ffb9-1ee9-45fd-8bb6-6854b5954f0e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1282), null, new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1282) },
                    { new Guid("c1da9533-7503-4e80-bce9-4646fe19f6c3"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1272), null, "admin1@example.com", "Admin Satu", new byte[] { 159, 131, 127, 62, 148, 90, 191, 24, 40, 49, 141, 76, 41, 145, 120, 72, 211, 99, 177, 165, 33, 191, 104, 119, 6, 125, 192, 108, 147, 44, 148, 69 }, new byte[] { 47, 0, 77, 102, 4, 241, 233, 197, 216, 51, 120, 4, 160, 241, 57, 40, 216, 100, 139, 52, 237, 87, 212, 8, 69, 119, 6, 123, 204, 112, 84, 237, 49, 113, 66, 164, 113, 45, 145, 247, 203, 67, 124, 139, 73, 127, 205, 24, 248, 14, 233, 222, 42, 247, 137, 183, 137, 38, 218, 163, 233, 225, 204, 164 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8583bf86-599d-4032-890a-b7a95a9b8375"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1274), null, new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1272) },
                    { new Guid("f49d7b45-5f94-43ce-91fa-80d3aba8d132"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1276), null, "admin2@example.com", "Admin Dua", new byte[] { 159, 131, 127, 62, 148, 90, 191, 24, 40, 49, 141, 76, 41, 145, 120, 72, 211, 99, 177, 165, 33, 191, 104, 119, 6, 125, 192, 108, 147, 44, 148, 69 }, new byte[] { 47, 0, 77, 102, 4, 241, 233, 197, 216, 51, 120, 4, 160, 241, 57, 40, 216, 100, 139, 52, 237, 87, 212, 8, 69, 119, 6, 123, 204, 112, 84, 237, 49, 113, 66, 164, 113, 45, 145, 247, 203, 67, 124, 139, 73, 127, 205, 24, 248, 14, 233, 222, 42, 247, 137, 183, 137, 38, 218, 163, 233, 225, 204, 164 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8583bf86-599d-4032-890a-b7a95a9b8375"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1277), null, new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1276) }
                });

            migrationBuilder.InsertData(
                table: "contacts",
                columns: new[] { "id", "address", "created_at", "deleted_at", "email", "name", "phone_number", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("03144141-7386-47e0-9822-cd5bd33c811a"), "96 Summerview Drive", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1636), null, "klanphere3@upenn.edu", "Kev Lanphere", "7268834724", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1636), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("033b8068-64c5-43e1-aa7b-785da7958dc9"), "54 Redwing Drive", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1652), null, "jorneblowa@youtube.com", "Jennee Orneblow", "9316403125", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1652), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("37dfd3fe-ef87-4d9e-adff-4ef3c1fb998b"), "34 Eastwood Avenue", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1632), null, "smahood1@wufoo.com", "Sarine Mahood", "6977095403", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1632), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("583240f2-4cfa-4564-af9a-f81f3934ad93"), "620 Lakeland Center", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1629), null, "rhavock0@gov.uk", "Randell Havock", "6765109874", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1629), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("62f26fb8-f132-450c-8e59-9d17e9795dc3"), "17 Onsgard Avenue", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1634), null, "bmcging2@ihg.com", "Benjie McGing", "5025308916", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1635), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("bd526479-68ec-4b6a-82f1-0d0e8968957b"), "4649 Swallow Trail", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1641), null, "tshew6@tripadvisor.com", "Tiphany Shew", "1457940882", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1642), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("e3e00d86-4967-41bd-871c-a2af36da0fa8"), "39494 Kartens Terrace", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1638), null, "sblanchard4@vinaora.com", "Sal Blanchard", "4904310249", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1638), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("e7d54baa-5fe8-45bf-a675-ccf81e79ff3e"), "4330 Doe Crossing Junction", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1650), null, "vwoodsford9@issuu.com", "Vasilis Woodsford", "8364764344", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1651), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("e8336b15-586d-4da5-9a36-e56ad0326433"), "11 Old Shore Terrace", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1649), null, "msivorn8@icq.com", "Margaretta Sivorn", "5311644464", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1649), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("f4c931a4-4c49-4a23-93f5-4266d199db77"), "10 Calypso Center", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1644), null, "candrelli7@intel.com", "Ceil Andrelli", "1401949061", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1645), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("ff3ab6e9-acd9-450e-8c6d-982ea7551a11"), "615 Jenifer Alley", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1640), null, "ldunster5@marketwatch.com", "Laurette Dunster", "7678939672", new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1640), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") }
                });

            migrationBuilder.InsertData(
                table: "records",
                columns: new[] { "id", "created_at", "deleted_at", "description", "record_group", "recorded_at", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("009046bf-8cd8-4aad-99ce-083c04c4df76"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1454), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1453), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1454), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("0a55e376-f056-4a15-910f-c8a4231fea2f"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1440), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1440), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1440), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("11f0cafb-44db-4f69-a87c-d7c10f07f27a"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1542), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1541), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1542), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("21cd814f-2d27-4893-8202-80142bb08f14"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1394), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1393), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1394), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("249d7482-b7d4-46ad-8951-ec2c77240d8a"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1443), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1443), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1443), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("2c42c386-155d-48a4-ad3b-2a700d8ec152"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1509), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1509), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1510), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("32723766-bf5a-4381-93a9-2060c3906eb4"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1533), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1533), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1533), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("32997332-635d-498d-b364-019da845db4c"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1552), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1552), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1553), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("334a1a1e-3fb6-47cf-b9b0-af8fd4910324"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1435), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1435), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1436), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("422dbf1f-e5e9-4ad2-b9dc-5c985ebbc90d"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1376), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1376), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1377), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("4442cacc-1657-443f-b236-0e30e7387b92"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1555), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1555), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1555), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("48f618a8-0d2b-41da-938c-9d07a53453b9"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1549), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1549), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1550), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("4c156dde-86bb-4e1f-aec9-f709ac1ebdf9"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1594), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1593), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1594), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("4faeadfe-7827-476c-a2cd-def1e99da5c5"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1446), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1446), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1446), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("5077a7a6-d1c3-4e0f-8234-3db952c7a50e"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1597), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1596), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1597), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("636e0632-134f-4d9a-8634-28dc1c84399b"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1357), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1347), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1357), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("6b4ebfae-35b7-45a8-888e-6135e5be53fb"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1523), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1522), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1523), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("7667b3cd-db60-4763-bcd3-1c43227a98b2"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1449), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1449), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1449), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("79a8ebf3-0f6f-49aa-ac88-eed0641afdc7"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1383), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1382), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1383), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("7d19f0ae-864b-4353-a1e4-db56af14470b"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1380), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1379), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1380), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("89aa48b3-e766-4197-87c5-be71ac068013"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1539), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1538), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1539), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("8d5576ac-2a1d-47f0-a294-381b9a2cf819"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1371), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1370), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1371), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("97948d52-b08b-496a-91c5-8e06c2bf6641"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1526), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1525), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1526), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("9c463c6b-e749-4cdd-aee4-b3c7cbccc2d4"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1547), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1546), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1547), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("a1cd2f9b-cb5b-4a64-ad87-18d7d01ac1e3"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1456), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1456), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1457), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("a41672cc-d754-41b9-9a62-8c3a58a4943c"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1385), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1385), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1386), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("a580c0a5-128a-45ce-a3b8-5cb8db357004"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1529), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1528), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1529), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("a74433b0-87d2-4e4e-8dab-259e35bcf462"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1520), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1520), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1520), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("aee26031-8cc0-4b46-9776-c73b6a09b4a2"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1512), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1512), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1513), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("b07b56eb-0d71-4d5f-bf30-027a6c6b4c19"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1368), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1367), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1368), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("b49e0bc6-f193-4f3c-b69b-3aa231c7734a"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1588), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1587), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1588), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") }
                });

            migrationBuilder.InsertData(
                table: "records",
                columns: new[] { "id", "created_at", "deleted_at", "description", "record_group", "recorded_at", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("ca22f92c-ac0d-4b37-9814-81fb508d7e32"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1391), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1390), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1391), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("d7688ffb-3fdb-43d1-a40c-f79b32c8e959"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1462), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1462), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1462), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("d817c9d4-84a2-4629-93f7-305384393cd1"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1365), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1364), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1365), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("e183d8d6-1aa9-4ab2-bbea-951c319b2144"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1459), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1459), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1460), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("e913cc47-a2d9-48b2-8136-fd29b642ff7a"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1515), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1515), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1515), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("e94e13de-848d-4506-9382-8bc03aef0fff"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1536), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1536), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1536), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("ebbfb507-fc92-410f-ba1d-ee2937ad1867"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1397), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1396), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1397), new Guid("10de2dd9-bea1-4442-b12f-a0cd34d47c22") },
                    { new Guid("f451bfe3-d1a3-44a3-821d-cb9659e6c9fc"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1506), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1506), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1506), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") },
                    { new Guid("f5d9809a-0d1a-4003-b3bb-c8d9c9039d7d"), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1591), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 8, 16, 20, 7, 31, 178, DateTimeKind.Local).AddTicks(1590), new DateTime(2023, 8, 16, 13, 7, 31, 178, DateTimeKind.Utc).AddTicks(1591), new Guid("7e746144-7175-40b1-93aa-4e75dc290847") }
                });

            migrationBuilder.InsertData(
                table: "ledgers",
                columns: new[] { "id", "account_id", "balance", "entry", "record_id" },
                values: new object[,]
                {
                    { new Guid("0ab30400-ec29-4e4b-89f8-b2f0bc421701"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 30000m, 0, new Guid("e183d8d6-1aa9-4ab2-bbea-951c319b2144") },
                    { new Guid("1320c89c-604b-43b0-9c86-8219f9c64949"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 30000m, 0, new Guid("4c156dde-86bb-4e1f-aec9-f709ac1ebdf9") },
                    { new Guid("19041061-5900-4aa8-92f6-3f41203b33d8"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 90000m, 0, new Guid("0a55e376-f056-4a15-910f-c8a4231fea2f") },
                    { new Guid("1d6ef694-cdca-4686-a304-362fd4cc77e2"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 160000m, 1, new Guid("6b4ebfae-35b7-45a8-888e-6135e5be53fb") },
                    { new Guid("2349bf2c-1362-4636-ab9c-f4a69787ac77"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 180000m, 1, new Guid("e913cc47-a2d9-48b2-8136-fd29b642ff7a") },
                    { new Guid("2349c5e3-3604-46c0-8477-c3ce22d94705"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 70000m, 0, new Guid("32997332-635d-498d-b364-019da845db4c") },
                    { new Guid("24f3babe-9b94-4106-a7d5-703606d8509a"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 20000m, 1, new Guid("d7688ffb-3fdb-43d1-a40c-f79b32c8e959") },
                    { new Guid("2777d7fd-ad1e-4263-85d6-1b703e8c5d08"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 190000m, 0, new Guid("b07b56eb-0d71-4d5f-bf30-027a6c6b4c19") },
                    { new Guid("2cdca935-f4ea-4376-946d-fdac81e0dd5f"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 40000m, 1, new Guid("a1cd2f9b-cb5b-4a64-ad87-18d7d01ac1e3") },
                    { new Guid("2e50c535-3bf3-4e9f-921a-ae32c859bfaa"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 120000m, 1, new Guid("e94e13de-848d-4506-9382-8bc03aef0fff") },
                    { new Guid("3a1551e5-caf9-46f1-8243-98c258a3f4fd"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 210000m, 0, new Guid("636e0632-134f-4d9a-8634-28dc1c84399b") },
                    { new Guid("4d531565-79d6-44df-ad8e-95ea59b70963"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 70000m, 0, new Guid("4faeadfe-7827-476c-a2cd-def1e99da5c5") },
                    { new Guid("51570fd1-34ed-4f12-8bf0-f6be4347cd23"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 60000m, 1, new Guid("4442cacc-1657-443f-b236-0e30e7387b92") },
                    { new Guid("546793b9-58f0-421e-ae32-bcb125b564b9"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 50000m, 0, new Guid("b49e0bc6-f193-4f3c-b69b-3aa231c7734a") },
                    { new Guid("57050b33-3f14-4d6b-be03-0aae61bc9f63"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 80000m, 1, new Guid("48f618a8-0d2b-41da-938c-9d07a53453b9") },
                    { new Guid("5879541b-2a88-4598-ba0e-cbb8840ba60c"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 130000m, 0, new Guid("32723766-bf5a-4381-93a9-2060c3906eb4") },
                    { new Guid("598721de-e63c-4df3-9803-157905abbd71"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 130000m, 0, new Guid("ca22f92c-ac0d-4b37-9814-81fb508d7e32") },
                    { new Guid("64a61685-bfa2-45a0-8db6-042f780a761b"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 140000m, 1, new Guid("a580c0a5-128a-45ce-a3b8-5cb8db357004") },
                    { new Guid("6cc53530-4656-41f2-9d9a-61d27a6dfc68"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 170000m, 0, new Guid("422dbf1f-e5e9-4ad2-b9dc-5c985ebbc90d") },
                    { new Guid("75868197-e5ed-4008-88db-f7c680db8389"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 150000m, 0, new Guid("97948d52-b08b-496a-91c5-8e06c2bf6641") },
                    { new Guid("7635d28b-20ad-4251-b374-dfda767ac825"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 120000m, 1, new Guid("21cd814f-2d27-4893-8202-80142bb08f14") },
                    { new Guid("78cde8e6-325a-4b85-b95c-41cf8ac4f8ee"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 150000m, 0, new Guid("79a8ebf3-0f6f-49aa-ac88-eed0641afdc7") },
                    { new Guid("8d449000-0d04-45a7-b19d-54285e466a0f"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 110000m, 0, new Guid("ebbfb507-fc92-410f-ba1d-ee2937ad1867") },
                    { new Guid("8d70c761-a8ab-4a80-8225-72bafa0c2c6d"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 90000m, 0, new Guid("9c463c6b-e749-4cdd-aee4-b3c7cbccc2d4") },
                    { new Guid("90c857a5-a57c-450a-8ca9-d47de2936a80"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 190000m, 0, new Guid("aee26031-8cc0-4b46-9776-c73b6a09b4a2") },
                    { new Guid("90c8a896-09d1-4a67-a3a5-9d161b100b9f"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 100000m, 1, new Guid("334a1a1e-3fb6-47cf-b9b0-af8fd4910324") },
                    { new Guid("ab288772-a6e8-4656-9a15-dc969666bffe"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 80000m, 1, new Guid("249d7482-b7d4-46ad-8951-ec2c77240d8a") },
                    { new Guid("b46b8f13-91d9-462e-9345-0fb3130f4685"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 200000m, 1, new Guid("2c42c386-155d-48a4-ad3b-2a700d8ec152") },
                    { new Guid("b50ac213-1931-43ff-bd95-ae678039c768"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 210000m, 0, new Guid("f451bfe3-d1a3-44a3-821d-cb9659e6c9fc") },
                    { new Guid("bbbb05ac-1daf-4b49-b832-529733359d8c"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 170000m, 0, new Guid("a74433b0-87d2-4e4e-8dab-259e35bcf462") },
                    { new Guid("bf5bb955-28a4-4555-89b1-08453ef5cd12"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 200000m, 1, new Guid("d817c9d4-84a2-4629-93f7-305384393cd1") },
                    { new Guid("c2555c14-ce68-454c-b367-eb67fea1c410"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 40000m, 1, new Guid("f5d9809a-0d1a-4003-b3bb-c8d9c9039d7d") },
                    { new Guid("c2a3d8ae-7de3-43c7-b2f0-b69174938e6c"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 160000m, 1, new Guid("7d19f0ae-864b-4353-a1e4-db56af14470b") },
                    { new Guid("c5a5d606-462e-409f-a2e9-47211484dec7"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 110000m, 0, new Guid("89aa48b3-e766-4197-87c5-be71ac068013") },
                    { new Guid("c9c7f956-fec8-46f6-87d1-381a3763e805"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 60000m, 1, new Guid("7667b3cd-db60-4763-bcd3-1c43227a98b2") },
                    { new Guid("dbab8809-afc2-4ff0-9869-18d11e878a62"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 180000m, 1, new Guid("8d5576ac-2a1d-47f0-a294-381b9a2cf819") },
                    { new Guid("e2958eca-24fc-4340-aa3a-afccd05ab3c2"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 50000m, 0, new Guid("009046bf-8cd8-4aad-99ce-083c04c4df76") },
                    { new Guid("e78b5587-5261-4cce-bbb6-5578f45cdb44"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 20000m, 1, new Guid("5077a7a6-d1c3-4e0f-8234-3db952c7a50e") },
                    { new Guid("e8694754-e9b7-4a71-a2ff-ebfa224f78c2"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 100000m, 1, new Guid("11f0cafb-44db-4f69-a87c-d7c10f07f27a") },
                    { new Guid("f6cb0707-c900-4228-b9bc-dd60e538135b"), new Guid("42f018e9-1f7d-48bd-b1a8-1ff469534875"), 140000m, 1, new Guid("a41672cc-d754-41b9-9a62-8c3a58a4943c") }
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
