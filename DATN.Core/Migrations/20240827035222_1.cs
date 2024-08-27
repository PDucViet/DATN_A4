using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DATN.Core.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastLoginTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    IsSentMail = table.Column<bool>(type: "bit", nullable: false),
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
                name: "Brand",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsOnList = table.Column<bool>(type: "bit", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "ImageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Magazines",
                columns: table => new
                {
                    MagazineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magazines", x => x.MagazineId);
                });

            migrationBuilder.CreateTable(
                name: "NewFeeds",
                columns: table => new
                {
                    NewFeedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewFeeds", x => x.NewFeedId);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                });

            migrationBuilder.CreateTable(
                name: "Origins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Origins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Percent = table.Column<double>(type: "float", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HowToParticipate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BannerUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    From = table.Column<DateTime>(type: "datetime2", nullable: true),
                    To = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeRanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeRanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountByPercent = table.Column<int>(type: "int", nullable: true),
                    DiscountByPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "CategoryTranslation",
                columns: table => new
                {
                    CategoryTranslationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TranslatedName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTranslation", x => x.CategoryTranslationId);
                    table.ForeignKey(
                        name: "FK_CategoryTranslation_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryTranslation_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    OriginId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "BrandId");
                    table.ForeignKey(
                        name: "FK_Products_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Origins_OriginId",
                        column: x => x.OriginId,
                        principalTable: "Origins",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CategoryTimeRange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    TimeRangeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTimeRange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryTimeRange_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryTimeRange_TimeRanges_TimeRangeId",
                        column: x => x.TimeRangeId,
                        principalTable: "TimeRanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoucherUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    VoucherId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoucherUsers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoucherUsers_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsShow = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attributes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    CategoryProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => x.CategoryProductId);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_ImageTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ImageTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Interesteds",
                columns: table => new
                {
                    InterestedID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interesteds", x => x.InterestedID);
                    table.ForeignKey(
                        name: "FK_Interesteds_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interesteds_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAddress",
                columns: table => new
                {
                    ProductAddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAddress", x => x.ProductAddressID);
                    table.ForeignKey(
                        name: "FK_ProductAddress_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAddress_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPromotions",
                columns: table => new
                {
                    ProductPromotionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPromotions", x => x.ProductPromotionId);
                    table.ForeignKey(
                        name: "FK_ProductPromotions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPromotions_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTranslation",
                columns: table => new
                {
                    ProductTranslationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductEAVId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    TranslatedName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TranslatedDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTranslation", x => x.ProductTranslationId);
                    table.ForeignKey(
                        name: "FK_ProductTranslation_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTranslation_Products_ProductEAVId",
                        column: x => x.ProductEAVId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoucherUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_VoucherUsers_VoucherUserId",
                        column: x => x.VoucherUserId,
                        principalTable: "VoucherUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttributeValues",
                columns: table => new
                {
                    AtributeValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsShow = table.Column<bool>(type: "bit", nullable: false),
                    AttributeId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeValues", x => x.AtributeValueId);
                    table.ForeignKey(
                        name: "FK_AttributeValues_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentInfos",
                columns: table => new
                {
                    PaymentInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: true),
                    PaymentMethod = table.Column<int>(type: "int", nullable: true),
                    PaymentStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInfos", x => x.PaymentInfoId);
                    table.ForeignKey(
                        name: "FK_PaymentInfos_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId");
                });

            migrationBuilder.CreateTable(
                name: "ShippingOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingOrders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShippingOrders_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Tax = table.Column<int>(type: "int", nullable: true),
                    PuscharPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AfterDiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    AttributeValueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_AttributeValues_AttributeValueId",
                        column: x => x.AttributeValueId,
                        principalTable: "AttributeValues",
                        principalColumn: "AtributeValueId");
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                columns: table => new
                {
                    InvoiceDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: true),
                    ProductAttributeId = table.Column<int>(type: "int", nullable: false),
                    OldPrice = table.Column<double>(type: "float", nullable: false),
                    NewPrice = table.Column<double>(type: "float", nullable: false),
                    PuscharPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetails", x => x.InvoiceDetailId);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId");
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_ProductAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    InvoiceDetailId = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_InvoiceDetails_InvoiceDetailId",
                        column: x => x.InvoiceDetailId,
                        principalTable: "InvoiceDetails",
                        principalColumn: "InvoiceDetailId");
                    table.ForeignKey(
                        name: "FK_Comments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressID", "Description" },
                values: new object[,]
                {
                    { 1, "Hà Nội" },
                    { 2, "Hồ Chí Minh" },
                    { 3, "Đà Nẵng" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"), null, "Admin", "ADMIN" },
                    { new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Description", "Dob", "Email", "EmailConfirmed", "FullName", "IsSentMail", "LastLoginTime", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "isActive" },
                values: new object[,]
                {
                    { new Guid("00bb44d1-f674-49f6-bdae-afb143ab9749"), 0, "456 Oak St, City B", "8a2f3780-0834-4e1b-beed-fb7fca3da736", "Customer", new DateTime(1991, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "customer@gmail.com", true, "Customer", false, new DateTime(2024, 8, 27, 3, 52, 21, 714, DateTimeKind.Utc).AddTicks(6269), false, null, "CUSTOMER@GMAIL.COM", "CUSTOMER@GMAIL.COM", "AQAAAAIAAYagAAAAEOb+4wOWTch7O6Ef4SzAgq2Jk/FIJS9V4fPiSW52j8n1aBfnN/F3LbE5h1+wXfFwuQ==", null, false, "", false, "customer@gmail.com", true },
                    { new Guid("2753c921-2304-4f8d-b8d5-75229d3b20d6"), 0, "123 Main St, City A", "c6d3fbd5-2449-400d-b74e-43e769c6f1c3", "Admin", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", true, "Admin", false, new DateTime(2024, 8, 27, 3, 52, 21, 591, DateTimeKind.Utc).AddTicks(2005), false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEJHwfC6vMZH6jpbDD6iYqG12A4/tjF1OUsam9jyiux+6w3D8edWPYH/49fcvsM5abg==", null, false, "", false, "admin@gmail.com", true },
                    { new Guid("aa7c5218-4f1e-4ac6-a3b4-08dcb162e29e"), 0, "456 Oak St, City B", "bcd94b3a-33fb-4b3e-b13f-fa841ae786ba", "Customer", new DateTime(1991, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "customer2@gmail.com", true, "Customer", false, new DateTime(2024, 8, 27, 3, 52, 21, 823, DateTimeKind.Utc).AddTicks(7700), false, null, "CUSTOMER2@GMAIL.COM", "CUSTOMER2@GMAIL.COM", "AQAAAAIAAYagAAAAEN+G3PZwWKeH8pnbRk0M2sDNKHSzZf8vITS5cE/Da5F3NcSCjcfZtT9vcw1dJLjzNw==", null, false, "", false, "customer2@gmail.com", true }
                });

            migrationBuilder.InsertData(
                table: "Brand",
                columns: new[] { "BrandId", "ImageUrl", "Name", "Status" },
                values: new object[,]
                {
                    { 1, "/Images/Brand/Lg.png", "LG", true },
                    { 2, "/Images/Brand/Samsung.png", "Samsung", true },
                    { 3, "/Images/Brand/Toshiba.png", "Toshiba", true },
                    { 4, "/Images/Brand/Sony.png", "Sony", true },
                    { 5, "/Images/Brand/Aqua.png", "Aqua", true },
                    { 6, "/Images/Brand/Xiaomi.png", "Xiaomi", true }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreateAt", "CreateBy", "DeleteAt", "DeleteBy", "Description", "ImageUrl", "IsOnList", "IsVisible", "Level", "Name", "ParentCategoryId", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8208), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8228), null, "", "/Images/Home/Luxury.png", true, true, 0, "Hàng cao cấp", null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8229), null },
                    { 2, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8235), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8235), null, "", "/Images/Home/Hot.png", true, true, 0, "Tivi, Loa, dàn karaoke", null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8236), null },
                    { 3, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8238), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8238), null, "", "/Images/Home/Hot.png", true, true, 0, "Tủ lạnh, Tủ đông, Tủ mát", null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8239), null },
                    { 4, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8241), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8242), null, "", "/Images/Home/Hot.png", true, true, 0, "Máy giặt, Máy sấy quần áo", null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8242), null },
                    { 5, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8244), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8245), null, "", "/Images/Home/Hot.png", true, true, 0, "Máy lạnh, Máy nước nóng", null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8245), null },
                    { 6, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8247), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8248), null, "", "/Images/Home/Icons-1.png", true, true, 0, "Điện gia dụng, Sinh tố, Xay ép", null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8248), null },
                    { 7, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8250), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8251), null, "", "/Images/Home//Hot.png", true, true, 0, "Bếp điện, Nồi cơm, Đồ bếp", null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8251), null },
                    { 8, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8254), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8255), null, "", "/Images/Home//Hot.png", true, true, 0, "Máy lọc nước, Nồi chiên", null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8255), null },
                    { 9, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8257), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8258), null, "", "/Images/Home/Icons-2.png", true, true, 0, "Máy hút bụi, Máy rửa chén", null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8258), null },
                    { 10, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8260), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8261), null, "", "/Images/Home/Icons-3.png", true, true, 0, "Xe đạp, Sức khỏe, Làm đẹp", null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8261), null },
                    { 11, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8263), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8264), null, "", "/Images/Home/Icons-4.png", true, true, 0, "Đồ nghề - dụng cụ sửa chữa", null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8264), null },
                    { 12, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8266), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8266), null, "", "/Images/Home/Icons-5.png", true, true, 0, "Điện thoại, Laptop, Tablet", null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8267), null },
                    { 13, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8269), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8270), null, "", "/Images/Home/Icons-6.png", true, true, 0, "Phụ kiện, Camera, Đồng hồ", null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8270), null },
                    { 14, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8272), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8273), null, "", "/Images/Home/Icons-7.png", true, true, 0, "Máy cũ, Dịch vụ hữu ích", null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8273), null }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "ContactId", "Address", "Email", "IsRead", "Phone" },
                values: new object[,]
                {
                    { 1, "123 Main St, City A", "example1@gmail.com", false, "0912345678" },
                    { 2, "456 Oak St, City B", "example2@gmail.com", true, "0912345679" },
                    { 3, "789 Pine St, City C", "example3@gmail.com", false, "0912345680" },
                    { 4, "101 Maple St, City D", "example4@gmail.com", true, "0912345681" },
                    { 5, "202 Birch St, City E", "example5@gmail.com", false, "0912345682" },
                    { 6, "303 Cedar St, City F", "example6@gmail.com", true, "0912345683" },
                    { 7, "404 Elm St, City G", "example7@gmail.com", false, "0912345684" },
                    { 8, "505 Ash St, City H", "example8@gmail.com", true, "0912345685" },
                    { 9, "606 Cherry St, City I", "example9@gmail.com", false, "0912345686" },
                    { 10, "707 Spruce St, City J", "example10@gmail.com", true, "0912345687" }
                });

            migrationBuilder.InsertData(
                table: "ImageTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Normal" },
                    { 2, "Banner" }
                });

            migrationBuilder.InsertData(
                table: "Language",
                columns: new[] { "Id", "CreateAt", "CreateBy", "DeleteAt", "DeleteBy", "Description", "Name", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(2253), null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(2254), null, "", "English", new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(2254), null },
                    { 2, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(2255), null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(2256), null, "", "VietNam", new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(2256), null }
                });

            migrationBuilder.InsertData(
                table: "Magazines",
                columns: new[] { "MagazineId", "Caption", "Content", "CreateAt", "Image", "Status" },
                values: new object[,]
                {
                    { 1, "Danh sách trúng thưởng - Tưng bừng \"Tháng Panasonic\": Cơ hội trúng 102 bộ quà giá trị lên đến 655 triệu đồng", "<h2 style=\"margin-left:0px;\">Cơn sốt game chưa bao giờ hạ nhiệt! Bạn muốn chiến game \"cực phê\" với trải nghiệm đỉnh cao? Chọn ngay <a href=\"https://www.dienmayxanh.com/man-hinh-may-tinh\">màn hình</a> chơi game ROG và nhận quà \"khủng\" - nâng cấp trải nghiệm, chiến thắng mọi thử thách!</h2><p style=\"margin-left:0px;\"><img class=\"image_resized\" style=\"aspect-ratio:845/442;height:auto !important;width:auto;\" src=\"https://cdnv2.tgdd.vn/mwg-static/common/News/1565736/ROG.jpg\" alt=\"Chiến gam hay nhận quà đỉnh\" width=\"845\" height=\"442\"></p><h3 style=\"margin-left:0px;\">1. Thời gian khuyến mãi: Từ 13/08 đến 30/09/2024</h3><h3 style=\"margin-left:0px;\">2. Nội dung chương trình</h3><p style=\"margin-left:0px;\">Trong thời gian khuyến mãi, khi mua mua <a href=\"https://www.dienmayxanh.com/man-hinh-may-tinh/asus-xg27acs-27-inch-2k\">Màn hình Asus Gaming ROG Strix XG27ACS</a> tại Thế giới Di động và Điện máy XANH, bạn sẽ nhận ngay 01 <a href=\"https://rog.asus.com/vn/mice-mouse-pads/mice/ambidextrous/rog-strix-impact-iii-model/\">Chuột ROG STRIX IMPACT III</a>.</p><p style=\"margin-left:0px;\">Áp dụng cho cả mua online và offline.</p><p style=\"margin-left:0px;\">Chương trình có thể kết thúc sớm nếu hết quà.</p><p style=\"margin-left:0px;\">Hạn chót đăng ký nhận quà: 03/10/2024.</p><h3 style=\"margin-left:0px;\">3. Sản phẩm áp dụng</h3><p style=\"margin-left:0px;\"><a href=\"https://www.dienmayxanh.com/man-hinh-may-tinh/asus-xg27acs-27-inch-2k?itm_source=khuyenmai&amp;itm_medium=shortcode&amp;itm_content=325113\"><img class=\"image_resized\" style=\"aspect-ratio:600/600;height:auto !important;width:150px;\" src=\"https://cdn.tgdd.vn/Products/Images/5697/325113/asus-xg27acs-27-inch-2k-thumb-600x600.jpg\" width=\"600\" height=\"600\"></a></p><figure class=\"image image_resized\" style=\"height:auto !important;width:40px !important;\"><a href=\"https://www.dienmayxanh.com/man-hinh-may-tinh/asus-xg27acs-27-inch-2k?itm_source=khuyenmai&amp;itm_medium=shortcode&amp;itm_content=325113\"><img style=\"aspect-ratio:40/40;\" src=\"https://cdn.tgdd.vn/ValueIcons/label-baohanh3nam.png\" width=\"40\" height=\"40\"></a></figure><p style=\"margin-left:0px;\"><a href=\"https://www.dienmayxanh.com/man-hinh-may-tinh/asus-xg27acs-27-inch-2k?itm_source=khuyenmai&amp;itm_medium=shortcode&amp;itm_content=325113\"><span style=\"color:rgb(51,51,51);\"><strong>Asus Gaming 27 inch 2K XG27ACS</strong></span></a></p><p style=\"margin-left:0px;\">Ngừng kinh doanh</p><p style=\"margin-left:0px;\"><a href=\"https://www.dienmayxanh.com/man-hinh-may-tinh/asus-xg27acs-27-inch-2k?itm_source=khuyenmai&amp;itm_medium=shortcode&amp;itm_content=325113\">Xem chi tiết</a></p><h3 style=\"margin-left:0px;\">4. Cách thức nhận quà</h3><p style=\"margin-left:0px;\">Click vào nút \"Đăng ký\" trên website sự kiện <a href=\"https://www.asus.com/vn/events/infoM/activity_ROGLCD\">https://www.asus.com/vn/events/infoM/activity_ROGLCD</a>.</p><p style=\"margin-left:0px;\">- Đính kèm hình ảnh hóa đơn mua hàng, phiếu thu, phiếu xuất kho có dấu xác nhận của cửa hàng, thể hiện rõ tên sản phẩm và ngày mua hàng trong thời gian chương trình diễn ra.</p><p style=\"margin-left:0px;\">- Đính kèm ảnh chụp số S/N của sản phẩm.</p><p style=\"margin-left:0px;\">- Điền serial của sản phẩm.</p><p style=\"margin-left:0px;\">- Điền Họ và tên người nhận quà.</p><p style=\"margin-left:0px;\">- Điền Địa chỉ nhận quà.</p><p style=\"margin-left:0px;\">- Điền Số điện thoại liên hệ.</p><p style=\"margin-left:0px;\">Hình chụp số S/N trên sản phẩm phải chụp kèm với hoá đơn, chỉ chấp nhận hình chụp số S/N trên sản phẩm, số S/N trên hộp không có hiệu lực.</p><p style=\"margin-left:0px;\"><img class=\"image_resized\" style=\"aspect-ratio:732/786;height:auto !important;width:auto;\" src=\"https://cdnv2.tgdd.vn/mwg-static/common/News/1565736/image1%20%283%29.jpg\" alt=\"Bước 1\" width=\"732\" height=\"786\"></p><p style=\"margin-left:0px;\"><img class=\"image_resized\" style=\"aspect-ratio:732/753;height:auto !important;width:100%;\" src=\"https://cdnv2.tgdd.vn/mwg-static/common/News/1565736/image2%20%283%29.jpg\" alt=\"Bước 2\" width=\"732\" height=\"753\"></p><h3 style=\"margin-left:0px;\">4. Thông tin lưu ý</h3><p style=\"margin-left:0px;\">- Trong trường hợp không thể đăng ký trên hệ thống, quý khách hàng vui lòng gửi thông tin nhận quà như bên dưới về địa chỉ Email: dangkyasus@gmail.com với tiêu đề: \"[Khuyen mai] LCD GAMING\".</p><p style=\"margin-left:0px;\">- ASUS sẽ kiểm tra và liên hệ gửi quà tới bạn trong 20 ngày làm việc (trừ thứ 7 &amp; Chủ nhật).</p><p style=\"margin-left:0px;\">- Chương trình không áp dụng đồng thời với các chương trình khuyến mãi khác.</p><p style=\"margin-left:0px;\">- Quà tặng không có giá trị quy đổi thành tiền mặt hoặc các giá trị khác tương đương.</p><p style=\"margin-left:0px;\">- Hóa đơn/ phiếu thu/ phiếu xuất kho phải có con dấu của cửa hàng bán lẻ.</p><p style=\"margin-left:0px;\">- Chương trình chỉ dành cho khách hàng mua lẻ từ cửa hàng, tất cả hóa đơn từ nhà phân phối đều không được tham gia chương trình khuyến mãi này.</p><p style=\"margin-left:0px;\">- Dung lượng hình ảnh hóa đơn tải lên hệ thống &lt;1Mb.</p><p style=\"margin-left:0px;\">- Trong trường hợp cần thiết nhằm bảo đảm quyền lợi khách hàng, phía ASUS sẽ cần thêm những thông tin khác để xác định khách mua hàng là chính xác như: hóa đơn đỏ, giấy tờ tùy thân có ảnh đại diện,...</p><p style=\"margin-left:0px;\">Đây là chương trình của ASUS không phải của dienmayxanh.com và thegioididong.com, mọi góp ý hay thắc mắc khác về chương trình, xin vui lòng post tại <a href=\"https://www.facebook.com/share/g/NB6YhgtTY4wDwQ47/?mibextid=K35XfP\">Hội linh kiện PC ASUS ROG Việt Nam</a>.</p>", new DateTime(2024, 8, 28, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(2217), "/Images/Magazine/Picture1.jpg", 1 },
                    { 2, "Mua ngay máy lạnh Daikin: Thêm năm bảo hành, an tâm chất \"Nhật\"", "Content 2", new DateTime(2024, 8, 29, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(2225), "/Images/Magazine/Picture2.jpg", 1 },
                    { 3, "Mua sớm máy lạnh - Tặng 2 lần vệ sinh chỉ có tại Điện máy XANH | Mới 2024", "Content 3", new DateTime(2024, 8, 30, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(2232), "/Images/Magazine/Picture3.jpg", 1 },
                    { 4, "Khai lộc thăng hạng, vượt đỉnh thăng hoa: Mua Tivi LG nhận ưu đãi đến 51.000.000đ cùng gói ứng dụng giải trí hấp dẫn", "Content 4", new DateTime(2024, 8, 31, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(2234), "/Images/Magazine/Picture4.jpg", 1 }
                });

            migrationBuilder.InsertData(
                table: "NewFeeds",
                columns: new[] { "NewFeedId", "Content", "ProductId", "Subject" },
                values: new object[,]
                {
                    { 1, "We are excited to announce the release of our new product.", 101, "New Product Release" },
                    { 2, "Our product has been updated with new features.", 102, "Product Update" },
                    { 3, "Enjoy a special offer on our latest product.", 103, "Special Offer" },
                    { 4, "Check out our new limited edition product.", 104, "Limited Edition" },
                    { 5, "Read the latest review on our product.", 105, "Product Review" },
                    { 6, "We value your feedback on our product.", 106, "Customer Feedback" },
                    { 7, "Important notice about a product recall.", 107, "Product Recall" },
                    { 8, "Don't miss out on our holiday sale.", 108, "Holiday Sale" },
                    { 9, "Join us for the launch event of our new product.", 109, "Product Launch Event" },
                    { 10, "Participate in our product survey and win prizes.", 110, "Product Survey" }
                });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "NotificationId", "Content", "Subject" },
                values: new object[,]
                {
                    { 1, "Welcome to our service!", "Welcome" },
                    { 2, "Your order has been shipped.", "Order Shipped" },
                    { 3, "Your order has been delivered.", "Order Delivered" },
                    { 4, "New promotions available!", "Promotions" },
                    { 5, "Your subscription is about to expire.", "Subscription Expiry" },
                    { 6, "We have updated our terms of service.", "Terms of Service Update" },
                    { 7, "Password reset request.", "Password Reset" },
                    { 8, "Your account has been activated.", "Account Activation" },
                    { 9, "Monthly newsletter available.", "Newsletter" },
                    { 10, "Your feedback is important to us.", "Feedback Request" }
                });

            migrationBuilder.InsertData(
                table: "Origins",
                columns: new[] { "Id", "CreateAt", "CreateBy", "DeleteAt", "DeleteBy", "Description", "Name", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8500), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8498), null, "Xuất xứ: Trung Quốc", "Trung Quốc", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8498), null },
                    { 2, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8503), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8502), null, "Xuất xứ: Hàn Quốc", "Hàn Quốc", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8502), null },
                    { 3, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8506), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8505), null, "Xuất xứ: Mỹ", "Mỹ", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8505), null },
                    { 4, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8509), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8507), null, "Xuất xứ: Nhật Bản", "Nhật Bản", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8508), null },
                    { 5, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8511), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8510), null, "Xuất xứ: Đức", "Đức", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8510), null },
                    { 6, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8514), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8512), null, "Xuất xứ: Anh", "Anh", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8513), null },
                    { 7, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8516), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8515), null, "Xuất xứ: Pháp", "Pháp", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8515), null },
                    { 8, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8519), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8517), null, "Xuất xứ: Việt Nam", "Việt Nam", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8518), null },
                    { 9, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8521), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8520), null, "Xuất xứ: Úc", "Úc", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8520), null },
                    { 10, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8524), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8522), null, "Xuất xứ: Canada", "Canada", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8523), null }
                });

            migrationBuilder.InsertData(
                table: "Promotions",
                columns: new[] { "Id", "BackgroundColor", "BannerUrl", "Content", "CreateAt", "CreateBy", "DeleteAt", "DeleteBy", "Description", "From", "HowToParticipate", "IsActive", "Name", "Percent", "To", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, null, null, null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1778), null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1793), null, "Khuyến mãi giảm giá 10% cho tất cả các sản phẩm", new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1801), null, false, "Giảm giá 10%", 10.0, new DateTime(2024, 9, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1803), new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1797), null },
                    { 2, null, null, null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1812), null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1812), null, "Chương trình mua 1 tặng 1 cho sản phẩm đã chọn", new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1814), null, false, "Mua 1 tặng 1", 0.0, new DateTime(2024, 10, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1814), new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1813), null },
                    { 3, null, null, null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1816), null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1817), null, "Miễn phí vận chuyển cho đơn hàng trên 500,000 VNĐ", new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1818), null, false, "Miễn phí vận chuyển", 0.0, new DateTime(2024, 11, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1819), new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1817), null },
                    { 4, null, null, null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1820), null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1820), null, "Nhận voucher trị giá 100,000 VNĐ cho mỗi đơn hàng", new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1822), null, false, "Tặng voucher 100,000 VNĐ", 0.0, new DateTime(2024, 12, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1822), new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1821), null },
                    { 5, null, null, null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1824), null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1824), null, "Khuyến mãi giảm giá 50% cho tất cả sản phẩm mới ra mắt", new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1826), null, false, "Giảm 50% cho sản phẩm mới", 50.0, new DateTime(2025, 1, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1826), new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1825), null },
                    { 6, null, null, null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1828), null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1829), null, "Nhận quà tặng hấp dẫn khi đặt hàng trên 1,000,000 VNĐ", new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1830), null, false, "Tặng quà cho đơn hàng trên 1,000,000 VNĐ", 0.0, new DateTime(2025, 2, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1831), new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1829), null },
                    { 7, null, null, null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1834), null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1835), null, "Khuyến mãi giảm giá 20% cho tất cả sản phẩm phụ kiện", new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1836), null, false, "Giảm 20% cho sản phẩm phụ kiện", 20.0, new DateTime(2025, 3, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1837), new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1835), null },
                    { 8, null, null, null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1838), null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1839), null, "Chương trình đổi sản phẩm cũ lấy sản phẩm mới với giá ưu đãi", new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1840), null, false, "Đổi cũ lấy mới", 0.0, new DateTime(2025, 4, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1841), new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1839), null },
                    { 9, null, null, null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1842), null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1842), null, "Mua hàng tặng phiếu mua hàng trị giá 200,000 VNĐ", new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1843), null, false, "Mua hàng tặng phiếu mua hàng", 0.0, new DateTime(2025, 5, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1844), new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1843), null },
                    { 10, null, "/Images/Component/BannerPromotion1.gif", null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1845), null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1846), null, "Siêu sale đồng giảm 50% giá sản phẩm", new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1847), null, true, "Đồng giảm giá 50%", 50.0, new DateTime(2025, 6, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1848), new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1846), null },
                    { 11, null, "/Images/Component/BannerPromotion2.webp", null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1851), null, new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1852), null, "Siêu giảm giá mùa Ơ-RÔ", new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1853), null, true, "Sôi động cùng mùa Ơ-rô", 0.0, new DateTime(2025, 6, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1854), new DateTime(2024, 8, 27, 10, 52, 21, 510, DateTimeKind.Local).AddTicks(1852), null }
                });

            migrationBuilder.InsertData(
                table: "TimeRanges",
                columns: new[] { "Id", "MaxPrice", "MinPrice", "Name" },
                values: new object[,]
                {
                    { 1, 1000000m, 0m, "0 - 1 triệu" },
                    { 2, 2000000m, 1000000m, "1 triệu - 2 triệu" },
                    { 3, 3000000m, 2000000m, "2 triệu - 3 triệu" },
                    { 4, 4000000m, 3000000m, "3 triệu - 4 triệu" },
                    { 5, 5000000m, 4000000m, "4 triệu - 5 triệu" },
                    { 6, 6000000m, 5000000m, "5 triệu - 6 triệu" },
                    { 7, 7000000m, 6000000m, "6 triệu - 7 triệu" },
                    { 8, 8000000m, 7000000m, "7 triệu - 8 triệu" },
                    { 9, 9000000m, 8000000m, "8 triệu - 9 triệu" },
                    { 10, 10000000m, 9000000m, "9 triệu - 10 triệu" },
                    { 11, 999999999m, 10000000m, "Hơn 10 triệu" }
                });

            migrationBuilder.InsertData(
                table: "Vouchers",
                columns: new[] { "Id", "CreateAt", "CreateBy", "DeleteAt", "DeleteBy", "Description", "DiscountByPercent", "DiscountByPrice", "Name", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9638), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9633), null, "", 10, 0m, "VOUCHER10", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9634), null },
                    { 2, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9641), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9639), null, "", 20, 0m, "VOUCHER20", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9640), null },
                    { 3, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9650), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9643), null, "", 0, 300000m, "VOUCHER300000", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9643), null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("fe0e9c2d-6abd-4f73-a635-63fc58ec700e"), new Guid("00bb44d1-f674-49f6-bdae-afb143ab9749") },
                    { new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"), new Guid("2753c921-2304-4f8d-b8d5-75229d3b20d6") }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreateAt", "CreateBy", "DeleteAt", "DeleteBy", "Description", "ImageUrl", "IsOnList", "IsVisible", "Level", "Name", "ParentCategoryId", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 15, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8275), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8275), null, "", null, true, true, 1, "Tivi", 2, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8276), null },
                    { 16, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8283), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8283), null, "", null, true, true, 1, "Loa", 2, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8284), null },
                    { 17, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8286), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8287), null, "", null, true, false, 1, "Phụ kiện Tivi", 2, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8287), null },
                    { 18, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8289), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8289), null, "", null, true, true, 1, "Tủ lạnh", 3, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8290), null },
                    { 19, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8292), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8292), null, "", null, true, true, 1, "Tủ đông", 3, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8293), null },
                    { 20, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8295), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8295), null, "", null, true, true, 1, "Tủ mát", 3, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8296), null },
                    { 21, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8298), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8299), null, "", null, true, true, 1, "Máy giặt", 4, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8300), null },
                    { 22, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8333), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8334), null, "", null, true, true, 1, "Máy sấy quần áo", 4, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8335), null },
                    { 23, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8337), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8337), null, "", null, true, false, 1, "Phụ kiện máy giặt", 4, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8338), null },
                    { 24, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8344), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8344), null, "", null, true, true, 1, "Máy lạnh", 5, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8345), null },
                    { 25, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8347), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8347), null, "", null, true, true, 1, "Máy nước nóng", 5, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8348), null },
                    { 26, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8350), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8351), null, "", null, true, true, 1, "Máy xay các loại", 6, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8351), null },
                    { 27, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8353), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8354), null, "", null, true, true, 1, "Máy ép trái cây", 6, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8354), null },
                    { 28, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8356), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8357), null, "", null, true, true, 1, "Điện gia dụng", 6, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8357), null },
                    { 29, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8359), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8360), null, "", null, true, false, 1, "Nồi", 6, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8360), null },
                    { 30, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8362), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8363), null, "", null, true, false, 1, "Bếp", 6, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8363), null },
                    { 31, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8365), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8366), null, "", null, true, false, 1, "Lò", 6, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8367), null }
                });

            migrationBuilder.InsertData(
                table: "CategoryTranslation",
                columns: new[] { "CategoryTranslationId", "CategoryId", "LanguageId", "TranslatedName" },
                values: new object[,]
                {
                    { 1, 1, 1, "Electronics" },
                    { 2, 2, 1, "Mobile Phones" },
                    { 3, 3, 1, "Laptops" },
                    { 4, 4, 1, "Tablets" },
                    { 5, 5, 1, "Cameras" },
                    { 6, 6, 1, "Audio" },
                    { 7, 7, 1, "Home Appliances" },
                    { 8, 8, 1, "Refrigerators" },
                    { 9, 9, 1, "Washing Machines" },
                    { 10, 10, 1, "Water Purifiers" },
                    { 11, 11, 1, "Microwave Ovens" },
                    { 12, 12, 1, "Electric Stoves" },
                    { 13, 13, 1, "Rice Cookers" },
                    { 14, 14, 1, "Air Purifiers" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ImagePath", "IsDefault", "ProductId", "TypeId" },
                values: new object[] { 13, "https://www.dienmayxanh.com/flashsale", false, null, 2 });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "CreateDate", "Note", "Status", "UserId", "VoucherUserId" },
                values: new object[,]
                {
                    { 2, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9441), null, 0, new Guid("00bb44d1-f674-49f6-bdae-afb143ab9749"), null },
                    { 3, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9469), null, 0, new Guid("00bb44d1-f674-49f6-bdae-afb143ab9749"), null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "CreateAt", "CreateBy", "Description", "Name", "OriginId", "Status", "UpdateAt" },
                values: new object[,]
                {
                    { 1, 2, null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8594), new Guid("d7d5c778-2d8a-4a2b-ab12-285f1cb4b08f"), "<h3 style=\"margin-left:0px;\">Tổng quan thiết kế&nbsp;</h3><p style=\"margin-left:0px;\">- <a href=\"https://www.dienmayxanh.com/tivi/smart-khung-tranh-the-frame-qled-samsung-4k-50-inch-qa50ls03b\">Smart Tivi Khung Tranh The Frame QLED Samsung 4K 50 inch QA50LS03B</a>&nbsp;với thiết kế đơn giản nhưng tinh tế, hòa mình một cách hoàn hảo vào bất kỳ không gian nội thất nào. Viền tivi siêu mỏng, chắc chắn hơn và mặt sau được hoàn thiện tỉ mỉ với màu sắc đơn sắc ấn tượng, The Frame làm nổi bật vẻ đẹp tinh tế của TV từ mọi góc nhìn\r\n\r\n\r\n<i>*Hình ảnh chỉ mang tính chất minh họa</i>\r\n</p><p style=\"margin-left:0px;\">Khung viền linh hoạt giúp The Frame hòa mình vào mọi không gian. Với sự đa dạng trong lựa chọn khung viền, bạn có thể thỏa sức ngắm nhìn tuyệt tác khung tranh của mình. Các khớp nối nam châm linh hoạt cho phép bạn dễ dàng chuyển đổi màu sắc và phong cách khung viền.\r\n\r\n\r\n<i>*Hình ảnh chỉ mang tính chất minh họa</i>\r\n</p><p style=\"margin-left:0px;\">- Bên cạnh giải pháp treo tường, The Frame còn có chân đế bằng nhựa chắc chắn và có thể điều chỉnh độ cao cho phép bạn nâng lên cao và hạ xuống dễ dàng.</p><p style=\"margin-left:0px;\">- Màn hình&nbsp;<a href=\"https://www.dienmayxanh.com/tivi?g=50-inch\">50 inch</a>&nbsp;phù hợp cho những không gian có diện tích vừa.</p><p style=\"margin-left:0px;\">- <strong>Bật lên là </strong><a href=\"https://www.dienmayxanh.com/tivi\"><strong>tivi</strong></a><strong>, tắt đi là khung tranh </strong>mang dáng vóc của một tác phẩm nghệ thuật,&nbsp;thích hợp&nbsp;với không gian phòng khách, phòng ngủ, phòng trưng bày,... đặc biệt phù hợp với những người yêu hội họa, yêu cái đẹp\r\n\r\n<i>*Hình ảnh chỉ mang tính chất minh họa</i>\r\n</p><h3 style=\"margin-left:0px;\">Công nghệ hình ảnh</h3><p style=\"margin-left:0px;\">-&nbsp;Độ phân giải<strong>&nbsp;</strong><a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/tivi-uhd-tivi-4k-la-gi-co-khac-gi-so-voi-tivi-full-578555\"><strong>4K</strong></a>&nbsp;cho hình ảnh sắc nét đến từng chi tiết.</p><p style=\"margin-left:0px;\">-&nbsp;<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/quantum-4k-bo-vi-xu-ly-manh-me-cua-samsung-1175879\"><strong>Bộ xử lý Quantum 4K</strong></a><strong>&nbsp;</strong>sẽ<strong>&nbsp;</strong>tự động nâng cấp chất lượng hình ảnh lên gần chuẩn 4K khi chất lượng nguồn vào chưa đạt chuẩn nhờ công nghệ trí thông minh nhân tạo AI.</p><p style=\"margin-left:0px;\">-&nbsp;<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/cong-nghe-hinh-anh-tren-tivi-samsung-838738#quantum-hdr\"><strong>Quantum HDR</strong></a><strong>&nbsp;</strong>điều chỉnh độ sáng theo từng khung hình hiển thị giúp mọi chi tiết hình ảnh đều trở nên rõ ràng, bắt mắt.</p><p style=\"margin-left:0px;\">-<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/cac-cong-nghe-noi-bat-cua-tivi-samsung-20-1339340#motion-xcelerator-turbo+\">&nbsp;<strong>Motion Xcelerator Turbo+</strong></a>&nbsp;thêm khung hình vào nội dung gốc,&nbsp;độ rõ nét của chuyển động được nâng cao, cho hình ảnh mượt mà, trải nghiệm chơi game đỉnh cao.</p><p style=\"margin-left:0px;\">-&nbsp;<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/tivi-samsung-2020-co-gi-moi-1240803#adaptive-picture\"><strong>Adaptive Picture</strong></a>&nbsp;tối ưu độ sáng hình ảnh theo điều kiện môi trường xung quanh, đem lại&nbsp;chất lượng hình ảnh tuyệt hảo, đồng nhất mọi nội dung dù xem ở bất kỳ thời điểm nào trong ngày.</p><p style=\"margin-left:0px;\">- Đặc biệt, chiếc tivi Lifestyle được trang bị kho tranh với hơn 1600 tác phẩm (sử dụng miễn phí trong 3 tháng đầu), tải miễn phí 25 tác phẩm trong thời gian trên.</p><p style=\"margin-left:0px;\">Mời bạn xem thêm:<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/diem-mat-nhung-do-phan-giai-pho-bien-hien-nay-tren-577178\">&nbsp;Những độ phân giải màn hình phổ biến hiện nay trên tivi</a>\r\n\r\n <i>*Hình ảnh chỉ mang tính chất minh họa</i>\r\n</p><h3 style=\"margin-left:0px;\">Công nghệ âm thanh</h3><p style=\"margin-left:0px;\">- Hệ thống loa <strong>20 W</strong> sống động.</p><p style=\"margin-left:0px;\">- Công nghệ <a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/cong-nghe-am-thanh-object-tracking-sound-1344551#object-tracking-sound-lite\"><strong>Object Tracking Sound Lite (OTS Lite)</strong></a><strong>&nbsp;</strong>làm cho âm thanh phù hợp với hình ảnh chuyển động trên khung hình, mang đến những làn âm thanh lan tỏa rộng khắp không gian.</p><p style=\"margin-left:0px;\">- <a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/tivi-samsung-2020-co-gi-moi-1240803#cong-nghe-am-thanh-ava\"><strong>Active Voice Amplifier (AVA)</strong></a> có thể điều chỉnh âm lượng hội thoại theo điều kiện môi trường bên ngoài, giúp bạn có thể vừa làm việc nhà vừa thưởng thức những bộ phim hấp dẫn.</p><p style=\"margin-left:0px;\">- <a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/cac-cong-nghe-noi-bat-cua-tivi-samsung-20-1339340#q-symphony\"><strong>Q-Symphony</strong></a> cầu nối giữa loa tivi và loa thanh giúp tối ưu công suất loa,&nbsp;tạo ra không gian giải trí đỉnh cao.</p><p style=\"margin-left:0px;\">- <strong>Adaptive Sound</strong> âm thanh được tinh chỉnh phù hợp với nội dung giải trí, đem đến cho người dùng trải nghiệm âm thanh hoành tráng, mạnh mẽ.\r\n\r\n<i>*Hình ảnh chỉ mang tính chất minh họa</i>\r\n</p><h3 style=\"margin-left:0px;\">Hệ điều hành</h3><p style=\"margin-left:0px;\">- <strong>Hệ điều hành Tizen</strong>&nbsp;trực quan, thân thiện với người dùng.</p><p style=\"margin-left:0px;\">- Hệ điều hành <a href=\"https://www.dienmayxanh.com/tivi-samsung\">tivi Samsung</a> sở hữu kho ứng dụng phong phú như: Clip TV, FPT Play, Galaxy Play (Fim+), MP3 Zing, MyTV, Netflix,&nbsp;<a href=\"https://www.thegioididong.com/game-app/pops-kids-phim-nhac-tv-show-220604\">POPS Kids</a>, Spotify, Trình duyệt web, VieON, YouTube,... đáp ứng cơ bản nhu cầu giải trí cho người dùng.</p><p style=\"margin-left:0px;\">Xem thêm:&nbsp;<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/cach-xem-phim-online-tren-tivi-887946#xem-phim-tren-trinh-duyet-web\">Cách xem phim bằng trình duyệt web trên tivi</a>\r\n\r\n <i>*Hình ảnh chỉ mang tính chất minh họa</i>\r\n</p><h3 style=\"margin-left:0px;\">Tiện ích</h3><p style=\"margin-left:0px;\">- Công nghệ&nbsp;<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/cac-cong-nghe-noi-bat-cua-tivi-samsung-20-1339340#multi-view\"><strong>Multi View</strong></a>&nbsp;giúp xem cùng lúc nhiều nội dung nhờ khả năng chia hai màn hình tivi.</p><p style=\"margin-left:0px;\">- Ứng dụng&nbsp;<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/huong-dan-su-dung-ung-dung-smartthings-dieu-khien-1126015\"><strong>SmartThings</strong></a>&nbsp;hỗ trợ kết nối và sử dụng tivi cũng như các thiết bị trong nhà dễ dàng và nhanh chóng.</p><p style=\"margin-left:0px;\">-&nbsp;<a href=\"https://xn--xem%20thm%20v%20one%20remote-csc9820u/\"><strong>One Remote</strong></a>&nbsp;có thể sạc bằng năng lượng mặt trời tiện lợi, bảo vệ môi trường.</p><p style=\"margin-left:0px;\">- Chiếu màn hình từ điện thoại nhanh chóng nhờ&nbsp;<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/air-play-2-tren-tivi-la-gi-nhung-tivi-nao-ho-tro-a-1172156\">AirPlay 2</a>, <a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/huong-dan-ghep-noi-screen-mirroring-giua-dien-thoa-571562\">Screen Mirroring</a>,&nbsp;<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/cac-cong-nghe-noi-bat-cua-tivi-samsung-20-1339340#tap-view\">Tap View.</a></p><p style=\"margin-left:0px;\">Xem thêm:&nbsp;<a href=\"https://www.dienmayxanh.com/kinh-nghiem-hay/cach-su-dung-tinh-nang-chia-doi-man-hinh-tivi-mult-1257139\">Cách sử dụng tính năng Multi View trên tivi Samsung</a>\r\n\r\n <i>Nhìn chung, với thiết kế khung tranh đặc biệt, mỏng, nhẹ, chắc chắn,&nbsp;bộ xử lý Quantum 4K, công nghệ OTS Lite âm thanh chuyển động theo hình ảnh, hệ điều hành Tizen OS 6.0 đa nhiệm, tiện ích phong phú sẽ là lựa chọn không thể tuyệt vời hơn dành cho các tín đồ nghệ thuật và yêu cái đẹp.</i></p>", "Samsung Smart TV QLED QA55Q70C", 1, 1, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8594) },
                    { 2, 1, null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8614), new Guid("be67907c-0ec8-46d5-996d-d175b68b6df3"), "Description of Product 2", "LG Smart TV NanoCell 55NANO76SQA", 4, 1, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8615) },
                    { 3, 1, null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8619), new Guid("fcc58311-d8f5-46cb-9996-0ea853751fb1"), "Description of Product 2", "LG Smart TV 65UQ8000PSC", 7, 1, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8620) },
                    { 4, 2, null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8625), new Guid("d5e1f711-fb00-430b-9b73-9e8d553c144c"), "Description of Product 2", "Samsung Smart TV QLED Khung tranh QA50LS03B", 8, 1, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8626) },
                    { 5, 1, null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8630), new Guid("e3f05096-733c-4020-a8dc-b0dcb95ad934"), "Description of Product 2", "AQUA Android TV AQT55D67UG", 10, 1, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8630) },
                    { 6, 6, null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8634), new Guid("fe81a7c0-8c11-412f-9257-4109602fa5b7"), "Description of Product 2", "LG Smart TV 50UQ7550PSF", 7, 1, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8672) },
                    { 7, 1, null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8676), new Guid("2bccda2f-32c1-42b5-ba57-b85eb0482d4b"), "Description of Product 2", "Bình đun siêu tốc Delites 1.8 lít ST18S05", 7, 1, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8676) },
                    { 8, 1, null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8680), new Guid("a124518b-3c01-4fb5-9da3-2b35ba5161b4"), "Description of Product 2", "Bình đun siêu tốc Delites 1.8 lít ST18S05", 7, 1, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8681) },
                    { 9, 1, null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8684), new Guid("348a4f29-c708-4b7f-a055-b93d9db50fd3"), "Description of Product 2", "Bình đun siêu tốc Delites 1.8 lít ST18S05", 7, 1, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8685) },
                    { 10, 1, null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8691), new Guid("c5c13496-61c5-463e-87bf-814c2e28fc57"), "Description of Product 2", "Bình đun siêu tốc Delites 1.8 lít ST18S05", 7, 1, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8691) },
                    { 11, 1, null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8695), new Guid("33863c0c-d33a-4ef1-96de-5123d707f9c6"), "Description of Product 2", "Bình đun siêu tốc Delites 1.8 lít ST18S05", 7, 1, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8696) },
                    { 12, 1, null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8699), new Guid("f3f36d4b-8330-4d08-b457-4c2dda50aa11"), "Description of Product 2", "Bình đun siêu tốc Delites 1.8 lít ST18S05", 7, 1, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8700) }
                });

            migrationBuilder.InsertData(
                table: "VoucherUsers",
                columns: new[] { "Id", "AppUserId", "CreateAt", "CreateBy", "DeleteAt", "DeleteBy", "Description", "From", "IsDeleted", "Name", "To", "UpdateAt", "UpdateBy", "VoucherId" },
                values: new object[,]
                {
                    { 1, new Guid("00bb44d1-f674-49f6-bdae-afb143ab9749"), new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9669), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9669), null, "", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9672), true, "", new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9670), null, 1 },
                    { 2, new Guid("00bb44d1-f674-49f6-bdae-afb143ab9749"), new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9674), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9674), null, "", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9676), false, "", new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9675), null, 2 },
                    { 3, new Guid("00bb44d1-f674-49f6-bdae-afb143ab9749"), new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9677), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9678), null, "", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9680), false, "", new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9678), null, 3 }
                });

            migrationBuilder.InsertData(
                table: "Attributes",
                columns: new[] { "Id", "IsActive", "IsShow", "Name", "ProductId", "Type" },
                values: new object[,]
                {
                    { 1, true, true, "Color", 2, 2 },
                    { 2, true, true, "Size", 2, 2 },
                    { 3, true, true, "Công xuất", 1, 2 },
                    { 4, true, true, "Loại máy", 1, 2 },
                    { 5, true, true, "Kích cỡ màn hình", 1, 2 },
                    { 6, true, true, "Loại tivi", 1, 0 },
                    { 7, true, true, "Hệ điều hành", 1, 0 },
                    { 8, true, true, "Ứng dụng phổ biến", 1, 0 },
                    { 9, true, true, "Công nghệ hình ảnh", 1, 0 },
                    { 10, true, true, "Điều khiển bằng giọng nói", 1, 0 },
                    { 11, true, true, "Remote thông minh", 1, 0 },
                    { 12, true, true, "Chiếu hình từ điện thoại lên TV", 1, 0 },
                    { 13, true, true, "Kích thước", 1, 0 },
                    { 14, true, true, "Độ phân giải", 1, 0 },
                    { 15, true, true, "Chất liệu viền tivi", 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreateAt", "CreateBy", "DeleteAt", "DeleteBy", "Description", "ImageUrl", "IsOnList", "IsVisible", "Level", "Name", "ParentCategoryId", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 32, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8368), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8369), null, "", null, true, true, 2, "Màn hình cong", 15, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8370), null },
                    { 33, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8372), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8373), null, "", null, true, true, 2, "Màn hình phẳng", 15, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8374), null },
                    { 34, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8376), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8376), null, "", null, true, true, 2, "Siêu mỏng", 15, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8377), null },
                    { 35, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8378), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8381), null, "", null, true, true, 2, "Tivi cao cấp", 15, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8382), null },
                    { 36, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8383), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8384), null, "", null, false, true, 2, "Tivi thiết kế đặc biệt", 15, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8384), null },
                    { 37, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8386), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8386), null, "", null, true, true, 2, "Loa kéo", 16, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8387), null },
                    { 38, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8388), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8388), null, "", null, true, true, 2, "Loa kéo điện", 16, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8389), null },
                    { 39, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8390), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8391), null, "", null, true, true, 2, "Loa karaoke xách tay", 16, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8391), null },
                    { 40, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8393), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8394), null, "", null, true, true, 2, "Loa bluetooth", 16, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8394), null },
                    { 41, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8396), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8396), null, "", null, true, true, 2, "Loa thanh (SoundBar)", 16, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8397), null },
                    { 42, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8399), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8400), null, "", null, true, true, 2, "Dàn Karaoke, Amply", 16, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8400), null },
                    { 43, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8402), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8402), null, "", null, true, true, 2, "Dàn âm thanh", 16, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8402), null },
                    { 44, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8404), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8404), null, "", null, true, true, 2, "Micro", 16, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8405), null },
                    { 45, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8406), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8407), null, "", null, true, true, 2, "Loa thùng", 16, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8407), null },
                    { 46, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8408), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8409), null, "", null, true, true, 2, "Loa mini", 16, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8409), null },
                    { 47, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8411), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8411), null, "", null, true, true, 2, "Loa đồ chơi", 16, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8411), null },
                    { 48, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8413), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8413), null, "", null, true, true, 2, "Cáp HDMI", 17, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8414), null },
                    { 49, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8415), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8416), null, "", null, true, true, 2, "Khung treo Tivi", 17, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8416), null },
                    { 50, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8418), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8418), null, "", null, true, true, 2, "Điều khiển Tivi", 17, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8419), null },
                    { 51, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8420), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8420), null, "", null, true, true, 2, "Android TV Box", 17, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8421), null },
                    { 52, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8423), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8424), null, "", null, false, true, 2, "Dán màn Tivi", 17, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8424), null },
                    { 53, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8426), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8426), null, "", null, false, true, 2, "Sticker Tivi", 17, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(8427), null }
                });

            migrationBuilder.InsertData(
                table: "CategoryTimeRange",
                columns: new[] { "Id", "CategoryId", "TimeRangeId" },
                values: new object[,]
                {
                    { 1, 15, 1 },
                    { 2, 15, 2 },
                    { 4, 15, 4 },
                    { 5, 15, 5 },
                    { 6, 15, 6 },
                    { 7, 15, 7 },
                    { 8, 15, 8 },
                    { 9, 15, 9 },
                    { 11, 15, 11 }
                });

            migrationBuilder.InsertData(
                table: "CategoryTranslation",
                columns: new[] { "CategoryTranslationId", "CategoryId", "LanguageId", "TranslatedName" },
                values: new object[,]
                {
                    { 15, 15, 1, "Ovens" },
                    { 16, 16, 1, "Kettles" },
                    { 17, 17, 1, "Vacuum Cleaners" },
                    { 18, 18, 1, "Air Conditioners" },
                    { 19, 19, 1, "Irons" },
                    { 20, 20, 1, "Electric Fans" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ImagePath", "IsDefault", "ProductId", "TypeId" },
                values: new object[,]
                {
                    { 1, "/Images/Component/ListProduct/product1.webp", true, 1, 1 },
                    { 2, "/Images/Component/ListProduct/product2.webp", true, 2, 1 },
                    { 3, "/Images/Component/ListProduct/product3.webp", true, 3, 1 },
                    { 4, "/Images/Component/ListProduct/product4.webp", true, 4, 1 },
                    { 5, "/Images/Component/ListProduct/product5.webp", true, 5, 1 },
                    { 6, "/Images/Component/ListProduct/product6.webp", true, 6, 1 },
                    { 7, "/Images/Component/ListProduct/product1.webp", true, 7, 1 },
                    { 8, "/Images/Component/ListProduct/product2.webp", true, 8, 1 },
                    { 9, "/Images/Component/ListProduct/product3.webp", true, 9, 1 },
                    { 10, "/Images/Component/ListProduct/product4.webp", true, 10, 1 },
                    { 11, "/Images/Component/ListProduct/product5.webp", true, 11, 1 },
                    { 12, "/Images/Component/ListProduct/product6.webp", true, 12, 1 },
                    { 14, "/Images/Component/ListProduct/product1-1.jpg", false, 1, 1 },
                    { 15, "/Images/Component/ListProduct/product1-2.jpg", false, 1, 1 },
                    { 16, "/Images/Component/ListProduct/product1-3.jpg", false, 1, 1 },
                    { 17, "/Images/Component/ListProduct/product1-4.jpg", false, 1, 1 },
                    { 18, "/Images/Component/ListProduct/product1-5.jpg", false, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Interesteds",
                columns: new[] { "InterestedID", "AppUserId", "ProductID" },
                values: new object[,]
                {
                    { 1, new Guid("00bb44d1-f674-49f6-bdae-afb143ab9749"), 1 },
                    { 2, new Guid("00bb44d1-f674-49f6-bdae-afb143ab9749"), 2 },
                    { 3, new Guid("00bb44d1-f674-49f6-bdae-afb143ab9749"), 3 }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "CreateDate", "Note", "Status", "UserId", "VoucherUserId" },
                values: new object[] { 1, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9438), null, 0, new Guid("00bb44d1-f674-49f6-bdae-afb143ab9749"), 1 });

            migrationBuilder.InsertData(
                table: "PaymentInfos",
                columns: new[] { "PaymentInfoId", "InvoiceId", "PaymentMethod", "PaymentStatus" },
                values: new object[,]
                {
                    { 2, 2, 1, 0 },
                    { 3, 3, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "ProductAddress",
                columns: new[] { "ProductAddressID", "AddressID", "ProductID", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 10 },
                    { 2, 2, 2, 15 },
                    { 3, 1, 3, 20 },
                    { 4, 1, 4, 20 },
                    { 5, 1, 5, 20 },
                    { 6, 3, 6, 20 }
                });

            migrationBuilder.InsertData(
                table: "ProductPromotions",
                columns: new[] { "ProductPromotionId", "ProductId", "PromotionId" },
                values: new object[,]
                {
                    { 1, 1, 11 },
                    { 2, 2, 11 },
                    { 3, 3, 11 },
                    { 4, 4, 11 },
                    { 5, 5, 11 },
                    { 6, 6, 11 },
                    { 7, 7, 11 },
                    { 8, 8, 10 },
                    { 9, 9, 10 },
                    { 10, 10, 10 },
                    { 11, 11, 10 },
                    { 12, 12, 10 }
                });

            migrationBuilder.InsertData(
                table: "ProductTranslation",
                columns: new[] { "ProductTranslationId", "LanguageId", "ProductEAVId", "TranslatedDescription", "TranslatedName" },
                values: new object[,]
                {
                    { 1, 1, 1, "Translated Description 1", "Translated Name 1" },
                    { 2, 1, 2, "Translated Description 2", "Translated Name 2" },
                    { 3, 1, 3, "Translated Description 3", "Translated Name 3" },
                    { 4, 1, 4, "Translated Description 4", "Translated Name 4" },
                    { 5, 1, 5, "Translated Description 5", "Translated Name 5" },
                    { 6, 1, 6, "Translated Description 6", "Translated Name 6" }
                });

            migrationBuilder.InsertData(
                table: "ShippingOrders",
                columns: new[] { "Id", "CreateAt", "CreateBy", "DeleteAt", "DeleteBy", "Description", "InvoiceId", "Name", "OrderCode", "Price", "UpdateAt", "UpdateBy", "UserId" },
                values: new object[] { 1, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9698), null, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9694), null, "", 3, "", "L6AHHM", 200000.0, new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9694), null, new Guid("aa7c5218-4f1e-4ac6-a3b4-08dcb162e29e") });

            migrationBuilder.InsertData(
                table: "AttributeValues",
                columns: new[] { "AtributeValueId", "AttributeId", "IsActive", "IsShow", "Type", "Value" },
                values: new object[,]
                {
                    { 1, 1, true, true, 1, "Red" },
                    { 2, 1, true, true, 1, "Blue" },
                    { 3, 2, true, true, 1, "Small" },
                    { 4, 2, true, true, 1, "Medium" },
                    { 5, 2, true, true, 1, "Large" },
                    { 6, 3, true, true, 1, "1 HP" },
                    { 7, 3, true, true, 1, "1.5 HP" },
                    { 8, 3, true, true, 1, "2 HP" },
                    { 9, 3, true, true, 1, "2.5 HP" },
                    { 10, 4, true, true, 1, "1 chiều" },
                    { 11, 4, true, true, 1, "2 chiều" },
                    { 12, 5, true, true, 1, "43 inch" },
                    { 13, 5, true, true, 1, "50 inch" },
                    { 14, 5, true, true, 1, "55 inch" },
                    { 15, 5, true, true, 1, "65 inch" },
                    { 16, 5, true, true, 1, "75 inch" },
                    { 17, 6, true, true, 0, "Smart TV NanoCell" },
                    { 18, 7, true, true, 0, "WebOS 22" },
                    { 19, 8, true, true, 0, "YouTube KidsYouTubeVTVcab ONVieONTV 360Trình duyệt webSpotifyPOPS KidsNhaccuatuiNetflixMyTVGalaxy Play (Fim+)FPT PlayClip TV" },
                    { 20, 9, true, true, 0, "Tương thích HEVCTương thích bộ mã hóa Video decoder (VP9, AV1) Nâng cấp độ phân giải 4K AI UpscalingNâng cấp hình ảnh AI Picture Pro 4KHLGHDR10 ProHDR Dynamic Tone MappingGiảm độ trễ chơi game Auto Low Latency Mode (ALLM)Dải màu rộng Nano ColorChế độ hình ảnh phù hợp nội dungChế độ game HGiGActive HDR" },
                    { 21, 10, true, true, 0, "Tìm kiếm giọng nói trên YouTube bằng tiếng ViệtNhận diện giọng nói LG Voice RecognitionLG Voice Search - tìm kiếm bằng giọng nói tiếng ViệtGoogle Assistant (Chưa có tiếng Việt)Alexa (Chưa có tiếng Việt)" },
                    { 22, 11, true, true, 0, "Magic Remote" },
                    { 23, 12, true, true, 0, "Screen ShareAirPlay 2" },
                    { 24, 13, true, true, 0, "Ngang 96.7 cm - Cao 62.9 cm - Dày 24.9 cm" },
                    { 25, 14, true, true, 0, "4K (Ultra HD)" },
                    { 26, 15, true, true, 0, "Nhựa" }
                });

            migrationBuilder.InsertData(
                table: "CategoryProduct",
                columns: new[] { "CategoryProductId", "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 33, 1 },
                    { 2, 35, 1 },
                    { 3, 34, 2 },
                    { 4, 36, 3 },
                    { 5, 33, 4 },
                    { 6, 34, 4 },
                    { 7, 35, 5 },
                    { 8, 36, 5 },
                    { 9, 33, 6 },
                    { 10, 34, 6 },
                    { 11, 53, 7 },
                    { 12, 53, 8 },
                    { 13, 53, 9 },
                    { 14, 53, 10 },
                    { 15, 53, 11 },
                    { 16, 53, 12 }
                });

            migrationBuilder.InsertData(
                table: "PaymentInfos",
                columns: new[] { "PaymentInfoId", "InvoiceId", "PaymentMethod", "PaymentStatus" },
                values: new object[] { 1, 1, 2, 0 });

            migrationBuilder.InsertData(
                table: "ProductAttributes",
                columns: new[] { "Id", "AfterDiscountPrice", "AttributeValueId", "IsDefault", "ProductId", "PuscharPrice", "Quantity", "ReleaseYear", "SalePrice", "Tax" },
                values: new object[,]
                {
                    { 1, 10000000m, 12, true, 1, 9354545m, 100, 2022, 10290000m, 10 },
                    { 2, 11000000m, 13, false, 1, 10809091m, 100, 2022, 11990000m, 10 },
                    { 3, 11500000m, 14, false, 1, 10627273m, 100, 2022, 11690000m, 10 },
                    { 4, 13000000m, 15, false, 1, 12263636m, 100, 2023, 13490000m, 10 },
                    { 5, 19990000m, 16, false, 1, 18172727m, 100, 2023, 20990000m, 10 },
                    { 6, 10000000m, 12, true, 2, 9354545m, 50, 2021, 10290000m, 10 },
                    { 7, 10000000m, 12, true, 3, 9354545m, 100, 2021, 10290000m, 10 },
                    { 8, 10000000m, 12, true, 4, 9354545m, 100, 2020, 10290000m, 10 },
                    { 9, 10000000m, 12, true, 5, 9354545m, 100, 2023, 10290000m, 10 },
                    { 10, 10000000m, 12, true, 6, 9354545m, 100, 2023, 10290000m, 10 },
                    { 11, 10000000m, 1, true, 7, 9354545m, 100, 2023, 10290000m, 10 },
                    { 12, 10000000m, 1, true, 8, 9354545m, 100, 2023, 10290000m, 10 },
                    { 13, 10000000m, 1, true, 9, 9354545m, 100, 2023, 10290000m, 10 },
                    { 14, 10000000m, 1, true, 10, 9354545m, 100, 2023, 10290000m, 10 },
                    { 15, 10000000m, 1, true, 11, 9354545m, 100, 2023, 10290000m, 10 },
                    { 16, 10000000m, 1, true, 12, 9354545m, 100, 2023, 10290000m, 10 }
                });

            migrationBuilder.InsertData(
                table: "InvoiceDetails",
                columns: new[] { "InvoiceDetailId", "InvoiceId", "NewPrice", "OldPrice", "ProductAttributeId", "PuscharPrice", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 10000000.0, 10290000.0, 1, 9354545.0, 2 },
                    { 2, 2, 11000000.0, 11990000.0, 2, 10809091.0, 1 },
                    { 3, 3, 11500000.0, 11690000.0, 3, 10627273.0, 1 },
                    { 4, 3, 11000000.0, 11990000.0, 2, 10809091.0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "Content", "Date", "InvoiceDetailId", "ProductId", "Rating", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, "Chất lượng khá tốt so với giá tiền", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9349), 1, 1, 4, 0, new Guid("00bb44d1-f674-49f6-bdae-afb143ab9749") },
                    { 2, "Sản phẩm tạm ổn", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9384), 2, 1, 3, 2, new Guid("00bb44d1-f674-49f6-bdae-afb143ab9749") },
                    { 3, "Chất lượng sản phẩm tuyệt vời", new DateTime(2024, 8, 27, 10, 52, 21, 823, DateTimeKind.Local).AddTicks(9389), 3, 1, 5, 1, new Guid("00bb44d1-f674-49f6-bdae-afb143ab9749") }
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
                name: "IX_Attributes_ProductId",
                table: "Attributes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValues_AttributeId",
                table: "AttributeValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_CategoryId",
                table: "CategoryProduct",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductId",
                table: "CategoryProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTimeRange_CategoryId",
                table: "CategoryTimeRange",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTimeRange_TimeRangeId",
                table: "CategoryTimeRange",
                column: "TimeRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslation_CategoryId",
                table: "CategoryTranslation",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslation_LanguageId",
                table: "CategoryTranslation",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_InvoiceDetailId",
                table: "Comments",
                column: "InvoiceDetailId",
                unique: true,
                filter: "[InvoiceDetailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductId",
                table: "Comments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_TypeId",
                table: "Images",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Interesteds_AppUserId",
                table: "Interesteds",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Interesteds_ProductID",
                table: "Interesteds",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_InvoiceId",
                table: "InvoiceDetails",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_ProductAttributeId",
                table: "InvoiceDetails",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_VoucherUserId",
                table: "Invoices",
                column: "VoucherUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInfos_InvoiceId",
                table: "PaymentInfos",
                column: "InvoiceId",
                unique: true,
                filter: "[InvoiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAddress_AddressID",
                table: "ProductAddress",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAddress_ProductID",
                table: "ProductAddress",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_AttributeValueId",
                table: "ProductAttributes",
                column: "AttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_ProductId",
                table: "ProductAttributes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPromotions_ProductId",
                table: "ProductPromotions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPromotions_PromotionId",
                table: "ProductPromotions",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OriginId",
                table: "Products",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTranslation_LanguageId",
                table: "ProductTranslation",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTranslation_ProductEAVId",
                table: "ProductTranslation",
                column: "ProductEAVId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingOrders_InvoiceId",
                table: "ShippingOrders",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingOrders_UserId",
                table: "ShippingOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherUsers_AppUserId",
                table: "VoucherUsers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherUsers_VoucherId",
                table: "VoucherUsers",
                column: "VoucherId");
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
                name: "CategoryProduct");

            migrationBuilder.DropTable(
                name: "CategoryTimeRange");

            migrationBuilder.DropTable(
                name: "CategoryTranslation");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Interesteds");

            migrationBuilder.DropTable(
                name: "Magazines");

            migrationBuilder.DropTable(
                name: "NewFeeds");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PaymentInfos");

            migrationBuilder.DropTable(
                name: "ProductAddress");

            migrationBuilder.DropTable(
                name: "ProductPromotions");

            migrationBuilder.DropTable(
                name: "ProductTranslation");

            migrationBuilder.DropTable(
                name: "ShippingOrders");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "TimeRanges");

            migrationBuilder.DropTable(
                name: "InvoiceDetails");

            migrationBuilder.DropTable(
                name: "ImageTypes");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "VoucherUsers");

            migrationBuilder.DropTable(
                name: "AttributeValues");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Origins");
        }
    }
}
