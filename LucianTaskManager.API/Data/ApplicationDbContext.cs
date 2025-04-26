using LucianTaskManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LucianTaskManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
    }
}