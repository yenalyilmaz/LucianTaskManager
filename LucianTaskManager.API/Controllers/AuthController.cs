using LucianTaskManager.Data;
using LucianTaskManager.DTOs;
using LucianTaskManager.Models;
using LucianTaskManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LucianTaskManager.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(ApplicationDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Registers a new user account with email and password.
        /// </summary>
        /// <param name="dto">User registration details (email and password).</param>
        /// <returns>Success message or error if email already exists.</returns>
        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("This email is already registered.");

            var passwordHash = HashPassword(dto.Password);

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = passwordHash,
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Registration successful.");
        }

        /// <summary>
        /// Authenticates the user and generates a JWT token.
        /// </summary>
        /// <param name="dto">User login credentials (email and password).</param>
        /// <returns>JWT token if authentication is successful.</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || user.PasswordHash != HashPassword(dto.Password))
                return Unauthorized("Invalid email or password.");

            var token = _tokenService.CreateToken(user);
            return Ok(new { token });
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
