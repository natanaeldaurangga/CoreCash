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
                    { new Guid("48f295a7-fd6d-42b4-813f-456d935d03cd"), 11001, 11, "CASH", new DateTime(2023, 7, 13, 5, 45, 46, 668, DateTimeKind.Utc).AddTicks(9933), null, new DateTime(2023, 7, 13, 5, 45, 46, 668, DateTimeKind.Utc).AddTicks(9934) },
                    { new Guid("4c1c39f9-1418-4419-98c7-79377a12f93b"), 21001, 21, "PAYABLE", new DateTime(2023, 7, 13, 5, 45, 46, 668, DateTimeKind.Utc).AddTicks(9937), null, new DateTime(2023, 7, 13, 5, 45, 46, 668, DateTimeKind.Utc).AddTicks(9938) },
                    { new Guid("a269c71f-324e-467e-b97e-3f85891dc402"), 11005, 11, "RECEIVABLE", new DateTime(2023, 7, 13, 5, 45, 46, 668, DateTimeKind.Utc).AddTicks(9936), null, new DateTime(2023, 7, 13, 5, 45, 46, 668, DateTimeKind.Utc).AddTicks(9936) }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_at", "deleted_at", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("4385ecc9-32af-4b73-ab43-a40bae8676de"), new DateTime(2023, 7, 13, 5, 45, 46, 668, DateTimeKind.Utc).AddTicks(8983), null, "ROLE_ADMIN", new DateTime(2023, 7, 13, 5, 45, 46, 668, DateTimeKind.Utc).AddTicks(8986) },
                    { new Guid("e85e271a-d8c6-4fc2-84a7-7c5e03e9be8e"), new DateTime(2023, 7, 13, 5, 45, 46, 668, DateTimeKind.Utc).AddTicks(9016), null, "ROLE_USER", new DateTime(2023, 7, 13, 5, 45, 46, 668, DateTimeKind.Utc).AddTicks(9016) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "deleted_at", "email", "full_name", "password_hash", "password_salt", "profile_picture", "refresh_token", "reset_password_token", "reset_token_expires", "role_id", "token_expires", "updated_at", "verification_token", "verified_at" },
                values: new object[,]
                {
                    { new Guid("53255e0f-be7b-4b17-9e9f-3d433ffec4f0"), new DateTime(2023, 7, 13, 5, 45, 46, 668, DateTimeKind.Utc).AddTicks(9890), null, "user2@example.com", "User Dua", new byte[] { 157, 188, 161, 154, 103, 178, 22, 19, 172, 62, 44, 244, 183, 20, 96, 99, 191, 21, 121, 121, 91, 47, 61, 119, 91, 228, 110, 233, 68, 33, 232, 85 }, new byte[] { 21, 138, 43, 111, 250, 170, 62, 177, 163, 214, 89, 137, 170, 125, 11, 253, 57, 71, 197, 106, 115, 77, 80, 200, 22, 92, 58, 150, 2, 37, 25, 250, 58, 205, 200, 219, 21, 110, 106, 126, 166, 152, 41, 22, 89, 11, 211, 85, 74, 25, 99, 77, 112, 202, 86, 250, 123, 211, 161, 94, 134, 238, 254, 238 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e85e271a-d8c6-4fc2-84a7-7c5e03e9be8e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 13, 5, 45, 46, 668, DateTimeKind.Utc).AddTicks(9890), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("67620865-baca-4a53-ac33-e5796c6c4986"), new DateTime(2023, 7, 13, 5, 45, 46, 668, DateTimeKind.Utc).AddTicks(9888), null, "user1@example.com", "User Satu", new byte[] { 157, 188, 161, 154, 103, 178, 22, 19, 172, 62, 44, 244, 183, 20, 96, 99, 191, 21, 121, 121, 91, 47, 61, 119, 91, 228, 110, 233, 68, 33, 232, 85 }, new byte[] { 21, 138, 43, 111, 250, 170, 62, 177, 163, 214, 89, 137, 170, 125, 11, 253, 57, 71, 197, 106, 115, 77, 80, 200, 22, 92, 58, 150, 2, 37, 25, 250, 58, 205, 200, 219, 21, 110, 106, 126, 166, 152, 41, 22, 89, 11, 211, 85, 74, 25, 99, 77, 112, 202, 86, 250, 123, 211, 161, 94, 134, 238, 254, 238 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e85e271a-d8c6-4fc2-84a7-7c5e03e9be8e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 13, 5, 45, 46, 668, DateTimeKind.Utc).AddTicks(9888), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("6bbfc943-9c22-4b8d-a7f0-9acbde1c47e9"), new DateTime(2023, 7, 13, 5, 45, 46, 668, DateTimeKind.Utc).AddTicks(9873), null, "admin1@example.com", "Admin Satu", new byte[] { 157, 188, 161, 154, 103, 178, 22, 19, 172, 62, 44, 244, 183, 20, 96, 99, 191, 21, 121, 121, 91, 47, 61, 119, 91, 228, 110, 233, 68, 33, 232, 85 }, new byte[] { 21, 138, 43, 111, 250, 170, 62, 177, 163, 214, 89, 137, 170, 125, 11, 253, 57, 71, 197, 106, 115, 77, 80, 200, 22, 92, 58, 150, 2, 37, 25, 250, 58, 205, 200, 219, 21, 110, 106, 126, 166, 152, 41, 22, 89, 11, 211, 85, 74, 25, 99, 77, 112, 202, 86, 250, 123, 211, 161, 94, 134, 238, 254, 238 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4385ecc9-32af-4b73-ab43-a40bae8676de"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 13, 5, 45, 46, 668, DateTimeKind.Utc).AddTicks(9873), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("cde13492-62dd-420d-a97b-559d7fe354a3"), new DateTime(2023, 7, 13, 5, 45, 46, 668, DateTimeKind.Utc).AddTicks(9885), null, "admin2@example.com", "Admin Dua", new byte[] { 157, 188, 161, 154, 103, 178, 22, 19, 172, 62, 44, 244, 183, 20, 96, 99, 191, 21, 121, 121, 91, 47, 61, 119, 91, 228, 110, 233, 68, 33, 232, 85 }, new byte[] { 21, 138, 43, 111, 250, 170, 62, 177, 163, 214, 89, 137, 170, 125, 11, 253, 57, 71, 197, 106, 115, 77, 80, 200, 22, 92, 58, 150, 2, 37, 25, 250, 58, 205, 200, 219, 21, 110, 106, 126, 166, 152, 41, 22, 89, 11, 211, 85, 74, 25, 99, 77, 112, 202, 86, 250, 123, 211, 161, 94, 134, 238, 254, 238 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4385ecc9-32af-4b73-ab43-a40bae8676de"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 13, 5, 45, 46, 668, DateTimeKind.Utc).AddTicks(9885), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
