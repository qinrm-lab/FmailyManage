using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FmailyManage.Migrations
{
    /// <inheritdoc />
    public partial class templecontext_20241105b : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Families",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Families", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Money",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Money", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NameClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NameClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StyleList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StyleList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Temples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TempleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Temples_Temples_TempleId",
                        column: x => x.TempleId,
                        principalTable: "Temples",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Style",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prompt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StyleListId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Style", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Style_StyleList_StyleListId",
                        column: x => x.StyleListId,
                        principalTable: "StyleList",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonInfoId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    UserStaus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperatorId = table.Column<int>(type: "int", nullable: true),
                    Time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Operating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountClassId = table.Column<int>(type: "int", nullable: true),
                    FamilyId = table.Column<int>(type: "int", nullable: true),
                    FundId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_Accounts_AccountClassId",
                        column: x => x.AccountClassId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operations_Accounts_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Operations_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operations_Money_FundId",
                        column: x => x.FundId,
                        principalTable: "Money",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GridModules",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    i = table.Column<int>(type: "int", nullable: false),
                    j = table.Column<int>(type: "int", nullable: false),
                    view = table.Column<bool>(type: "bit", nullable: false),
                    words = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stylesId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GridModules", x => x.id);
                    table.ForeignKey(
                        name: "FK_GridModules_StyleList_stylesId",
                        column: x => x.stylesId,
                        principalTable: "StyleList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GridTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    sLen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nRows = table.Column<int>(type: "int", nullable: false),
                    nColumns = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GridTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GridTables_GridModules_ParentId",
                        column: x => x.ParentId,
                        principalTable: "GridModules",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JsonString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GridTableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreaterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Templates_Accounts_CreaterId",
                        column: x => x.CreaterId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Templates_GridTables_GridTableId",
                        column: x => x.GridTableId,
                        principalTable: "GridTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KeyValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FamilyId = table.Column<int>(type: "int", nullable: true),
                    NameId = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    PersonalIDId = table.Column<int>(type: "int", nullable: false),
                    FromId = table.Column<int>(type: "int", nullable: true),
                    Generation = table.Column<int>(type: "int", nullable: false, comment: "家族第几代"),
                    PersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Persons_KeyValues_PersonalIDId",
                        column: x => x.PersonalIDId,
                        principalTable: "KeyValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persons_NameClass_NameId",
                        column: x => x.NameId,
                        principalTable: "NameClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persons_Persons_FromId",
                        column: x => x.FromId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Persons_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CreatedById",
                table: "Accounts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PersonInfoId",
                table: "Accounts",
                column: "PersonInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_GridModules_ParentId",
                table: "GridModules",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_GridModules_stylesId",
                table: "GridModules",
                column: "stylesId");

            migrationBuilder.CreateIndex(
                name: "IX_GridTables_ParentId",
                table: "GridTables",
                column: "ParentId",
                unique: true,
                filter: "[ParentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_KeyValues_PersonId",
                table: "KeyValues",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_AccountClassId",
                table: "Operations",
                column: "AccountClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_FamilyId",
                table: "Operations",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_FundId",
                table: "Operations",
                column: "FundId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_OperatorId",
                table: "Operations",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_FamilyId",
                table: "Persons",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_FromId",
                table: "Persons",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_NameId",
                table: "Persons",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PersonalIDId",
                table: "Persons",
                column: "PersonalIDId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PersonId",
                table: "Persons",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Style_StyleListId",
                table: "Style",
                column: "StyleListId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_CreaterId",
                table: "Templates",
                column: "CreaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_GridTableId",
                table: "Templates",
                column: "GridTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Temples_TempleId",
                table: "Temples",
                column: "TempleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Persons_PersonInfoId",
                table: "Accounts",
                column: "PersonInfoId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GridModules_GridTables_ParentId",
                table: "GridModules",
                column: "ParentId",
                principalTable: "GridTables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyValues_Persons_PersonId",
                table: "KeyValues",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyValues_Persons_PersonId",
                table: "KeyValues");

            migrationBuilder.DropForeignKey(
                name: "FK_GridModules_GridTables_ParentId",
                table: "GridModules");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Style");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "Temples");

            migrationBuilder.DropTable(
                name: "Money");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Families");

            migrationBuilder.DropTable(
                name: "KeyValues");

            migrationBuilder.DropTable(
                name: "NameClass");

            migrationBuilder.DropTable(
                name: "GridTables");

            migrationBuilder.DropTable(
                name: "GridModules");

            migrationBuilder.DropTable(
                name: "StyleList");
        }
    }
}
