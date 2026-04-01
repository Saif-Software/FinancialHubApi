
using FinancialsHubWebAPI.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FinancialsHubWebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register repositories
            builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            builder.Services.AddScoped<ITransactionReportRepo, TransactionReportRepo>();

            // CORS - allow frontend origin
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // ── Schema migration: add missing columns via raw ADO.NET ──
            // This runs BEFORE EF Core touches the TransactionReport table
            var connString = builder.Configuration.GetConnectionString("DefaultConnection");
            using (var conn = new SqlConnection(connString))
            {
                await conn.OpenAsync();

                // Add CategoryId column
                using (var cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = @"
                        IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('TransactionReport') AND name = 'CategoryId')
                            ALTER TABLE TransactionReport ADD CategoryId BIGINT NULL;";
                    await cmd1.ExecuteNonQueryAsync();
                }

                // Add CreatedAt column
                using (var cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = @"
                        IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('TransactionReport') AND name = 'CreatedAt')
                            ALTER TABLE TransactionReport ADD CreatedAt DATETIME NULL DEFAULT GETDATE();";
                    await cmd2.ExecuteNonQueryAsync();
                }

                // Backfill CreatedAt for existing rows (separate batch so column is visible)
                using (var cmd3 = conn.CreateCommand())
                {
                    cmd3.CommandText = "UPDATE TransactionReport SET CreatedAt = GETDATE() WHERE CreatedAt IS NULL;";
                    await cmd3.ExecuteNonQueryAsync();
                }

                // Add FK constraint
                using (var cmd4 = conn.CreateCommand())
                {
                    cmd4.CommandText = @"
                        IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_TransactionReport_Category')
                            ALTER TABLE TransactionReport ADD CONSTRAINT FK_TransactionReport_Category
                                FOREIGN KEY (CategoryId) REFERENCES Category(Id);";
                    await cmd4.ExecuteNonQueryAsync();
                }
            }

            // Seed test data (now safe because the columns exist)
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await DbSeeder.SeedAsync(dbContext);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("AllowFrontend");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
