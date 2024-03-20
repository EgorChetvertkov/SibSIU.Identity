using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SibSIU.Auth.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicForms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    DeanCode = table.Column<int>(type: "integer", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AcademicLevels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    DeanCode = table.Column<int>(type: "integer", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClaimTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    IncludeInAccessToken = table.Column<bool>(type: "boolean", nullable: false),
                    IncludeInIdentityToken = table.Column<bool>(type: "boolean", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    OGRN = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    TIN = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    KPP = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scopes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempStudents",
                columns: table => new
                {
                    DeanCode = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: true),
                    GroupName = table.Column<string>(type: "text", nullable: false),
                    Rank = table.Column<double>(type: "double precision", nullable: false),
                    Birthday = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempStudents", x => x.DeanCode);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    DeanCode = table.Column<int>(type: "integer", nullable: true),
                    ParentId = table.Column<string>(type: "text", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Units_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Patronymic = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PasswordSalt = table.Column<string>(type: "text", nullable: false),
                    IsTemporaryPassword = table.Column<bool>(type: "boolean", nullable: false),
                    BirthOfDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsConfirmedUser = table.Column<bool>(type: "boolean", nullable: false),
                    GenderId = table.Column<string>(type: "text", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClaimTypeSettings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ClaimTypeId = table.Column<string>(type: "text", nullable: false),
                    ScopeId = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimTypeSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaimTypeSettings_ClaimTypes_ClaimTypeId",
                        column: x => x.ClaimTypeId,
                        principalTable: "ClaimTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClaimTypeSettings_Scopes_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "Scopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentUnits",
                columns: table => new
                {
                    UnitId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentUnits", x => x.UnitId);
                    table.ForeignKey(
                        name: "FK_DepartmentUnits_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstituteUnits",
                columns: table => new
                {
                    UnitId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstituteUnits", x => x.UnitId);
                    table.ForeignKey(
                        name: "FK_InstituteUnits_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimTypeId = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_ClaimTypes_ClaimTypeId",
                        column: x => x.ClaimTypeId,
                        principalTable: "ClaimTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Claims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    OrganizationId = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partners_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partners_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partners_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pupils",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClassNumber = table.Column<int>(type: "integer", nullable: false),
                    ClassLitter = table.Column<char>(type: "character(1)", nullable: false),
                    SchoolId = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pupils", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pupils_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pupils_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkPlaces",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    UnitId = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkPlaces_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkPlaces_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkPlaces_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectionOfTraining",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    DeanCode = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    ImplementingChairId = table.Column<string>(type: "text", nullable: false),
                    DeveloperInstituteId = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectionOfTraining", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectionOfTraining_DepartmentUnits_ImplementingChairId",
                        column: x => x.ImplementingChairId,
                        principalTable: "DepartmentUnits",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectionOfTraining_InstituteUnits_DeveloperInstituteId",
                        column: x => x.DeveloperInstituteId,
                        principalTable: "InstituteUnits",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcademicGroups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    StartYear = table.Column<int>(type: "integer", nullable: false),
                    AcademicLevelId = table.Column<string>(type: "text", nullable: false),
                    AcademicFormId = table.Column<string>(type: "text", nullable: false),
                    DirectionOfTrainingId = table.Column<string>(type: "text", nullable: false),
                    DirectorateInstituteId = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicGroups_AcademicForms_AcademicFormId",
                        column: x => x.AcademicFormId,
                        principalTable: "AcademicForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicGroups_AcademicLevels_AcademicLevelId",
                        column: x => x.AcademicLevelId,
                        principalTable: "AcademicLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicGroups_DirectionOfTraining_DirectionOfTrainingId",
                        column: x => x.DirectionOfTrainingId,
                        principalTable: "DirectionOfTraining",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicGroups_InstituteUnits_DirectorateInstituteId",
                        column: x => x.DirectorateInstituteId,
                        principalTable: "InstituteUnits",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    DeanCode = table.Column<int>(type: "integer", nullable: false),
                    Rank = table.Column<double>(type: "double precision", nullable: false),
                    AcademicGroupId = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_AcademicGroups_AcademicGroupId",
                        column: x => x.AcademicGroupId,
                        principalTable: "AcademicGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClaimTypes",
                columns: new[] { "Id", "CreateAt", "IncludeInAccessToken", "IncludeInIdentityToken", "IsActive", "Name", "UpdateAt" },
                values: new object[,]
                {
                    { "01HSD738SG0AYHR2QJDJKM7E2C", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "birthdate", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG0G4821WN1VZGW3ZN", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "token_usage", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG16SD5JFG53X82SAX", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "zoneinfo", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG1BM7RYESSB3KG1JE", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "profile", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG1D8A2267BBGA35NF", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "middle_name", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG1JBG8974Q69F1A2Y", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "rfp", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG2ZEKSJ2ZMX40C3B0", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "role", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG3ZKTQB2MDXVM7FN7", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "c_hash", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG4MKQZ3S7QVW2YAW9", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "nbf", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG4RPZJ231694WB85D", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "acr", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG60S2B2ZV54QF1TYD", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "street_address", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG7QSRQQG4ZV26YWPA", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "phone_number_verified", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG7RY2YQADTS6GH20M", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "postal_code", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG8TY0KMCEG5VEGJEQ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "auth_time", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG9AXMAFBHBSW7H482", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "preferred_username", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG9XQTQWX81N58BESF", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "family_name", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGAPNNBY86P9B6TJJB", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "active", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGAQAYMK05VCQB8F09", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "kid", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGASQ781MVC1MMB85Z", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "sub", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGAXWFXZAPM9X9AV19", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "country", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGB372K9CACNFWN003", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "given_name", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGCB61SN13FKV80FHA", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "updated_at", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGCWAK8P8080G2AWMD", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "jti", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGDAR2HVJN4VRZCXXN", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "at_hash", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGEVM9YBM493HCQ981", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "locale", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGFQQXBMQX33F98PQ9", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "website", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGGKPS68GXW1J56WVC", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "aud", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGHQWS4BHV6PN3DRGY", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "name", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGJ8PP0G2WGNPE420X", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "gender", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGKHA138RP7FDFPX6H", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "token_type", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGKVZRDWAKH27RYMTR", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "client_id", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGKZTG470TCSY7MFPK", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "region", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGM4RN34B6T0V5ZHC8", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "nickname", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGMCK1HQ694X027PNH", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "as", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGMH6PQBEDQBQTHJF7", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "iss", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGN4381YD6TMM7DSSZ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "target_link_uri", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGNWM10FJK2MA0BHT7", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "locality", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGP5EQ75GF11W5Z3R1", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "iat", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGQ37F2FYAE2D7AXZJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "formatted", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGRC3F051BTBPJXZC3", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "amr", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGRTQA0V5Q3MJG87BD", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "picture", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGSPQ2J37S9VPSS5F5", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "nonce", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGT8A6RW3AJEY2YQZK", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "exp", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGTY5PE7DQKVWEE4SR", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "scope", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGWZ03W6743Q291NJ5", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "phone_number", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGY11JTJ33VBRA809E", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, true, true, "azp", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGY4Z54XPV9SZBT1C7", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "username", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGYHWFBDBDF9RMZ1KA", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "email_verified", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGZKQ38YH6ZK83B0QB", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "address", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGZYB8QEJJJBHZX89B", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), false, true, true, "email", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "CreateAt", "IsActive", "Name", "UpdateAt" },
                values: new object[,]
                {
                    { "01HSD738SG8GGCBAAWDGH2RFKF", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "Мужской", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGC17HZE70XCW8WA6R", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "Женский", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "CreateAt", "FullName", "IsActive", "KPP", "OGRN", "ShortName", "TIN", "UpdateAt" },
                values: new object[] { "01HSD738SGCNRC89WGXT8S59SF", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), "Федеральное государственное бюджетное образовательное учреждение высшего образования «Сибирский государственный индустриальный университет»", true, "421701001", "1024201470908", "СибГИУ", "4216003509", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateAt", "IsActive", "Name", "UpdateAt" },
                values: new object[,]
                {
                    { "01HSD738SGVETBE7RK30RRZ8XA", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "Пользователь", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGW1ZJETSDH3AMKJG1", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "Администратор", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Scopes",
                columns: new[] { "Id", "CreateAt", "IsActive", "Name", "UpdateAt" },
                values: new object[,]
                {
                    { "01HSD738SG02WSRPD20D4C9EKJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "address", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG2R81Q0MK3GZ7S95J", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "openid", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG6M73ZK4XBF1ZD1F8", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "roles", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGAM54BA5K5NQ9QW3Q", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "email", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGTZ88MZ7DMH538BE8", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "offline_access", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGV3BMBGNW16Q0G8KJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "profile", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGVGK7HZEK09RD1DWN", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "phone", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "CreateAt", "DeanCode", "FullName", "IsActive", "ParentId", "ShortName", "UpdateAt" },
                values: new object[] { "01HSD738SG4T26TQHC3QTNP77M", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), null, "Федеральное государственное бюджетное образовательное учреждение высшего образования «Сибирский государственный индустриальный университет»", true, null, "СибГИУ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "ClaimTypeSettings",
                columns: new[] { "Id", "ClaimTypeId", "CreateAt", "IsActive", "ScopeId", "UpdateAt" },
                values: new object[,]
                {
                    { "01HSD738SG1EERF5RFG1732WB2", "01HSD738SGKZTG470TCSY7MFPK", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SG02WSRPD20D4C9EKJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG1JVH9J5MQ8GXBS74", "01HSD738SG9XQTQWX81N58BESF", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SGV3BMBGNW16Q0G8KJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG5PVFT3NXR72XNA9R", "01HSD738SG7RY2YQADTS6GH20M", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SG02WSRPD20D4C9EKJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG7TF7XGBVRBEWAAQ3", "01HSD738SG9AXMAFBHBSW7H482", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SGV3BMBGNW16Q0G8KJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG8QPP5VN8N0P3J5AN", "01HSD738SGNWM10FJK2MA0BHT7", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SG02WSRPD20D4C9EKJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SG8XKJAVA1YP1FYPJX", "01HSD738SG1D8A2267BBGA35NF", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SGV3BMBGNW16Q0G8KJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGAZJB080FDBE7HKR3", "01HSD738SGHQWS4BHV6PN3DRGY", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SGV3BMBGNW16Q0G8KJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGB82CX1NW7SY7N96Y", "01HSD738SGM4RN34B6T0V5ZHC8", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SGV3BMBGNW16Q0G8KJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGEPN3VTJJMVKMYFQM", "01HSD738SGB372K9CACNFWN003", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SGV3BMBGNW16Q0G8KJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGHPRFDJQH2Z8XBH9C", "01HSD738SG60S2B2ZV54QF1TYD", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SG02WSRPD20D4C9EKJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGKDTMEZMFXB3SG0CF", "01HSD738SG0AYHR2QJDJKM7E2C", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SGV3BMBGNW16Q0G8KJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGMYCC48HNRKXD08JE", "01HSD738SG7QSRQQG4ZV26YWPA", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SGVGK7HZEK09RD1DWN", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGNRXXG4T7050MCE2A", "01HSD738SGWZ03W6743Q291NJ5", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SGVGK7HZEK09RD1DWN", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGQM290RD3Z13WR92A", "01HSD738SGASQ781MVC1MMB85Z", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SG2R81Q0MK3GZ7S95J", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGR6SYH3BMBZA63MTF", "01HSD738SGRTQA0V5Q3MJG87BD", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SGV3BMBGNW16Q0G8KJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGR9N4FKVNV20C91G6", "01HSD738SGZYB8QEJJJBHZX89B", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SGAM54BA5K5NQ9QW3Q", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGSABQ6JBZN7QGCG3P", "01HSD738SGAXWFXZAPM9X9AV19", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SG02WSRPD20D4C9EKJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGSBS035P933M5CV8D", "01HSD738SGZKQ38YH6ZK83B0QB", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SG02WSRPD20D4C9EKJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGSJ53M4GY9C4CX8QH", "01HSD738SGCB61SN13FKV80FHA", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SGV3BMBGNW16Q0G8KJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGTKG93MMCW7G96CNY", "01HSD738SGFQQXBMQX33F98PQ9", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SGV3BMBGNW16Q0G8KJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGVHMDYSY8FWRPTQHK", "01HSD738SGJ8PP0G2WGNPE420X", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SGV3BMBGNW16Q0G8KJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGWC8S6GFRJHR9Z5ZD", "01HSD738SG1BM7RYESSB3KG1JE", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SGV3BMBGNW16Q0G8KJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGX72Q3CW75MM9HCFA", "01HSD738SG16SD5JFG53X82SAX", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SGV3BMBGNW16Q0G8KJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGY4JSYS832XT5ZBN6", "01HSD738SGYHWFBDBDF9RMZ1KA", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SGAM54BA5K5NQ9QW3Q", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) },
                    { "01HSD738SGYGJ5Q0W94NKZ0BD0", "01HSD738SGEVM9YBM493HCQ981", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), true, "01HSD738SGV3BMBGNW16Q0G8KJ", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthOfDate", "CreateAt", "Email", "EmailConfirmed", "FirstName", "GenderId", "IsActive", "IsConfirmedUser", "IsTemporaryPassword", "LastName", "Password", "PasswordSalt", "Patronymic", "PhoneNumber", "UpdateAt", "UserName" },
                values: new object[] { "01HSD7391ARHMG2JEQD0MGNFY8", new DateTimeOffset(new DateTime(2001, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), "admin@sibsiu.ru", true, "Admin", "01HSD738SG8GGCBAAWDGH2RFKF", true, true, false, "Admin", "bP8yMCscAfJFHtkMiAQHxDdz1WoNESV9iX+WWtjN03cZPTVrqXhTC2nHHLjc27uZ/hcsett3VzYa2uOZgtRgIw==", "Y/uwds2b2dji7OTpMaGjG0trtxD2APzVV94K68owey5lSbesPusaLE/zqsP7bKEHXFTHcFGPxgt3zJmGtRYwrQ==", "Admin", "+7-(900)-00-00-0000", new DateTimeOffset(new DateTime(2024, 3, 20, 5, 59, 40, 592, DateTimeKind.Unspecified).AddTicks(909), new TimeSpan(0, 0, 0, 0, 0)), "Admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "Version" },
                values: new object[] { "01HSD738SGW1ZJETSDH3AMKJG1", "01HSD7391ARHMG2JEQD0MGNFY8", 0L });

            migrationBuilder.CreateIndex(
                name: "FormDeanCodeIndex",
                table: "AcademicForms",
                column: "DeanCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "GroupNameIndex",
                table: "AcademicGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicGroups_AcademicFormId",
                table: "AcademicGroups",
                column: "AcademicFormId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicGroups_AcademicLevelId",
                table: "AcademicGroups",
                column: "AcademicLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicGroups_DirectionOfTrainingId",
                table: "AcademicGroups",
                column: "DirectionOfTrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicGroups_DirectorateInstituteId",
                table: "AcademicGroups",
                column: "DirectorateInstituteId");

            migrationBuilder.CreateIndex(
                name: "LevelDeanCodeIndex",
                table: "AcademicLevels",
                column: "DeanCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ClaimTypeId",
                table: "Claims",
                column: "ClaimTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_UserId",
                table: "Claims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimTypeSettings_ClaimTypeId",
                table: "ClaimTypeSettings",
                column: "ClaimTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimTypeSettings_ScopeId",
                table: "ClaimTypeSettings",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "DotDeanCodeIndex",
                table: "DirectionOfTraining",
                column: "DeanCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DirectionOfTraining_DeveloperInstituteId",
                table: "DirectionOfTraining",
                column: "DeveloperInstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectionOfTraining_ImplementingChairId",
                table: "DirectionOfTraining",
                column: "ImplementingChairId");

            migrationBuilder.CreateIndex(
                name: "OrganizationOGRNIndex",
                table: "Organizations",
                column: "OGRN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "OrganizationTINIndex",
                table: "Organizations",
                column: "TIN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partners_OrganizationId",
                table: "Partners",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_PostId",
                table: "Partners",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_UserId",
                table: "Partners",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "PostNameIndex",
                table: "Posts",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pupils_SchoolId",
                table: "Pupils",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Pupils_UserId",
                table: "Pupils",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_AcademicGroupId",
                table: "Students",
                column: "AcademicGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "StudentDeanCodeIndex",
                table: "Students",
                column: "DeanCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Units_ParentId",
                table: "Units",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderId",
                table: "Users",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlaces_PostId",
                table: "WorkPlaces",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlaces_UnitId",
                table: "WorkPlaces",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "UserWorkPlaceIndex",
                table: "WorkPlaces",
                columns: new[] { "UserId", "UnitId", "PostId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "ClaimTypeSettings");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "Pupils");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "TempStudents");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "WorkPlaces");

            migrationBuilder.DropTable(
                name: "ClaimTypes");

            migrationBuilder.DropTable(
                name: "Scopes");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "AcademicGroups");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AcademicForms");

            migrationBuilder.DropTable(
                name: "AcademicLevels");

            migrationBuilder.DropTable(
                name: "DirectionOfTraining");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "DepartmentUnits");

            migrationBuilder.DropTable(
                name: "InstituteUnits");

            migrationBuilder.DropTable(
                name: "Units");
        }
    }
}
