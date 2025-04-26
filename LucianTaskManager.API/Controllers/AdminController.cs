using LucianTaskManager.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LucianTaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves the list of all registered users.
        /// Only accessible by admin users.
        /// </summary>
        /// <returns>List of users with their Id, Email, and Role.</returns>
        [HttpGet("users")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users
                .Select(u => new
                {
                    u.Id,
                    u.Email,
                    u.Role
                })
                .ToListAsync();

            return Ok(users);
        }

        /// <summary>
        /// Retrieves the list of all tasks created by users.
        /// Only accessible by admin users.
        /// </summary>
        /// <returns>List of tasks with basic details and owner information.</returns>
        [HttpGet("tasks")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _context.TaskItems
                .Select(t => new
                {
                    t.Id,
                    t.Title,
                    t.Description,
                    t.CreatedAt,
                    t.UserId
                })
                .ToListAsync();

            return Ok(tasks);
        }
    }
}
