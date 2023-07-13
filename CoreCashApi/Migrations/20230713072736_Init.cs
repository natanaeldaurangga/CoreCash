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
                name: "contacts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contacts", x => x.id);
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
                    debtor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payables", x => x.id);
                    table.ForeignKey(
                        name: "FK_payables_contacts_debtor_id",
                        column: x => x.debtor_id,
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
                    creditor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receivables", x => x.id);
                    table.ForeignKey(
                        name: "FK_receivables_contacts_creditor_id",
                        column: x => x.creditor_id,
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payable_ledger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payable_ledger_payables_PayableId",
                        column: x => x.PayableId,
                        principalTable: "payables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payable_ledger_records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "receivable_ledger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceivableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receivable_ledger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_receivable_ledger_receivables_ReceivableId",
                        column: x => x.ReceivableId,
                        principalTable: "receivables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_receivable_ledger_records_ReceivableId",
                        column: x => x.ReceivableId,
                        principalTable: "records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "id", "account_code", "account_group", "account_name", "created_at", "deleted_at", "updated_at" },
                values: new object[,]
                {
                    { new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 11001, 11, "CASH", new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2493), null, new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2494) },
                    { new Guid("3e3aa53e-ca6b-41fa-801d-559f4320fc79"), 11005, 11, "RECEIVABLE", new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2503), null, new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2504) },
                    { new Guid("d1113193-b495-413d-a710-493e463ab3a3"), 21001, 21, "PAYABLE", new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2506), null, new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2506) }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_at", "deleted_at", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("2f9ab61a-c93c-42ce-addf-100426d585a7"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(1408), null, "ROLE_USER", new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(1408) },
                    { new Guid("a21496c5-cb61-4821-ab99-e5173fbf6898"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(1368), null, "ROLE_ADMIN", new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(1371) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "deleted_at", "email", "full_name", "password_hash", "password_salt", "profile_picture", "refresh_token", "reset_password_token", "reset_token_expires", "role_id", "token_expires", "updated_at", "verification_token", "verified_at" },
                values: new object[,]
                {
                    { new Guid("47a0904d-c49e-40fc-9090-4b0718c6fa57"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2399), null, "admin1@example.com", "Admin Satu", new byte[] { 165, 92, 223, 219, 34, 129, 153, 220, 56, 153, 117, 169, 33, 39, 71, 230, 115, 136, 185, 187, 40, 151, 150, 145, 250, 123, 146, 147, 55, 24, 35, 25 }, new byte[] { 71, 65, 120, 139, 188, 117, 54, 250, 48, 20, 67, 255, 181, 111, 28, 143, 250, 36, 111, 194, 117, 8, 173, 225, 137, 171, 43, 25, 11, 204, 67, 42, 99, 179, 203, 237, 219, 244, 214, 16, 49, 104, 70, 160, 4, 202, 17, 252, 228, 136, 253, 245, 149, 118, 200, 181, 103, 27, 240, 29, 111, 83, 20, 186 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a21496c5-cb61-4821-ab99-e5173fbf6898"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2399), null, new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2398) },
                    { new Guid("6223ba12-2632-4398-a238-c101527076ce"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2408), null, "user2@example.com", "User Dua", new byte[] { 165, 92, 223, 219, 34, 129, 153, 220, 56, 153, 117, 169, 33, 39, 71, 230, 115, 136, 185, 187, 40, 151, 150, 145, 250, 123, 146, 147, 55, 24, 35, 25 }, new byte[] { 71, 65, 120, 139, 188, 117, 54, 250, 48, 20, 67, 255, 181, 111, 28, 143, 250, 36, 111, 194, 117, 8, 173, 225, 137, 171, 43, 25, 11, 204, 67, 42, 99, 179, 203, 237, 219, 244, 214, 16, 49, 104, 70, 160, 4, 202, 17, 252, 228, 136, 253, 245, 149, 118, 200, 181, 103, 27, 240, 29, 111, 83, 20, 186 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2f9ab61a-c93c-42ce-addf-100426d585a7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2408), null, new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2408) },
                    { new Guid("a24439a4-d594-4284-b9a5-13b4d143f3cc"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2402), null, "admin2@example.com", "Admin Dua", new byte[] { 165, 92, 223, 219, 34, 129, 153, 220, 56, 153, 117, 169, 33, 39, 71, 230, 115, 136, 185, 187, 40, 151, 150, 145, 250, 123, 146, 147, 55, 24, 35, 25 }, new byte[] { 71, 65, 120, 139, 188, 117, 54, 250, 48, 20, 67, 255, 181, 111, 28, 143, 250, 36, 111, 194, 117, 8, 173, 225, 137, 171, 43, 25, 11, 204, 67, 42, 99, 179, 203, 237, 219, 244, 214, 16, 49, 104, 70, 160, 4, 202, 17, 252, 228, 136, 253, 245, 149, 118, 200, 181, 103, 27, 240, 29, 111, 83, 20, 186 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a21496c5-cb61-4821-ab99-e5173fbf6898"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2403), null, new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2402) },
                    { new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2405), null, "user1@example.com", "User Satu", new byte[] { 165, 92, 223, 219, 34, 129, 153, 220, 56, 153, 117, 169, 33, 39, 71, 230, 115, 136, 185, 187, 40, 151, 150, 145, 250, 123, 146, 147, 55, 24, 35, 25 }, new byte[] { 71, 65, 120, 139, 188, 117, 54, 250, 48, 20, 67, 255, 181, 111, 28, 143, 250, 36, 111, 194, 117, 8, 173, 225, 137, 171, 43, 25, 11, 204, 67, 42, 99, 179, 203, 237, 219, 244, 214, 16, 49, 104, 70, 160, 4, 202, 17, 252, 228, 136, 253, 245, 149, 118, 200, 181, 103, 27, 240, 29, 111, 83, 20, 186 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2f9ab61a-c93c-42ce-addf-100426d585a7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2406), null, new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2405) }
                });

            migrationBuilder.InsertData(
                table: "records",
                columns: new[] { "id", "created_at", "deleted_at", "description", "record_group", "recorded_at", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("0223711b-98b1-4db8-b740-21e733cade95"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2725), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2724), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2725), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("15488259-5e71-4e96-b78d-f0d045c40056"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2735), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2734), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2735), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("1937e013-4399-4df5-9512-62b7f6f0efb8"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2753), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2752), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2753), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("195c7e97-c6b1-4040-a5d2-d87eeaf68e15"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2719), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2719), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2720), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("19cd1220-78ec-44cc-8c53-a26b477f7a11"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2727), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2727), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2728), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("27ccd3f9-5822-43c2-904e-358f1a0d1334"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2707), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2706), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2707), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("2822cec6-6152-48f2-b5be-c5a62034b5b5"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2745), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2744), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2745), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("43044eed-5b01-4d11-bad5-9c4dfd7d8098"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2740), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2740), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2740), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("4ff3b87a-9a4c-440f-a17d-4bc0a3a684ac"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2618), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2618), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2619), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("59524c5e-0754-4a11-bab6-91ee8b5163e8"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2758), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2757), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2758), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("622b144a-b4a0-43c8-97dd-ba38ece002c3"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2611), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2611), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2611), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("6a994aac-ead4-4772-afa3-e30a921eef66"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2624), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2623), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2624), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("6ddd8b25-206c-40d1-a0ab-033f71e3d035"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2616), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2615), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2616), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("6e594604-8c57-4d10-b0ff-a06c06adae37"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2748), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2747), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2748), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("74f5f7b1-5acd-4f7f-b51a-36f9469babe4"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2621), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2621), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2621), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("7675f019-49ca-45b7-9153-dcd56f3e19d7"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2712), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2712), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2712), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("76ec51df-5c3a-4cc2-9dde-cf2a7198bbe9"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2710), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2709), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2710), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("7a6ed797-2afa-4b40-87af-3c8c70f9eb13"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2608), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2608), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2609), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("7bbff028-977f-48f8-8c30-a96db1a15387"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2582), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2581), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2582), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("7dc9110b-53f2-444b-9e2d-7b2715ac736c"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2696), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2695), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2696), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("82215da8-dd5c-4021-bf8e-beec7393131f"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2603), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2602), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2603), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("8cbf2a1a-16ca-472b-a793-2dec1fa191cd"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2579), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2578), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2579), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("8e5b1f08-dbec-4741-a3f8-641b0b08ac4f"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2575), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2575), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2576), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("93e31782-09ba-40d2-a37e-9a080c8eb090"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2750), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2750), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2751), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("a517378b-2526-440f-b741-49dbb8f8230b"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2595), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2595), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2595), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("a76dfb35-138b-43db-a08a-48e4562c15ec"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2558), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2545), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2558), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("aa776f14-bd2c-4fcd-9622-74fc49eb336d"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2566), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2566), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2567), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("b0f3ef1e-ca2a-4b83-8d7a-a826793a77b9"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2598), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2598), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2598), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("b2b47257-a707-4474-9b87-1a25567ae19c"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2715), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2714), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2715), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("b38feed3-6ac3-4f5f-ad86-7f260c2d37ff"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2702), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2702), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2702), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("b5a61714-5f69-4405-90da-8f76e1f792ac"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2592), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2592), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2593), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("c0bdf84b-9158-49ce-b07c-e4455ef9f6e1"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2569), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2569), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2570), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("c223d8f9-d3ae-458b-8931-e856d6843dba"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2732), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2732), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2732), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("c3bfb73e-cbaf-4565-a502-b7e77a8e6d59"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2699), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2698), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2700), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("c67ac942-f57c-4b47-a966-d927b29bf0ca"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2722), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2722), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2722), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("cac816c5-85e4-45e6-9bb6-4e94c5dfab9b"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2737), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2737), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2738), new Guid("6223ba12-2632-4398-a238-c101527076ce") },
                    { new Guid("d5b0945f-f659-42a1-accd-0da13fdbb39f"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2585), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2584), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2585), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("df33c6ac-a937-41d7-a0bb-ede7ff728691"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2628), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2628), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2629), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("ef95c3fa-9434-4219-ad71-d86b6113e222"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2590), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2589), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2590), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") },
                    { new Guid("f0afa4b8-b4bc-4fe2-ad9c-32fa47cb9804"), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2605), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 14, 27, 36, 360, DateTimeKind.Local).AddTicks(2605), new DateTime(2023, 7, 13, 7, 27, 36, 360, DateTimeKind.Utc).AddTicks(2606), new Guid("b08ab9f5-41ac-4253-8b02-e2eeaa1ef45e") }
                });

            migrationBuilder.InsertData(
                table: "ledgers",
                columns: new[] { "id", "account_id", "balance", "entry", "record_id" },
                values: new object[,]
                {
                    { new Guid("0574e523-b45e-4d84-9e3e-6b61957cbdc6"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 190000m, 0, new Guid("c0bdf84b-9158-49ce-b07c-e4455ef9f6e1") },
                    { new Guid("08157dfc-ebdd-466f-9e7d-0d22e0cf7ee2"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 210000m, 0, new Guid("7dc9110b-53f2-444b-9e2d-7b2715ac736c") },
                    { new Guid("0c7266cb-1b7a-4cf3-b2c8-d0da61560d33"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 50000m, 0, new Guid("6e594604-8c57-4d10-b0ff-a06c06adae37") },
                    { new Guid("17582e87-dba2-4c23-8a89-fa8216f41aa5"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 170000m, 0, new Guid("8cbf2a1a-16ca-472b-a793-2dec1fa191cd") },
                    { new Guid("18890b68-4da8-4f52-a469-994ada47f318"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 180000m, 1, new Guid("27ccd3f9-5822-43c2-904e-358f1a0d1334") },
                    { new Guid("2a75a986-73bf-47a1-adcf-fec95d24cd84"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 150000m, 0, new Guid("d5b0945f-f659-42a1-accd-0da13fdbb39f") },
                    { new Guid("2c814858-827a-4d7a-bd29-7951142ac2e2"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 120000m, 1, new Guid("a517378b-2526-440f-b741-49dbb8f8230b") },
                    { new Guid("2e84b50d-add5-4b04-b267-f64a9b9a2c42"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 180000m, 1, new Guid("8e5b1f08-dbec-4741-a3f8-641b0b08ac4f") },
                    { new Guid("2fd4ea29-b0e7-4172-954d-2c2f1751a053"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 20000m, 1, new Guid("df33c6ac-a937-41d7-a0bb-ede7ff728691") },
                    { new Guid("32ee4eaf-48f2-41b6-8ce6-ba3fa33e94d1"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 60000m, 1, new Guid("2822cec6-6152-48f2-b5be-c5a62034b5b5") },
                    { new Guid("36a5afaa-b923-4bff-8b77-a3dc47123275"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 20000m, 1, new Guid("59524c5e-0754-4a11-bab6-91ee8b5163e8") },
                    { new Guid("3ca24a3f-2336-4e00-ac1d-3fd2a7f5173f"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 80000m, 1, new Guid("7a6ed797-2afa-4b40-87af-3c8c70f9eb13") },
                    { new Guid("4638978d-b116-4212-ac1a-44882a4890ce"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 200000m, 1, new Guid("c3bfb73e-cbaf-4565-a502-b7e77a8e6d59") },
                    { new Guid("47d076d8-f90c-4373-ae23-d22965e26df3"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 30000m, 0, new Guid("6a994aac-ead4-4772-afa3-e30a921eef66") },
                    { new Guid("59a13b18-3cdf-46a0-9182-ab260bc28abd"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 120000m, 1, new Guid("0223711b-98b1-4db8-b740-21e733cade95") },
                    { new Guid("5c67f16e-0b5e-404e-bc99-2b9353c1b225"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 200000m, 1, new Guid("aa776f14-bd2c-4fcd-9622-74fc49eb336d") },
                    { new Guid("68817f88-2fbc-46b2-8194-6e361c15a978"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 100000m, 1, new Guid("82215da8-dd5c-4021-bf8e-beec7393131f") },
                    { new Guid("713ff017-e383-4cf7-977b-d83cc1026f53"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 70000m, 0, new Guid("43044eed-5b01-4d11-bad5-9c4dfd7d8098") },
                    { new Guid("732544e3-cac5-426b-80dd-acdb8fc1efd8"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 170000m, 0, new Guid("76ec51df-5c3a-4cc2-9dde-cf2a7198bbe9") },
                    { new Guid("7eccd373-e4f1-4ff8-85eb-aed091ca1bba"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 80000m, 1, new Guid("cac816c5-85e4-45e6-9bb6-4e94c5dfab9b") },
                    { new Guid("851d6406-4c0f-4f51-9b07-3416ddf4dcc5"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 150000m, 0, new Guid("b2b47257-a707-4474-9b87-1a25567ae19c") },
                    { new Guid("8f14bc63-77d9-4b7c-9046-9a6693ccba65"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 190000m, 0, new Guid("b38feed3-6ac3-4f5f-ad86-7f260c2d37ff") },
                    { new Guid("923a4ae1-13a3-4418-b6cd-1b716c61799d"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 160000m, 1, new Guid("7675f019-49ca-45b7-9153-dcd56f3e19d7") },
                    { new Guid("9bbdd8de-d856-437c-8327-dc198afad4f3"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 130000m, 0, new Guid("c67ac942-f57c-4b47-a966-d927b29bf0ca") },
                    { new Guid("a70e347b-7c11-4976-970c-9109fc5a1750"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 160000m, 1, new Guid("7bbff028-977f-48f8-8c30-a96db1a15387") },
                    { new Guid("ad81cc32-d118-4d0f-85d7-2edaf3137f87"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 90000m, 0, new Guid("15488259-5e71-4e96-b78d-f0d045c40056") },
                    { new Guid("ae16c732-01fa-4ced-8fd0-f97600f89903"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 140000m, 1, new Guid("ef95c3fa-9434-4219-ad71-d86b6113e222") },
                    { new Guid("baaddfcb-6de5-405d-8820-24fb4a497091"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 140000m, 1, new Guid("195c7e97-c6b1-4040-a5d2-d87eeaf68e15") },
                    { new Guid("bbd7d597-d00d-47fa-acb4-31c381fab703"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 40000m, 1, new Guid("74f5f7b1-5acd-4f7f-b51a-36f9469babe4") },
                    { new Guid("bf173ed7-1a93-435a-b09e-4ccd7b2aa749"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 60000m, 1, new Guid("6ddd8b25-206c-40d1-a0ab-033f71e3d035") },
                    { new Guid("bfec407e-df4b-49d5-8a33-cb51d1a106d9"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 130000m, 0, new Guid("b5a61714-5f69-4405-90da-8f76e1f792ac") },
                    { new Guid("cb12be2c-3319-4155-a1c5-74a6abf7103a"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 50000m, 0, new Guid("4ff3b87a-9a4c-440f-a17d-4bc0a3a684ac") },
                    { new Guid("d48bfd33-faaa-47dd-8d89-ad16ca51c727"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 110000m, 0, new Guid("19cd1220-78ec-44cc-8c53-a26b477f7a11") },
                    { new Guid("da65935a-7dc7-40ca-9196-c088d2682d57"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 70000m, 0, new Guid("622b144a-b4a0-43c8-97dd-ba38ece002c3") },
                    { new Guid("e17b440c-4b50-4cc0-bc52-2e7f7e995acc"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 100000m, 1, new Guid("c223d8f9-d3ae-458b-8931-e856d6843dba") },
                    { new Guid("e98d621a-111d-437d-9b15-02834b2f4e46"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 40000m, 1, new Guid("93e31782-09ba-40d2-a37e-9a080c8eb090") },
                    { new Guid("eb91d20f-6047-4cc5-97d5-0fb449aa72b7"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 30000m, 0, new Guid("1937e013-4399-4df5-9512-62b7f6f0efb8") },
                    { new Guid("f1c71b13-345d-4f9a-a214-fd1729ea000e"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 110000m, 0, new Guid("b0f3ef1e-ca2a-4b83-8d7a-a826793a77b9") },
                    { new Guid("fb4c0b5e-8ce0-446c-9a47-acfcd2439d95"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 210000m, 0, new Guid("a76dfb35-138b-43db-a08a-48e4562c15ec") },
                    { new Guid("ff4e9757-83dd-4e8e-a03a-de9133e50db8"), new Guid("0352c056-b696-47ed-8a89-55cf2110dc78"), 90000m, 0, new Guid("f0afa4b8-b4bc-4fe2-ad9c-32fa47cb9804") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_contacts_name",
                table: "contacts",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_ledgers_account_id",
                table: "ledgers",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_ledgers_record_id_account_id",
                table: "ledgers",
                columns: new[] { "record_id", "account_id" });

            migrationBuilder.CreateIndex(
                name: "IX_payable_ledger_PayableId",
                table: "payable_ledger",
                column: "PayableId");

            migrationBuilder.CreateIndex(
                name: "IX_payable_ledger_RecordId",
                table: "payable_ledger",
                column: "RecordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payable_ledger_RecordId_PayableId",
                table: "payable_ledger",
                columns: new[] { "RecordId", "PayableId" });

            migrationBuilder.CreateIndex(
                name: "IX_payables_debtor_id",
                table: "payables",
                column: "debtor_id");

            migrationBuilder.CreateIndex(
                name: "IX_payables_record_id",
                table: "payables",
                column: "record_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payables_record_id_debtor_id",
                table: "payables",
                columns: new[] { "record_id", "debtor_id" });

            migrationBuilder.CreateIndex(
                name: "IX_receivable_ledger_ReceivableId",
                table: "receivable_ledger",
                column: "ReceivableId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_receivable_ledger_RecordId_ReceivableId",
                table: "receivable_ledger",
                columns: new[] { "RecordId", "ReceivableId" });

            migrationBuilder.CreateIndex(
                name: "IX_receivables_creditor_id",
                table: "receivables",
                column: "creditor_id");

            migrationBuilder.CreateIndex(
                name: "IX_receivables_record_id",
                table: "receivables",
                column: "record_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_receivables_record_id_creditor_id",
                table: "receivables",
                columns: new[] { "record_id", "creditor_id" });

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
