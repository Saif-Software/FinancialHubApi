using FinancialsHubWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialsHubWebAPI
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Open connection explicitly so IDENTITY_INSERT stays on the same session
            await context.Database.OpenConnectionAsync();

            try
            {
                // ── 1. Seed Roles ───────────────────────────────
                if (!await context.Roles.AnyAsync())
                {
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Roles ON");
                    context.Roles.AddRange(
                        new Role { Id = 1, RoleName = "Admin", OrderNo = 1, BusinessEntity = "System" },
                        new Role { Id = 2, RoleName = "Accountant", OrderNo = 2, BusinessEntity = "Finance" },
                        new Role { Id = 3, RoleName = "Manager", OrderNo = 3, BusinessEntity = "Finance" }
                    );
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Roles OFF");
                }

                // ── 2. Seed Statuses ────────────────────────────
                if (!await context.Statuses.AnyAsync())
                {
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Status ON");
                    context.Statuses.AddRange(
                        new Status { Id = 1, StatusName = "Active", BusinessEntity = "General", OrderNo = 1 },
                        new Status { Id = 2, StatusName = "Draft", BusinessEntity = "TransactionReport", OrderNo = 1 },
                        new Status { Id = 3, StatusName = "Under Review", BusinessEntity = "TransactionReport", OrderNo = 2 },
                        new Status { Id = 4, StatusName = "Approved", BusinessEntity = "TransactionReport", OrderNo = 3 },
                        new Status { Id = 5, StatusName = "Rejected", BusinessEntity = "TransactionReport", OrderNo = 4 },
                        new Status { Id = 6, StatusName = "Completed", BusinessEntity = "TransactionReport", OrderNo = 5 }
                    );
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Status OFF");
                }

                // ── 3. Seed Accounts ────────────────────────────
                if (!await context.Accounts.AnyAsync())
                {
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Account ON");
                    context.Accounts.AddRange(
                        new Account
                        {
                            Id = 1,
                            NationalId = "1234567890",
                            PasswordHash = "hashed_password_1",
                            Email = "ahmed.yahya@example.com",
                            Phone = "0501234567",
                            RoleId = 1,
                            FullNameEn = "Ahmed Yahya",
                            FullNameAr = "أحمد يحيى",
                            IsActive = true,
                            StatusId = 1,
                            CreatedAt = DateOnly.FromDateTime(DateTime.Now)
                        },
                        new Account
                        {
                            Id = 2,
                            NationalId = "0987654321",
                            PasswordHash = "hashed_password_2",
                            Email = "sara.ali@example.com",
                            Phone = "0509876543",
                            RoleId = 2,
                            FullNameEn = "Sara Ali",
                            FullNameAr = "سارة علي",
                            IsActive = true,
                            StatusId = 1,
                            CreatedAt = DateOnly.FromDateTime(DateTime.Now)
                        }
                    );
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Account OFF");
                }

                // ── 4. Seed Categories ──────────────────────────
                if (!await context.Categories.AnyAsync())
                {
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Category ON");
                    context.Categories.AddRange(
                        new Category { Id = 1, CategoryName = "Internet Subscriptions", AmountMultiplier = 1, OrderNumber = 1, StatusId = 1 },
                        new Category { Id = 2, CategoryName = "Various Purchases", AmountMultiplier = 1, OrderNumber = 2, StatusId = 1 },
                        new Category { Id = 3, CategoryName = "Custody", AmountMultiplier = 1, OrderNumber = 3, StatusId = 1 },
                        new Category { Id = 4, CategoryName = "Transfers", AmountMultiplier = 1, OrderNumber = 4, StatusId = 1 },
                        new Category { Id = 5, CategoryName = "Salaries", AmountMultiplier = 1, OrderNumber = 5, StatusId = 1 },
                        new Category { Id = 6, CategoryName = "Maintenance", AmountMultiplier = 1, OrderNumber = 6, StatusId = 1 }
                    );
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Category OFF");
                }

                // ── 5. Seed Transaction Reports ─────────────────
                if (!await context.TransactionReports.AnyAsync())
                {
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT TransactionReport ON");
                    context.TransactionReports.AddRange(
                        new TransactionReport
                        {
                            Id = 1,
                            ReportName = "Monthly Internet Bill - January",
                            Notes = "Internet subscription payments for January 2026",
                            CreatorAccountId = 1,
                            CategoryId = 1,
                            CreatedAt = new DateTime(2026, 1, 1, 9, 0, 0)
                        },
                        new TransactionReport
                        {
                            Id = 2,
                            ReportName = "Office Supplies Purchase",
                            Notes = "Purchasing office supplies and stationery",
                            CreatorAccountId = 2,
                            CategoryId = 2,
                            CreatedAt = new DateTime(2026, 1, 15, 10, 30, 0)
                        },
                        new TransactionReport
                        {
                            Id = 3,
                            ReportName = "Salary Advances - February",
                            Notes = "Salary advance requests for February 2026",
                            CreatorAccountId = 1,
                            CategoryId = 5,
                            CreatedAt = new DateTime(2026, 2, 10, 14, 0, 0)
                        }
                    );
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT TransactionReport OFF");
                }

                // ── 6. Seed Transaction Records ─────────────────
                if (!await context.TransactionRecords.AnyAsync())
                {
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT TransactionRecord ON");
                    context.TransactionRecords.AddRange(
                        new TransactionRecord
                        {
                            Id = 1,
                            TransactionDate = new DateOnly(2026, 1, 5),
                            TransactionReportId = 1,
                            CategoryId = 1,
                            Amount = 250.00m,
                            Description = "Fiber internet - Main office"
                        },
                        new TransactionRecord
                        {
                            Id = 2,
                            TransactionDate = new DateOnly(2026, 1, 10),
                            TransactionReportId = 1,
                            CategoryId = 1,
                            Amount = 150.00m,
                            Description = "Mobile data plan - Staff"
                        },
                        new TransactionRecord
                        {
                            Id = 3,
                            TransactionDate = new DateOnly(2026, 2, 1),
                            TransactionReportId = 2,
                            CategoryId = 2,
                            Amount = 500.00m,
                            Description = "Printer paper and toner cartridges"
                        },
                        new TransactionRecord
                        {
                            Id = 4,
                            TransactionDate = new DateOnly(2026, 2, 3),
                            TransactionReportId = 2,
                            CategoryId = 2,
                            Amount = 320.50m,
                            Description = "Desk organizers and filing cabinets"
                        },
                        new TransactionRecord
                        {
                            Id = 5,
                            TransactionDate = new DateOnly(2026, 2, 5),
                            TransactionReportId = 2,
                            CategoryId = 6,
                            Amount = 180.00m,
                            Description = "Office chair repair"
                        },
                        new TransactionRecord
                        {
                            Id = 6,
                            TransactionDate = new DateOnly(2026, 2, 15),
                            TransactionReportId = 3,
                            CategoryId = 5,
                            Amount = 3000.00m,
                            Description = "Salary advance - Employee A"
                        },
                        new TransactionRecord
                        {
                            Id = 7,
                            TransactionDate = new DateOnly(2026, 2, 15),
                            TransactionReportId = 3,
                            CategoryId = 5,
                            Amount = 2500.00m,
                            Description = "Salary advance - Employee B"
                        }
                    );
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT TransactionRecord OFF");
                }
            }
            finally
            {
                await context.Database.CloseConnectionAsync();
            }
        }
    }
}
