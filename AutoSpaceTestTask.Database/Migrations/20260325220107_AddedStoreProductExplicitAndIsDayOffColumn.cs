using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoSpaceTestTask.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedStoreProductExplicitAndIsDayOffColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UtcStartTime",
                table: "StoreSchedules",
                newName: "OpenTime");

            migrationBuilder.RenameColumn(
                name: "UtcEndTime",
                table: "StoreSchedules",
                newName: "CloseTime");

            migrationBuilder.AddColumn<bool>(
                name: "IsDayOff",
                table: "StoreSchedules",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDayOff",
                table: "StoreSchedules");

            migrationBuilder.RenameColumn(
                name: "OpenTime",
                table: "StoreSchedules",
                newName: "UtcStartTime");

            migrationBuilder.RenameColumn(
                name: "CloseTime",
                table: "StoreSchedules",
                newName: "UtcEndTime");
        }
    }
}
