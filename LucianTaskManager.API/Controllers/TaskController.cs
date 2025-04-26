using LucianTaskManager.Data;
using LucianTaskManager.DTOs;
using LucianTaskManager.Extensions;
using LucianTaskManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LucianTaskManager.Controllers
{
    [ApiController]
    [Route("tasks")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all tasks belonging to the authenticated user.
        /// </summary>
        /// <returns>List of user's tasks.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<TaskItem>), 200)]
        public async Task<IActionResult> GetTasks()
        {
            var userId = User.GetUserId();
            var tasks = await _context.TaskItems
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return Ok(tasks);
        }

        /// <summary>
        /// Creates a new task for the authenticated user.
        /// </summary>
        /// <param name="dto">Task details (title and description).</param>
        /// <returns>The created task.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TaskItem), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateTask(TaskDto dto)
        {
            var userId = User.GetUserId();

            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = userId
            };

            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();

            return Ok(task);
        }

        /// <summary>
        /// Updates an existing task owned by the authenticated user.
        /// </summary>
        /// <param name="id">Task ID to update.</param>
        /// <param name="dto">Updated task details.</param>
        /// <returns>The updated task.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TaskItem), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateTask(int id, TaskDto dto)
        {
            var userId = User.GetUserId();

            var task = await _context.TaskItems.FindAsync(id);
            if (task == null || task.UserId != userId)
                return NotFound("Task not found or unauthorized access.");

            task.Title = dto.Title;
            task.Description = dto.Description;
            await _context.SaveChangesAsync();

            return Ok(task);
        }

        /// <summary>
        /// Deletes a task owned by the authenticated user.
        /// </summary>
        /// <param name="id">Task ID to delete.</param>
        /// <returns>Success message if deleted.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userId = User.GetUserId();

            var task = await _context.TaskItems.FindAsync(id);
            if (task == null || task.UserId != userId)
                return NotFound("Task not found or unauthorized access.");

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();

            return Ok("Task deleted successfully.");
        }
    }
}
