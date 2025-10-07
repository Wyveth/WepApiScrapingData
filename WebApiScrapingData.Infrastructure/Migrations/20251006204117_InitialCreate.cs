using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSelected = table.Column<bool>(type: "bit", nullable: false),
                    IsCorrectID = table.Column<long>(type: "bigint", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionVx = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionVy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Talent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionTalent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Types = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weakness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Evolutions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhenEvolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Difficulties",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_ES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_IT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_DE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_RU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_CO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_CN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_JP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_ES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_IT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_DE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_RU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_CO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_CN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_JP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Talents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_ES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_ES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_IT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_IT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_DE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_DE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_RU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_RU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_CO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_CO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_CN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_CN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_JP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_JP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeAttaques",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_ES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_ES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_IT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_IT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_DE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_DE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_RU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_RU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_CO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_CO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_CN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_CN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_JP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_JP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeAttaques", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypesPok",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathMiniHome_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathMiniHome_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_ES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathMiniHome_ES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_IT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathMiniHome_IT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_DE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathMiniHome_DE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_RU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathMiniHome_RU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_CO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathMiniHome_CO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_CN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathMiniHome_CN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_JP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathMiniHome_JP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlMiniHome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlMiniGo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathMiniGo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlFondGo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathFondGo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlIconHome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathIconHome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlAutoHome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathAutoHome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InfoColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesPok", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quizzs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gen1 = table.Column<bool>(type: "bit", nullable: false),
                    Gen2 = table.Column<bool>(type: "bit", nullable: false),
                    Gen3 = table.Column<bool>(type: "bit", nullable: false),
                    Gen4 = table.Column<bool>(type: "bit", nullable: false),
                    Gen5 = table.Column<bool>(type: "bit", nullable: false),
                    Gen6 = table.Column<bool>(type: "bit", nullable: false),
                    Gen7 = table.Column<bool>(type: "bit", nullable: false),
                    Gen8 = table.Column<bool>(type: "bit", nullable: false),
                    Gen9 = table.Column<bool>(type: "bit", nullable: false),
                    GenArceus = table.Column<bool>(type: "bit", nullable: false),
                    Easy = table.Column<bool>(type: "bit", nullable: false),
                    Normal = table.Column<bool>(type: "bit", nullable: false),
                    Hard = table.Column<bool>(type: "bit", nullable: false),
                    Done = table.Column<bool>(type: "bit", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quizzs_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_ES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_IT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_DE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_RU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_CO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_CN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libelle_JP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NbAnswers = table.Column<int>(type: "int", nullable: false),
                    IsMultipleAnswers = table.Column<bool>(type: "bit", nullable: false),
                    NbAnswersPossible = table.Column<int>(type: "int", nullable: false),
                    IsBlurred = table.Column<bool>(type: "bit", nullable: false),
                    IsGrayscale = table.Column<bool>(type: "bit", nullable: false),
                    IsHide = table.Column<bool>(type: "bit", nullable: false),
                    IsSameType = table.Column<bool>(type: "bit", nullable: false),
                    DifficultyId = table.Column<long>(type: "bigint", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionTypes_Difficulties_DifficultyId",
                        column: x => x.DifficultyId,
                        principalTable: "Difficulties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_FR = table.Column<long>(type: "bigint", nullable: false),
                    Id_EN = table.Column<long>(type: "bigint", nullable: false),
                    Id_ES = table.Column<long>(type: "bigint", nullable: false),
                    Id_IT = table.Column<long>(type: "bigint", nullable: false),
                    Id_DE = table.Column<long>(type: "bigint", nullable: false),
                    Id_RU = table.Column<long>(type: "bigint", nullable: false),
                    Id_CO = table.Column<long>(type: "bigint", nullable: false),
                    Id_CN = table.Column<long>(type: "bigint", nullable: false),
                    Id_JP = table.Column<long>(type: "bigint", nullable: false),
                    TypeEvolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatPv = table.Column<int>(type: "int", nullable: false),
                    StatAttaque = table.Column<int>(type: "int", nullable: false),
                    StatDefense = table.Column<int>(type: "int", nullable: false),
                    StatAttaqueSpe = table.Column<int>(type: "int", nullable: false),
                    StatDefenseSpe = table.Column<int>(type: "int", nullable: false),
                    StatVitesse = table.Column<int>(type: "int", nullable: false),
                    StatTotal = table.Column<int>(type: "int", nullable: false),
                    EggMoves = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaptureRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicHappiness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Generation = table.Column<int>(type: "int", nullable: false),
                    UrlImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathImgLegacy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathImgNormal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathImgShiny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathAnimatedImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathAnimatedImgShiny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlSprite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathSpriteLegacy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathSpriteNormal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathSpriteShiny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlSound = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathSound = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathSoundLegacy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathSoundCurrent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameId = table.Column<long>(type: "bigint", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemons_DataInfos_Id_CN",
                        column: x => x.Id_CN,
                        principalTable: "DataInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_DataInfos_Id_CO",
                        column: x => x.Id_CO,
                        principalTable: "DataInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_DataInfos_Id_DE",
                        column: x => x.Id_DE,
                        principalTable: "DataInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_DataInfos_Id_EN",
                        column: x => x.Id_EN,
                        principalTable: "DataInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_DataInfos_Id_ES",
                        column: x => x.Id_ES,
                        principalTable: "DataInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_DataInfos_Id_FR",
                        column: x => x.Id_FR,
                        principalTable: "DataInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_DataInfos_Id_IT",
                        column: x => x.Id_IT,
                        principalTable: "DataInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_DataInfos_Id_JP",
                        column: x => x.Id_JP,
                        principalTable: "DataInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_DataInfos_Id_RU",
                        column: x => x.Id_RU,
                        principalTable: "DataInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemons_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attaques",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_ES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_ES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_IT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_IT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_DE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_DE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_RU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_RU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_CO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_CO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_CN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_CN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_JP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_JP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeAttaqueId = table.Column<long>(type: "bigint", nullable: true),
                    TypePokId = table.Column<long>(type: "bigint", nullable: true),
                    Power = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attaques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attaques_TypeAttaques_TypeAttaqueId",
                        column: x => x.TypeAttaqueId,
                        principalTable: "TypeAttaques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attaques_TypesPok_TypePokId",
                        column: x => x.TypePokId,
                        principalTable: "TypesPok",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuizzDifficulties",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizzId = table.Column<long>(type: "bigint", nullable: true),
                    Easy = table.Column<bool>(type: "bit", nullable: false),
                    Normal = table.Column<bool>(type: "bit", nullable: false),
                    Hard = table.Column<bool>(type: "bit", nullable: false),
                    ResumeQuestion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizzDifficulties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizzDifficulties_Quizzs_QuizzId",
                        column: x => x.QuizzId,
                        principalTable: "Quizzs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    DataObjectID = table.Column<long>(type: "bigint", nullable: false),
                    QuestionTypeId = table.Column<long>(type: "bigint", nullable: true),
                    Done = table.Column<bool>(type: "bit", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_QuestionTypes_QuestionTypeId",
                        column: x => x.QuestionTypeId,
                        principalTable: "QuestionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pokemon_Talent",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonId = table.Column<long>(type: "bigint", nullable: false),
                    TalentId = table.Column<long>(type: "bigint", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon_Talent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemon_Talent_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemon_Talent_Talents_TalentId",
                        column: x => x.TalentId,
                        principalTable: "Talents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pokemon_TypePok",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonId = table.Column<long>(type: "bigint", nullable: false),
                    TypePokId = table.Column<long>(type: "bigint", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon_TypePok", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemon_TypePok_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemon_TypePok_TypesPok_TypePokId",
                        column: x => x.TypePokId,
                        principalTable: "TypesPok",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pokemon_Weakness",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonId = table.Column<long>(type: "bigint", nullable: false),
                    TypePokId = table.Column<long>(type: "bigint", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon_Weakness", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemon_Weakness_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemon_Weakness_TypesPok_TypePokId",
                        column: x => x.TypePokId,
                        principalTable: "TypesPok",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pokemon_Attaque",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonId = table.Column<long>(type: "bigint", nullable: false),
                    AttaqueId = table.Column<long>(type: "bigint", nullable: false),
                    TypeLearn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CTCS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon_Attaque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemon_Attaque_Attaques_AttaqueId",
                        column: x => x.AttaqueId,
                        principalTable: "Attaques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pokemon_Attaque_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Question_Answer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    AnswerId = table.Column<long>(type: "bigint", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question_Answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Answer_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_Answer_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Quizz_Question",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizzId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    versionModification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizz_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quizz_Question_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quizz_Question_Quizzs_QuizzId",
                        column: x => x.QuizzId,
                        principalTable: "Quizzs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Attaques_TypeAttaqueId",
                table: "Attaques",
                column: "TypeAttaqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Attaques_TypePokId",
                table: "Attaques",
                column: "TypePokId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_Attaque_AttaqueId",
                table: "Pokemon_Attaque",
                column: "AttaqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_Attaque_PokemonId",
                table: "Pokemon_Attaque",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_Talent_PokemonId",
                table: "Pokemon_Talent",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_Talent_TalentId",
                table: "Pokemon_Talent",
                column: "TalentId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_TypePok_PokemonId",
                table: "Pokemon_TypePok",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_TypePok_TypePokId",
                table: "Pokemon_TypePok",
                column: "TypePokId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_Weakness_PokemonId",
                table: "Pokemon_Weakness",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_Weakness_TypePokId",
                table: "Pokemon_Weakness",
                column: "TypePokId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_GameId",
                table: "Pokemons",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Id_CN",
                table: "Pokemons",
                column: "Id_CN");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Id_CO",
                table: "Pokemons",
                column: "Id_CO");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Id_DE",
                table: "Pokemons",
                column: "Id_DE");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Id_EN",
                table: "Pokemons",
                column: "Id_EN");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Id_ES",
                table: "Pokemons",
                column: "Id_ES");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Id_FR",
                table: "Pokemons",
                column: "Id_FR");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Id_IT",
                table: "Pokemons",
                column: "Id_IT");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Id_JP",
                table: "Pokemons",
                column: "Id_JP");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_Id_RU",
                table: "Pokemons",
                column: "Id_RU");

            migrationBuilder.CreateIndex(
                name: "IX_Question_Answer_AnswerId",
                table: "Question_Answer",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_Answer_QuestionId",
                table: "Question_Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionTypeId",
                table: "Questions",
                column: "QuestionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTypes_DifficultyId",
                table: "QuestionTypes",
                column: "DifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizz_Question_QuestionId",
                table: "Quizz_Question",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizz_Question_QuizzId",
                table: "Quizz_Question",
                column: "QuizzId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizzDifficulties_QuizzId",
                table: "QuizzDifficulties",
                column: "QuizzId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzs_IdentityUserId",
                table: "Quizzs",
                column: "IdentityUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Pokemon_Attaque");

            migrationBuilder.DropTable(
                name: "Pokemon_Talent");

            migrationBuilder.DropTable(
                name: "Pokemon_TypePok");

            migrationBuilder.DropTable(
                name: "Pokemon_Weakness");

            migrationBuilder.DropTable(
                name: "Question_Answer");

            migrationBuilder.DropTable(
                name: "Quizz_Question");

            migrationBuilder.DropTable(
                name: "QuizzDifficulties");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Attaques");

            migrationBuilder.DropTable(
                name: "Talents");

            migrationBuilder.DropTable(
                name: "Pokemons");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Quizzs");

            migrationBuilder.DropTable(
                name: "TypeAttaques");

            migrationBuilder.DropTable(
                name: "TypesPok");

            migrationBuilder.DropTable(
                name: "DataInfos");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "QuestionTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Difficulties");
        }
    }
}
