using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class gis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "gis");

            migrationBuilder.EnsureSchema(
                name: "meta");

            migrationBuilder.CreateTable(
                name: "GlobalRegion",
                schema: "gis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    CreatedTimestamp = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<string>(nullable: false),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalRegion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlobalRegion_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GlobalRegion_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                schema: "meta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    CreatedTimestamp = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<string>(nullable: false),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Discriminator = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gender_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gender_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IDType",
                schema: "meta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    CreatedTimestamp = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<string>(nullable: false),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDType_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IDType_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Title",
                schema: "meta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    CreatedTimestamp = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<string>(nullable: false),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Title", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Title_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Title_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "gis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    CreatedTimestamp = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<string>(nullable: false),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    GlobalRegionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Country_GlobalRegion_GlobalRegionId",
                        column: x => x.GlobalRegionId,
                        principalSchema: "gis",
                        principalTable: "GlobalRegion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Country_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                schema: "gis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    CreatedTimestamp = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<string>(nullable: false),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Province_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "gis",
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Province_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Province_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                schema: "gis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    CreatedTimestamp = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<string>(nullable: false),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_City_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalSchema: "gis",
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_City_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Town",
                schema: "gis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    CreatedTimestamp = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<string>(nullable: false),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Town", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Town_City_CityId",
                        column: x => x.CityId,
                        principalSchema: "gis",
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Town_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Town_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suburb",
                schema: "gis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    CreatedTimestamp = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<string>(nullable: false),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    BoxCode = table.Column<string>(nullable: true),
                    StreetCode = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(nullable: true),
                    Latitude = table.Column<decimal>(nullable: true),
                    Longitude = table.Column<decimal>(nullable: true),
                    TownId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suburb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suburb_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Suburb_Town_TownId",
                        column: x => x.TownId,
                        principalSchema: "gis",
                        principalTable: "Town",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Suburb_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedTimestamp = table.Column<DateTime>(nullable: false),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<string>(nullable: false),
                    UpdatedUserId = table.Column<string>(nullable: true),
                    Line1 = table.Column<string>(nullable: true),
                    Line2 = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    SuburbId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_Suburb_SuburbId",
                        column: x => x.SuburbId,
                        principalSchema: "gis",
                        principalTable: "Suburb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_AspNetUsers_UpdatedUserId",
                        column: x => x.UpdatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberStaging_AddressId",
                schema: "worker",
                table: "MemberStaging",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberStaging_CountryId",
                schema: "worker",
                table: "MemberStaging",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberStaging_GenderId",
                schema: "worker",
                table: "MemberStaging",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberStaging_IDTypeId",
                schema: "worker",
                table: "MemberStaging",
                column: "IDTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberStaging_TitleId",
                schema: "worker",
                table: "MemberStaging",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CreatedUserId",
                table: "Address",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_SuburbId",
                table: "Address",
                column: "SuburbId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_UpdatedUserId",
                table: "Address",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_City_CreatedUserId",
                schema: "gis",
                table: "City",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_City_UpdatedUserId",
                schema: "gis",
                table: "City",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_City_ProvinceId_Name",
                schema: "gis",
                table: "City",
                columns: new[] { "ProvinceId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country_CreatedUserId",
                schema: "gis",
                table: "Country",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_UpdatedUserId",
                schema: "gis",
                table: "Country",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_GlobalRegionId_Name",
                schema: "gis",
                table: "Country",
                columns: new[] { "GlobalRegionId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GlobalRegion_CreatedUserId",
                schema: "gis",
                table: "GlobalRegion",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalRegion_Name",
                schema: "gis",
                table: "GlobalRegion",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GlobalRegion_UpdatedUserId",
                schema: "gis",
                table: "GlobalRegion",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Province_CreatedUserId",
                schema: "gis",
                table: "Province",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Province_UpdatedUserId",
                schema: "gis",
                table: "Province",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Province_CountryId_Name",
                schema: "gis",
                table: "Province",
                columns: new[] { "CountryId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suburb_CreatedUserId",
                schema: "gis",
                table: "Suburb",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Suburb_TownId",
                schema: "gis",
                table: "Suburb",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_Suburb_UpdatedUserId",
                schema: "gis",
                table: "Suburb",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Town_CreatedUserId",
                schema: "gis",
                table: "Town",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Town_UpdatedUserId",
                schema: "gis",
                table: "Town",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Town_CityId_Name",
                schema: "gis",
                table: "Town",
                columns: new[] { "CityId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gender_CreatedUserId",
                schema: "meta",
                table: "Gender",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Gender_Discriminator",
                schema: "meta",
                table: "Gender",
                column: "Discriminator",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gender_Name",
                schema: "meta",
                table: "Gender",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gender_UpdatedUserId",
                schema: "meta",
                table: "Gender",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IDType_CreatedUserId",
                schema: "meta",
                table: "IDType",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IDType_Name",
                schema: "meta",
                table: "IDType",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IDType_UpdatedUserId",
                schema: "meta",
                table: "IDType",
                column: "UpdatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Title_CreatedUserId",
                schema: "meta",
                table: "Title",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Title_Name",
                schema: "meta",
                table: "Title",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Title_UpdatedUserId",
                schema: "meta",
                table: "Title",
                column: "UpdatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberStaging_Address_AddressId",
                schema: "worker",
                table: "MemberStaging",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberStaging_Country_CountryId",
                schema: "worker",
                table: "MemberStaging",
                column: "CountryId",
                principalSchema: "gis",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberStaging_Gender_GenderId",
                schema: "worker",
                table: "MemberStaging",
                column: "GenderId",
                principalSchema: "meta",
                principalTable: "Gender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberStaging_IDType_IDTypeId",
                schema: "worker",
                table: "MemberStaging",
                column: "IDTypeId",
                principalSchema: "meta",
                principalTable: "IDType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberStaging_Title_TitleId",
                schema: "worker",
                table: "MemberStaging",
                column: "TitleId",
                principalSchema: "meta",
                principalTable: "Title",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberStaging_Address_AddressId",
                schema: "worker",
                table: "MemberStaging");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberStaging_Country_CountryId",
                schema: "worker",
                table: "MemberStaging");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberStaging_Gender_GenderId",
                schema: "worker",
                table: "MemberStaging");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberStaging_IDType_IDTypeId",
                schema: "worker",
                table: "MemberStaging");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberStaging_Title_TitleId",
                schema: "worker",
                table: "MemberStaging");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Gender",
                schema: "meta");

            migrationBuilder.DropTable(
                name: "IDType",
                schema: "meta");

            migrationBuilder.DropTable(
                name: "Title",
                schema: "meta");

            migrationBuilder.DropTable(
                name: "Suburb",
                schema: "gis");

            migrationBuilder.DropTable(
                name: "Town",
                schema: "gis");

            migrationBuilder.DropTable(
                name: "City",
                schema: "gis");

            migrationBuilder.DropTable(
                name: "Province",
                schema: "gis");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "gis");

            migrationBuilder.DropTable(
                name: "GlobalRegion",
                schema: "gis");

            migrationBuilder.DropIndex(
                name: "IX_MemberStaging_AddressId",
                schema: "worker",
                table: "MemberStaging");

            migrationBuilder.DropIndex(
                name: "IX_MemberStaging_CountryId",
                schema: "worker",
                table: "MemberStaging");

            migrationBuilder.DropIndex(
                name: "IX_MemberStaging_GenderId",
                schema: "worker",
                table: "MemberStaging");

            migrationBuilder.DropIndex(
                name: "IX_MemberStaging_IDTypeId",
                schema: "worker",
                table: "MemberStaging");

            migrationBuilder.DropIndex(
                name: "IX_MemberStaging_TitleId",
                schema: "worker",
                table: "MemberStaging");
        }
    }
}
