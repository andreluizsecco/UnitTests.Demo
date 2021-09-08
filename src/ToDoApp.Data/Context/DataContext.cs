using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Mappings;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TarefaMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
