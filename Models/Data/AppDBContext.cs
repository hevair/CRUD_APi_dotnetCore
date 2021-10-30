using Microsoft.EntityFrameworkCore;

namespace MeuTodo{
    public class AppDBContext : DbContext{
        public DbSet<Todo> Todos {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
            => options.UseSqlite(connectionString:"Data Source=app.db");

    }
}