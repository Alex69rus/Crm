using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crm.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "crm");

            migrationBuilder.CreateTable(
                name: "accounts",
                schema: "crm",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    account_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    short_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    registration_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    setup_date = table.Column<DateOnly>(type: "date", nullable: true),
                    closed_date = table.Column<DateOnly>(type: "date", nullable: true),
                    sponsor_company = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    managed = table.Column<bool>(type: "boolean", nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    discretion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contacts",
                schema: "crm",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    middle_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    name_prefix = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    gender = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: true),
                    date_of_death = table.Column<DateOnly>(type: "date", nullable: true),
                    tax_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    citizenship = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contacts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "account_balances",
                schema: "crm",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    balance = table.Column<decimal>(type: "numeric", nullable: false),
                    last_updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    account_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_account_balances", x => x.id);
                    table.ForeignKey(
                        name: "fk_account_balances_accounts_account_id",
                        column: x => x.account_id,
                        principalSchema: "crm",
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "account_owners",
                schema: "crm",
                columns: table => new
                {
                    account_id = table.Column<string>(type: "text", nullable: false),
                    contact_id = table.Column<string>(type: "text", nullable: false),
                    assigned_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_account_owners", x => new { x.account_id, x.contact_id });
                    table.ForeignKey(
                        name: "fk_account_owners_accounts_account_id",
                        column: x => x.account_id,
                        principalSchema: "crm",
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_account_owners_contacts_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "crm",
                        principalTable: "contacts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contact_addresses",
                schema: "crm",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    street = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    state = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    postal_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    contact_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contact_addresses", x => x.id);
                    table.ForeignKey(
                        name: "fk_contact_addresses_contacts_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "crm",
                        principalTable: "contacts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contact_phones",
                schema: "crm",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    contact_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contact_phones", x => x.id);
                    table.ForeignKey(
                        name: "fk_contact_phones_contacts_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "crm",
                        principalTable: "contacts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_account_balances_account_id",
                schema: "crm",
                table: "account_balances",
                column: "account_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_account_owners_contact_id",
                schema: "crm",
                table: "account_owners",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "ix_contact_addresses_contact_id",
                schema: "crm",
                table: "contact_addresses",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "ix_contact_phones_contact_id",
                schema: "crm",
                table: "contact_phones",
                column: "contact_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account_balances",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "account_owners",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "contact_addresses",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "contact_phones",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "accounts",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "contacts",
                schema: "crm");
        }
    }
}
