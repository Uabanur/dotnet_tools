namespace TodoApi.Context
{
    using Microsoft.EntityFrameworkCore;

    public class TodoContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public string DbPath { get; }

        public TodoContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "todoapi.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
            => options.UseSqlite($"Data Source={DbPath}");

    }

    public class Todo 
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool Completed { get; set; }
    }
}