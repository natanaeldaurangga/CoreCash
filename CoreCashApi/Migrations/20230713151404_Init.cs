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
                        onDelete: ReferentialAction.Cascade);
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
                    { new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 11001, 11, "CASH", new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7522), null, new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7522) },
                    { new Guid("a4951a47-3c29-492a-a217-39141ea46bd5"), 11005, 11, "RECEIVABLE", new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7525), null, new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7525) },
                    { new Guid("c1713ba1-74c2-4161-b12d-25640f581c52"), 21001, 21, "PAYABLE", new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7526), null, new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7527) }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_at", "deleted_at", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("07b959e6-02a9-4d51-92af-23acbbb8fa84"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(6309), null, "ROLE_USER", new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(6309) },
                    { new Guid("795b1227-89e3-4686-a9fa-4ca0a8646be5"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(6266), null, "ROLE_ADMIN", new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(6270) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "deleted_at", "email", "full_name", "password_hash", "password_salt", "profile_picture", "refresh_token", "reset_password_token", "reset_token_expires", "role_id", "token_expires", "updated_at", "verification_token", "verified_at" },
                values: new object[,]
                {
                    { new Guid("2675e3b9-1af8-4224-8efd-3c49042e27e0"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7477), null, "admin1@example.com", "Admin Satu", new byte[] { 104, 181, 250, 19, 18, 216, 231, 68, 160, 250, 43, 181, 154, 103, 225, 152, 67, 161, 217, 176, 157, 148, 18, 148, 49, 121, 0, 49, 109, 158, 98, 50 }, new byte[] { 158, 153, 67, 146, 58, 8, 219, 37, 186, 245, 252, 165, 52, 105, 36, 75, 163, 221, 222, 151, 200, 130, 57, 91, 9, 239, 199, 159, 106, 255, 228, 193, 31, 51, 19, 198, 93, 36, 177, 91, 46, 178, 213, 181, 120, 140, 72, 7, 218, 188, 36, 0, 137, 99, 220, 211, 66, 185, 77, 200, 238, 10, 107, 56 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("795b1227-89e3-4686-a9fa-4ca0a8646be5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7479), null, new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7477) },
                    { new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7485), null, "user1@example.com", "User Satu", new byte[] { 104, 181, 250, 19, 18, 216, 231, 68, 160, 250, 43, 181, 154, 103, 225, 152, 67, 161, 217, 176, 157, 148, 18, 148, 49, 121, 0, 49, 109, 158, 98, 50 }, new byte[] { 158, 153, 67, 146, 58, 8, 219, 37, 186, 245, 252, 165, 52, 105, 36, 75, 163, 221, 222, 151, 200, 130, 57, 91, 9, 239, 199, 159, 106, 255, 228, 193, 31, 51, 19, 198, 93, 36, 177, 91, 46, 178, 213, 181, 120, 140, 72, 7, 218, 188, 36, 0, 137, 99, 220, 211, 66, 185, 77, 200, 238, 10, 107, 56 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("07b959e6-02a9-4d51-92af-23acbbb8fa84"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7485), null, new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7484) },
                    { new Guid("8a7322fe-e74d-46c7-a198-6e8884e69371"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7482), null, "admin2@example.com", "Admin Dua", new byte[] { 104, 181, 250, 19, 18, 216, 231, 68, 160, 250, 43, 181, 154, 103, 225, 152, 67, 161, 217, 176, 157, 148, 18, 148, 49, 121, 0, 49, 109, 158, 98, 50 }, new byte[] { 158, 153, 67, 146, 58, 8, 219, 37, 186, 245, 252, 165, 52, 105, 36, 75, 163, 221, 222, 151, 200, 130, 57, 91, 9, 239, 199, 159, 106, 255, 228, 193, 31, 51, 19, 198, 93, 36, 177, 91, 46, 178, 213, 181, 120, 140, 72, 7, 218, 188, 36, 0, 137, 99, 220, 211, 66, 185, 77, 200, 238, 10, 107, 56 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("795b1227-89e3-4686-a9fa-4ca0a8646be5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7482), null, new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7481) },
                    { new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7487), null, "user2@example.com", "User Dua", new byte[] { 104, 181, 250, 19, 18, 216, 231, 68, 160, 250, 43, 181, 154, 103, 225, 152, 67, 161, 217, 176, 157, 148, 18, 148, 49, 121, 0, 49, 109, 158, 98, 50 }, new byte[] { 158, 153, 67, 146, 58, 8, 219, 37, 186, 245, 252, 165, 52, 105, 36, 75, 163, 221, 222, 151, 200, 130, 57, 91, 9, 239, 199, 159, 106, 255, 228, 193, 31, 51, 19, 198, 93, 36, 177, 91, 46, 178, 213, 181, 120, 140, 72, 7, 218, 188, 36, 0, 137, 99, 220, 211, 66, 185, 77, 200, 238, 10, 107, 56 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("07b959e6-02a9-4d51-92af-23acbbb8fa84"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7488), null, new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7487) }
                });

            migrationBuilder.InsertData(
                table: "records",
                columns: new[] { "id", "created_at", "deleted_at", "description", "record_group", "recorded_at", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("00b47351-5a1a-4c11-94c0-04b01c4dfa0e"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7594), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7594), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7595), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("0233e2a2-1e8f-4c0e-9e2b-9040c84b409c"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7739), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7739), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7740), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("11895422-29a2-4fba-a43a-478ed75fbda7"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7808), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7808), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7809), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("151cb60c-273b-4745-afb2-0d6db7d0780a"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7589), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7589), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7589), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("1d2b2a16-2d0a-4a48-8b1d-97ef604a9e4e"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7814), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7813), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7814), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("2dafd583-139e-4227-b2d3-f5272a4d3eba"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7608), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7608), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7608), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("32cd40b9-080a-4d03-b454-186df3737abb"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7765), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7765), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7765), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("39156453-a4ec-4f14-8dc4-c32e1080a628"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7676), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7676), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7676), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("3a4aa2e5-d176-4471-a71d-71b04edb2de3"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7760), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7759), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7760), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("3a876adc-5526-4c05-8830-0ca239b0bc5b"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7747), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7747), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7747), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("42aa4840-e4f9-4c1a-aed4-c9ff6429a80b"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7652), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7651), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7652), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("4cf99914-a41e-4f92-9387-3845ab897220"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7663), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7663), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7663), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("4d4ebf13-3071-4dec-a12a-1b153bda35ac"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7731), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7730), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7731), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("54398122-ddbb-4327-bb2e-a9abb8188e19"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7586), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7585), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7586), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("6921907e-c03d-4c53-b168-edf5cf0dc250"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7734), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7733), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7734), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("69e4659a-8bc8-4bc8-aa74-cba0363ee64c"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7801), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7800), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7801), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("711efa22-dab9-4bdc-9c78-27a1f7130d4a"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7737), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7736), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7737), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("7384ed80-9d6a-4342-8e28-6e93b163907e"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7666), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7665), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7666), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("7af7e31b-5312-4d9b-a63b-a32f3f59dd53"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7750), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7749), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7750), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("7c4704e5-feac-49ed-9f69-6bc06da3730e"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7603), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7603), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7604), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("7c63311c-7069-4939-b47f-d1e23229c9fe"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7679), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7678), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7679), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("8b9a4751-431c-41ad-99f8-cf85d49852ab"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7804), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7803), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7804), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("8d318abc-22ae-4181-82f5-38f90a340b30"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7798), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7798), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7798), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("942120c3-09aa-492b-9212-a286a65a9d6b"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7655), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7654), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7655), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("951b2f3b-3dbc-4c29-9e9e-d76a04a25771"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7682), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7681), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7682), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("9d4f6acb-62b4-4024-8130-137764e9f7dc"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7600), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7600), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7601), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("a59253e5-8730-4667-aff8-74e1919f2475"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7669), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7668), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7669), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("af12ce69-9567-41fc-b0f5-fe40ebe8c736"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7576), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7563), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7576), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("b899a460-1e38-4605-9532-4d323fe73610"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7598), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7597), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7598), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("bee73a18-09a6-4114-b257-9e326b94230f"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7744), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7744), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7745), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("c2d9d56e-4128-4927-a245-3ee73024191b"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7763), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7762), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7763), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("c6787d17-0732-4e96-b398-26ea932a2531"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7684), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7684), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7684), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("c8dc1544-f9d0-4bcb-91dd-f1801af7a063"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7583), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7583), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7583), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("caeee4b9-dfdd-498d-bebe-5f6022ae62f1"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7770), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7770), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7770), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("dc2ab3c7-fcca-4963-b145-bdddfeaa58e5"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7752), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7752), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7753), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("e6a62875-bba5-450f-8947-7a19b0682591"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7811), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7811), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7811), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("f2999aae-df9f-4d2d-9fdc-57d500ffe3e5"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7817), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7816), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7817), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("f3888595-ba54-4a9d-887b-350d9c696801"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7757), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7757), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7757), new Guid("df7ff2b9-af90-4c5f-9c40-2b00a4adfff3") },
                    { new Guid("f551f791-920e-48c2-9f5f-06b2eae14266"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7672), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7671), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7672), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") },
                    { new Guid("fb0ce76b-9c79-4616-be7f-85ae028f9267"), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7658), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 13, 22, 14, 3, 886, DateTimeKind.Local).AddTicks(7657), new DateTime(2023, 7, 13, 15, 14, 3, 886, DateTimeKind.Utc).AddTicks(7658), new Guid("3d776ada-d0e7-477d-a339-a4e20f7ee9d6") }
                });

            migrationBuilder.InsertData(
                table: "ledgers",
                columns: new[] { "id", "account_id", "balance", "entry", "record_id" },
                values: new object[,]
                {
                    { new Guid("0ea26f40-7eba-4d20-aa4e-5a7dc1925b57"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 160000m, 1, new Guid("3a876adc-5526-4c05-8830-0ca239b0bc5b") },
                    { new Guid("121b38d4-38d2-49e9-b55a-d2acd5696f5c"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 140000m, 1, new Guid("dc2ab3c7-fcca-4963-b145-bdddfeaa58e5") },
                    { new Guid("12f0302a-3c79-49be-baf5-0d3afb86a33f"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 50000m, 0, new Guid("11895422-29a2-4fba-a43a-478ed75fbda7") },
                    { new Guid("1341e0af-5a88-4afa-98ec-ae81376e57c6"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 200000m, 1, new Guid("6921907e-c03d-4c53-b168-edf5cf0dc250") },
                    { new Guid("2200316a-c757-4865-b71e-d0100fb13b56"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 100000m, 1, new Guid("32cd40b9-080a-4d03-b454-186df3737abb") },
                    { new Guid("2bb12e74-0583-4670-b25e-1f4ae4a2f400"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 130000m, 0, new Guid("2dafd583-139e-4227-b2d3-f5272a4d3eba") },
                    { new Guid("361d1a44-585c-4cf1-ba58-8a507e79fdd2"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 70000m, 0, new Guid("a59253e5-8730-4667-aff8-74e1919f2475") },
                    { new Guid("3a1dc172-ab7f-4cf7-bbdb-134f901ef0af"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 30000m, 0, new Guid("951b2f3b-3dbc-4c29-9e9e-d76a04a25771") },
                    { new Guid("3c6efadc-c0cd-4115-80e7-4a0305521ea3"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 140000m, 1, new Guid("7c4704e5-feac-49ed-9f69-6bc06da3730e") },
                    { new Guid("3f661820-d30d-4723-a08c-62dd4fed001c"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 20000m, 1, new Guid("c6787d17-0732-4e96-b398-26ea932a2531") },
                    { new Guid("3f99d24a-f9ce-4bb8-b7dd-bab64ee41b25"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 110000m, 0, new Guid("c2d9d56e-4128-4927-a245-3ee73024191b") },
                    { new Guid("59b50ac9-0518-44a9-8309-221543258a7f"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 170000m, 0, new Guid("00b47351-5a1a-4c11-94c0-04b01c4dfa0e") },
                    { new Guid("62df47f8-ea0e-4403-bca4-b02516047924"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 100000m, 1, new Guid("fb0ce76b-9c79-4616-be7f-85ae028f9267") },
                    { new Guid("7819ed05-ced1-4229-b29c-0b68af7d6b2f"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 210000m, 0, new Guid("4d4ebf13-3071-4dec-a12a-1b153bda35ac") },
                    { new Guid("7c273a7f-4452-4df8-be75-834cfe8969e3"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 200000m, 1, new Guid("c8dc1544-f9d0-4bcb-91dd-f1801af7a063") },
                    { new Guid("7c961387-a6dd-466b-a6ac-5170873f56ea"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 40000m, 1, new Guid("e6a62875-bba5-450f-8947-7a19b0682591") },
                    { new Guid("830dcf0f-3c2e-4461-9a18-8efe2257506d"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 60000m, 1, new Guid("8b9a4751-431c-41ad-99f8-cf85d49852ab") },
                    { new Guid("85f796ae-b4cd-4348-a066-44d3fee43311"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 80000m, 1, new Guid("8d318abc-22ae-4181-82f5-38f90a340b30") },
                    { new Guid("8743b8b8-c8bd-4397-be95-fdc279fcf58d"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 80000m, 1, new Guid("7384ed80-9d6a-4342-8e28-6e93b163907e") },
                    { new Guid("8dc46cd7-fa35-4b58-861b-2e289a55a947"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 40000m, 1, new Guid("7c63311c-7069-4939-b47f-d1e23229c9fe") },
                    { new Guid("8e67387f-f229-4fd3-863d-265e678f3570"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 60000m, 1, new Guid("f551f791-920e-48c2-9f5f-06b2eae14266") },
                    { new Guid("a18ae82d-9ae3-4598-a12c-40f0180c8d91"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 190000m, 0, new Guid("711efa22-dab9-4bdc-9c78-27a1f7130d4a") },
                    { new Guid("a7c1f9fc-e222-4aa3-ad37-4a4727dad6fb"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 90000m, 0, new Guid("4cf99914-a41e-4f92-9387-3845ab897220") },
                    { new Guid("b09531fd-d450-403e-ac25-00739f64c25d"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 90000m, 0, new Guid("caeee4b9-dfdd-498d-bebe-5f6022ae62f1") },
                    { new Guid("b3694acf-adbc-4952-92cb-605902ec88b0"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 150000m, 0, new Guid("7af7e31b-5312-4d9b-a63b-a32f3f59dd53") },
                    { new Guid("b471c6d0-dab4-4dd5-8ccb-c62dc8420635"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 50000m, 0, new Guid("39156453-a4ec-4f14-8dc4-c32e1080a628") },
                    { new Guid("c1790adf-fad9-4d90-bfc4-eb7e4f2c385d"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 180000m, 1, new Guid("151cb60c-273b-4745-afb2-0d6db7d0780a") },
                    { new Guid("c3143290-dd68-4b12-9ee6-e3d11c77f87f"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 170000m, 0, new Guid("bee73a18-09a6-4114-b257-9e326b94230f") },
                    { new Guid("c73ab1f1-4559-490b-9d79-41b94032d452"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 180000m, 1, new Guid("0233e2a2-1e8f-4c0e-9e2b-9040c84b409c") },
                    { new Guid("caa5fba2-4047-483e-898a-39f3c9be27fc"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 120000m, 1, new Guid("42aa4840-e4f9-4c1a-aed4-c9ff6429a80b") },
                    { new Guid("cdc4a57a-3fc8-4bdd-9e29-8bbc35ab89e5"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 130000m, 0, new Guid("f3888595-ba54-4a9d-887b-350d9c696801") },
                    { new Guid("ced42e3b-d06c-47f7-9993-46e280672f98"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 160000m, 1, new Guid("b899a460-1e38-4605-9532-4d323fe73610") },
                    { new Guid("d3340f27-83bb-48d5-819d-c376fb0ee8da"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 190000m, 0, new Guid("54398122-ddbb-4327-bb2e-a9abb8188e19") },
                    { new Guid("d93b4c5a-695c-471c-a070-c1b18bf2001e"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 70000m, 0, new Guid("69e4659a-8bc8-4bc8-aa74-cba0363ee64c") },
                    { new Guid("dde447a4-4181-42f1-941e-75826d3b6877"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 20000m, 1, new Guid("f2999aae-df9f-4d2d-9fdc-57d500ffe3e5") },
                    { new Guid("e34c36d2-2bcb-45e1-b182-c542447092c6"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 110000m, 0, new Guid("942120c3-09aa-492b-9212-a286a65a9d6b") },
                    { new Guid("e8520051-c92b-4574-9e5c-cb8386f8dc86"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 120000m, 1, new Guid("3a4aa2e5-d176-4471-a71d-71b04edb2de3") },
                    { new Guid("e933ba71-306c-4e2f-9864-0748762ae3c6"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 150000m, 0, new Guid("9d4f6acb-62b4-4024-8130-137764e9f7dc") },
                    { new Guid("eb81d16c-71fd-4198-8ba4-1205dcf4e4a1"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 210000m, 0, new Guid("af12ce69-9567-41fc-b0f5-fe40ebe8c736") },
                    { new Guid("fe7f0b72-0194-4d16-bb25-7ec5249530ac"), new Guid("4050088b-bd0b-4e58-97f4-be830cce69d6"), 30000m, 0, new Guid("1d2b2a16-2d0a-4a48-8b1d-97ef604a9e4e") }
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
