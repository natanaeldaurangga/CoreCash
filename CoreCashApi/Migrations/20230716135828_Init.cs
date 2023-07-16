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
                name: "payables",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    record_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    creditor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payables", x => x.id);
                    table.ForeignKey(
                        name: "FK_payables_contacts_creditor_id",
                        column: x => x.creditor_id,
                        principalTable: "contacts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_payables_records_record_id",
                        column: x => x.record_id,
                        principalTable: "records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "receivables",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    record_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    debtor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receivables", x => x.id);
                    table.ForeignKey(
                        name: "FK_receivables_contacts_debtor_id",
                        column: x => x.debtor_id,
                        principalTable: "contacts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_receivables_records_record_id",
                        column: x => x.record_id,
                        principalTable: "records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payable_ledger",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    record_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    payable_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payable_ledger", x => x.id);
                    table.ForeignKey(
                        name: "FK_payable_ledger_payables_payable_id",
                        column: x => x.payable_id,
                        principalTable: "payables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payable_ledger_records_record_id",
                        column: x => x.record_id,
                        principalTable: "records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "receivable_ledger",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    record_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    receivable_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receivable_ledger", x => x.id);
                    table.ForeignKey(
                        name: "FK_receivable_ledger_receivables_receivable_id",
                        column: x => x.receivable_id,
                        principalTable: "receivables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_receivable_ledger_records_record_id",
                        column: x => x.record_id,
                        principalTable: "records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "id", "account_code", "account_group", "account_name", "created_at", "deleted_at", "updated_at" },
                values: new object[,]
                {
                    { new Guid("21725d6b-80d2-4248-bef3-574dc7a76c73"), 21001, 21, "PAYABLE", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5255), null, new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5256) },
                    { new Guid("65352e5e-3182-414f-b1fa-d091bf617726"), 11005, 11, "RECEIVABLE", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5254), null, new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5254) },
                    { new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 11001, 11, "CASH", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5251), null, new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5251) }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_at", "deleted_at", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("4739e3fa-8030-499c-9eac-0f648722a33b"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(4116), null, "ROLE_ADMIN", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(4119) },
                    { new Guid("fc70765b-d527-4700-a992-b8ccb950b134"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(4151), null, "ROLE_USER", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(4151) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "deleted_at", "email", "full_name", "password_hash", "password_salt", "profile_picture", "refresh_token", "reset_password_token", "reset_token_expires", "role_id", "token_expires", "updated_at", "verification_token", "verified_at" },
                values: new object[,]
                {
                    { new Guid("704e4fbf-c7fb-4464-a9f2-6b7d82c03f0f"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5200), null, "admin1@example.com", "Admin Satu", new byte[] { 252, 220, 227, 110, 30, 82, 178, 64, 52, 121, 129, 79, 179, 152, 39, 206, 199, 239, 76, 197, 252, 215, 160, 221, 132, 213, 237, 30, 103, 87, 113, 184 }, new byte[] { 106, 150, 70, 249, 234, 129, 117, 15, 126, 89, 154, 20, 243, 158, 217, 234, 146, 24, 204, 94, 164, 77, 219, 163, 238, 89, 75, 137, 151, 128, 227, 216, 18, 199, 203, 60, 245, 172, 246, 79, 117, 82, 16, 225, 121, 210, 151, 174, 117, 89, 116, 236, 167, 119, 31, 141, 227, 102, 122, 179, 213, 212, 252, 144 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4739e3fa-8030-499c-9eac-0f648722a33b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5201), null, new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5200) },
                    { new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5213), null, "user2@example.com", "User Dua", new byte[] { 252, 220, 227, 110, 30, 82, 178, 64, 52, 121, 129, 79, 179, 152, 39, 206, 199, 239, 76, 197, 252, 215, 160, 221, 132, 213, 237, 30, 103, 87, 113, 184 }, new byte[] { 106, 150, 70, 249, 234, 129, 117, 15, 126, 89, 154, 20, 243, 158, 217, 234, 146, 24, 204, 94, 164, 77, 219, 163, 238, 89, 75, 137, 151, 128, 227, 216, 18, 199, 203, 60, 245, 172, 246, 79, 117, 82, 16, 225, 121, 210, 151, 174, 117, 89, 116, 236, 167, 119, 31, 141, 227, 102, 122, 179, 213, 212, 252, 144 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("fc70765b-d527-4700-a992-b8ccb950b134"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5214), null, new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5213) },
                    { new Guid("af546513-ee69-4cde-8849-ba88d4b93070"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5206), null, "user1@example.com", "User Satu", new byte[] { 252, 220, 227, 110, 30, 82, 178, 64, 52, 121, 129, 79, 179, 152, 39, 206, 199, 239, 76, 197, 252, 215, 160, 221, 132, 213, 237, 30, 103, 87, 113, 184 }, new byte[] { 106, 150, 70, 249, 234, 129, 117, 15, 126, 89, 154, 20, 243, 158, 217, 234, 146, 24, 204, 94, 164, 77, 219, 163, 238, 89, 75, 137, 151, 128, 227, 216, 18, 199, 203, 60, 245, 172, 246, 79, 117, 82, 16, 225, 121, 210, 151, 174, 117, 89, 116, 236, 167, 119, 31, 141, 227, 102, 122, 179, 213, 212, 252, 144 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("fc70765b-d527-4700-a992-b8ccb950b134"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5207), null, new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5206) },
                    { new Guid("ce8e7880-e541-436f-b22b-6e606c643285"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5204), null, "admin2@example.com", "Admin Dua", new byte[] { 252, 220, 227, 110, 30, 82, 178, 64, 52, 121, 129, 79, 179, 152, 39, 206, 199, 239, 76, 197, 252, 215, 160, 221, 132, 213, 237, 30, 103, 87, 113, 184 }, new byte[] { 106, 150, 70, 249, 234, 129, 117, 15, 126, 89, 154, 20, 243, 158, 217, 234, 146, 24, 204, 94, 164, 77, 219, 163, 238, 89, 75, 137, 151, 128, 227, 216, 18, 199, 203, 60, 245, 172, 246, 79, 117, 82, 16, 225, 121, 210, 151, 174, 117, 89, 116, 236, 167, 119, 31, 141, 227, 102, 122, 179, 213, 212, 252, 144 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4739e3fa-8030-499c-9eac-0f648722a33b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5204), null, new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5203) }
                });

            migrationBuilder.InsertData(
                table: "contacts",
                columns: new[] { "id", "address", "created_at", "deleted_at", "email", "name", "phone_number", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("139796f6-c463-48df-83e0-676251f2a2fd"), "4330 Doe Crossing Junction", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5588), null, "vwoodsford9@issuu.com", "Vasilis Woodsford", "8364764344", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5589), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("14610336-e056-443a-aef8-3785990f0c84"), "96 Summerview Drive", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5576), null, "klanphere3@upenn.edu", "Kev Lanphere", "7268834724", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5576), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("190aa137-ab44-43bd-8051-97d3f5cc2d97"), "54 Redwing Drive", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5590), null, "jorneblowa@youtube.com", "Jennee Orneblow", "9316403125", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5590), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("1dfd04ed-bb84-45e5-bca1-85109082d780"), "10 Calypso Center", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5585), null, "candrelli7@intel.com", "Ceil Andrelli", "1401949061", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5585), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("3ca980b0-0a5c-436d-8a1d-57fc0ae432db"), "615 Jenifer Alley", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5582), null, "ldunster5@marketwatch.com", "Laurette Dunster", "7678939672", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5582), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("66b478ba-6b5f-40ca-b702-c4c987be57c4"), "620 Lakeland Center", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5569), null, "rhavock0@gov.uk", "Randell Havock", "6765109874", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5569), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("949cc9c6-bbcd-4b7f-b3e1-f83ce8106116"), "17 Onsgard Avenue", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5574), null, "bmcging2@ihg.com", "Benjie McGing", "5025308916", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5574), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("a34fecda-ca26-4bd2-817e-2f5f848372e9"), "39494 Kartens Terrace", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5577), null, "sblanchard4@vinaora.com", "Sal Blanchard", "4904310249", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5578), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("adbe26da-b9e9-4397-a08f-fb55fa52beca"), "34 Eastwood Avenue", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5572), null, "smahood1@wufoo.com", "Sarine Mahood", "6977095403", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5572), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("c565453b-8a6a-4228-808a-fd880a69e6a6"), "4649 Swallow Trail", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5583), null, "tshew6@tripadvisor.com", "Tiphany Shew", "1457940882", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5584), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("f40f8dd6-cde3-4e1f-8aec-38c975fe0c07"), "11 Old Shore Terrace", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5587), null, "msivorn8@icq.com", "Margaretta Sivorn", "5311644464", new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5587), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") }
                });

            migrationBuilder.InsertData(
                table: "records",
                columns: new[] { "id", "created_at", "deleted_at", "description", "record_group", "recorded_at", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("026c1e95-237a-4032-bdee-d930d57dc92d"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5534), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5533), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5534), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("0a32c82e-6a2a-4c2a-97bd-6a24a1635570"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5365), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5364), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5365), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("130644fa-f07b-4597-ab12-eaa561b323d5"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5362), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5361), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5362), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("13c0e408-7231-4ac4-8368-645282655e71"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5447), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5446), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5447), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("221af0ae-469e-4a6a-a6c4-d10825abc8ab"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5456), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5455), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5456), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("24f44f19-df8f-487d-a4f3-287112fe4d18"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5402), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5402), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5402), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("2ae825c3-52dd-4475-acd6-4b86df2f62ed"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5513), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5512), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5513), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("2cfb2469-e6fd-4d65-98e4-452a8be39bd2"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5464), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5464), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5465), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("3009dac3-69e7-4a15-95ca-de29697cbb77"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5373), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5372), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5373), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("36218657-1c1c-4784-874c-32906046c523"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5358), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5358), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5359), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("3a1abe5d-2aa9-4029-a4fa-525320b9d182"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5316), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5315), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5316), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("4006c79d-6fb6-4107-a68f-7ee1996567e1"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5395), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5394), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5395), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("494581bd-c6e7-4f77-a3ef-d4063969ffc7"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5313), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5313), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5313), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("529c82ff-e104-4247-a0a6-10493b2eb829"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5301), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5300), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5301), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("59e9d463-8127-4709-b125-0de16ce0dda5"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5469), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5469), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5469), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("647567e3-75ec-4e98-a8a0-95114546f356"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5375), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5375), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5376), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("66e490f5-6455-4686-b3dc-4029beff0149"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5386), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5385), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5386), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("6f627c3d-13d7-4a1d-b70f-352eed59005b"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5520), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5520), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5521), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("71d480bf-8203-4ddf-a7d0-9358bb536706"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5510), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5510), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5510), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("76ea028b-76d3-43e8-a878-f7372ccb4634"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5450), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5450), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5451), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("8dd53680-ae87-49ae-a7ff-522309065bc8"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5462), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5461), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5462), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("9dfb521a-f691-4dd3-a19b-5165a0f9e31d"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5526), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5526), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5526), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("9e230e26-894d-413b-970f-ee5cd3fe4b2d"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5529), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5528), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5529), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("a5038fe2-d86e-4fe9-8717-521648c7c2ae"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5507), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5506), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5507), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("a5d505bc-03f1-4697-a9c8-447ccfa9c5ef"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5399), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5399), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5400), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("ae0cc123-f484-421f-bd6b-6c21af6cda2c"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5523), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5523), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5524), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("b1668541-9036-4905-b3ac-1eed7ab6c115"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5516), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5515), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5516), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("b2ff0829-a05a-4986-8e61-e1ac9e06cbc8"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5368), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5367), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5368), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("b537d3ac-09c9-4f00-8d0b-537ec398bc7f"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5310), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5309), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5310), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("b845117e-29e8-4e6e-9323-e7cf61b58ae9"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5459), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5458), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5459), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("c1de93d8-bc37-43e0-b7f0-25f8c753a4bf"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5293), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5282), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5293), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") }
                });

            migrationBuilder.InsertData(
                table: "records",
                columns: new[] { "id", "created_at", "deleted_at", "description", "record_group", "recorded_at", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("c399d5bb-8c00-4f49-a179-91f0042e351e"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5478), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5477), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5478), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("c82f7dc5-1046-47d4-a4a6-812477dfd2f3"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5472), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5471), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5472), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("cbebe4d6-1661-46e6-8a39-e1d2d0378a28"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5536), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5536), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5537), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") },
                    { new Guid("cf343d99-a81f-48aa-8e8d-110999f094d9"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5392), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5391), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5392), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("daf6c2d2-5da1-407d-a8f2-6c4d5cece308"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5381), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5380), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5381), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("db8bf679-08a0-4126-bcf2-50118a09b2e6"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5388), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5388), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5389), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("ec24ae22-9af7-42f0-8a8d-44ae0e861d97"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5307), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5306), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5307), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("f27baaba-059d-42b5-af71-524c8dc95b3a"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5378), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5378), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5378), new Guid("af546513-ee69-4cde-8849-ba88d4b93070") },
                    { new Guid("fd8365ce-2e45-4378-878d-047e3bd41c77"), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5475), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 16, 20, 58, 27, 850, DateTimeKind.Local).AddTicks(5475), new DateTime(2023, 7, 16, 13, 58, 27, 850, DateTimeKind.Utc).AddTicks(5475), new Guid("8719cba1-c077-4bfd-8912-e12c5df1e20a") }
                });

            migrationBuilder.InsertData(
                table: "ledgers",
                columns: new[] { "id", "account_id", "balance", "entry", "record_id" },
                values: new object[,]
                {
                    { new Guid("00e18ee6-c850-4acf-b72a-b1a252403f43"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 160000m, 1, new Guid("2cfb2469-e6fd-4d65-98e4-452a8be39bd2") },
                    { new Guid("039d86b9-d9d3-41ae-8b32-1ff7a25f2e57"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 150000m, 0, new Guid("59e9d463-8127-4709-b125-0de16ce0dda5") },
                    { new Guid("065270d5-a3d6-4296-aaac-2f0f9b4fae71"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 70000m, 0, new Guid("66e490f5-6455-4686-b3dc-4029beff0149") },
                    { new Guid("0f21238e-c580-4ee5-bb28-4aa64cc85583"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 60000m, 1, new Guid("ae0cc123-f484-421f-bd6b-6c21af6cda2c") },
                    { new Guid("1288ef9f-8711-406f-91d5-436f7cf1724d"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 110000m, 0, new Guid("a5038fe2-d86e-4fe9-8717-521648c7c2ae") },
                    { new Guid("129cc393-a960-424a-af9c-0b558d95d285"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 140000m, 1, new Guid("c82f7dc5-1046-47d4-a4a6-812477dfd2f3") },
                    { new Guid("12ebf6fa-b264-4c25-94f0-3e612843fd01"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 190000m, 0, new Guid("221af0ae-469e-4a6a-a6c4-d10825abc8ab") },
                    { new Guid("19779711-4343-4b33-a5a2-453e8fadbb8c"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 120000m, 1, new Guid("c399d5bb-8c00-4f49-a179-91f0042e351e") },
                    { new Guid("1993bb14-bdba-4bee-be52-e0503d381954"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 170000m, 0, new Guid("494581bd-c6e7-4f77-a3ef-d4063969ffc7") },
                    { new Guid("1fe80eb6-23b6-499f-a56e-5c62ac40980e"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 100000m, 1, new Guid("71d480bf-8203-4ddf-a7d0-9358bb536706") },
                    { new Guid("20bd01ea-a750-44fc-9dc9-e9d45e259c07"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 150000m, 0, new Guid("36218657-1c1c-4784-874c-32906046c523") },
                    { new Guid("24419a46-9c4b-4452-b861-a42089f020ec"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 200000m, 1, new Guid("76ea028b-76d3-43e8-a878-f7372ccb4634") },
                    { new Guid("30de2ad3-0db5-42fc-9b7b-4a68c8c0eb7f"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 30000m, 0, new Guid("026c1e95-237a-4032-bdee-d930d57dc92d") },
                    { new Guid("394c82db-a490-4b69-9a58-59e38f8a6b1d"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 90000m, 0, new Guid("f27baaba-059d-42b5-af71-524c8dc95b3a") },
                    { new Guid("3d4e33d6-6386-43ac-955e-a20b5574ac4d"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 90000m, 0, new Guid("2ae825c3-52dd-4475-acd6-4b86df2f62ed") },
                    { new Guid("44c33a15-e437-44aa-9779-040865d01e25"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 80000m, 1, new Guid("daf6c2d2-5da1-407d-a8f2-6c4d5cece308") },
                    { new Guid("474a4f8a-4c5a-45b6-9dcd-c4d775b93a98"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 100000m, 1, new Guid("647567e3-75ec-4e98-a8a0-95114546f356") },
                    { new Guid("4a97d899-a02f-4143-b073-61bb468e9c54"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 130000m, 0, new Guid("fd8365ce-2e45-4378-878d-047e3bd41c77") },
                    { new Guid("527b4ed0-7a32-459e-8b9b-06c907634275"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 110000m, 0, new Guid("3009dac3-69e7-4a15-95ca-de29697cbb77") },
                    { new Guid("55feb9cf-a204-4388-a025-d0899c1a8f5c"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 50000m, 0, new Guid("cf343d99-a81f-48aa-8e8d-110999f094d9") },
                    { new Guid("5ed83654-9175-4d24-bbae-0cacbe4c48a5"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 120000m, 1, new Guid("b2ff0829-a05a-4986-8e61-e1ac9e06cbc8") },
                    { new Guid("6d0ec5c6-d49a-4520-9fa5-dd598ee82f1f"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 160000m, 1, new Guid("3a1abe5d-2aa9-4029-a4fa-525320b9d182") },
                    { new Guid("6e72afb2-d38a-4a7c-a5da-067c690f3c9c"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 200000m, 1, new Guid("529c82ff-e104-4247-a0a6-10493b2eb829") },
                    { new Guid("7328079a-eee1-40bc-b55c-0d7d49a835a7"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 30000m, 0, new Guid("a5d505bc-03f1-4697-a9c8-447ccfa9c5ef") },
                    { new Guid("89c65e0e-a7d3-49cd-88e3-8e84be9cfeff"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 140000m, 1, new Guid("130644fa-f07b-4597-ab12-eaa561b323d5") },
                    { new Guid("8d3a5a15-b368-455f-aff1-2b274629244d"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 60000m, 1, new Guid("db8bf679-08a0-4126-bcf2-50118a09b2e6") },
                    { new Guid("91c6277f-91f1-4575-8a98-cffc4f815e6e"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 40000m, 1, new Guid("9e230e26-894d-413b-970f-ee5cd3fe4b2d") },
                    { new Guid("a0ac3af8-93b0-4270-ac9b-5de945b2b7ed"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 170000m, 0, new Guid("8dd53680-ae87-49ae-a7ff-522309065bc8") },
                    { new Guid("a6e896e3-99fd-4e9b-a043-1e925cd91812"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 70000m, 0, new Guid("6f627c3d-13d7-4a1d-b70f-352eed59005b") },
                    { new Guid("ad856580-f4dc-46a9-9efb-5931a2aecb0d"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 210000m, 0, new Guid("13c0e408-7231-4ac4-8368-645282655e71") },
                    { new Guid("afca9287-bf39-4f4e-a525-244aa796f5a9"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 190000m, 0, new Guid("ec24ae22-9af7-42f0-8a8d-44ae0e861d97") },
                    { new Guid("b1672e55-e212-4eca-8118-1749f58703a4"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 20000m, 1, new Guid("cbebe4d6-1661-46e6-8a39-e1d2d0378a28") },
                    { new Guid("b4b2a218-0502-4359-88f1-bd6f1e467a0c"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 40000m, 1, new Guid("4006c79d-6fb6-4107-a68f-7ee1996567e1") },
                    { new Guid("cf0326f5-37de-4ac2-9b1a-1008b93eba24"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 20000m, 1, new Guid("24f44f19-df8f-487d-a4f3-287112fe4d18") },
                    { new Guid("d0781fa1-5eee-4a17-bdc6-d975889a3d83"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 80000m, 1, new Guid("b1668541-9036-4905-b3ac-1eed7ab6c115") },
                    { new Guid("d9c4bdfb-32e0-4773-9434-845f549c8458"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 180000m, 1, new Guid("b845117e-29e8-4e6e-9323-e7cf61b58ae9") },
                    { new Guid("d9d4b824-3661-401c-bc3d-4b023569b875"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 50000m, 0, new Guid("9dfb521a-f691-4dd3-a19b-5165a0f9e31d") },
                    { new Guid("d9d97307-5aab-47ba-8a8b-c200ff26d39b"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 130000m, 0, new Guid("0a32c82e-6a2a-4c2a-97bd-6a24a1635570") },
                    { new Guid("e980c55a-6b6f-48b0-9371-61572e73c7f4"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 210000m, 0, new Guid("c1de93d8-bc37-43e0-b7f0-25f8c753a4bf") },
                    { new Guid("f257613a-742c-4b2d-9482-6bd62cdb08ba"), new Guid("d9aeda2d-9c6c-432e-b011-dfaec955bb14"), 180000m, 1, new Guid("b537d3ac-09c9-4f00-8d0b-537ec398bc7f") }
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
                name: "IX_payable_ledger_payable_id",
                table: "payable_ledger",
                column: "payable_id");

            migrationBuilder.CreateIndex(
                name: "IX_payable_ledger_record_id",
                table: "payable_ledger",
                column: "record_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payable_ledger_record_id_payable_id",
                table: "payable_ledger",
                columns: new[] { "record_id", "payable_id" });

            migrationBuilder.CreateIndex(
                name: "IX_payables_creditor_id",
                table: "payables",
                column: "creditor_id");

            migrationBuilder.CreateIndex(
                name: "IX_payables_record_id",
                table: "payables",
                column: "record_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payables_record_id_creditor_id",
                table: "payables",
                columns: new[] { "record_id", "creditor_id" });

            migrationBuilder.CreateIndex(
                name: "IX_receivable_ledger_receivable_id",
                table: "receivable_ledger",
                column: "receivable_id");

            migrationBuilder.CreateIndex(
                name: "IX_receivable_ledger_record_id",
                table: "receivable_ledger",
                column: "record_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_receivable_ledger_record_id_receivable_id",
                table: "receivable_ledger",
                columns: new[] { "record_id", "receivable_id" });

            migrationBuilder.CreateIndex(
                name: "IX_receivables_debtor_id",
                table: "receivables",
                column: "debtor_id");

            migrationBuilder.CreateIndex(
                name: "IX_receivables_record_id",
                table: "receivables",
                column: "record_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_receivables_record_id_debtor_id",
                table: "receivables",
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
                name: "payable_ledger");

            migrationBuilder.DropTable(
                name: "receivable_ledger");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "payables");

            migrationBuilder.DropTable(
                name: "receivables");

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
