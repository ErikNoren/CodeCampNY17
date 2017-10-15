using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EFCoreJson.Angular.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JsonComplexEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SchemaVersion = table.Column<decimal>(type: "decimal(9,5)", nullable: false),
                    SerializedData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JsonComplexEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JsonMixedTypesEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchemaVersion = table.Column<decimal>(type: "decimal(9,5)", nullable: false),
                    SerializedAddresses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JsonMixedTypesEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JsonPassThroughEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SchemaVersion = table.Column<decimal>(type: "decimal(9,5)", nullable: false),
                    SerializedData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JsonPassThroughEntity", x => x.Id);
                });


            //Manual Edits to add check constraints on the columns that expect to store JSON
            migrationBuilder.Sql("ALTER TABLE [dbo].[JsonComplexEntity] WITH CHECK ADD CONSTRAINT [CK_Complex_SerializedData_IsJson] CHECK ((isjson([SerializedData])>(0)))");
            migrationBuilder.Sql("ALTER TABLE [dbo].[JsonComplexEntity] CHECK CONSTRAINT [CK_Complex_SerializedData_IsJson]");

            migrationBuilder.Sql("ALTER TABLE [dbo].[JsonMixedTypesEntity] WITH CHECK ADD CONSTRAINT [CK_Mixed_SerializedAddresses_IsJson] CHECK ((isjson([SerializedAddresses])>(0)))");
            migrationBuilder.Sql("ALTER TABLE [dbo].[JsonMixedTypesEntity] CHECK CONSTRAINT [CK_Mixed_SerializedAddresses_IsJson]");

            migrationBuilder.Sql("ALTER TABLE [dbo].[JsonPassThroughEntity] WITH CHECK ADD CONSTRAINT [CK_PassThrough_SerializedData_IsJson] CHECK ((isjson([SerializedData])>(0)))");
            migrationBuilder.Sql("ALTER TABLE [dbo].[JsonPassThroughEntity] CHECK CONSTRAINT [CK_PassThrough_SerializedData_IsJson]");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JsonComplexEntity");

            migrationBuilder.DropTable(
                name: "JsonMixedTypesEntity");

            migrationBuilder.DropTable(
                name: "JsonPassThroughEntity");
        }
    }
}
