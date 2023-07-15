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
                        name: "FK_receivable_ledger_records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "id", "account_code", "account_group", "account_name", "created_at", "deleted_at", "updated_at" },
                values: new object[,]
                {
                    { new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 11001, 11, "CASH", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3566), null, new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3566) },
                    { new Guid("936352ff-7ef5-44e8-8bda-1574cf67c8ac"), 11005, 11, "RECEIVABLE", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3568), null, new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3569) },
                    { new Guid("b9a663f1-0fcd-46c4-873b-d49edb7b7430"), 21001, 21, "PAYABLE", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3570), null, new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3570) }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_at", "deleted_at", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("66f73381-f433-43b9-b3ec-0ddaceb350d5"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(2577), null, "ROLE_ADMIN", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(2578) },
                    { new Guid("bacf9086-da56-4e8b-8d4b-f9681ee57f16"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(2611), null, "ROLE_USER", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(2611) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "deleted_at", "email", "full_name", "password_hash", "password_salt", "profile_picture", "refresh_token", "reset_password_token", "reset_token_expires", "role_id", "token_expires", "updated_at", "verification_token", "verified_at" },
                values: new object[,]
                {
                    { new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3534), null, "user1@example.com", "User Satu", new byte[] { 73, 155, 215, 159, 94, 72, 122, 251, 103, 48, 108, 47, 207, 174, 97, 44, 52, 246, 248, 235, 189, 128, 24, 170, 73, 210, 154, 170, 85, 246, 220, 81 }, new byte[] { 38, 212, 145, 175, 221, 6, 152, 241, 93, 218, 174, 190, 162, 27, 104, 248, 85, 78, 36, 71, 105, 73, 14, 162, 184, 215, 154, 106, 184, 111, 27, 150, 58, 164, 71, 98, 17, 233, 39, 126, 217, 176, 93, 212, 102, 34, 199, 97, 73, 221, 109, 129, 145, 196, 144, 134, 184, 36, 142, 45, 24, 174, 122, 212 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bacf9086-da56-4e8b-8d4b-f9681ee57f16"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3534), null, new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3533) },
                    { new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3536), null, "user2@example.com", "User Dua", new byte[] { 73, 155, 215, 159, 94, 72, 122, 251, 103, 48, 108, 47, 207, 174, 97, 44, 52, 246, 248, 235, 189, 128, 24, 170, 73, 210, 154, 170, 85, 246, 220, 81 }, new byte[] { 38, 212, 145, 175, 221, 6, 152, 241, 93, 218, 174, 190, 162, 27, 104, 248, 85, 78, 36, 71, 105, 73, 14, 162, 184, 215, 154, 106, 184, 111, 27, 150, 58, 164, 71, 98, 17, 233, 39, 126, 217, 176, 93, 212, 102, 34, 199, 97, 73, 221, 109, 129, 145, 196, 144, 134, 184, 36, 142, 45, 24, 174, 122, 212 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bacf9086-da56-4e8b-8d4b-f9681ee57f16"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3537), null, new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3536) },
                    { new Guid("7d44a5e6-ebdd-4570-a9a0-dfe17eb94a12"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3531), null, "admin2@example.com", "Admin Dua", new byte[] { 73, 155, 215, 159, 94, 72, 122, 251, 103, 48, 108, 47, 207, 174, 97, 44, 52, 246, 248, 235, 189, 128, 24, 170, 73, 210, 154, 170, 85, 246, 220, 81 }, new byte[] { 38, 212, 145, 175, 221, 6, 152, 241, 93, 218, 174, 190, 162, 27, 104, 248, 85, 78, 36, 71, 105, 73, 14, 162, 184, 215, 154, 106, 184, 111, 27, 150, 58, 164, 71, 98, 17, 233, 39, 126, 217, 176, 93, 212, 102, 34, 199, 97, 73, 221, 109, 129, 145, 196, 144, 134, 184, 36, 142, 45, 24, 174, 122, 212 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("66f73381-f433-43b9-b3ec-0ddaceb350d5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3531), null, new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3531) },
                    { new Guid("8edc88ca-9f5f-43da-8673-781c80d24a64"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3526), null, "admin1@example.com", "Admin Satu", new byte[] { 73, 155, 215, 159, 94, 72, 122, 251, 103, 48, 108, 47, 207, 174, 97, 44, 52, 246, 248, 235, 189, 128, 24, 170, 73, 210, 154, 170, 85, 246, 220, 81 }, new byte[] { 38, 212, 145, 175, 221, 6, 152, 241, 93, 218, 174, 190, 162, 27, 104, 248, 85, 78, 36, 71, 105, 73, 14, 162, 184, 215, 154, 106, 184, 111, 27, 150, 58, 164, 71, 98, 17, 233, 39, 126, 217, 176, 93, 212, 102, 34, 199, 97, 73, 221, 109, 129, 145, 196, 144, 134, 184, 36, 142, 45, 24, 174, 122, 212 }, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("66f73381-f433-43b9-b3ec-0ddaceb350d5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3526), null, new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3525) }
                });

            migrationBuilder.InsertData(
                table: "contacts",
                columns: new[] { "id", "address", "created_at", "deleted_at", "email", "name", "phone_number", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("02839cac-9434-48f1-a965-3ad3191c92e9"), "4330 Doe Crossing Junction", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3875), null, "vwoodsford9@issuu.com", "Vasilis Woodsford", "8364764344", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3876), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("10c7eb86-827e-46da-813a-23f83d0f23be"), "10 Calypso Center", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3872), null, "candrelli7@intel.com", "Ceil Andrelli", "1401949061", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3872), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("1fd90831-853b-4527-bf78-7a6374f1c7b3"), "39494 Kartens Terrace", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3867), null, "sblanchard4@vinaora.com", "Sal Blanchard", "4904310249", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3867), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("3bbb3bc1-549d-40b8-915d-811ded432a47"), "615 Jenifer Alley", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3869), null, "ldunster5@marketwatch.com", "Laurette Dunster", "7678939672", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3869), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("6cc86818-4984-463d-8658-5dcc989fc9bd"), "34 Eastwood Avenue", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3859), null, "smahood1@wufoo.com", "Sarine Mahood", "6977095403", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3859), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("7803a8d9-a950-4ebb-953b-f003b170cd30"), "4649 Swallow Trail", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3870), null, "tshew6@tripadvisor.com", "Tiphany Shew", "1457940882", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3871), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("8ee4adf0-af8d-4a16-bf76-e1170e1e2dfa"), "620 Lakeland Center", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3832), null, "rhavock0@gov.uk", "Randell Havock", "6765109874", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3832), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("99409d94-efe9-469b-b56e-bd1c93bcaea7"), "54 Redwing Drive", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3879), null, "jorneblowa@youtube.com", "Jennee Orneblow", "9316403125", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3879), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("b687fab1-1f9d-4d66-b0d6-3521011199b2"), "17 Onsgard Avenue", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3864), null, "bmcging2@ihg.com", "Benjie McGing", "5025308916", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3864), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("c73e776c-251c-433b-9771-29b114fc0740"), "11 Old Shore Terrace", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3874), null, "msivorn8@icq.com", "Margaretta Sivorn", "5311644464", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3874), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("e9ed3ad3-f0fc-4146-8864-2403287ef7c7"), "96 Summerview Drive", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3865), null, "klanphere3@upenn.edu", "Kev Lanphere", "7268834724", new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3865), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") }
                });

            migrationBuilder.InsertData(
                table: "records",
                columns: new[] { "id", "created_at", "deleted_at", "description", "record_group", "recorded_at", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("0a8004a6-7b37-488c-9bd5-90b9de27d634"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3648), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3648), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3648), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("0ed14b88-7e77-4e32-8a69-585b9d5d16d6"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3672), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3671), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3672), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("19487ce2-3653-4d77-8ac7-7b2df5c803ae"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3756), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3756), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3756), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("1acd963e-cb2b-4a5a-83ac-6d5d3a1cfa03"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3753), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3753), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3754), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("26eeddba-dafc-4c4f-848a-d57a61b47476"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3769), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3769), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3769), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("2e2d80c4-19ea-4b6c-aedf-9b289b89bb14"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3792), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3792), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3793), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("47c884a9-0c86-4e10-a1f8-a0cd18fdaf51"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3787), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3786), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3787), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("508af46d-56e6-4321-81a1-c7a0ff1e62bf"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3651), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3651), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3651), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("5312c3d5-7023-4cef-bbf8-68266b87d09d"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3645), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3645), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3646), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("5e675c9f-0423-4fad-9688-82146ffc1301"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3654), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3653), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3654), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("648b5fbe-1eac-45df-b735-5ead6f29dde4"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3632), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3631), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3632), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("6afa484d-8909-4cb8-8445-2d8efe170e5e"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3751), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3750), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3751), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("6f40e3d1-e093-4064-8d03-740020269efd"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3677), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3677), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3678), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("72e5b4be-432c-421b-8691-952ec02ebf99"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3661), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3661), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3662), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("75543645-00c0-477d-9232-58c0b4d2b125"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3742), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3742), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3742), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("76015793-bb6b-4ba9-8329-328d853b5c33"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3603), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3591), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3603), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("7be62ac8-8472-4a03-b896-1a3e7939142f"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3764), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3763), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3764), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("88adf5ae-a0de-4a95-b00c-b129ceddd904"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3659), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3658), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3659), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("8a56e9de-6e5e-44a9-8afb-3148ec63b529"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3774), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3773), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3774), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("8fc86498-5866-4db3-a3fb-11c145501e0a"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3674), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3674), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3675), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("91ff9e4c-b308-4f21-8f21-eab3f30cc546"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3766), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3766), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3767), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("9be5ee26-e44f-44cc-ad2c-be55e7ba8286"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3620), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3620), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3621), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("9eb8807e-8ba7-4114-b4ac-0b166242243b"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3805), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3805), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3806), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("9ee56887-4bb4-4d20-aa5d-a787028fd364"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3626), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3626), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3627), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("a843434a-91b8-449c-881f-ead30e9773d7"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3779), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3779), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3780), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("ac589c83-b25a-49ff-9998-d46278f54e1a"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3761), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3760), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3761), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("ad079156-1986-4051-a194-73164a8fb277"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3635), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3634), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3635), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("ad6c8cf8-3a07-4512-907c-b2c0c74c7adf"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3790), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3789), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3790), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("adc242e1-0ec5-46e5-8471-f3359a9506f9"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3623), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3623), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3624), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("b8c34d66-7b25-42f9-a956-947f9b5fa4d2"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3795), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3795), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3795), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("baa1996d-741b-4551-b047-0b391bd4fea0"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3800), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3799), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3800), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") }
                });

            migrationBuilder.InsertData(
                table: "records",
                columns: new[] { "id", "created_at", "deleted_at", "description", "record_group", "recorded_at", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("cfcc0ee3-8e70-4a36-b938-bb72077df6c5"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3776), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3776), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3777), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("d485212e-8a9f-4f31-a3e9-2306445cd9fb"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3641), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3640), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3641), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("db49fb4a-a0c1-43ca-8555-2c1334778642"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3638), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3637), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3638), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("dffd0cc7-7406-410c-94f5-ed684036331b"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3664), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3664), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3664), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("e2e3d2eb-b43a-4eb4-9aa3-ac9483ec4060"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3747), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3747), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3748), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("e5fa2070-acf8-4c5a-a637-c7dc01a2ab1e"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3667), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3666), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3667), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("f0911fdd-54ba-444a-aa3c-21b325897b01"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3617), null, "Lorem ipsum dolor sit amet.", 0, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3617), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3618), new Guid("33e8b9a7-e1ce-4121-a273-9f66d538a19d") },
                    { new Guid("f3007be4-d064-40d3-bb07-f22241db0f7c"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3782), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3782), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3782), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") },
                    { new Guid("fe19c737-818d-4e8e-9d24-73fcfe488eb4"), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3802), null, "Lorem ipsum dolor sit amet.", 1, new DateTime(2023, 7, 15, 17, 33, 59, 300, DateTimeKind.Local).AddTicks(3802), new DateTime(2023, 7, 15, 10, 33, 59, 300, DateTimeKind.Utc).AddTicks(3803), new Guid("3eedcbf7-0212-4039-b544-6d93e8384b33") }
                });

            migrationBuilder.InsertData(
                table: "ledgers",
                columns: new[] { "id", "account_id", "balance", "entry", "record_id" },
                values: new object[,]
                {
                    { new Guid("0047943d-514b-40e1-9f84-9579fbbc8c0c"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 60000m, 1, new Guid("dffd0cc7-7406-410c-94f5-ed684036331b") },
                    { new Guid("0263c3ea-61e1-4f77-a21c-8279fe57a467"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 40000m, 1, new Guid("0ed14b88-7e77-4e32-8a69-585b9d5d16d6") },
                    { new Guid("05c16cde-2aeb-47f7-846d-d4b884f9b8ee"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 60000m, 1, new Guid("2e2d80c4-19ea-4b6c-aedf-9b289b89bb14") },
                    { new Guid("08529493-e808-47f1-8ec6-a11f09e9c649"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 80000m, 1, new Guid("88adf5ae-a0de-4a95-b00c-b129ceddd904") },
                    { new Guid("0c8924f2-0cae-4a6c-91c2-5d13115187dc"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 100000m, 1, new Guid("a843434a-91b8-449c-881f-ead30e9773d7") },
                    { new Guid("0ed95e68-6c97-4322-b730-f5732939e2ae"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 50000m, 0, new Guid("b8c34d66-7b25-42f9-a956-947f9b5fa4d2") },
                    { new Guid("11102969-454e-4065-827f-87008fc13e81"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 140000m, 1, new Guid("db49fb4a-a0c1-43ca-8555-2c1334778642") },
                    { new Guid("14f09a31-9fd2-4cde-b237-af8e57f9ca58"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 160000m, 1, new Guid("648b5fbe-1eac-45df-b735-5ead6f29dde4") },
                    { new Guid("155a0eb1-7402-4280-a4c5-103dcea3e141"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 180000m, 1, new Guid("adc242e1-0ec5-46e5-8471-f3359a9506f9") },
                    { new Guid("18f4a21a-6476-4fb6-8a2f-deefd116b5d6"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 190000m, 0, new Guid("9be5ee26-e44f-44cc-ad2c-be55e7ba8286") },
                    { new Guid("1d91615b-fca5-46d4-a18a-471e6553204b"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 40000m, 1, new Guid("baa1996d-741b-4551-b047-0b391bd4fea0") },
                    { new Guid("1fb0c03c-89af-4e1f-98e5-d5d33c3e04b7"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 170000m, 0, new Guid("9ee56887-4bb4-4d20-aa5d-a787028fd364") },
                    { new Guid("2a6fe932-e1f6-4a27-939d-0ae998a7db2f"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 70000m, 0, new Guid("ad6c8cf8-3a07-4512-907c-b2c0c74c7adf") },
                    { new Guid("3013eebe-47a1-46ba-8ba3-cc2bf09b2c8a"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 20000m, 1, new Guid("9eb8807e-8ba7-4114-b4ac-0b166242243b") },
                    { new Guid("30be721f-75eb-43ed-ac5a-4a840ddfce29"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 150000m, 0, new Guid("7be62ac8-8472-4a03-b896-1a3e7939142f") },
                    { new Guid("4709170b-ec62-43d1-84ff-c48541b1cbad"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 200000m, 1, new Guid("e2e3d2eb-b43a-4eb4-9aa3-ac9483ec4060") },
                    { new Guid("5c3dc1d0-0d4b-44ea-8830-f1852902ca21"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 20000m, 1, new Guid("6f40e3d1-e093-4064-8d03-740020269efd") },
                    { new Guid("628d11c4-f96e-4263-a546-6d50b3b5b919"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 140000m, 1, new Guid("91ff9e4c-b308-4f21-8f21-eab3f30cc546") },
                    { new Guid("7233179d-761e-448a-ba67-535406bdd635"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 210000m, 0, new Guid("75543645-00c0-477d-9232-58c0b4d2b125") },
                    { new Guid("77d76989-1777-48bf-a73b-6b4289443f73"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 50000m, 0, new Guid("e5fa2070-acf8-4c5a-a637-c7dc01a2ab1e") },
                    { new Guid("7d52b52b-3fab-489b-9e00-be26f32314ed"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 90000m, 0, new Guid("5e675c9f-0423-4fad-9688-82146ffc1301") },
                    { new Guid("85a5bb84-a4fc-475d-9d63-3ff8cacccfe3"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 210000m, 0, new Guid("76015793-bb6b-4ba9-8329-328d853b5c33") },
                    { new Guid("91855855-d30d-48f7-b798-1d95de9fc19c"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 70000m, 0, new Guid("72e5b4be-432c-421b-8691-952ec02ebf99") },
                    { new Guid("9663f5d0-db05-4770-a709-04eb8427ea49"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 130000m, 0, new Guid("d485212e-8a9f-4f31-a3e9-2306445cd9fb") },
                    { new Guid("9a580471-581e-47a5-ab13-d15fda195919"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 120000m, 1, new Guid("5312c3d5-7023-4cef-bbf8-68266b87d09d") },
                    { new Guid("9b28d615-f65c-4781-953b-85b431103eb1"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 190000m, 0, new Guid("6afa484d-8909-4cb8-8445-2d8efe170e5e") },
                    { new Guid("a245dc55-8800-4d2e-9125-aa03040d6623"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 180000m, 1, new Guid("1acd963e-cb2b-4a5a-83ac-6d5d3a1cfa03") },
                    { new Guid("a35cd7fa-46e3-4c39-9b6e-e1a0e66a1123"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 30000m, 0, new Guid("8fc86498-5866-4db3-a3fb-11c145501e0a") },
                    { new Guid("a9fd0a70-cf08-41a1-838e-fbdea5b2343e"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 100000m, 1, new Guid("508af46d-56e6-4321-81a1-c7a0ff1e62bf") },
                    { new Guid("af0c30e2-f633-4232-b6f6-87a622dd40f8"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 200000m, 1, new Guid("f0911fdd-54ba-444a-aa3c-21b325897b01") },
                    { new Guid("bc23f7e9-d9b6-47db-b655-44f7075d0af8"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 110000m, 0, new Guid("cfcc0ee3-8e70-4a36-b938-bb72077df6c5") },
                    { new Guid("bd4a47f4-a429-4506-b402-fa795c01daba"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 160000m, 1, new Guid("ac589c83-b25a-49ff-9998-d46278f54e1a") },
                    { new Guid("c2d9e0fa-f3d0-482d-9092-85ac8f43277f"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 170000m, 0, new Guid("19487ce2-3653-4d77-8ac7-7b2df5c803ae") },
                    { new Guid("ca903358-5087-4264-852f-8ccfa124e636"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 130000m, 0, new Guid("26eeddba-dafc-4c4f-848a-d57a61b47476") },
                    { new Guid("d1303649-00b7-40bd-9011-e7bcc0427439"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 120000m, 1, new Guid("8a56e9de-6e5e-44a9-8afb-3148ec63b529") },
                    { new Guid("dba86fc9-a30f-4181-968e-58a059516b97"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 90000m, 0, new Guid("f3007be4-d064-40d3-bb07-f22241db0f7c") },
                    { new Guid("ec77f8d9-28fd-49c5-95e7-bf418b83a225"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 80000m, 1, new Guid("47c884a9-0c86-4e10-a1f8-a0cd18fdaf51") },
                    { new Guid("f01b373a-544e-4293-96b9-fdef4ad064e2"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 30000m, 0, new Guid("fe19c737-818d-4e8e-9d24-73fcfe488eb4") },
                    { new Guid("f9f24682-93e2-4e11-958c-1cb0a453bfae"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 150000m, 0, new Guid("ad079156-1986-4051-a194-73164a8fb277") },
                    { new Guid("fb9b7c1f-a724-41d2-addf-9049c6dfa088"), new Guid("615d9ec7-42f2-44e6-a0dd-a2480ce1ccd6"), 110000m, 0, new Guid("0a8004a6-7b37-488c-9bd5-90b9de27d634") }
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
                column: "ReceivableId");

            migrationBuilder.CreateIndex(
                name: "IX_receivable_ledger_RecordId",
                table: "receivable_ledger",
                column: "RecordId",
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
