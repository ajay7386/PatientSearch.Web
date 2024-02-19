using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientSearch.Infrastructure.Migrations
{
    public partial class InitialMigration_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Department",
                table: "patients",
                newName: "Problem");

            migrationBuilder.RenameColumn(
                name: "AdmissionDate",
                table: "patients",
                newName: "LastUpdate");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "patients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "patients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "patients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "patients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_patients_AddressId",
                table: "patients",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_DepartmentId",
                table: "patients",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_patients_Address_AddressId",
                table: "patients",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_patients_Department_DepartmentId",
                table: "patients",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_patients_Address_AddressId",
                table: "patients");

            migrationBuilder.DropForeignKey(
                name: "FK_patients_Department_DepartmentId",
                table: "patients");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropIndex(
                name: "IX_patients_AddressId",
                table: "patients");

            migrationBuilder.DropIndex(
                name: "IX_patients_DepartmentId",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "patients");

            migrationBuilder.RenameColumn(
                name: "Problem",
                table: "patients",
                newName: "Department");

            migrationBuilder.RenameColumn(
                name: "LastUpdate",
                table: "patients",
                newName: "AdmissionDate");
        }
    }
}
