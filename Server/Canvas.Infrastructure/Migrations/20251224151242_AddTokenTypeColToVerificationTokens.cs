using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Canvas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTokenTypeColToVerificationTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VerificationTokenType",
                table: "VerificationTokens",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationTokenType",
                table: "VerificationTokens");
        }
    }
}
