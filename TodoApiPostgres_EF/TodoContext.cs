namespace TodoApi.Context
{
    using Microsoft.EntityFrameworkCore;

    public class TodoContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Todo> Todos { get; set; }
        public string DbPath { get; }


        public TodoContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
        {
            options.UseNpgsql(_configuration.GetConnectionString("TodoPostgressDb"));
        }
    }

    public class Todo 
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool Completed { get; set; }
    }
}