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
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
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

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "id", "account_code", "account_group", "account_name", "created_at", "deleted_at", "updated_at" },
                values: new object[,]
                {
                    { new Guid("60414049-ab95-4e56-bc58-216d58bad441"), "21001", 2, "PAYABLE", new DateTime(2023, 7, 5, 17, 0, 14, 902, DateTimeKind.Utc).AddTicks(3196), null, new DateTime(2023, 7, 5, 17, 0, 14, 902, DateTimeKind.Utc).AddTicks(3196) },
                    { new Guid("8e7d2572-0de9-43bb-95cf-fcdac665ba46"), "11005", 0, "RECEIVABLE", new DateTime(2023, 7, 5, 17, 0, 14, 902, DateTimeKind.Utc).AddTicks(3194), null, new DateTime(2023, 7, 5, 17, 0, 14, 902, DateTimeKind.Utc).AddTicks(3194) },
                    { new Guid("eeb01fad-c1d0-4371-adb0-ec6a0cba78ce"), "11001", 0, "CASH", new DateTime(2023, 7, 5, 17, 0, 14, 902, DateTimeKind.Utc).AddTicks(3191), null, new DateTime(2023, 7, 5, 17, 0, 14, 902, DateTimeKind.Utc).AddTicks(3191) }
                });

            migrationBuilder.InsertData(
                table: "record_types",
                columns: new[] { "id", "created_at", "deleted_at", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("4a79784f-303a-4b4b-9ab9-589da9856bd0"), new DateTime(2023, 7, 5, 17, 0, 14, 902, DateTimeKind.Utc).AddTicks(3067), null, "PAYABLE_PAYMENT", new DateTime(2023, 7, 5, 17, 0, 14, 902, DateTimeKind.Utc).AddTicks(3068) },
                    { new Guid("514f33e5-fa62-401b-8400-c18ab94fd1d9"), new DateTime(2023, 7, 5, 17, 0, 14, 902, DateTimeKind.Utc).AddTicks(3001), null, "CASH_IN", new DateTime(2023, 7, 5, 17, 0, 14, 902, DateTimeKind.Utc).AddTicks(3003) },
                    { new Guid("6286b7f2-3a1d-4c04-95c5-fe5106901da0"), new DateTime(2023, 7, 5, 17, 0, 14, 902, DateTimeKind.Utc).AddTicks(3062), null, "CASH_OUT", new DateTime(2023, 7, 5, 17, 0, 14, 902, DateTimeKind.Utc).AddTicks(3062) },
                    { new Guid("8268ebda-bc9d-4216-abdb-2b24de9d8d39"), new DateTime(2023, 7, 5, 17, 0, 14, 902, DateTimeKind.Utc).AddTicks(3066), null, "RECEIVABLE_PAYMENT", new DateTime(2023, 7, 5, 17, 0, 14, 902, DateTimeKind.Utc).AddTicks(3066) },
                    { new Guid("ef4f2a33-c881-4f06-84a9-4727ca9fc8d7"), new DateTime(2023, 7, 5, 17, 0, 14, 902, DateTimeKind.Utc).AddTicks(3065), null, "NEW_PAYABLE", new DateTime(2023, 7, 5, 17, 0, 14, 902, DateTimeKind.Utc).AddTicks(3065) },
                    { new Guid("fbf34de6-3331-492f-bf6d-38622dea059c"), new DateTime(2023, 7, 5, 17, 0, 14, 902, DateTimeKind.Utc).AddTicks(3064), null, "NEW_RECEIVABLE", new DateTime(2023, 7, 5, 17, 0, 14, 902, DateTimeKind.Utc).AddTicks(3064) }
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
                name: "IX_payables_debtor_id",
                table: "payables",
                column: "debtor_id");

            migrationBuilder.CreateIndex(
                name: "IX_payables_record_id",
                table: "payables",
                column: "record_id");

            migrationBuilder.CreateIndex(
                name: "IX_receivables_creditor_id",
                table: "receivables",
                column: "creditor_id");

            migrationBuilder.CreateIndex(
                name: "IX_receivables_record_id",
                table: "receivables",
                column: "record_id");

            migrationBuilder.CreateIndex(
                name: "IX_records_record_type_id",
                table: "records",
                column: "record_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_records_UserId",
                table: "records",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
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
                name: "payables");

            migrationBuilder.DropTable(
                name: "receivables");

            migrationBuilder.DropTable(
                name: "accounts");

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
