using System.Data.Entity;
using DataAccess.Entities;

namespace DataAccess
{
    public class TaskListContext : DbContext
    {
        public TaskListContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TaskListContext>());
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<SubTask> SubTasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .HasMany(t => t.SubTasks)
                .WithRequired(s => s.Task)
                .HasForeignKey(s => s.TaskId)
                .WillCascadeOnDelete();
        }
    }
}