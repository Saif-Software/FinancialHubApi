using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialsHubWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbsenceRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    ClassId = table.Column<long>(type: "bigint", nullable: false),
                    DateOfAbsence = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    lectuerID = table.Column<long>(type: "bigint", nullable: true),
                    SessionID = table.Column<long>(type: "bigint", nullable: true),
                    AbsenceTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AbsenceR__3214EC0794E16F35", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account_Temp",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    FullNameEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullNameAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L),
                    governoratesID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountRoles",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<long>(type: "bigint", nullable: true),
                    AccountID = table.Column<long>(type: "bigint", nullable: true),
                    BusinessEntityName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRoles_Account", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdmissionQuiz_MATH",
                columns: table => new
                {
                    Question = table.Column<string>(type: "varchar(4000)", unicode: false, maxLength: 4000, nullable: true),
                    a = table.Column<string>(type: "varchar(4000)", unicode: false, maxLength: 4000, nullable: true),
                    b = table.Column<string>(type: "varchar(4000)", unicode: false, maxLength: 4000, nullable: true),
                    c = table.Column<string>(type: "varchar(4000)", unicode: false, maxLength: 4000, nullable: true),
                    d = table.Column<string>(type: "varchar(4000)", unicode: false, maxLength: 4000, nullable: true),
                    Answer = table.Column<string>(type: "varchar(4000)", unicode: false, maxLength: 4000, nullable: true),
                    CorrectAnswer_Txt = table.Column<string>(type: "varchar(4000)", unicode: false, maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmountMultiplier = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderNumber = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__3214EC0768DF3679", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelStatusId = table.Column<long>(type: "bigint", nullable: false),
                    DurationHours = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseMaterial",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseRoundID = table.Column<long>(type: "bigint", nullable: true),
                    Created_byAccountID = table.Column<long>(type: "bigint", nullable: true),
                    WeekID = table.Column<long>(type: "bigint", nullable: true),
                    ParentMaterialID = table.Column<long>(type: "bigint", nullable: true),
                    StatusID = table.Column<long>(type: "bigint", nullable: true),
                    MaterialTypeStatusID = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetingID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetingPassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "EducationalLevel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Educatio__3214EC07E48C5AB9", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SmtpServer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmtpPort = table.Column<int>(type: "int", nullable: true),
                    SmtpUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmtpPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exam_Details",
                columns: table => new
                {
                    Exam_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exam_Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exam_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy_AccID = table.Column<long>(type: "bigint", nullable: false),
                    Class_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade_ID = table.Column<long>(type: "bigint", nullable: true),
                    Subject_ID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Exam_Det__C782CA79487C7372", x => x.Exam_ID);
                });

            migrationBuilder.CreateTable(
                name: "Exam_QuestionBank",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Exam_ID = table.Column<long>(type: "bigint", nullable: true),
                    Question_ID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Exam_Que__3214EC275A2735A7", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Choice1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Choice2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Choice3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Choice4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamQuestion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestion_Math",
                columns: table => new
                {
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionA = table.Column<string>(name: "Option A", type: "nvarchar(max)", nullable: true),
                    OptionB = table.Column<string>(name: "Option B", type: "nvarchar(max)", nullable: true),
                    OptionC = table.Column<string>(name: "Option C", type: "nvarchar(max)", nullable: true),
                    OptionD = table.Column<string>(name: "Option D", type: "nvarchar(max)", nullable: true),
                    CorrectAnswer = table.Column<string>(name: "Correct Answer", type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CorrectAnswer_Txt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SectionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ExternalStudent",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateOnly>(type: "date", nullable: true),
                    GenderStatesId = table.Column<long>(type: "bigint", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: true),
                    GovernoratesId = table.Column<long>(type: "bigint", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__External__3214EC07E4EA0B8C", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Gender__3214EC07A5E702E0", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "governorates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    governorate_name_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    governorate_name_en = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_governorates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Juniors",
                columns: table => new
                {
                    _ = table.Column<double>(name: "#", type: "float", nullable: true),
                    Class = table.Column<double>(type: "float", nullable: true),
                    NationalID = table.Column<double>(type: "float", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(name: "PhoneNumber ", type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FullNameAR = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FullNameEN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Grad = table.Column<double>(type: "float", nullable: true),
                    ClassID = table.Column<double>(type: "float", nullable: true),
                    F10 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Level__3214EC0708A61CF3", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatedTable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatedId = table.Column<long>(type: "bigint", nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Media__3214EC0750545F4A", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Question_Bank",
                columns: table => new
                {
                    Question_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsedOptions = table.Column<int>(type: "int", nullable: true),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question_Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mark = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    BankDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade_ID = table.Column<long>(type: "bigint", nullable: true),
                    AccountId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Question__B0B2E4C67AD53AAB", x => x.Question_ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrderNo = table.Column<int>(type: "int", nullable: true),
                    BusinessEntity = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seniors",
                columns: table => new
                {
                    _ = table.Column<double>(name: "#", type: "float", nullable: true),
                    Class = table.Column<double>(type: "float", nullable: true),
                    NationalID = table.Column<double>(type: "float", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(name: "PhoneNumber ", type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FullNameAR = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FullNameEN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Grad = table.Column<double>(type: "float", nullable: true),
                    ClassID = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionNo = table.Column<int>(type: "int", nullable: true),
                    FromDate = table.Column<TimeOnly>(type: "time", nullable: true),
                    ToDate = table.Column<TimeOnly>(type: "time", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessEntity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderNo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Grade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StudentP__3214EC07E0265BEB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentProfile_Selected",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DaysAbsent = table.Column<int>(type: "int", nullable: true),
                    GoodNotesJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BadNotesJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClassName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "StudentProfile_tobeDeleted",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DaysAbsent = table.Column<int>(type: "int", nullable: true),
                    GoodNotesJson = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    BadNotesJson = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClassId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StudentP__3214EC07D31B0984", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_absencetype",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    AbsenceType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_abse__3214EC074ECDEB05", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Class",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradeId = table.Column<long>(type: "bigint", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tbl_media",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Table_ID = table.Column<long>(type: "bigint", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TransactionRecord",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TransactionReportId = table.Column<long>(type: "bigint", nullable: true),
                    CategoryId = table.Column<long>(type: "bigint", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__3214EC0715BF5703", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionReport",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorAccountId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__3214EC0767D960F8", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weeks",
                columns: table => new
                {
                    WeekTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    BusinessEntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "WHEELERS",
                columns: table => new
                {
                    _ = table.Column<double>(name: "#", type: "float", nullable: true),
                    Class = table.Column<double>(type: "float", nullable: true),
                    NationalID = table.Column<double>(type: "float", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(name: "PhoneNumber ", type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FullNameAR = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FullNameEN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Grad = table.Column<double>(type: "float", nullable: true),
                    ClassID = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CourseRound",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<long>(type: "bigint", nullable: false),
                    RoundNumber = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    MaxStudents = table.Column<long>(type: "bigint", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Question1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinStudents = table.Column<long>(type: "bigint", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    AutomatedWorkFlowJump = table.Column<int>(type: "int", nullable: true),
                    CourseRoundGroupId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CourseRo__3214EC07A418E5B0", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseRound_Course",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    FullNameEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullNameAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_at = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_Roles",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Account_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmploymentRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkedInUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmploymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmploymentRequests_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReportSpecialist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date_report = table.Column<DateTime>(type: "datetime", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecialistSignature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ReportSp__3214EC077E928D74", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportSpecialist_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Scholarship",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScholarshipName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scholarship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scholarship_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderNo = table.Column<int>(type: "int", nullable: true),
                    BusinessEntity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketType_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdmissionProfile",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftwareInterviewScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    MathInterviewScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    EnglishInterviewScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    ArabicInterviewScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MathScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    EnglishScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    ThirdPrepScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    IsAcceptanceLetterReceived = table.Column<bool>(type: "bit", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L),
                    HasOnlineTrainingCourses = table.Column<bool>(type: "bit", nullable: false),
                    HasICDLLicense = table.Column<bool>(type: "bit", nullable: false),
                    HasLaptop = table.Column<bool>(type: "bit", nullable: false),
                    ParentPhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PreviousSchoolType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinistryExamPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ParentOccupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthCertificatePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuccessReportPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TuitionFeeReceiptPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferencesSheetPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionProfile", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_AdmissionProfile_Account",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdmissionProfile_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseRoundId = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    StatusId = table.Column<long>(type: "bigint", nullable: false),
                    Answer1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Applicat__3214EC0714A385CA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Application_Account",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Application_CourseRound",
                        column: x => x.CourseRoundId,
                        principalTable: "CourseRound",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttendanceRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    SessionNumber = table.Column<int>(type: "int", nullable: false),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false),
                    NoteId = table.Column<long>(type: "bigint", nullable: true),
                    ClassId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Attendan__3214EC0723E966C3", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceRecords_Account",
                        column: x => x.StudentId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CapstoneSupervisorExtension",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapstoneSupervisorExtension", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_CapstoneSupervisorExtension_Account",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CapstoneSupervisorExtension_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CourseRoundInstructor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseRoundId = table.Column<long>(type: "bigint", nullable: false),
                    InstructorAccountId = table.Column<long>(type: "bigint", nullable: false),
                    AssignedDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRoundInstructor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseRoundInstructor_CourseRound",
                        column: x => x.CourseRoundId,
                        principalTable: "CourseRound",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseRoundInstructor_Instructor",
                        column: x => x.InstructorAccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentGradeId = table.Column<long>(type: "bigint", nullable: true),
                    AdminAccountId = table.Column<long>(type: "bigint", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grade_AdminAccount",
                        column: x => x.AdminAccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Grade_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InterviewScore",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    InterviewerId = table.Column<long>(type: "bigint", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterviewScore_Admin_Account",
                        column: x => x.InterviewerId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InterviewScore_Student_Account",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Login_Account",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Login_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Read_statusID = table.Column<long>(type: "bigint", nullable: true, defaultValue: 0L),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__3214EC278015B410", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notifications_Account",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "ELSEWEDY"),
                    AdditionalInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ProjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L),
                    SupervisorAccountId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Project_SupervisorAccount",
                        column: x => x.SupervisorAccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ReportMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmitterAccountId = table.Column<long>(type: "bigint", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L),
                    Reviewer_ID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Report_SubmitterAccount",
                        column: x => x.SubmitterAccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReviewerSupervisorExtension",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    AssignedClassId = table.Column<long>(type: "bigint", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewerSupervisorExtension", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_ReviewerSupervisorExtension_Account",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReviewerSupervisorExtension_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentExamAnswer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    ExamQuestionId = table.Column<long>(type: "bigint", nullable: true),
                    ChoosedAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<bool>(type: "bit", nullable: false),
                    QuestionbankId = table.Column<long>(type: "bigint", nullable: true),
                    ExamDetailsID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExamAnswer_PK", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentExamAnswer_Account_FK",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentExamAnswer_Tmmp",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    ExamId = table.Column<long>(type: "bigint", nullable: false),
                    ChoosedAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<bool>(type: "bit", nullable: false),
                    QuestionbankId = table.Column<long>(type: "bigint", nullable: true),
                    ExamDetailsID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExamAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentExamAnswer_Account",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentExamResult",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    ExamArabicScore = table.Column<int>(type: "int", nullable: false),
                    ExamEnglishScore = table.Column<int>(type: "int", nullable: false),
                    ExamMathScore = table.Column<int>(type: "int", nullable: false),
                    ExamSoftwareScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExamResult", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_StudentExamResult_Account",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentExtension",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    IsLeader = table.Column<bool>(type: "bit", nullable: false),
                    ClassId = table.Column<long>(type: "bigint", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L),
                    MACAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationalLevelStatusId = table.Column<long>(type: "bigint", nullable: true),
                    HostName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExtension", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_StudentExtension_Account",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentExtension_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SuperAdminExtension",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperAdminExtension", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_SuperAdminExtension_Account",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SuperAdminExtension_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubordinateTicket",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupervisorAccountId = table.Column<long>(type: "bigint", nullable: true),
                    GradeId = table.Column<long>(type: "bigint", nullable: true),
                    ClassId = table.Column<long>(type: "bigint", nullable: true),
                    SessionId = table.Column<long>(type: "bigint", nullable: true),
                    SubordinateAccountId = table.Column<long>(type: "bigint", nullable: true),
                    TicketTypeId = table.Column<long>(type: "bigint", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubordinateTicket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubordinateTicket_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubordinateTicket_SupervisorAccount",
                        column: x => x.SupervisorAccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubordinateTicket_TicketType",
                        column: x => x.TicketTypeId,
                        principalTable: "TicketType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BehaviorNotes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttendanceRecordId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NoteType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    gen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Behavior__3214EC07452FA35F", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BehaviorNotes_AttendanceRecords",
                        column: x => x.AttendanceRecordId,
                        principalTable: "AttendanceRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Task",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedToID = table.Column<long>(type: "bigint", nullable: true),
                    AssignedByID = table.Column<long>(type: "bigint", nullable: true),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    TaskDeadline = table.Column<DateTime>(type: "datetime", nullable: false),
                    GradeId = table.Column<long>(type: "bigint", nullable: true),
                    AdminAccountId = table.Column<long>(type: "bigint", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L),
                    Class_Id = table.Column<int>(type: "int", nullable: true),
                    Team_Id = table.Column<int>(type: "int", nullable: true),
                    WeekID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_AdminAccount",
                        column: x => x.AdminAccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Task_Grade",
                        column: x => x.GradeId,
                        principalTable: "Grade",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Task_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamLeaderAccountId = table.Column<long>(type: "bigint", nullable: true),
                    ClassId = table.Column<long>(type: "bigint", nullable: false),
                    SupervisorAccountId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L),
                    TeamCode = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_Project",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Team_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Team_SupervisorAccount",
                        column: x => x.SupervisorAccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Team_TeamLeaderAccount",
                        column: x => x.TeamLeaderAccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentTask",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentAccountId = table.Column<long>(type: "bigint", nullable: false),
                    TaskId = table.Column<long>(type: "bigint", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentTask_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentTask_StudentAccount",
                        column: x => x.StudentAccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentTask_Task",
                        column: x => x.TaskId,
                        principalTable: "Tbl_Task",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaskSubmission",
                columns: table => new
                {
                    TaskSubmission_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Team_ID = table.Column<long>(type: "bigint", nullable: false),
                    TeamLeader_ID = table.Column<long>(type: "bigint", nullable: false),
                    Task_ID = table.Column<long>(type: "bigint", nullable: false),
                    Grade_ID = table.Column<long>(type: "bigint", nullable: true),
                    GLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status_ID = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reviewer_ID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TaskSubm__39F484D072E29E3A", x => x.TaskSubmission_ID);
                    table.ForeignKey(
                        name: "FK_TaskSubmission_Grade",
                        column: x => x.Grade_ID,
                        principalTable: "Grade",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskSubmission_Task",
                        column: x => x.Task_ID,
                        principalTable: "Tbl_Task",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskSubmission_Team",
                        column: x => x.Team_ID,
                        principalTable: "Team",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskSubmission_TeamLeader",
                        column: x => x.TeamLeader_ID,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TeamMember",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<long>(type: "bigint", nullable: false),
                    TeamMemberAccountId = table.Column<long>(type: "bigint", nullable: false),
                    TeamMemberDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamMember_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeamMember_Team",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeamMember_TeamMemberAccount",
                        column: x => x.TeamMemberAccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_RoleId",
                table: "Account",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_StatusId",
                table: "Account",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "UQ__Account__A9D10534CCE8DFA0",
                table: "Account",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Account__E9AA32FA70EBBAC3",
                table: "Account",
                column: "NationalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Account___E9AA32FA8C3F47C9",
                table: "Account_Temp",
                column: "NationalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionProfile_StatusId",
                table: "AdmissionProfile",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_AccountId",
                table: "Application",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_CourseRoundId",
                table: "Application",
                column: "CourseRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceRecords_StudentId",
                table: "AttendanceRecords",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_BehaviorNotes_AttendanceRecordId",
                table: "BehaviorNotes",
                column: "AttendanceRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_CapstoneSupervisorExtension_StatusId",
                table: "CapstoneSupervisorExtension",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRound_CourseId",
                table: "CourseRound",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRoundInstructor_CourseRoundId",
                table: "CourseRoundInstructor",
                column: "CourseRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRoundInstructor_InstructorAccountId",
                table: "CourseRoundInstructor",
                column: "InstructorAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentRequests_StatusId",
                table: "EmploymentRequests",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_AdminAccountId",
                table: "Grade",
                column: "AdminAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_StatusId",
                table: "Grade",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewScore_AccountId",
                table: "InterviewScore",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewScore_InterviewerId",
                table: "InterviewScore",
                column: "InterviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Login_AccountId",
                table: "Login",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Login_StatusId",
                table: "Login",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AccountId",
                table: "Notifications",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_StatusId",
                table: "Project",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_SupervisorAccountId",
                table: "Project",
                column: "SupervisorAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_StatusId",
                table: "Report",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_SubmitterAccountId",
                table: "Report",
                column: "SubmitterAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportSpecialist_StatusId",
                table: "ReportSpecialist",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewerSupervisorExtension_StatusId",
                table: "ReviewerSupervisorExtension",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "NonClusteredIndex-20250911-154853",
                table: "Roles",
                column: "RoleName");

            migrationBuilder.CreateIndex(
                name: "IX_Scholarship_StatusId",
                table: "Scholarship",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "UQ_Section_SectionName",
                table: "Section",
                column: "SectionName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamAnswer_AccountId",
                table: "StudentExamAnswer",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "UQ_StudentExamAnswer_AccountExam",
                table: "StudentExamAnswer_Tmmp",
                columns: new[] { "AccountId", "ExamId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_StudentExamAnswer_AccountExamQuestion",
                table: "StudentExamAnswer_Tmmp",
                columns: new[] { "AccountId", "ExamDetailsID", "QuestionbankId" },
                unique: true,
                filter: "([ExamDetailsID] IS NOT NULL AND [QuestionbankId] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExtension_StatusId",
                table: "StudentExtension",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTask_StatusId",
                table: "StudentTask",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTask_StudentAccountId",
                table: "StudentTask",
                column: "StudentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTask_TaskId",
                table: "StudentTask",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_SubordinateTicket_StatusId",
                table: "SubordinateTicket",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SubordinateTicket_SupervisorAccountId",
                table: "SubordinateTicket",
                column: "SupervisorAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SubordinateTicket_TicketTypeId",
                table: "SubordinateTicket",
                column: "TicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SuperAdminExtension_StatusId",
                table: "SuperAdminExtension",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmission_Grade_ID",
                table: "TaskSubmission",
                column: "Grade_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmission_Task_ID",
                table: "TaskSubmission",
                column: "Task_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmission_Team_ID",
                table: "TaskSubmission",
                column: "Team_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmission_TeamLeader_ID",
                table: "TaskSubmission",
                column: "TeamLeader_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Task_AdminAccountId",
                table: "Tbl_Task",
                column: "AdminAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Task_GradeId",
                table: "Tbl_Task",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Task_StatusId",
                table: "Tbl_Task",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_ProjectId",
                table: "Team",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_StatusId",
                table: "Team",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_SupervisorAccountId",
                table: "Team",
                column: "SupervisorAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_TeamLeaderAccountId",
                table: "Team",
                column: "TeamLeaderAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMember_StatusId",
                table: "TeamMember",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMember_TeamId",
                table: "TeamMember",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMember_TeamMemberAccountId",
                table: "TeamMember",
                column: "TeamMemberAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketType_StatusId",
                table: "TicketType",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbsenceRecords");

            migrationBuilder.DropTable(
                name: "Account_Temp");

            migrationBuilder.DropTable(
                name: "AccountRoles");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "AdmissionProfile");

            migrationBuilder.DropTable(
                name: "AdmissionQuiz_MATH");

            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "BehaviorNotes");

            migrationBuilder.DropTable(
                name: "CapstoneSupervisorExtension");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "CourseMaterial");

            migrationBuilder.DropTable(
                name: "CourseRoundInstructor");

            migrationBuilder.DropTable(
                name: "EducationalLevel");

            migrationBuilder.DropTable(
                name: "EmailSettings");

            migrationBuilder.DropTable(
                name: "EmploymentRequests");

            migrationBuilder.DropTable(
                name: "Exam_Details");

            migrationBuilder.DropTable(
                name: "Exam_QuestionBank");

            migrationBuilder.DropTable(
                name: "ExamQuestion");

            migrationBuilder.DropTable(
                name: "ExamQuestion_Math");

            migrationBuilder.DropTable(
                name: "ExternalStudent");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "governorates");

            migrationBuilder.DropTable(
                name: "InterviewScore");

            migrationBuilder.DropTable(
                name: "Juniors");

            migrationBuilder.DropTable(
                name: "Level");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Question_Bank");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "ReportSpecialist");

            migrationBuilder.DropTable(
                name: "ReviewerSupervisorExtension");

            migrationBuilder.DropTable(
                name: "Scholarship");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "Seniors");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "StudentExamAnswer");

            migrationBuilder.DropTable(
                name: "StudentExamAnswer_Tmmp");

            migrationBuilder.DropTable(
                name: "StudentExamResult");

            migrationBuilder.DropTable(
                name: "StudentExtension");

            migrationBuilder.DropTable(
                name: "StudentProfile");

            migrationBuilder.DropTable(
                name: "StudentProfile_Selected");

            migrationBuilder.DropTable(
                name: "StudentProfile_tobeDeleted");

            migrationBuilder.DropTable(
                name: "StudentTask");

            migrationBuilder.DropTable(
                name: "SubordinateTicket");

            migrationBuilder.DropTable(
                name: "SuperAdminExtension");

            migrationBuilder.DropTable(
                name: "TaskSubmission");

            migrationBuilder.DropTable(
                name: "tbl_absencetype");

            migrationBuilder.DropTable(
                name: "Tbl_Class");

            migrationBuilder.DropTable(
                name: "tbl_media");

            migrationBuilder.DropTable(
                name: "TeamMember");

            migrationBuilder.DropTable(
                name: "TransactionRecord");

            migrationBuilder.DropTable(
                name: "TransactionReport");

            migrationBuilder.DropTable(
                name: "Weeks");

            migrationBuilder.DropTable(
                name: "WHEELERS");

            migrationBuilder.DropTable(
                name: "AttendanceRecords");

            migrationBuilder.DropTable(
                name: "CourseRound");

            migrationBuilder.DropTable(
                name: "TicketType");

            migrationBuilder.DropTable(
                name: "Tbl_Task");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
