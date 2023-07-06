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
                    account_code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
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
                name: "record_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_record_types", x => x.id);
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    record_type_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    recorded_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_records_record_types_record_type_id",
                        column: x => x.record_type_id,
                        principalTable: "record_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_records_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "journal_entries",
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
                    table.PrimaryKey("PK_journal_entries", x => x.id);
                    table.ForeignKey(
                        name: "FK_journal_entries_accounts_account_id",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_journal_entries_records_record_id",
                        column: x => x.record_id,
                        principalTable: "records",
                        principalColumn: "Id",
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
                        principalColumn: "Id",
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
                        principalColumn: "Id",
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
                        principalColumn: "Id",
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "id", "account_code", "account_group", "account_name", "created_at", "deleted_at", "updated_at" },
                values: new object[,]
                {
                    { new Guid("4c6226b0-93bc-4831-a12e-c5f432bafa25"), "21001", 2, "PAYABLE", new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7389), null, new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7389) },
                    { new Guid("5414b27b-c213-482a-b2f2-d10a8919ed05"), "11001", 0, "CASH", new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7385), null, new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7385) },
                    { new Guid("74784147-9754-454d-adf1-61643bf89c28"), "11005", 0, "RECEIVABLE", new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7387), null, new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7388) }
                });

            migrationBuilder.InsertData(
                table: "record_types",
                columns: new[] { "id", "created_at", "deleted_at", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("2d441482-5493-447e-ba2f-a4f384e6e9a5"), new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7323), null, "NEW_PAYABLE", new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7323) },
                    { new Guid("4135ce72-e364-4890-ac89-3ba518bf767c"), new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7321), null, "NEW_RECEIVABLE", new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7322) },
                    { new Guid("4ceca343-e8cf-4219-a05e-91dc391a4b7e"), new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7320), null, "CASH_OUT", new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7320) },
                    { new Guid("96e5cce4-f0f7-434a-ae7d-61288934ce56"), new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7317), null, "CASH_IN", new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7317) },
                    { new Guid("ae05e464-7b6f-4000-9a3b-71704493c49e"), new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7324), null, "RECEIVABLE_PAYMENT", new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7324) },
                    { new Guid("e614a5ad-cd91-4b81-b96e-f58becb2c361"), new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7336), null, "PAYABLE_PAYMENT", new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7337) }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_at", "deleted_at", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("1a84bfc8-a9f8-49b3-8386-5ba4c8b40281"), new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7153), null, "ROLE_USER", new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7153) },
                    { new Guid("799b9acb-af1c-4fd6-95dd-8a932bb50dba"), new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7117), null, "ROLE_ADMIN", new DateTime(2023, 7, 6, 12, 21, 21, 718, DateTimeKind.Utc).AddTicks(7120) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_journal_entries_account_id",
                table: "journal_entries",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_journal_entries_record_id",
                table: "journal_entries",
                column: "record_id");

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
                name: "IX_payables_debtor_id",
                table: "payables",
                column: "debtor_id");

            migrationBuilder.CreateIndex(
                name: "IX_payables_record_id",
                table: "payables",
                column: "record_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_receivable_ledger_ReceivableId",
                table: "receivable_ledger",
                column: "ReceivableId",
                unique: true);

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
                name: "IX_records_record_type_id",
                table: "records",
                column: "record_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_records_UserId",
                table: "records",
                column: "UserId");

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
                name: "journal_entries");

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
                name: "record_types");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
