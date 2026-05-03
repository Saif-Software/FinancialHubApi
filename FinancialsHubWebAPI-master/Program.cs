
using FinancialsHubWebAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
            // CORS - allow frontend origin
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowVercel",
                    policy =>
                    {
                        policy.WithOrigins("https://fainancial.vercel.app", "https://fainancial-hubbb-main.vercel.app", "http://localhost:3000") // اسمح بالفيرسل واللوكال هوست
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });



            var app = builder.Build();


          

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinancialsHub API V1");
                c.RoutePrefix = "swagger"; // أو جربها string.Empty لو عايزه على الـ root
            });


            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("AllowVercel");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
