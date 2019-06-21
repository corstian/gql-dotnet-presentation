using Microsoft.EntityFrameworkCore;

namespace Sample.TaskList.Data
{
    public class TaskDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\.;Initial Catalog=TaskList;Integrated Security=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
