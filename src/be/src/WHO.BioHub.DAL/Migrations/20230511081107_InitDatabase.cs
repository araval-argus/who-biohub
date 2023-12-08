using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WHO.BioHub.DAL.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Annex2OfSMTA2Conditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    PointNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Condition = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Current = table.Column<bool>(type: "bit", nullable: false),
                    Mandatory = table.Column<bool>(type: "bit", nullable: false),
                    Selectable = table.Column<bool>(type: "bit", nullable: false),
                    FlagPresetValue = table.Column<bool>(type: "bit", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Annex2OfSMTA2Conditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BiosafetyChecklistOfSMTA2s",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FlagPresetValue = table.Column<bool>(type: "bit", nullable: true),
                    Current = table.Column<bool>(type: "bit", nullable: false),
                    Mandatory = table.Column<bool>(type: "bit", nullable: false),
                    Selectable = table.Column<bool>(type: "bit", nullable: false),
                    IsParentCondition = table.Column<bool>(type: "bit", nullable: false),
                    ParentConditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShowOnParentValue = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiosafetyChecklistOfSMTA2s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BSLLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BSLLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Uncode = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Iso2 = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
                    Iso3 = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    GmtHour = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    GmtMin = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CultivabilityTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CultivabilityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneticSequenceDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneticSequenceDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InternationalTaxonomyClassifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternationalTaxonomyClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IsolationHostTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IsolationHostTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IsolationTechniqueTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IsolationTechniqueTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialUsagePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialUsagePermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriorityRequestTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    HexColor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorityRequestTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PublicName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RoleType = table.Column<int>(type: "int", nullable: false),
                    AddToRegistration = table.Column<bool>(type: "bit", nullable: false),
                    OnBehalfOf = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecimenTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecimenTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemperatureUnitOfMeasures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureUnitOfMeasures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    HexColor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportModes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    HexColor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportModes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRequestStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsResponseMessage = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRequestStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorklistItemUsedReferenceNumbers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsPast = table.Column<bool>(type: "bit", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistItemUsedReferenceNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BioHubFacilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Abbreviation = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BSLLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LaboratoryTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPublicFacing = table.Column<bool>(type: "bit", nullable: false),
                    LastOperationByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BioHubFacilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BioHubFacilities_BSLLevels_BSLLevelId",
                        column: x => x.BSLLevelId,
                        principalTable: "BSLLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BioHubFacilities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Couriers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WHOAccountNumber = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BusinessPhone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Couriers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Couriers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Laboratories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Abbreviation = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BSLLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPublicFacing = table.Column<bool>(type: "bit", nullable: false),
                    LastOperationByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Laboratories_BSLLevels_BSLLevelId",
                        column: x => x.BSLLevelId,
                        principalTable: "BSLLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Laboratories_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SMTA1WorkflowEmails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromStatus = table.Column<int>(type: "int", nullable: false),
                    ToStatus = table.Column<int>(type: "int", nullable: false),
                    ApprovedSubmission = table.Column<bool>(type: "bit", nullable: false),
                    EmailSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCourier = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMTA1WorkflowEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SMTA1WorkflowEmails_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SMTA2WorkflowEmails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromStatus = table.Column<int>(type: "int", nullable: false),
                    ToStatus = table.Column<int>(type: "int", nullable: false),
                    ApprovedSubmission = table.Column<bool>(type: "bit", nullable: false),
                    EmailSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCourier = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMTA2WorkflowEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SMTA2WorkflowEmails_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorklistFromBioHubEmails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromStatus = table.Column<int>(type: "int", nullable: false),
                    ToStatus = table.Column<int>(type: "int", nullable: false),
                    ApprovedSubmission = table.Column<bool>(type: "bit", nullable: false),
                    EmailSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCourier = table.Column<bool>(type: "bit", nullable: false),
                    IsNumberOfVialsWarning = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistFromBioHubEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubEmails_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorklistToBioHubEmails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromStatus = table.Column<int>(type: "int", nullable: false),
                    ToStatus = table.Column<int>(type: "int", nullable: false),
                    ApprovedSubmission = table.Column<bool>(type: "bit", nullable: false),
                    EmailSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCourier = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistToBioHubEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubEmails_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BioHubFacilitiesHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Abbreviation = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BSLLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LaboratoryTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPublicFacing = table.Column<bool>(type: "bit", nullable: false),
                    BioHubFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BioHubFacilitiesHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BioHubFacilitiesHistory_BioHubFacilities_BioHubFacilityId",
                        column: x => x.BioHubFacilityId,
                        principalTable: "BioHubFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BioHubFacilitiesHistory_BSLLevels_BSLLevelId",
                        column: x => x.BSLLevelId,
                        principalTable: "BSLLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BioHubFacilitiesHistory_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CouriersHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WHOAccountNumber = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BusinessPhone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CourierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouriersHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CouriersHistory_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CouriersHistory_Couriers_CourierId",
                        column: x => x.CourierId,
                        principalTable: "Couriers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LaboratoriesHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Abbreviation = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BSLLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPublicFacing = table.Column<bool>(type: "bit", nullable: false),
                    LastOperationByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoriesHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaboratoriesHistory_BSLLevels_BSLLevelId",
                        column: x => x.BSLLevelId,
                        principalTable: "BSLLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LaboratoriesHistory_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LaboratoriesHistory_Laboratories_LaboratoryId",
                        column: x => x.LaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TermsAndConditionAccepted = table.Column<bool>(type: "bit", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    InstituteName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ConfirmationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRequests_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRequests_Laboratories_LaboratoryId",
                        column: x => x.LaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRequests_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BusinessPhone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationalFocalPoint = table.Column<bool>(type: "bit", nullable: false),
                    LaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BioHubFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_BioHubFacilities_BioHubFacilityId",
                        column: x => x.BioHubFacilityId,
                        principalTable: "BioHubFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Couriers_CourierId",
                        column: x => x.CourierId,
                        principalTable: "Couriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Laboratories_LaboratoryId",
                        column: x => x.LaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FileType = table.Column<int>(type: "int", nullable: true),
                    Current = table.Column<bool>(type: "bit", nullable: true),
                    UploadTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UploadedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentTemplates_Users_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SuspectedEpidemiologicalOriginId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OriginalProductTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransportCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Temperature = table.Column<double>(type: "float", nullable: true),
                    UnitOfMeasureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UsagePermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SampleId = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Lineage = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Variant = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    VariantAssessment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StrainDesignation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Genotype = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Serotype = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GeneticSequenceDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DatabaseAccessionId = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OriginalGeneticSequence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityGSD = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    InternationalTaxonomyClassificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GMO = table.Column<bool>(type: "bit", nullable: false),
                    IsolationHostTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CultivabilityTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductionCellLine = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsolationTechniqueTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Infectivity = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ViralTiter = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsNew = table.Column<bool>(type: "bit", nullable: false),
                    ProviderLaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProviderBioHubFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CollectionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    PatientStatus = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OwnerBioHubFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShipmentNumberOfVials = table.Column<int>(type: "int", nullable: true),
                    CurrentNumberOfVials = table.Column<int>(type: "int", nullable: true),
                    WarningEmailCurrentNumberOfVialsThreshold = table.Column<int>(type: "int", nullable: true),
                    ManualCreation = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReferenceNumberValidation = table.Column<int>(type: "int", nullable: true),
                    NameValidation = table.Column<int>(type: "int", nullable: true),
                    DescriptionValidation = table.Column<int>(type: "int", nullable: true),
                    TypeValidation = table.Column<int>(type: "int", nullable: true),
                    SuspectedEpidemiologicalOriginValidation = table.Column<int>(type: "int", nullable: true),
                    OriginalProductTypeValidation = table.Column<int>(type: "int", nullable: true),
                    TransportCategoryValidation = table.Column<int>(type: "int", nullable: true),
                    TemperatureValidation = table.Column<int>(type: "int", nullable: true),
                    UnitOfMeasureValidation = table.Column<int>(type: "int", nullable: true),
                    UsagePermissionValidation = table.Column<int>(type: "int", nullable: true),
                    SampleIdValidation = table.Column<int>(type: "int", nullable: true),
                    LineageValidation = table.Column<int>(type: "int", nullable: true),
                    VariantValidation = table.Column<int>(type: "int", nullable: true),
                    VariantAssessmentValidation = table.Column<int>(type: "int", nullable: true),
                    StrainDesignationValidation = table.Column<int>(type: "int", nullable: true),
                    GenotypeValidation = table.Column<int>(type: "int", nullable: true),
                    SerotypeValidation = table.Column<int>(type: "int", nullable: true),
                    GeneticSequenceDataValidation = table.Column<int>(type: "int", nullable: true),
                    DatabaseAccessionIdValidation = table.Column<int>(type: "int", nullable: true),
                    OriginalGeneticSequenceValidation = table.Column<int>(type: "int", nullable: true),
                    FacilityGSDValidation = table.Column<int>(type: "int", nullable: true),
                    InternationalTaxonomyClassificationValidation = table.Column<int>(type: "int", nullable: true),
                    GMOValidation = table.Column<int>(type: "int", nullable: true),
                    IsolationHostTypeValidation = table.Column<int>(type: "int", nullable: true),
                    CultivabilityTypeValidation = table.Column<int>(type: "int", nullable: true),
                    ProductionCellLineValidation = table.Column<int>(type: "int", nullable: true),
                    IsolationTechniqueTypeValidation = table.Column<int>(type: "int", nullable: true),
                    InfectivityValidation = table.Column<int>(type: "int", nullable: true),
                    ViralTiterValidation = table.Column<int>(type: "int", nullable: true),
                    CollectionDateValidation = table.Column<int>(type: "int", nullable: true),
                    LocationValidation = table.Column<int>(type: "int", nullable: true),
                    GenderValidation = table.Column<int>(type: "int", nullable: true),
                    AgeValidation = table.Column<int>(type: "int", nullable: true),
                    PatientStatusValidation = table.Column<int>(type: "int", nullable: true),
                    ReferenceNumberComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    NameComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DescriptionComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TypeComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SuspectedEpidemiologicalOriginComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OriginalProductTypeComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TransportCategoryComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TemperatureComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UnitOfMeasureComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UsagePermissionComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SampleIdComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LineageComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    VariantComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    VariantAssessmentComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StrainDesignationComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GenotypeComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SerotypeComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GeneticSequenceDataComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DatabaseAccessionIdComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OriginalGeneticSequenceComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FacilityGSDComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    InternationalTaxonomyClassificationComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GMOComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsolationHostTypeComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CultivabilityTypeComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ProductionCellLineComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsolationTechniqueTypeComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    InfectivityComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ViralTiterComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CollectionDateComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LocationComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GenderComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AgeComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PatientStatusComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LastOperationById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfBMEPPReceipt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BHFShareReadiness = table.Column<int>(type: "int", nullable: true),
                    PublicShare = table.Column<int>(type: "int", nullable: true),
                    ProductTypeValidation = table.Column<int>(type: "int", nullable: true),
                    ProductTypeComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Pathogen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PathogenValidation = table.Column<int>(type: "int", nullable: true),
                    PathogenComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FreezingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VirusConcentration = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FreezingDateValidation = table.Column<int>(type: "int", nullable: true),
                    VirusConcentrationValidation = table.Column<int>(type: "int", nullable: true),
                    FreezingDateComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    VirusConcentrationComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ShipmentAmount = table.Column<double>(type: "float", nullable: true),
                    ShipmentTemperature = table.Column<double>(type: "float", nullable: true),
                    ShipmentUnitOfMeasureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CulturingCellLine = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CulturingPassagesNumber = table.Column<int>(type: "int", nullable: true),
                    TypeOfTransportMedium = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BrandOfTransportMedium = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DatabaseUploadedBy = table.Column<int>(type: "int", nullable: true),
                    ShipmentNumberOfVialsValidation = table.Column<int>(type: "int", nullable: true),
                    ShipmentAmountValidation = table.Column<int>(type: "int", nullable: true),
                    ShipmentTemperatureValidation = table.Column<int>(type: "int", nullable: true),
                    ShipmentUnitOfMeasureValidation = table.Column<int>(type: "int", nullable: true),
                    CulturingCellLineValidation = table.Column<int>(type: "int", nullable: true),
                    CulturingPassagesNumberValidation = table.Column<int>(type: "int", nullable: true),
                    TypeOfTransportMediumValidation = table.Column<int>(type: "int", nullable: true),
                    BrandOfTransportMediumValidation = table.Column<int>(type: "int", nullable: true),
                    MaterialCollectedSpecimenTypesValidation = table.Column<int>(type: "int", nullable: true),
                    DatabaseUploadedByValidation = table.Column<int>(type: "int", nullable: true),
                    ShipmentNumberOfVialsComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ShipmentAmountComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ShipmentTemperatureComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ShipmentUnitOfMeasureComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CulturingCellLineComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CulturingPassagesNumberComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TypeOfTransportMediumComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BrandOfTransportMediumComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    MaterialCollectedSpecimenTypesComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DatabaseUploadedByComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CulturingResult = table.Column<int>(type: "int", nullable: true),
                    CulturingResultDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QualityControlResult = table.Column<int>(type: "int", nullable: true),
                    QualityControlResultDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GSDAnalysisResult = table.Column<int>(type: "int", nullable: true),
                    GSDAnalysisResultDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GSDUploadingStatus = table.Column<int>(type: "int", nullable: true),
                    GSDUploadingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CulturingResultValidation = table.Column<int>(type: "int", nullable: true),
                    CulturingResultDateValidation = table.Column<int>(type: "int", nullable: true),
                    QualityControlResultValidation = table.Column<int>(type: "int", nullable: true),
                    QualityControlResultDateValidation = table.Column<int>(type: "int", nullable: true),
                    GSDAnalysisResultValidation = table.Column<int>(type: "int", nullable: true),
                    GSDAnalysisResultDateValidation = table.Column<int>(type: "int", nullable: true),
                    GSDUploadingStatusValidation = table.Column<int>(type: "int", nullable: true),
                    GSDUploadingDateValidation = table.Column<int>(type: "int", nullable: true),
                    CulturingResultComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CulturingResultDateComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    QualityControlResultComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    QualityControlResultDateComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GSDAnalysisResultComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GSDAnalysisResultDateComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GSDUploadingStatusComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GSDUploadingDateComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AddedAliquots = table.Column<int>(type: "int", nullable: true),
                    LastAliquotsAdditionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaterialGSDInfoValidation = table.Column<int>(type: "int", nullable: true),
                    MaterialGSDInfoComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OwnerBioHubFacilityValidation = table.Column<int>(type: "int", nullable: true),
                    OwnerBioHubFacilityComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DateOfBMEPPReceiptValidation = table.Column<int>(type: "int", nullable: true),
                    DateOfBMEPPReceiptComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StartingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPast = table.Column<bool>(type: "bit", nullable: false),
                    ShipmentMaterialCondition = table.Column<int>(type: "int", nullable: true),
                    ShipmentMaterialConditionNote = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ShipmentMaterialConditionValidation = table.Column<int>(type: "int", nullable: true),
                    ShipmentMaterialConditionComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_BioHubFacilities_OwnerBioHubFacilityId",
                        column: x => x.OwnerBioHubFacilityId,
                        principalTable: "BioHubFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_BioHubFacilities_ProviderBioHubFacilityId",
                        column: x => x.ProviderBioHubFacilityId,
                        principalTable: "BioHubFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_Countries_SuspectedEpidemiologicalOriginId",
                        column: x => x.SuspectedEpidemiologicalOriginId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_CultivabilityTypes_CultivabilityTypeId",
                        column: x => x.CultivabilityTypeId,
                        principalTable: "CultivabilityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_GeneticSequenceDatas_GeneticSequenceDataId",
                        column: x => x.GeneticSequenceDataId,
                        principalTable: "GeneticSequenceDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_InternationalTaxonomyClassifications_InternationalTaxonomyClassificationId",
                        column: x => x.InternationalTaxonomyClassificationId,
                        principalTable: "InternationalTaxonomyClassifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_IsolationHostTypes_IsolationHostTypeId",
                        column: x => x.IsolationHostTypeId,
                        principalTable: "IsolationHostTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_IsolationTechniqueTypes_IsolationTechniqueTypeId",
                        column: x => x.IsolationTechniqueTypeId,
                        principalTable: "IsolationTechniqueTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_Laboratories_ProviderLaboratoryId",
                        column: x => x.ProviderLaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_MaterialProducts_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "MaterialProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_MaterialTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "MaterialTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_MaterialUsagePermissions_UsagePermissionId",
                        column: x => x.UsagePermissionId,
                        principalTable: "MaterialUsagePermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_TemperatureUnitOfMeasures_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "TemperatureUnitOfMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_TransportCategories_TransportCategoryId",
                        column: x => x.TransportCategoryId,
                        principalTable: "TransportCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_Users_LastOperationById",
                        column: x => x.LastOperationById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    UploadTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UploadedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FileType = table.Column<int>(type: "int", nullable: true),
                    Current = table.Column<bool>(type: "bit", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_Users_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SMTA1WorkflowItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PreviousStatus = table.Column<int>(type: "int", nullable: false),
                    WorkflowItemTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    LaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastSubmissionApproved = table.Column<bool>(type: "bit", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPast = table.Column<bool>(type: "bit", nullable: true),
                    BioHubFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMTA1WorkflowItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SMTA1WorkflowItems_BioHubFacilities_BioHubFacilityId",
                        column: x => x.BioHubFacilityId,
                        principalTable: "BioHubFacilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SMTA1WorkflowItems_Laboratories_LaboratoryId",
                        column: x => x.LaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SMTA1WorkflowItems_Users_LastOperationUserId",
                        column: x => x.LastOperationUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SMTA2WorkflowItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PreviousStatus = table.Column<int>(type: "int", nullable: false),
                    WorkflowItemTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    LaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastSubmissionApproved = table.Column<bool>(type: "bit", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPast = table.Column<bool>(type: "bit", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMTA2WorkflowItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SMTA2WorkflowItems_Laboratories_LaboratoryId",
                        column: x => x.LaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SMTA2WorkflowItems_Users_LastOperationUserId",
                        column: x => x.LastOperationUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BusinessPhone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationalFocalPoint = table.Column<bool>(type: "bit", nullable: false),
                    LaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BioHubFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersHistory_BioHubFacilities_BioHubFacilityId",
                        column: x => x.BioHubFacilityId,
                        principalTable: "BioHubFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersHistory_Couriers_CourierId",
                        column: x => x.CourierId,
                        principalTable: "Couriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersHistory_Laboratories_LaboratoryId",
                        column: x => x.LaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersHistory_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersHistory_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorklistFromBioHubItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PreviousStatus = table.Column<int>(type: "int", nullable: false),
                    WorklistItemTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    RequestInitiationToLaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestInitiationFromBioHubFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastSubmissionApproved = table.Column<bool>(type: "bit", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Annex2Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Annex2TermsAndConditions = table.Column<bool>(type: "bit", nullable: true),
                    Annex2FillingOption = table.Column<int>(type: "int", nullable: true),
                    Annex2ApprovalFlag = table.Column<bool>(type: "bit", nullable: true),
                    Annex2ApprovalComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Annex2OfSMTA2ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OriginalAnnex2OfSMTA2DocumentTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BiosafetyChecklistFillingOption = table.Column<int>(type: "int", nullable: true),
                    BiosafetyChecklistApprovalFlag = table.Column<bool>(type: "bit", nullable: true),
                    BiosafetyChecklistApprovalComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BiosafetyChecklistApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OriginalBiosafetyChecklistDocumentTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BookingFormFillingOption = table.Column<int>(type: "int", nullable: true),
                    BookingFormApprovalFlag = table.Column<bool>(type: "bit", nullable: true),
                    BookingFormApprovalComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BookingFormOfSMTA2ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OriginalBookingFormOfSMTA2DocumentTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WaitForArrivalConditionCheckApprovalFlag = table.Column<bool>(type: "bit", nullable: true),
                    WaitForArrivalConditionCheckApprovalComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WHODocumentRegistrationNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SavedBiosafetyChecklistThreadComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsPast = table.Column<bool>(type: "bit", nullable: true),
                    Annex2OfSMTA2SignatureText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BookingFormOfSMTA2SignatureText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BiosafetyChecklistOfSMTA2SignatureText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistFromBioHubItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubItems_BioHubFacilities_RequestInitiationFromBioHubFacilityId",
                        column: x => x.RequestInitiationFromBioHubFacilityId,
                        principalTable: "BioHubFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubItems_Laboratories_RequestInitiationToLaboratoryId",
                        column: x => x.RequestInitiationToLaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubItems_Users_LastOperationUserId",
                        column: x => x.LastOperationUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorklistToBioHubItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PreviousStatus = table.Column<int>(type: "int", nullable: false),
                    WorklistItemTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    RequestInitiationFromLaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestInitiationToBioHubFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastSubmissionApproved = table.Column<bool>(type: "bit", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Annex2Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Annex2TermsAndConditions = table.Column<bool>(type: "bit", nullable: true),
                    Annex2FillingOption = table.Column<int>(type: "int", nullable: true),
                    Annex2ApprovalFlag = table.Column<bool>(type: "bit", nullable: true),
                    Annex2ApprovalComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Annex2OfSMTA1ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OriginalAnnex2OfSMTA1DocumentTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BookingFormFillingOption = table.Column<int>(type: "int", nullable: true),
                    BookingFormApprovalFlag = table.Column<bool>(type: "bit", nullable: true),
                    BookingFormApprovalComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BookingFormOfSMTA1ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OriginalBookingFormOfSMTA1DocumentTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WaitForArrivalConditionCheckApprovalFlag = table.Column<bool>(type: "bit", nullable: true),
                    WaitForArrivalConditionCheckApprovalComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WHODocumentRegistrationNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPast = table.Column<bool>(type: "bit", nullable: true),
                    Annex2OfSMTA1SignatureText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BookingFormOfSMTA1SignatureText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistToBioHubItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubItems_BioHubFacilities_RequestInitiationToBioHubFacilityId",
                        column: x => x.RequestInitiationToBioHubFacilityId,
                        principalTable: "BioHubFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubItems_Laboratories_RequestInitiationFromLaboratoryId",
                        column: x => x.RequestInitiationFromLaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubItems_Users_LastOperationUserId",
                        column: x => x.LastOperationUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: true),
                    OriginalDocumentTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDocumentFile = table.Column<bool>(type: "bit", nullable: false),
                    Base64String = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BioHubFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_BioHubFacilities_BioHubFacilityId",
                        column: x => x.BioHubFacilityId,
                        principalTable: "BioHubFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentTemplates_OriginalDocumentTemplateId",
                        column: x => x.OriginalDocumentTemplateId,
                        principalTable: "DocumentTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_Laboratories_LaboratoryId",
                        column: x => x.LaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_Users_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MaterialCollectedSpecimenTypes",
                columns: table => new
                {
                    SpecimenTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialCollectedSpecimenTypes", x => new { x.MaterialId, x.SpecimenTypeId });
                    table.ForeignKey(
                        name: "FK_MaterialCollectedSpecimenTypes_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialCollectedSpecimenTypes_SpecimenTypes_SpecimenTypeId",
                        column: x => x.SpecimenTypeId,
                        principalTable: "SpecimenTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialGSDInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GSDType = table.Column<int>(type: "int", nullable: true),
                    CellLine = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PassageNumber = table.Column<int>(type: "int", nullable: true),
                    GSDFasta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialGSDInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialGSDInfo_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialsHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SuspectedEpidemiologicalOriginId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OriginalProductTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransportCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Temperature = table.Column<double>(type: "float", nullable: true),
                    UnitOfMeasureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UsagePermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SampleId = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Lineage = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Variant = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    VariantAssessment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StrainDesignation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Genotype = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Serotype = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GeneticSequenceDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DatabaseAccessionId = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OriginalGeneticSequence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityGSD = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    InternationalTaxonomyClassificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GMO = table.Column<bool>(type: "bit", nullable: false),
                    IsolationHostTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CultivabilityTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductionCellLine = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsolationTechniqueTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Infectivity = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ViralTiter = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsNew = table.Column<bool>(type: "bit", nullable: false),
                    ProviderLaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProviderBioHubFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CollectionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    PatientStatus = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OwnerBioHubFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShipmentNumberOfVials = table.Column<int>(type: "int", nullable: true),
                    CurrentNumberOfVials = table.Column<int>(type: "int", nullable: true),
                    WarningEmailCurrentNumberOfVialsThreshold = table.Column<int>(type: "int", nullable: true),
                    ManualCreation = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReferenceNumberValidation = table.Column<int>(type: "int", nullable: true),
                    NameValidation = table.Column<int>(type: "int", nullable: true),
                    DescriptionValidation = table.Column<int>(type: "int", nullable: true),
                    TypeValidation = table.Column<int>(type: "int", nullable: true),
                    SuspectedEpidemiologicalOriginValidation = table.Column<int>(type: "int", nullable: true),
                    OriginalProductTypeValidation = table.Column<int>(type: "int", nullable: true),
                    TransportCategoryValidation = table.Column<int>(type: "int", nullable: true),
                    TemperatureValidation = table.Column<int>(type: "int", nullable: true),
                    UnitOfMeasureValidation = table.Column<int>(type: "int", nullable: true),
                    UsagePermissionValidation = table.Column<int>(type: "int", nullable: true),
                    SampleIdValidation = table.Column<int>(type: "int", nullable: true),
                    LineageValidation = table.Column<int>(type: "int", nullable: true),
                    VariantValidation = table.Column<int>(type: "int", nullable: true),
                    VariantAssessmentValidation = table.Column<int>(type: "int", nullable: true),
                    StrainDesignationValidation = table.Column<int>(type: "int", nullable: true),
                    GenotypeValidation = table.Column<int>(type: "int", nullable: true),
                    SerotypeValidation = table.Column<int>(type: "int", nullable: true),
                    GeneticSequenceDataValidation = table.Column<int>(type: "int", nullable: true),
                    DatabaseAccessionIdValidation = table.Column<int>(type: "int", nullable: true),
                    OriginalGeneticSequenceValidation = table.Column<int>(type: "int", nullable: true),
                    FacilityGSDValidation = table.Column<int>(type: "int", nullable: true),
                    InternationalTaxonomyClassificationValidation = table.Column<int>(type: "int", nullable: true),
                    GMOValidation = table.Column<int>(type: "int", nullable: true),
                    IsolationHostTypeValidation = table.Column<int>(type: "int", nullable: true),
                    CultivabilityTypeValidation = table.Column<int>(type: "int", nullable: true),
                    ProductionCellLineValidation = table.Column<int>(type: "int", nullable: true),
                    IsolationTechniqueTypeValidation = table.Column<int>(type: "int", nullable: true),
                    InfectivityValidation = table.Column<int>(type: "int", nullable: true),
                    ViralTiterValidation = table.Column<int>(type: "int", nullable: true),
                    CollectionDateValidation = table.Column<int>(type: "int", nullable: true),
                    LocationValidation = table.Column<int>(type: "int", nullable: true),
                    GenderValidation = table.Column<int>(type: "int", nullable: true),
                    AgeValidation = table.Column<int>(type: "int", nullable: true),
                    PatientStatusValidation = table.Column<int>(type: "int", nullable: true),
                    ReferenceNumberComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    NameComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DescriptionComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TypeComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SuspectedEpidemiologicalOriginComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OriginalProductTypeComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TransportCategoryComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TemperatureComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UnitOfMeasureComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UsagePermissionComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SampleIdComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LineageComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    VariantComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    VariantAssessmentComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StrainDesignationComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GenotypeComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SerotypeComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GeneticSequenceDataComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DatabaseAccessionIdComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OriginalGeneticSequenceComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FacilityGSDComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    InternationalTaxonomyClassificationComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GMOComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsolationHostTypeComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CultivabilityTypeComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ProductionCellLineComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsolationTechniqueTypeComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    InfectivityComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ViralTiterComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CollectionDateComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LocationComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GenderComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AgeComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PatientStatusComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LastOperationById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BHFShareReadiness = table.Column<int>(type: "int", nullable: true),
                    PublicShare = table.Column<int>(type: "int", nullable: true),
                    ProductTypeValidation = table.Column<int>(type: "int", nullable: true),
                    ProductTypeComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Pathogen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PathogenValidation = table.Column<int>(type: "int", nullable: true),
                    PathogenComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FreezingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VirusConcentration = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FreezingDateValidation = table.Column<int>(type: "int", nullable: true),
                    VirusConcentrationValidation = table.Column<int>(type: "int", nullable: true),
                    FreezingDateComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    VirusConcentrationComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ShipmentAmount = table.Column<double>(type: "float", nullable: true),
                    ShipmentTemperature = table.Column<double>(type: "float", nullable: true),
                    ShipmentUnitOfMeasureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CulturingCellLine = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CulturingPassagesNumber = table.Column<int>(type: "int", nullable: true),
                    TypeOfTransportMedium = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BrandOfTransportMedium = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DatabaseUploadedBy = table.Column<int>(type: "int", nullable: true),
                    ShipmentNumberOfVialsValidation = table.Column<int>(type: "int", nullable: true),
                    ShipmentAmountValidation = table.Column<int>(type: "int", nullable: true),
                    ShipmentTemperatureValidation = table.Column<int>(type: "int", nullable: true),
                    ShipmentUnitOfMeasureValidation = table.Column<int>(type: "int", nullable: true),
                    CulturingCellLineValidation = table.Column<int>(type: "int", nullable: true),
                    CulturingPassagesNumberValidation = table.Column<int>(type: "int", nullable: true),
                    TypeOfTransportMediumValidation = table.Column<int>(type: "int", nullable: true),
                    BrandOfTransportMediumValidation = table.Column<int>(type: "int", nullable: true),
                    MaterialCollectedSpecimenTypesValidation = table.Column<int>(type: "int", nullable: true),
                    DatabaseUploadedByValidation = table.Column<int>(type: "int", nullable: true),
                    ShipmentNumberOfVialsComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ShipmentAmountComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ShipmentTemperatureComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ShipmentUnitOfMeasureComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CulturingCellLineComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CulturingPassagesNumberComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TypeOfTransportMediumComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BrandOfTransportMediumComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    MaterialCollectedSpecimenTypesComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DatabaseUploadedByComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CulturingResult = table.Column<int>(type: "int", nullable: true),
                    CulturingResultDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QualityControlResult = table.Column<int>(type: "int", nullable: true),
                    QualityControlResultDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GSDAnalysisResult = table.Column<int>(type: "int", nullable: true),
                    GSDAnalysisResultDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GSDUploadingStatus = table.Column<int>(type: "int", nullable: true),
                    GSDUploadingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CulturingResultValidation = table.Column<int>(type: "int", nullable: true),
                    CulturingResultDateValidation = table.Column<int>(type: "int", nullable: true),
                    QualityControlResultValidation = table.Column<int>(type: "int", nullable: true),
                    QualityControlResultDateValidation = table.Column<int>(type: "int", nullable: true),
                    GSDAnalysisResultValidation = table.Column<int>(type: "int", nullable: true),
                    GSDAnalysisResultDateValidation = table.Column<int>(type: "int", nullable: true),
                    GSDUploadingStatusValidation = table.Column<int>(type: "int", nullable: true),
                    GSDUploadingDateValidation = table.Column<int>(type: "int", nullable: true),
                    CulturingResultComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CulturingResultDateComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    QualityControlResultComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    QualityControlResultDateComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GSDAnalysisResultComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GSDAnalysisResultDateComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GSDUploadingStatusComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GSDUploadingDateComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LastAliquotsAdditionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaterialGSDInfoValidation = table.Column<int>(type: "int", nullable: true),
                    MaterialGSDInfoComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OwnerBioHubFacilityValidation = table.Column<int>(type: "int", nullable: true),
                    OwnerBioHubFacilityComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DateOfBMEPPReceiptValidation = table.Column<int>(type: "int", nullable: true),
                    DateOfBMEPPReceiptComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AddedAliquots = table.Column<int>(type: "int", nullable: true),
                    StartingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPast = table.Column<bool>(type: "bit", nullable: false),
                    ShipmentMaterialCondition = table.Column<int>(type: "int", nullable: true),
                    ShipmentMaterialConditionNote = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ShipmentMaterialConditionValidation = table.Column<int>(type: "int", nullable: true),
                    ShipmentMaterialConditionComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DateOfBMEPPReceipt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialsHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialsHistory_BioHubFacilities_OwnerBioHubFacilityId",
                        column: x => x.OwnerBioHubFacilityId,
                        principalTable: "BioHubFacilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialsHistory_BioHubFacilities_ProviderBioHubFacilityId",
                        column: x => x.ProviderBioHubFacilityId,
                        principalTable: "BioHubFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialsHistory_Countries_SuspectedEpidemiologicalOriginId",
                        column: x => x.SuspectedEpidemiologicalOriginId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialsHistory_CultivabilityTypes_CultivabilityTypeId",
                        column: x => x.CultivabilityTypeId,
                        principalTable: "CultivabilityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialsHistory_GeneticSequenceDatas_GeneticSequenceDataId",
                        column: x => x.GeneticSequenceDataId,
                        principalTable: "GeneticSequenceDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialsHistory_InternationalTaxonomyClassifications_InternationalTaxonomyClassificationId",
                        column: x => x.InternationalTaxonomyClassificationId,
                        principalTable: "InternationalTaxonomyClassifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialsHistory_IsolationHostTypes_IsolationHostTypeId",
                        column: x => x.IsolationHostTypeId,
                        principalTable: "IsolationHostTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialsHistory_IsolationTechniqueTypes_IsolationTechniqueTypeId",
                        column: x => x.IsolationTechniqueTypeId,
                        principalTable: "IsolationTechniqueTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialsHistory_Laboratories_ProviderLaboratoryId",
                        column: x => x.ProviderLaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialsHistory_MaterialProducts_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "MaterialProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialsHistory_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialsHistory_MaterialTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "MaterialTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialsHistory_MaterialUsagePermissions_UsagePermissionId",
                        column: x => x.UsagePermissionId,
                        principalTable: "MaterialUsagePermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialsHistory_TemperatureUnitOfMeasures_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "TemperatureUnitOfMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialsHistory_TransportCategories_TransportCategoryId",
                        column: x => x.TransportCategoryId,
                        principalTable: "TransportCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialsHistory_Users_LastOperationById",
                        column: x => x.LastOperationById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SMTA1WorkflowHistoryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PreviousStatus = table.Column<int>(type: "int", nullable: false),
                    WorkflowItemTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    LaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastSubmissionApproved = table.Column<bool>(type: "bit", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SMTA1WorkflowItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPast = table.Column<bool>(type: "bit", nullable: true),
                    BioHubFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMTA1WorkflowHistoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SMTA1WorkflowHistoryItems_BioHubFacilities_BioHubFacilityId",
                        column: x => x.BioHubFacilityId,
                        principalTable: "BioHubFacilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SMTA1WorkflowHistoryItems_Laboratories_LaboratoryId",
                        column: x => x.LaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SMTA1WorkflowHistoryItems_SMTA1WorkflowItems_SMTA1WorkflowItemId",
                        column: x => x.SMTA1WorkflowItemId,
                        principalTable: "SMTA1WorkflowItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SMTA1WorkflowHistoryItems_Users_LastOperationUserId",
                        column: x => x.LastOperationUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SMTA2WorkflowHistoryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PreviousStatus = table.Column<int>(type: "int", nullable: false),
                    WorkflowItemTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    LaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastSubmissionApproved = table.Column<bool>(type: "bit", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SMTA2WorkflowItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPast = table.Column<bool>(type: "bit", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMTA2WorkflowHistoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SMTA2WorkflowHistoryItems_Laboratories_LaboratoryId",
                        column: x => x.LaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SMTA2WorkflowHistoryItems_SMTA2WorkflowItems_SMTA2WorkflowItemId",
                        column: x => x.SMTA2WorkflowItemId,
                        principalTable: "SMTA2WorkflowItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SMTA2WorkflowHistoryItems_Users_LastOperationUserId",
                        column: x => x.LastOperationUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorklistFromBioHubHistoryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PreviousStatus = table.Column<int>(type: "int", nullable: false),
                    WorklistItemTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    RequestInitiationToLaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestInitiationFromBioHubFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastSubmissionApproved = table.Column<bool>(type: "bit", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WorklistFromBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Annex2Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Annex2TermsAndConditions = table.Column<bool>(type: "bit", nullable: true),
                    Annex2FillingOption = table.Column<int>(type: "int", nullable: true),
                    Annex2ApprovalFlag = table.Column<bool>(type: "bit", nullable: true),
                    Annex2ApprovalComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Annex2OfSMTA2ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OriginalAnnex2OfSMTA2DocumentTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BiosafetyChecklistFillingOption = table.Column<int>(type: "int", nullable: true),
                    BiosafetyChecklistApprovalFlag = table.Column<bool>(type: "bit", nullable: true),
                    BiosafetyChecklistApprovalComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BiosafetyChecklistApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OriginalBiosafetyChecklistDocumentTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BookingFormFillingOption = table.Column<int>(type: "int", nullable: true),
                    BookingFormApprovalFlag = table.Column<bool>(type: "bit", nullable: true),
                    BookingFormApprovalComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BookingFormOfSMTA2ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OriginalBookingFormOfSMTA2DocumentTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WaitForArrivalConditionCheckApprovalFlag = table.Column<bool>(type: "bit", nullable: true),
                    WaitForArrivalConditionCheckApprovalComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WHODocumentRegistrationNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SavedBiosafetyChecklistThreadComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsPast = table.Column<bool>(type: "bit", nullable: true),
                    Annex2OfSMTA2SignatureText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BookingFormOfSMTA2SignatureText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BiosafetyChecklistOfSMTA2SignatureText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistFromBioHubHistoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubHistoryItems_BioHubFacilities_RequestInitiationFromBioHubFacilityId",
                        column: x => x.RequestInitiationFromBioHubFacilityId,
                        principalTable: "BioHubFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubHistoryItems_Laboratories_RequestInitiationToLaboratoryId",
                        column: x => x.RequestInitiationToLaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubHistoryItems_Users_LastOperationUserId",
                        column: x => x.LastOperationUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubHistoryItems_WorklistFromBioHubItems_WorklistFromBioHubItemId",
                        column: x => x.WorklistFromBioHubItemId,
                        principalTable: "WorklistFromBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorklistFromBioHubItemAnnex2OfSMTA2Conditions",
                columns: table => new
                {
                    WorklistFromBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Annex2OfSMTA2ConditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Flag = table.Column<bool>(type: "bit", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistFromBioHubItemAnnex2OfSMTA2Conditions", x => new { x.WorklistFromBioHubItemId, x.Annex2OfSMTA2ConditionId });
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubItemAnnex2OfSMTA2Conditions_Annex2OfSMTA2Conditions_Annex2OfSMTA2ConditionId",
                        column: x => x.Annex2OfSMTA2ConditionId,
                        principalTable: "Annex2OfSMTA2Conditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubItemAnnex2OfSMTA2Conditions_WorklistFromBioHubItems_WorklistFromBioHubItemId",
                        column: x => x.WorklistFromBioHubItemId,
                        principalTable: "WorklistFromBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s",
                columns: table => new
                {
                    WorklistFromBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BiosafetyChecklistOfSMTA2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Flag = table.Column<bool>(type: "bit", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s", x => new { x.WorklistFromBioHubItemId, x.BiosafetyChecklistOfSMTA2Id });
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s_BiosafetyChecklistOfSMTA2s_BiosafetyChecklistOfSMTA2Id",
                        column: x => x.BiosafetyChecklistOfSMTA2Id,
                        principalTable: "BiosafetyChecklistOfSMTA2s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s_WorklistFromBioHubItems_WorklistFromBioHubItemId",
                        column: x => x.WorklistFromBioHubItemId,
                        principalTable: "WorklistFromBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorklistFromBioHubItemBiosafetyChecklistThreadComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorklistFromBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistFromBioHubItemBiosafetyChecklistThreadComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubItemBiosafetyChecklistThreadComments_Users_PostedById",
                        column: x => x.PostedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubItemBiosafetyChecklistThreadComments_WorklistFromBioHubItems_WorklistFromBioHubItemId",
                        column: x => x.WorklistFromBioHubItemId,
                        principalTable: "WorklistFromBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorklistFromBioHubItemFeedback",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorklistFromBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistFromBioHubItemFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubItemFeedback_Users_PostedById",
                        column: x => x.PostedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubItemFeedback_WorklistFromBioHubItems_WorklistFromBioHubItemId",
                        column: x => x.WorklistFromBioHubItemId,
                        principalTable: "WorklistFromBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorklistFromBioHubItemLaboratoryFocalPoints",
                columns: table => new
                {
                    WorklistFromBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Other = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistFromBioHubItemLaboratoryFocalPoints", x => new { x.WorklistFromBioHubItemId, x.UserId });
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubItemLaboratoryFocalPoints_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubItemLaboratoryFocalPoints_WorklistFromBioHubItems_WorklistFromBioHubItemId",
                        column: x => x.WorklistFromBioHubItemId,
                        principalTable: "WorklistFromBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorklistFromBioHubItemMaterials",
                columns: table => new
                {
                    WorklistFromBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Condition = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistFromBioHubItemMaterials", x => new { x.WorklistFromBioHubItemId, x.MaterialId });
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubItemMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubItemMaterials_WorklistFromBioHubItems_WorklistFromBioHubItemId",
                        column: x => x.WorklistFromBioHubItemId,
                        principalTable: "WorklistFromBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorklistToBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorklistFromBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TotalNumberOfVials = table.Column<int>(type: "int", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaterialProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestDateOfPickup = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TemperatureTransportCondition = table.Column<int>(type: "int", nullable: true),
                    NumberOfInnerPackagingAndSize = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CourierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EstimateDateOfPickup = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfPickup = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShipmentReferenceNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DateOfDelivery = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransportCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransportModeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingForms_Couriers_CourierId",
                        column: x => x.CourierId,
                        principalTable: "Couriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingForms_MaterialProducts_MaterialProductId",
                        column: x => x.MaterialProductId,
                        principalTable: "MaterialProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingForms_TransportCategories_TransportCategoryId",
                        column: x => x.TransportCategoryId,
                        principalTable: "TransportCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingForms_TransportModes_TransportModeId",
                        column: x => x.TransportModeId,
                        principalTable: "TransportModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingForms_WorklistFromBioHubItems_WorklistFromBioHubItemId",
                        column: x => x.WorklistFromBioHubItemId,
                        principalTable: "WorklistFromBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingForms_WorklistToBioHubItems_WorklistToBioHubItemId",
                        column: x => x.WorklistToBioHubItemId,
                        principalTable: "WorklistToBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialShippingInformations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MaterialProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransportCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Condition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AdditionalInformation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WorklistToBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialShippingInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialShippingInformations_MaterialProducts_MaterialProductId",
                        column: x => x.MaterialProductId,
                        principalTable: "MaterialProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialShippingInformations_TransportCategories_TransportCategoryId",
                        column: x => x.TransportCategoryId,
                        principalTable: "TransportCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialShippingInformations_WorklistToBioHubItems_WorklistToBioHubItemId",
                        column: x => x.WorklistToBioHubItemId,
                        principalTable: "WorklistToBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorklistToBioHubHistoryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PreviousStatus = table.Column<int>(type: "int", nullable: false),
                    WorklistItemTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    RequestInitiationFromLaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestInitiationToBioHubFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOperationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastSubmissionApproved = table.Column<bool>(type: "bit", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WorklistToBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Annex2Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Annex2TermsAndConditions = table.Column<bool>(type: "bit", nullable: true),
                    Annex2FillingOption = table.Column<int>(type: "int", nullable: true),
                    Annex2ApprovalFlag = table.Column<bool>(type: "bit", nullable: true),
                    Annex2ApprovalComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Annex2OfSMTA1ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OriginalAnnex2OfSMTA1DocumentTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BookingFormFillingOption = table.Column<int>(type: "int", nullable: true),
                    BookingFormApprovalFlag = table.Column<bool>(type: "bit", nullable: true),
                    BookingFormApprovalComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BookingFormOfSMTA1ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OriginalBookingFormOfSMTA1DocumentTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WaitForArrivalConditionCheckApprovalFlag = table.Column<bool>(type: "bit", nullable: true),
                    WaitForArrivalConditionCheckApprovalComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WHODocumentRegistrationNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPast = table.Column<bool>(type: "bit", nullable: true),
                    Annex2OfSMTA1SignatureText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BookingFormOfSMTA1SignatureText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistToBioHubHistoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubHistoryItems_BioHubFacilities_RequestInitiationToBioHubFacilityId",
                        column: x => x.RequestInitiationToBioHubFacilityId,
                        principalTable: "BioHubFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubHistoryItems_Laboratories_RequestInitiationFromLaboratoryId",
                        column: x => x.RequestInitiationFromLaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubHistoryItems_Users_LastOperationUserId",
                        column: x => x.LastOperationUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubHistoryItems_WorklistToBioHubItems_WorklistToBioHubItemId",
                        column: x => x.WorklistToBioHubItemId,
                        principalTable: "WorklistToBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorklistToBioHubItemBioHubFacilityFocalPoints",
                columns: table => new
                {
                    WorklistToBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Other = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistToBioHubItemBioHubFacilityFocalPoints", x => new { x.WorklistToBioHubItemId, x.UserId });
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubItemBioHubFacilityFocalPoints_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubItemBioHubFacilityFocalPoints_WorklistToBioHubItems_WorklistToBioHubItemId",
                        column: x => x.WorklistToBioHubItemId,
                        principalTable: "WorklistToBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorklistToBioHubItemFeedback",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorklistToBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistToBioHubItemFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubItemFeedback_Users_PostedById",
                        column: x => x.PostedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubItemFeedback_WorklistToBioHubItems_WorklistToBioHubItemId",
                        column: x => x.WorklistToBioHubItemId,
                        principalTable: "WorklistToBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorklistToBioHubItemLaboratoryFocalPoints",
                columns: table => new
                {
                    WorklistToBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Other = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistToBioHubItemLaboratoryFocalPoints", x => new { x.WorklistToBioHubItemId, x.UserId });
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubItemLaboratoryFocalPoints_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubItemLaboratoryFocalPoints_WorklistToBioHubItems_WorklistToBioHubItemId",
                        column: x => x.WorklistToBioHubItemId,
                        principalTable: "WorklistToBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorklistToBioHubItemMaterials",
                columns: table => new
                {
                    WorklistToBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistToBioHubItemMaterials", x => new { x.WorklistToBioHubItemId, x.MaterialId });
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubItemMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubItemMaterials_WorklistToBioHubItems_WorklistToBioHubItemId",
                        column: x => x.WorklistToBioHubItemId,
                        principalTable: "WorklistToBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SMTA1WorkflowItemDocuments",
                columns: table => new
                {
                    SMTA1WorkflowItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsDocumentFile = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMTA1WorkflowItemDocuments", x => new { x.SMTA1WorkflowItemId, x.DocumentId });
                    table.ForeignKey(
                        name: "FK_SMTA1WorkflowItemDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SMTA1WorkflowItemDocuments_SMTA1WorkflowItems_SMTA1WorkflowItemId",
                        column: x => x.SMTA1WorkflowItemId,
                        principalTable: "SMTA1WorkflowItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SMTA2WorkflowItemDocuments",
                columns: table => new
                {
                    SMTA2WorkflowItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsDocumentFile = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMTA2WorkflowItemDocuments", x => new { x.SMTA2WorkflowItemId, x.DocumentId });
                    table.ForeignKey(
                        name: "FK_SMTA2WorkflowItemDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SMTA2WorkflowItemDocuments_SMTA2WorkflowItems_SMTA2WorkflowItemId",
                        column: x => x.SMTA2WorkflowItemId,
                        principalTable: "SMTA2WorkflowItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorklistFromBioHubItemDocuments",
                columns: table => new
                {
                    WorklistFromBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsDocumentFile = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistFromBioHubItemDocuments", x => new { x.WorklistFromBioHubItemId, x.DocumentId });
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubItemDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubItemDocuments_WorklistFromBioHubItems_WorklistFromBioHubItemId",
                        column: x => x.WorklistFromBioHubItemId,
                        principalTable: "WorklistFromBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorklistToBioHubItemDocuments",
                columns: table => new
                {
                    WorklistToBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsDocumentFile = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistToBioHubItemDocuments", x => new { x.WorklistToBioHubItemId, x.DocumentId });
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubItemDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubItemDocuments_WorklistToBioHubItems_WorklistToBioHubItemId",
                        column: x => x.WorklistToBioHubItemId,
                        principalTable: "WorklistToBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialCollectedSpecimenTypesHistory",
                columns: table => new
                {
                    SpecimenTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialCollectedSpecimenTypesHistory", x => new { x.MaterialHistoryId, x.SpecimenTypeId });
                    table.ForeignKey(
                        name: "FK_MaterialCollectedSpecimenTypesHistory_MaterialsHistory_MaterialHistoryId",
                        column: x => x.MaterialHistoryId,
                        principalTable: "MaterialsHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialCollectedSpecimenTypesHistory_SpecimenTypes_SpecimenTypeId",
                        column: x => x.SpecimenTypeId,
                        principalTable: "SpecimenTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialGSDInfoHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GSDType = table.Column<int>(type: "int", nullable: true),
                    CellLine = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PassageNumber = table.Column<int>(type: "int", nullable: true),
                    GSDFasta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialGSDInfoHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialGSDInfoHistory_MaterialsHistory_MaterialHistoryId",
                        column: x => x.MaterialHistoryId,
                        principalTable: "MaterialsHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SMTA1WorkflowHistoryItemDocuments",
                columns: table => new
                {
                    SMTA1WorkflowHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDocumentFile = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMTA1WorkflowHistoryItemDocuments", x => new { x.SMTA1WorkflowHistoryItemId, x.DocumentId });
                    table.ForeignKey(
                        name: "FK_SMTA1WorkflowHistoryItemDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SMTA1WorkflowHistoryItemDocuments_SMTA1WorkflowHistoryItems_SMTA1WorkflowHistoryItemId",
                        column: x => x.SMTA1WorkflowHistoryItemId,
                        principalTable: "SMTA1WorkflowHistoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SMTA2WorkflowHistoryItemDocuments",
                columns: table => new
                {
                    SMTA2WorkflowHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDocumentFile = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMTA2WorkflowHistoryItemDocuments", x => new { x.SMTA2WorkflowHistoryItemId, x.DocumentId });
                    table.ForeignKey(
                        name: "FK_SMTA2WorkflowHistoryItemDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SMTA2WorkflowHistoryItemDocuments_SMTA2WorkflowHistoryItems_SMTA2WorkflowHistoryItemId",
                        column: x => x.SMTA2WorkflowHistoryItemId,
                        principalTable: "SMTA2WorkflowHistoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorklistFromBioHubHistoryItemAnnex2OfSMTA2Conditions",
                columns: table => new
                {
                    WorklistFromBioHubHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Annex2OfSMTA2ConditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Flag = table.Column<bool>(type: "bit", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistFromBioHubHistoryItemAnnex2OfSMTA2Conditions", x => new { x.WorklistFromBioHubHistoryItemId, x.Annex2OfSMTA2ConditionId });
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubHistoryItemAnnex2OfSMTA2Conditions_Annex2OfSMTA2Conditions_Annex2OfSMTA2ConditionId",
                        column: x => x.Annex2OfSMTA2ConditionId,
                        principalTable: "Annex2OfSMTA2Conditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubHistoryItemAnnex2OfSMTA2Conditions_WorklistFromBioHubHistoryItems_WorklistFromBioHubHistoryItemId",
                        column: x => x.WorklistFromBioHubHistoryItemId,
                        principalTable: "WorklistFromBioHubHistoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2s",
                columns: table => new
                {
                    WorklistFromBioHubHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BiosafetyChecklistOfSMTA2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Flag = table.Column<bool>(type: "bit", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2s", x => new { x.WorklistFromBioHubHistoryItemId, x.BiosafetyChecklistOfSMTA2Id });
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2s_BiosafetyChecklistOfSMTA2s_BiosafetyChecklistOfSMTA2Id",
                        column: x => x.BiosafetyChecklistOfSMTA2Id,
                        principalTable: "BiosafetyChecklistOfSMTA2s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2s_WorklistFromBioHubHistoryItems_WorklistFromBioHubHistoryItemId",
                        column: x => x.WorklistFromBioHubHistoryItemId,
                        principalTable: "WorklistFromBioHubHistoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorklistFromBioHubHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComments_Users_PostedById",
                        column: x => x.PostedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComments_WorklistFromBioHubHistoryItems_WorklistFromBioHubHistoryItemId",
                        column: x => x.WorklistFromBioHubHistoryItemId,
                        principalTable: "WorklistFromBioHubHistoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorklistFromBioHubHistoryItemDocuments",
                columns: table => new
                {
                    WorklistFromBioHubHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDocumentFile = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistFromBioHubHistoryItemDocuments", x => new { x.WorklistFromBioHubHistoryItemId, x.DocumentId });
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubHistoryItemDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubHistoryItemDocuments_WorklistFromBioHubHistoryItems_WorklistFromBioHubHistoryItemId",
                        column: x => x.WorklistFromBioHubHistoryItemId,
                        principalTable: "WorklistFromBioHubHistoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorklistFromBioHubHistoryItemFeedback",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorklistFromBioHubHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistFromBioHubHistoryItemFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubHistoryItemFeedback_Users_PostedById",
                        column: x => x.PostedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubHistoryItemFeedback_WorklistFromBioHubHistoryItems_WorklistFromBioHubHistoryItemId",
                        column: x => x.WorklistFromBioHubHistoryItemId,
                        principalTable: "WorklistFromBioHubHistoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorklistFromBioHubHistoryItemLaboratoryFocalPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorklistFromBioHubHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistFromBioHubHistoryItemLaboratoryFocalPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubHistoryItemLaboratoryFocalPoints_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubHistoryItemLaboratoryFocalPoints_WorklistFromBioHubHistoryItems_WorklistFromBioHubHistoryItemId",
                        column: x => x.WorklistFromBioHubHistoryItemId,
                        principalTable: "WorklistFromBioHubHistoryItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorklistFromBioHubHistoryItemMaterials",
                columns: table => new
                {
                    WorklistFromBioHubHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Condition = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistFromBioHubHistoryItemMaterials", x => new { x.WorklistFromBioHubHistoryItemId, x.MaterialId });
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubHistoryItemMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorklistFromBioHubHistoryItemMaterials_WorklistFromBioHubHistoryItems_WorklistFromBioHubHistoryItemId",
                        column: x => x.WorklistFromBioHubHistoryItemId,
                        principalTable: "WorklistFromBioHubHistoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingFormCourierUsers",
                columns: table => new
                {
                    BookingFormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Other = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingFormCourierUsers", x => new { x.BookingFormId, x.UserId });
                    table.ForeignKey(
                        name: "FK_BookingFormCourierUsers_BookingForms_BookingFormId",
                        column: x => x.BookingFormId,
                        principalTable: "BookingForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingFormCourierUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingFormPickupUsers",
                columns: table => new
                {
                    BookingFormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Other = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingFormPickupUsers", x => new { x.BookingFormId, x.UserId });
                    table.ForeignKey(
                        name: "FK_BookingFormPickupUsers_BookingForms_BookingFormId",
                        column: x => x.BookingFormId,
                        principalTable: "BookingForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingFormPickupUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialClinicalDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialShippingInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CollectionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsolationHostTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    PatientStatus = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Condition = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialClinicalDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialClinicalDetails_IsolationHostTypes_IsolationHostTypeId",
                        column: x => x.IsolationHostTypeId,
                        principalTable: "IsolationHostTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialClinicalDetails_MaterialShippingInformations_MaterialShippingInformationId",
                        column: x => x.MaterialShippingInformationId,
                        principalTable: "MaterialShippingInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialLaboratoryAnalysisInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialShippingInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FreezingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Temperature = table.Column<double>(type: "float", nullable: true),
                    UnitOfMeasureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VirusConcentration = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CulturingCellLine = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CulturingPassagesNumber = table.Column<int>(type: "int", nullable: true),
                    TypeOfTransportMedium = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BrandOfTransportMedium = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GSDUploadedToDatabase = table.Column<int>(type: "int", nullable: true),
                    DatabaseUsedForGSDUploadingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccessionNumberInGSDDatabase = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialLaboratoryAnalysisInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialLaboratoryAnalysisInformation_GeneticSequenceDatas_DatabaseUsedForGSDUploadingId",
                        column: x => x.DatabaseUsedForGSDUploadingId,
                        principalTable: "GeneticSequenceDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialLaboratoryAnalysisInformation_MaterialShippingInformations_MaterialShippingInformationId",
                        column: x => x.MaterialShippingInformationId,
                        principalTable: "MaterialShippingInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialLaboratoryAnalysisInformation_TemperatureUnitOfMeasures_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "TemperatureUnitOfMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingFormsHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorklistToBioHubHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorklistFromBioHubHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestDateOfPickup = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TemperatureTransportCondition = table.Column<int>(type: "int", nullable: true),
                    TotalNumberOfVials = table.Column<int>(type: "int", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaterialProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NumberOfInnerPackagingAndSize = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CourierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EstimateDateOfPickup = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShipmentReferenceNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DateOfPickup = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfDelivery = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransportCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransportModeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingFormsHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingFormsHistory_Couriers_CourierId",
                        column: x => x.CourierId,
                        principalTable: "Couriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingFormsHistory_MaterialProducts_MaterialProductId",
                        column: x => x.MaterialProductId,
                        principalTable: "MaterialProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingFormsHistory_TransportCategories_TransportCategoryId",
                        column: x => x.TransportCategoryId,
                        principalTable: "TransportCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingFormsHistory_TransportModes_TransportModeId",
                        column: x => x.TransportModeId,
                        principalTable: "TransportModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingFormsHistory_WorklistFromBioHubHistoryItems_WorklistFromBioHubHistoryItemId",
                        column: x => x.WorklistFromBioHubHistoryItemId,
                        principalTable: "WorklistFromBioHubHistoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingFormsHistory_WorklistToBioHubHistoryItems_WorklistToBioHubHistoryItemId",
                        column: x => x.WorklistToBioHubHistoryItemId,
                        principalTable: "WorklistToBioHubHistoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialShippingInformationsHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MaterialProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransportCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Condition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AdditionalInformation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WorklistToBioHubHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorklistFromBioHubHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialShippingInformationsHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialShippingInformationsHistory_MaterialProducts_MaterialProductId",
                        column: x => x.MaterialProductId,
                        principalTable: "MaterialProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialShippingInformationsHistory_TransportCategories_TransportCategoryId",
                        column: x => x.TransportCategoryId,
                        principalTable: "TransportCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialShippingInformationsHistory_WorklistFromBioHubHistoryItems_WorklistFromBioHubHistoryItemId",
                        column: x => x.WorklistFromBioHubHistoryItemId,
                        principalTable: "WorklistFromBioHubHistoryItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialShippingInformationsHistory_WorklistToBioHubHistoryItems_WorklistToBioHubHistoryItemId",
                        column: x => x.WorklistToBioHubHistoryItemId,
                        principalTable: "WorklistToBioHubHistoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StatusOfRequest = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    QELaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BioHubFacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorklistToBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorklistFromBioHubItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaterialHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PriorityRequestTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TemperatureUnitOfMeasureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransportModeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorklistFromBioHubHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorklistToBioHubHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipments_BioHubFacilities_BioHubFacilityId",
                        column: x => x.BioHubFacilityId,
                        principalTable: "BioHubFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipments_Laboratories_QELaboratoryId",
                        column: x => x.QELaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipments_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shipments_MaterialsHistory_MaterialHistoryId",
                        column: x => x.MaterialHistoryId,
                        principalTable: "MaterialsHistory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shipments_PriorityRequestTypes_PriorityRequestTypeId",
                        column: x => x.PriorityRequestTypeId,
                        principalTable: "PriorityRequestTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shipments_TemperatureUnitOfMeasures_TemperatureUnitOfMeasureId",
                        column: x => x.TemperatureUnitOfMeasureId,
                        principalTable: "TemperatureUnitOfMeasures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shipments_TransportModes_TransportModeId",
                        column: x => x.TransportModeId,
                        principalTable: "TransportModes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shipments_WorklistFromBioHubHistoryItems_WorklistFromBioHubHistoryItemId",
                        column: x => x.WorklistFromBioHubHistoryItemId,
                        principalTable: "WorklistFromBioHubHistoryItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shipments_WorklistFromBioHubItems_WorklistFromBioHubItemId",
                        column: x => x.WorklistFromBioHubItemId,
                        principalTable: "WorklistFromBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipments_WorklistToBioHubHistoryItems_WorklistToBioHubHistoryItemId",
                        column: x => x.WorklistToBioHubHistoryItemId,
                        principalTable: "WorklistToBioHubHistoryItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shipments_WorklistToBioHubItems_WorklistToBioHubItemId",
                        column: x => x.WorklistToBioHubItemId,
                        principalTable: "WorklistToBioHubItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorklistToBioHubHistoryItemBioHubFacilityFocalPoints",
                columns: table => new
                {
                    WorklistToBioHubHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Other = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistToBioHubHistoryItemBioHubFacilityFocalPoints", x => new { x.WorklistToBioHubHistoryItemId, x.UserId });
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubHistoryItemBioHubFacilityFocalPoints_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubHistoryItemBioHubFacilityFocalPoints_WorklistToBioHubHistoryItems_WorklistToBioHubHistoryItemId",
                        column: x => x.WorklistToBioHubHistoryItemId,
                        principalTable: "WorklistToBioHubHistoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorklistToBioHubHistoryItemDocuments",
                columns: table => new
                {
                    WorklistToBioHubHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDocumentFile = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistToBioHubHistoryItemDocuments", x => new { x.WorklistToBioHubHistoryItemId, x.DocumentId });
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubHistoryItemDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubHistoryItemDocuments_WorklistToBioHubHistoryItems_WorklistToBioHubHistoryItemId",
                        column: x => x.WorklistToBioHubHistoryItemId,
                        principalTable: "WorklistToBioHubHistoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorklistToBioHubHistoryItemFeedback",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorklistToBioHubHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistToBioHubHistoryItemFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubHistoryItemFeedback_Users_PostedById",
                        column: x => x.PostedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubHistoryItemFeedback_WorklistToBioHubHistoryItems_WorklistToBioHubHistoryItemId",
                        column: x => x.WorklistToBioHubHistoryItemId,
                        principalTable: "WorklistToBioHubHistoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorklistToBioHubHistoryItemLaboratoryFocalPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorklistToBioHubHistoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorklistToBioHubHistoryItemLaboratoryFocalPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubHistoryItemLaboratoryFocalPoints_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorklistToBioHubHistoryItemLaboratoryFocalPoints_WorklistToBioHubHistoryItems_WorklistToBioHubHistoryItemId",
                        column: x => x.WorklistToBioHubHistoryItemId,
                        principalTable: "WorklistToBioHubHistoryItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CollectedSpecimenTypes",
                columns: table => new
                {
                    SpecimenTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialLaboratoryAnalysisInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectedSpecimenTypes", x => new { x.MaterialLaboratoryAnalysisInformationId, x.SpecimenTypeId });
                    table.ForeignKey(
                        name: "FK_CollectedSpecimenTypes_MaterialLaboratoryAnalysisInformation_MaterialLaboratoryAnalysisInformationId",
                        column: x => x.MaterialLaboratoryAnalysisInformationId,
                        principalTable: "MaterialLaboratoryAnalysisInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectedSpecimenTypes_SpecimenTypes_SpecimenTypeId",
                        column: x => x.SpecimenTypeId,
                        principalTable: "SpecimenTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingFormCourierUsersHistory",
                columns: table => new
                {
                    BookingFormHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Other = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingFormCourierUsersHistory", x => new { x.BookingFormHistoryId, x.UserId });
                    table.ForeignKey(
                        name: "FK_BookingFormCourierUsersHistory_BookingFormsHistory_BookingFormHistoryId",
                        column: x => x.BookingFormHistoryId,
                        principalTable: "BookingFormsHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingFormCourierUsersHistory_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingFormPickupUsersHistory",
                columns: table => new
                {
                    BookingFormHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Other = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingFormPickupUsersHistory", x => new { x.BookingFormHistoryId, x.UserId });
                    table.ForeignKey(
                        name: "FK_BookingFormPickupUsersHistory_BookingFormsHistory_BookingFormHistoryId",
                        column: x => x.BookingFormHistoryId,
                        principalTable: "BookingFormsHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingFormPickupUsersHistory_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialClinicalDetailsHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialShippingInformationHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CollectionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsolationHostTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    PatientStatus = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Condition = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialClinicalDetailsHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialClinicalDetailsHistory_IsolationHostTypes_IsolationHostTypeId",
                        column: x => x.IsolationHostTypeId,
                        principalTable: "IsolationHostTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialClinicalDetailsHistory_MaterialShippingInformationsHistory_MaterialShippingInformationHistoryId",
                        column: x => x.MaterialShippingInformationHistoryId,
                        principalTable: "MaterialShippingInformationsHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialLaboratoryAnalysisInformationHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialShippingInformationHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FreezingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Temperature = table.Column<double>(type: "float", nullable: true),
                    UnitOfMeasureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VirusConcentration = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CulturingCellLine = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CulturingPassagesNumber = table.Column<int>(type: "int", nullable: true),
                    TypeOfTransportMedium = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BrandOfTransportMedium = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GSDUploadedToDatabase = table.Column<int>(type: "int", nullable: true),
                    DatabaseUsedForGSDUploadingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccessionNumberInGSDDatabase = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialLaboratoryAnalysisInformationHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialLaboratoryAnalysisInformationHistory_GeneticSequenceDatas_DatabaseUsedForGSDUploadingId",
                        column: x => x.DatabaseUsedForGSDUploadingId,
                        principalTable: "GeneticSequenceDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialLaboratoryAnalysisInformationHistory_MaterialShippingInformationsHistory_MaterialShippingInformationHistoryId",
                        column: x => x.MaterialShippingInformationHistoryId,
                        principalTable: "MaterialShippingInformationsHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialLaboratoryAnalysisInformationHistory_TemperatureUnitOfMeasures_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "TemperatureUnitOfMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollectedSpecimenTypesHistory",
                columns: table => new
                {
                    SpecimenTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialLaboratoryAnalysisInformationHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectedSpecimenTypesHistory", x => new { x.MaterialLaboratoryAnalysisInformationHistoryId, x.SpecimenTypeId });
                    table.ForeignKey(
                        name: "FK_CollectedSpecimenTypesHistory_MaterialLaboratoryAnalysisInformationHistory_MaterialLaboratoryAnalysisInformationHistoryId",
                        column: x => x.MaterialLaboratoryAnalysisInformationHistoryId,
                        principalTable: "MaterialLaboratoryAnalysisInformationHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectedSpecimenTypesHistory_SpecimenTypes_SpecimenTypeId",
                        column: x => x.SpecimenTypeId,
                        principalTable: "SpecimenTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Current",
                table: "Annex2OfSMTA2Conditions",
                column: "Current");

            migrationBuilder.CreateIndex(
                name: "IX_Abbreviation",
                table: "BioHubFacilities",
                column: "Abbreviation");

            migrationBuilder.CreateIndex(
                name: "IX_BioHubFacilities_BSLLevelId",
                table: "BioHubFacilities",
                column: "BSLLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_BioHubFacilities_CountryId",
                table: "BioHubFacilities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Id",
                table: "BioHubFacilities",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IsPublicFacing",
                table: "BioHubFacilities",
                column: "IsPublicFacing");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "BioHubFacilities",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BioHubFacilitiesHistory_BSLLevelId",
                table: "BioHubFacilitiesHistory",
                column: "BSLLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_BioHubFacilitiesHistory_CountryId",
                table: "BioHubFacilitiesHistory",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_BioHubFacilityId",
                table: "BioHubFacilitiesHistory",
                column: "BioHubFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_LastOperationDate",
                table: "BioHubFacilitiesHistory",
                column: "LastOperationDate");

            migrationBuilder.CreateIndex(
                name: "IX_Current",
                table: "BiosafetyChecklistOfSMTA2s",
                column: "Current");

            migrationBuilder.CreateIndex(
                name: "IX_BookingFormCourierUsers_UserId",
                table: "BookingFormCourierUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingFormCourierUsersHistory_UserId",
                table: "BookingFormCourierUsersHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingFormPickupUsers_UserId",
                table: "BookingFormPickupUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingFormPickupUsersHistory_UserId",
                table: "BookingFormPickupUsersHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingForms_CourierId",
                table: "BookingForms",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingForms_MaterialProductId",
                table: "BookingForms",
                column: "MaterialProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingForms_TransportCategoryId",
                table: "BookingForms",
                column: "TransportCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingForms_TransportModeId",
                table: "BookingForms",
                column: "TransportModeId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingForms_WorklistFromBioHubItemId",
                table: "BookingForms",
                column: "WorklistFromBioHubItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingForms_WorklistToBioHubItemId",
                table: "BookingForms",
                column: "WorklistToBioHubItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingFormsHistory_CourierId",
                table: "BookingFormsHistory",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingFormsHistory_MaterialProductId",
                table: "BookingFormsHistory",
                column: "MaterialProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingFormsHistory_TransportCategoryId",
                table: "BookingFormsHistory",
                column: "TransportCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingFormsHistory_TransportModeId",
                table: "BookingFormsHistory",
                column: "TransportModeId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingFormsHistory_WorklistFromBioHubHistoryItemId",
                table: "BookingFormsHistory",
                column: "WorklistFromBioHubHistoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingFormsHistory_WorklistToBioHubHistoryItemId",
                table: "BookingFormsHistory",
                column: "WorklistToBioHubHistoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Code",
                table: "BSLLevels",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Id",
                table: "BSLLevels",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "BSLLevels",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CollectedSpecimenTypes_SpecimenTypeId",
                table: "CollectedSpecimenTypes",
                column: "SpecimenTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectedSpecimenTypesHistory_SpecimenTypeId",
                table: "CollectedSpecimenTypesHistory",
                column: "SpecimenTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Id",
                table: "Countries",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Iso2",
                table: "Countries",
                column: "Iso2");

            migrationBuilder.CreateIndex(
                name: "IX_Iso3",
                table: "Countries",
                column: "Iso3");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "Countries",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Uncode",
                table: "Countries",
                column: "Uncode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Couriers_CountryId",
                table: "Couriers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "Couriers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CourierId",
                table: "CouriersHistory",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_CouriersHistory_CountryId",
                table: "CouriersHistory",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_LastOperationDate",
                table: "CouriersHistory",
                column: "LastOperationDate");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "CultivabilityTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Approved",
                table: "Documents",
                column: "Approved");

            migrationBuilder.CreateIndex(
                name: "IX_BioHubFacilityId",
                table: "Documents",
                column: "BioHubFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_OriginalDocumentTemplateId",
                table: "Documents",
                column: "OriginalDocumentTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UploadedById",
                table: "Documents",
                column: "UploadedById");

            migrationBuilder.CreateIndex(
                name: "IX_IsDocumentFile",
                table: "Documents",
                column: "IsDocumentFile");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryId",
                table: "Documents",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Type",
                table: "Documents",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplates_UploadedById",
                table: "DocumentTemplates",
                column: "UploadedById");

            migrationBuilder.CreateIndex(
                name: "IX_ParentId",
                table: "DocumentTemplates",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Code",
                table: "GeneticSequenceDatas",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "GeneticSequenceDatas",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "InternationalTaxonomyClassifications",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "IsolationHostTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "IsolationTechniqueTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Abbreviation",
                table: "Laboratories",
                column: "Abbreviation");

            migrationBuilder.CreateIndex(
                name: "IX_IsPublicFacing",
                table: "Laboratories",
                column: "IsPublicFacing");

            migrationBuilder.CreateIndex(
                name: "IX_Laboratories_BSLLevelId",
                table: "Laboratories",
                column: "BSLLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Laboratories_CountryId",
                table: "Laboratories",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "Laboratories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoriesHistory_BSLLevelId",
                table: "LaboratoriesHistory",
                column: "BSLLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoriesHistory_CountryId",
                table: "LaboratoriesHistory",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryId",
                table: "LaboratoriesHistory",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LastOperationDate",
                table: "LaboratoriesHistory",
                column: "LastOperationDate");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialClinicalDetails_IsolationHostTypeId",
                table: "MaterialClinicalDetails",
                column: "IsolationHostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialClinicalDetails_MaterialShippingInformationId",
                table: "MaterialClinicalDetails",
                column: "MaterialShippingInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialNumber",
                table: "MaterialClinicalDetails",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialClinicalDetailsHistory_IsolationHostTypeId",
                table: "MaterialClinicalDetailsHistory",
                column: "IsolationHostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialClinicalDetailsHistory_MaterialShippingInformationHistoryId",
                table: "MaterialClinicalDetailsHistory",
                column: "MaterialShippingInformationHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialNumber",
                table: "MaterialClinicalDetailsHistory",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialCollectedSpecimenTypes_SpecimenTypeId",
                table: "MaterialCollectedSpecimenTypes",
                column: "SpecimenTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialCollectedSpecimenTypesHistory_SpecimenTypeId",
                table: "MaterialCollectedSpecimenTypesHistory",
                column: "SpecimenTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialGSDInfo_MaterialId",
                table: "MaterialGSDInfo",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialGSDInfoHistory_MaterialHistoryId",
                table: "MaterialGSDInfoHistory",
                column: "MaterialHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialLaboratoryAnalysisInformation_DatabaseUsedForGSDUploadingId",
                table: "MaterialLaboratoryAnalysisInformation",
                column: "DatabaseUsedForGSDUploadingId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialLaboratoryAnalysisInformation_MaterialShippingInformationId",
                table: "MaterialLaboratoryAnalysisInformation",
                column: "MaterialShippingInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialLaboratoryAnalysisInformation_UnitOfMeasureId",
                table: "MaterialLaboratoryAnalysisInformation",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialNumber",
                table: "MaterialLaboratoryAnalysisInformation",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialLaboratoryAnalysisInformationHistory_DatabaseUsedForGSDUploadingId",
                table: "MaterialLaboratoryAnalysisInformationHistory",
                column: "DatabaseUsedForGSDUploadingId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialLaboratoryAnalysisInformationHistory_MaterialShippingInformationHistoryId",
                table: "MaterialLaboratoryAnalysisInformationHistory",
                column: "MaterialShippingInformationHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialLaboratoryAnalysisInformationHistory_UnitOfMeasureId",
                table: "MaterialLaboratoryAnalysisInformationHistory",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialNumber",
                table: "MaterialLaboratoryAnalysisInformationHistory",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "MaterialProducts",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_CultivabilityTypeId",
                table: "Materials",
                column: "CultivabilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_GeneticSequenceDataId",
                table: "Materials",
                column: "GeneticSequenceDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_InternationalTaxonomyClassificationId",
                table: "Materials",
                column: "InternationalTaxonomyClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_IsolationHostTypeId",
                table: "Materials",
                column: "IsolationHostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_IsolationTechniqueTypeId",
                table: "Materials",
                column: "IsolationTechniqueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_LastOperationById",
                table: "Materials",
                column: "LastOperationById");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_OwnerBioHubFacilityId",
                table: "Materials",
                column: "OwnerBioHubFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_ProductTypeId",
                table: "Materials",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_ProviderBioHubFacilityId",
                table: "Materials",
                column: "ProviderBioHubFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_ProviderLaboratoryId",
                table: "Materials",
                column: "ProviderLaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_ReferenceNumber",
                table: "Materials",
                column: "ReferenceNumber",
                unique: true,
                filter: "[ReferenceNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_SuspectedEpidemiologicalOriginId",
                table: "Materials",
                column: "SuspectedEpidemiologicalOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_TransportCategoryId",
                table: "Materials",
                column: "TransportCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_TypeId",
                table: "Materials",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_UnitOfMeasureId",
                table: "Materials",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_UsagePermissionId",
                table: "Materials",
                column: "UsagePermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialShippingInformations_MaterialProductId",
                table: "MaterialShippingInformations",
                column: "MaterialProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialShippingInformations_TransportCategoryId",
                table: "MaterialShippingInformations",
                column: "TransportCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialShippingInformations_WorklistToBioHubItemId",
                table: "MaterialShippingInformations",
                column: "WorklistToBioHubItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialShippingInformationsHistory_MaterialProductId",
                table: "MaterialShippingInformationsHistory",
                column: "MaterialProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialShippingInformationsHistory_TransportCategoryId",
                table: "MaterialShippingInformationsHistory",
                column: "TransportCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialShippingInformationsHistory_WorklistFromBioHubHistoryItemId",
                table: "MaterialShippingInformationsHistory",
                column: "WorklistFromBioHubHistoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialShippingInformationsHistory_WorklistToBioHubHistoryItemId",
                table: "MaterialShippingInformationsHistory",
                column: "WorklistToBioHubHistoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsHistory_CultivabilityTypeId",
                table: "MaterialsHistory",
                column: "CultivabilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsHistory_GeneticSequenceDataId",
                table: "MaterialsHistory",
                column: "GeneticSequenceDataId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsHistory_InternationalTaxonomyClassificationId",
                table: "MaterialsHistory",
                column: "InternationalTaxonomyClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsHistory_IsolationHostTypeId",
                table: "MaterialsHistory",
                column: "IsolationHostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsHistory_IsolationTechniqueTypeId",
                table: "MaterialsHistory",
                column: "IsolationTechniqueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsHistory_LastOperationById",
                table: "MaterialsHistory",
                column: "LastOperationById");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsHistory_MaterialId",
                table: "MaterialsHistory",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsHistory_OwnerBioHubFacilityId",
                table: "MaterialsHistory",
                column: "OwnerBioHubFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsHistory_ProductTypeId",
                table: "MaterialsHistory",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsHistory_ProviderBioHubFacilityId",
                table: "MaterialsHistory",
                column: "ProviderBioHubFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsHistory_ProviderLaboratoryId",
                table: "MaterialsHistory",
                column: "ProviderLaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsHistory_SuspectedEpidemiologicalOriginId",
                table: "MaterialsHistory",
                column: "SuspectedEpidemiologicalOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsHistory_TransportCategoryId",
                table: "MaterialsHistory",
                column: "TransportCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsHistory_TypeId",
                table: "MaterialsHistory",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsHistory_UnitOfMeasureId",
                table: "MaterialsHistory",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsHistory_UsagePermissionId",
                table: "MaterialsHistory",
                column: "UsagePermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "MaterialTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "MaterialUsagePermissions",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "Permissions",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "PriorityRequestTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_UploadedById",
                table: "Resources",
                column: "UploadedById");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "Roles",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_BioHubFacilityId",
                table: "Shipments",
                column: "BioHubFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_MaterialHistoryId",
                table: "Shipments",
                column: "MaterialHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_MaterialId",
                table: "Shipments",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_PriorityRequestTypeId",
                table: "Shipments",
                column: "PriorityRequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_QELaboratoryId",
                table: "Shipments",
                column: "QELaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_TemperatureUnitOfMeasureId",
                table: "Shipments",
                column: "TemperatureUnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_TransportModeId",
                table: "Shipments",
                column: "TransportModeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_WorklistFromBioHubHistoryItemId",
                table: "Shipments",
                column: "WorklistFromBioHubHistoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_WorklistFromBioHubItemId",
                table: "Shipments",
                column: "WorklistFromBioHubItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_WorklistToBioHubHistoryItemId",
                table: "Shipments",
                column: "WorklistToBioHubHistoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_WorklistToBioHubItemId",
                table: "Shipments",
                column: "WorklistToBioHubItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedSubmission",
                table: "SMTA1WorkflowEmails",
                column: "ApprovedSubmission");

            migrationBuilder.CreateIndex(
                name: "IX_FromStatus",
                table: "SMTA1WorkflowEmails",
                column: "FromStatus");

            migrationBuilder.CreateIndex(
                name: "IX_SMTA1WorkflowEmails_RoleId",
                table: "SMTA1WorkflowEmails",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ToStatus",
                table: "SMTA1WorkflowEmails",
                column: "ToStatus");

            migrationBuilder.CreateIndex(
                name: "IX_SMTA1WorkflowHistoryItemDocuments_DocumentId",
                table: "SMTA1WorkflowHistoryItemDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SMTA1WorkflowHistoryItems_BioHubFacilityId",
                table: "SMTA1WorkflowHistoryItems",
                column: "BioHubFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_SMTA1WorkflowHistoryItems_LaboratoryId",
                table: "SMTA1WorkflowHistoryItems",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SMTA1WorkflowHistoryItems_LastOperationUserId",
                table: "SMTA1WorkflowHistoryItems",
                column: "LastOperationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SMTA1WorkflowHistoryItems_SMTA1WorkflowItemId",
                table: "SMTA1WorkflowHistoryItems",
                column: "SMTA1WorkflowItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Status",
                table: "SMTA1WorkflowHistoryItems",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_SMTA1WorkflowItemDocuments_DocumentId",
                table: "SMTA1WorkflowItemDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SMTA1WorkflowItems_BioHubFacilityId",
                table: "SMTA1WorkflowItems",
                column: "BioHubFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_SMTA1WorkflowItems_LaboratoryId",
                table: "SMTA1WorkflowItems",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SMTA1WorkflowItems_LastOperationUserId",
                table: "SMTA1WorkflowItems",
                column: "LastOperationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Status",
                table: "SMTA1WorkflowItems",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedSubmission",
                table: "SMTA2WorkflowEmails",
                column: "ApprovedSubmission");

            migrationBuilder.CreateIndex(
                name: "IX_FromStatus",
                table: "SMTA2WorkflowEmails",
                column: "FromStatus");

            migrationBuilder.CreateIndex(
                name: "IX_SMTA2WorkflowEmails_RoleId",
                table: "SMTA2WorkflowEmails",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ToStatus",
                table: "SMTA2WorkflowEmails",
                column: "ToStatus");

            migrationBuilder.CreateIndex(
                name: "IX_SMTA2WorkflowHistoryItemDocuments_DocumentId",
                table: "SMTA2WorkflowHistoryItemDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SMTA2WorkflowHistoryItems_LaboratoryId",
                table: "SMTA2WorkflowHistoryItems",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SMTA2WorkflowHistoryItems_LastOperationUserId",
                table: "SMTA2WorkflowHistoryItems",
                column: "LastOperationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SMTA2WorkflowHistoryItems_SMTA2WorkflowItemId",
                table: "SMTA2WorkflowHistoryItems",
                column: "SMTA2WorkflowItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Status",
                table: "SMTA2WorkflowHistoryItems",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_SMTA2WorkflowItemDocuments_DocumentId",
                table: "SMTA2WorkflowItemDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SMTA2WorkflowItems_LaboratoryId",
                table: "SMTA2WorkflowItems",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SMTA2WorkflowItems_LastOperationUserId",
                table: "SMTA2WorkflowItems",
                column: "LastOperationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Status",
                table: "SMTA2WorkflowItems",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "SpecimenTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "TemperatureUnitOfMeasures",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "TransportCategories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Name",
                table: "TransportModes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_UserRequests_CountryId",
                table: "UserRequests",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRequests_LaboratoryId",
                table: "UserRequests",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRequests_RoleId",
                table: "UserRequests",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_DeletedOn",
                table: "Users",
                column: "DeletedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Email",
                table: "Users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalId",
                table: "Users",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BioHubFacilityId",
                table: "Users",
                column: "BioHubFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CourierId",
                table: "Users",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LaboratoryId",
                table: "Users",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_LastOperationDate",
                table: "UsersHistory",
                column: "LastOperationDate");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "UsersHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersHistory_BioHubFacilityId",
                table: "UsersHistory",
                column: "BioHubFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersHistory_CourierId",
                table: "UsersHistory",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersHistory_LaboratoryId",
                table: "UsersHistory",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersHistory_RoleId",
                table: "UsersHistory",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedSubmission",
                table: "WorklistFromBioHubEmails",
                column: "ApprovedSubmission");

            migrationBuilder.CreateIndex(
                name: "IX_FromStatus",
                table: "WorklistFromBioHubEmails",
                column: "FromStatus");

            migrationBuilder.CreateIndex(
                name: "IX_ToStatus",
                table: "WorklistFromBioHubEmails",
                column: "ToStatus");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubEmails_RoleId",
                table: "WorklistFromBioHubEmails",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubHistoryItemAnnex2OfSMTA2Conditions_Annex2OfSMTA2ConditionId",
                table: "WorklistFromBioHubHistoryItemAnnex2OfSMTA2Conditions",
                column: "Annex2OfSMTA2ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2s_BiosafetyChecklistOfSMTA2Id",
                table: "WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2s",
                column: "BiosafetyChecklistOfSMTA2Id");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComments_PostedById",
                table: "WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComments",
                column: "PostedById");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComments_WorklistFromBioHubHistoryItemId",
                table: "WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComments",
                column: "WorklistFromBioHubHistoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubHistoryItemDocuments_DocumentId",
                table: "WorklistFromBioHubHistoryItemDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubHistoryItemFeedback_PostedById",
                table: "WorklistFromBioHubHistoryItemFeedback",
                column: "PostedById");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubHistoryItemFeedback_WorklistFromBioHubHistoryItemId",
                table: "WorklistFromBioHubHistoryItemFeedback",
                column: "WorklistFromBioHubHistoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubHistoryItemLaboratoryFocalPoints_UserId",
                table: "WorklistFromBioHubHistoryItemLaboratoryFocalPoints",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubHistoryItemLaboratoryFocalPoints_WorklistFromBioHubHistoryItemId",
                table: "WorklistFromBioHubHistoryItemLaboratoryFocalPoints",
                column: "WorklistFromBioHubHistoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubHistoryItemMaterials_MaterialId",
                table: "WorklistFromBioHubHistoryItemMaterials",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Status",
                table: "WorklistFromBioHubHistoryItems",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubHistoryItems_LastOperationUserId",
                table: "WorklistFromBioHubHistoryItems",
                column: "LastOperationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubHistoryItems_RequestInitiationFromBioHubFacilityId",
                table: "WorklistFromBioHubHistoryItems",
                column: "RequestInitiationFromBioHubFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubHistoryItems_RequestInitiationToLaboratoryId",
                table: "WorklistFromBioHubHistoryItems",
                column: "RequestInitiationToLaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubHistoryItems_WorklistFromBioHubItemId",
                table: "WorklistFromBioHubHistoryItems",
                column: "WorklistFromBioHubItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubItemAnnex2OfSMTA2Conditions_Annex2OfSMTA2ConditionId",
                table: "WorklistFromBioHubItemAnnex2OfSMTA2Conditions",
                column: "Annex2OfSMTA2ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s_BiosafetyChecklistOfSMTA2Id",
                table: "WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s",
                column: "BiosafetyChecklistOfSMTA2Id");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubItemBiosafetyChecklistThreadComments_PostedById",
                table: "WorklistFromBioHubItemBiosafetyChecklistThreadComments",
                column: "PostedById");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubItemBiosafetyChecklistThreadComments_WorklistFromBioHubItemId",
                table: "WorklistFromBioHubItemBiosafetyChecklistThreadComments",
                column: "WorklistFromBioHubItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubItemDocuments_DocumentId",
                table: "WorklistFromBioHubItemDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubItemFeedback_PostedById",
                table: "WorklistFromBioHubItemFeedback",
                column: "PostedById");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubItemFeedback_WorklistFromBioHubItemId",
                table: "WorklistFromBioHubItemFeedback",
                column: "WorklistFromBioHubItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubItemLaboratoryFocalPoints_UserId",
                table: "WorklistFromBioHubItemLaboratoryFocalPoints",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubItemMaterials_MaterialId",
                table: "WorklistFromBioHubItemMaterials",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Status",
                table: "WorklistFromBioHubItems",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubItems_LastOperationUserId",
                table: "WorklistFromBioHubItems",
                column: "LastOperationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubItems_ReferenceNumber",
                table: "WorklistFromBioHubItems",
                column: "ReferenceNumber",
                unique: true,
                filter: "[ReferenceNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubItems_RequestInitiationFromBioHubFacilityId",
                table: "WorklistFromBioHubItems",
                column: "RequestInitiationFromBioHubFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistFromBioHubItems_RequestInitiationToLaboratoryId",
                table: "WorklistFromBioHubItems",
                column: "RequestInitiationToLaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceNumber",
                table: "WorklistItemUsedReferenceNumbers",
                column: "ReferenceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedSubmission",
                table: "WorklistToBioHubEmails",
                column: "ApprovedSubmission");

            migrationBuilder.CreateIndex(
                name: "IX_FromStatus",
                table: "WorklistToBioHubEmails",
                column: "FromStatus");

            migrationBuilder.CreateIndex(
                name: "IX_ToStatus",
                table: "WorklistToBioHubEmails",
                column: "ToStatus");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubEmails_RoleId",
                table: "WorklistToBioHubEmails",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubHistoryItemBioHubFacilityFocalPoints_UserId",
                table: "WorklistToBioHubHistoryItemBioHubFacilityFocalPoints",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubHistoryItemDocuments_DocumentId",
                table: "WorklistToBioHubHistoryItemDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubHistoryItemFeedback_PostedById",
                table: "WorklistToBioHubHistoryItemFeedback",
                column: "PostedById");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubHistoryItemFeedback_WorklistToBioHubHistoryItemId",
                table: "WorklistToBioHubHistoryItemFeedback",
                column: "WorklistToBioHubHistoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubHistoryItemLaboratoryFocalPoints_UserId",
                table: "WorklistToBioHubHistoryItemLaboratoryFocalPoints",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubHistoryItemLaboratoryFocalPoints_WorklistToBioHubHistoryItemId",
                table: "WorklistToBioHubHistoryItemLaboratoryFocalPoints",
                column: "WorklistToBioHubHistoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Status",
                table: "WorklistToBioHubHistoryItems",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubHistoryItems_LastOperationUserId",
                table: "WorklistToBioHubHistoryItems",
                column: "LastOperationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubHistoryItems_RequestInitiationFromLaboratoryId",
                table: "WorklistToBioHubHistoryItems",
                column: "RequestInitiationFromLaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubHistoryItems_RequestInitiationToBioHubFacilityId",
                table: "WorklistToBioHubHistoryItems",
                column: "RequestInitiationToBioHubFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubHistoryItems_WorklistToBioHubItemId",
                table: "WorklistToBioHubHistoryItems",
                column: "WorklistToBioHubItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubItemBioHubFacilityFocalPoints_UserId",
                table: "WorklistToBioHubItemBioHubFacilityFocalPoints",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubItemDocuments_DocumentId",
                table: "WorklistToBioHubItemDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubItemFeedback_PostedById",
                table: "WorklistToBioHubItemFeedback",
                column: "PostedById");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubItemFeedback_WorklistToBioHubItemId",
                table: "WorklistToBioHubItemFeedback",
                column: "WorklistToBioHubItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubItemLaboratoryFocalPoints_UserId",
                table: "WorklistToBioHubItemLaboratoryFocalPoints",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubItemMaterials_MaterialId",
                table: "WorklistToBioHubItemMaterials",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Status",
                table: "WorklistToBioHubItems",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubItems_LastOperationUserId",
                table: "WorklistToBioHubItems",
                column: "LastOperationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubItems_ReferenceNumber",
                table: "WorklistToBioHubItems",
                column: "ReferenceNumber",
                unique: true,
                filter: "[ReferenceNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubItems_RequestInitiationFromLaboratoryId",
                table: "WorklistToBioHubItems",
                column: "RequestInitiationFromLaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorklistToBioHubItems_RequestInitiationToBioHubFacilityId",
                table: "WorklistToBioHubItems",
                column: "RequestInitiationToBioHubFacilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BioHubFacilitiesHistory");

            migrationBuilder.DropTable(
                name: "BookingFormCourierUsers");

            migrationBuilder.DropTable(
                name: "BookingFormCourierUsersHistory");

            migrationBuilder.DropTable(
                name: "BookingFormPickupUsers");

            migrationBuilder.DropTable(
                name: "BookingFormPickupUsersHistory");

            migrationBuilder.DropTable(
                name: "CollectedSpecimenTypes");

            migrationBuilder.DropTable(
                name: "CollectedSpecimenTypesHistory");

            migrationBuilder.DropTable(
                name: "CouriersHistory");

            migrationBuilder.DropTable(
                name: "LaboratoriesHistory");

            migrationBuilder.DropTable(
                name: "MaterialClinicalDetails");

            migrationBuilder.DropTable(
                name: "MaterialClinicalDetailsHistory");

            migrationBuilder.DropTable(
                name: "MaterialCollectedSpecimenTypes");

            migrationBuilder.DropTable(
                name: "MaterialCollectedSpecimenTypesHistory");

            migrationBuilder.DropTable(
                name: "MaterialGSDInfo");

            migrationBuilder.DropTable(
                name: "MaterialGSDInfoHistory");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "SMTA1WorkflowEmails");

            migrationBuilder.DropTable(
                name: "SMTA1WorkflowHistoryItemDocuments");

            migrationBuilder.DropTable(
                name: "SMTA1WorkflowItemDocuments");

            migrationBuilder.DropTable(
                name: "SMTA2WorkflowEmails");

            migrationBuilder.DropTable(
                name: "SMTA2WorkflowHistoryItemDocuments");

            migrationBuilder.DropTable(
                name: "SMTA2WorkflowItemDocuments");

            migrationBuilder.DropTable(
                name: "UserRequests");

            migrationBuilder.DropTable(
                name: "UserRequestStatuses");

            migrationBuilder.DropTable(
                name: "UsersHistory");

            migrationBuilder.DropTable(
                name: "WorklistFromBioHubEmails");

            migrationBuilder.DropTable(
                name: "WorklistFromBioHubHistoryItemAnnex2OfSMTA2Conditions");

            migrationBuilder.DropTable(
                name: "WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2s");

            migrationBuilder.DropTable(
                name: "WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComments");

            migrationBuilder.DropTable(
                name: "WorklistFromBioHubHistoryItemDocuments");

            migrationBuilder.DropTable(
                name: "WorklistFromBioHubHistoryItemFeedback");

            migrationBuilder.DropTable(
                name: "WorklistFromBioHubHistoryItemLaboratoryFocalPoints");

            migrationBuilder.DropTable(
                name: "WorklistFromBioHubHistoryItemMaterials");

            migrationBuilder.DropTable(
                name: "WorklistFromBioHubItemAnnex2OfSMTA2Conditions");

            migrationBuilder.DropTable(
                name: "WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s");

            migrationBuilder.DropTable(
                name: "WorklistFromBioHubItemBiosafetyChecklistThreadComments");

            migrationBuilder.DropTable(
                name: "WorklistFromBioHubItemDocuments");

            migrationBuilder.DropTable(
                name: "WorklistFromBioHubItemFeedback");

            migrationBuilder.DropTable(
                name: "WorklistFromBioHubItemLaboratoryFocalPoints");

            migrationBuilder.DropTable(
                name: "WorklistFromBioHubItemMaterials");

            migrationBuilder.DropTable(
                name: "WorklistItemUsedReferenceNumbers");

            migrationBuilder.DropTable(
                name: "WorklistToBioHubEmails");

            migrationBuilder.DropTable(
                name: "WorklistToBioHubHistoryItemBioHubFacilityFocalPoints");

            migrationBuilder.DropTable(
                name: "WorklistToBioHubHistoryItemDocuments");

            migrationBuilder.DropTable(
                name: "WorklistToBioHubHistoryItemFeedback");

            migrationBuilder.DropTable(
                name: "WorklistToBioHubHistoryItemLaboratoryFocalPoints");

            migrationBuilder.DropTable(
                name: "WorklistToBioHubItemBioHubFacilityFocalPoints");

            migrationBuilder.DropTable(
                name: "WorklistToBioHubItemDocuments");

            migrationBuilder.DropTable(
                name: "WorklistToBioHubItemFeedback");

            migrationBuilder.DropTable(
                name: "WorklistToBioHubItemLaboratoryFocalPoints");

            migrationBuilder.DropTable(
                name: "WorklistToBioHubItemMaterials");

            migrationBuilder.DropTable(
                name: "BookingForms");

            migrationBuilder.DropTable(
                name: "BookingFormsHistory");

            migrationBuilder.DropTable(
                name: "MaterialLaboratoryAnalysisInformation");

            migrationBuilder.DropTable(
                name: "MaterialLaboratoryAnalysisInformationHistory");

            migrationBuilder.DropTable(
                name: "SpecimenTypes");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "MaterialsHistory");

            migrationBuilder.DropTable(
                name: "PriorityRequestTypes");

            migrationBuilder.DropTable(
                name: "SMTA1WorkflowHistoryItems");

            migrationBuilder.DropTable(
                name: "SMTA2WorkflowHistoryItems");

            migrationBuilder.DropTable(
                name: "Annex2OfSMTA2Conditions");

            migrationBuilder.DropTable(
                name: "BiosafetyChecklistOfSMTA2s");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "TransportModes");

            migrationBuilder.DropTable(
                name: "MaterialShippingInformations");

            migrationBuilder.DropTable(
                name: "MaterialShippingInformationsHistory");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "SMTA1WorkflowItems");

            migrationBuilder.DropTable(
                name: "SMTA2WorkflowItems");

            migrationBuilder.DropTable(
                name: "DocumentTemplates");

            migrationBuilder.DropTable(
                name: "WorklistFromBioHubHistoryItems");

            migrationBuilder.DropTable(
                name: "WorklistToBioHubHistoryItems");

            migrationBuilder.DropTable(
                name: "CultivabilityTypes");

            migrationBuilder.DropTable(
                name: "GeneticSequenceDatas");

            migrationBuilder.DropTable(
                name: "InternationalTaxonomyClassifications");

            migrationBuilder.DropTable(
                name: "IsolationHostTypes");

            migrationBuilder.DropTable(
                name: "IsolationTechniqueTypes");

            migrationBuilder.DropTable(
                name: "MaterialProducts");

            migrationBuilder.DropTable(
                name: "MaterialTypes");

            migrationBuilder.DropTable(
                name: "MaterialUsagePermissions");

            migrationBuilder.DropTable(
                name: "TemperatureUnitOfMeasures");

            migrationBuilder.DropTable(
                name: "TransportCategories");

            migrationBuilder.DropTable(
                name: "WorklistFromBioHubItems");

            migrationBuilder.DropTable(
                name: "WorklistToBioHubItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BioHubFacilities");

            migrationBuilder.DropTable(
                name: "Couriers");

            migrationBuilder.DropTable(
                name: "Laboratories");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "BSLLevels");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
