using FinancialsHubWebAPI.DTOs;
using FinancialsHubWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FinancialsHubWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // ── POST: api/Auth/register ──────────────────────────────
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto dto)
        {
            // Check if email already exists
            var emailExists = await _context.TransictionAccounts.AnyAsync(a => a.Email == dto.Email.ToLower());
            if (emailExists)
                return BadRequest(new { message = "البريد الإلكتروني مستخدم بالفعل." });

            // Validate role
            if (dto.Role != "Admin" && dto.Role != "User")
                return BadRequest(new { message = "الدور يجب أن يكون Admin أو User." });

            var account = new TransictionAccount
            {
                FullNameEn = dto.FullNameEn,
                FullNameAr = dto.FullNameAr,
                Email = dto.Email.ToLower(),
                PasswordHash = HashPassword(dto.Password),
                Role = dto.Role,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            await _context.TransictionAccounts.AddAsync(account);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(account);
            var expiry = DateTime.Now.AddDays(7);

            return CreatedAtAction(nameof(Register), new AuthResponseDto
            {
                Id = account.Id,
                FullNameEn = account.FullNameEn,
                FullNameAr = account.FullNameAr,
                Email = account.Email,
                Role = account.Role,
                Token = token,
                ExpiresAt = expiry
            });
        }

        // ── POST: api/Auth/login ─────────────────────────────────
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto dto)
        {
            var account = await _context.TransictionAccounts
                .FirstOrDefaultAsync(a => a.Email == dto.Email.ToLower() && a.IsActive);

            if (account == null || !VerifyPassword(dto.Password, account.PasswordHash))
                return Unauthorized(new { message = "البريد الإلكتروني أو كلمة المرور غير صحيحة." });

            var token = GenerateJwtToken(account);
            var expiry = DateTime.Now.AddDays(7);

            return Ok(new AuthResponseDto
            {
                Id = account.Id,
                FullNameEn = account.FullNameEn,
                FullNameAr = account.FullNameAr,
                Email = account.Email,
                Role = account.Role,
                Token = token,
                ExpiresAt = expiry
            });
        }

        // ── GET: api/Auth/me ─────────────────────────────────────
        [HttpGet("me")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<ActionResult> GetCurrentUser()
        {
            var accountIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountIdClaim == null) return Unauthorized();

            var account = await _context.TransictionAccounts.FindAsync(long.Parse(accountIdClaim));
            if (account == null) return NotFound();

            return Ok(new
            {
                account.Id,
                account.FullNameEn,
                account.FullNameAr,
                account.Email,
                account.Role,
                account.CreatedAt
            });
        }

        // ══════════════════════════════════════════════════════════
        // ── HELPER METHODS ───────────────────────────────────────
        // ══════════════════════════════════════════════════════════

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }

        private string GenerateJwtToken(TransictionAccount account)
        {
            var jwtKey = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Role, account.Role),
                new Claim("FullNameEn", account.FullNameEn),
                new Claim("FullNameAr", account.FullNameAr)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}