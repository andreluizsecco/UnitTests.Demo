using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Data.Tests
{
    public class DBInMemory
    {
        private DataContext _context;

        public DBInMemory()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(connection)
                .EnableSensitiveDataLogging()
                .Options;

            _context = new DataContext(options);
            InsertFakeData();
        }

        public DataContext GetContext() => _context;

        private void InsertFakeData()
        {
            if (_context.Database.EnsureCreated())
            {
                _context.Tarefas.Add(
                    new Tarefa(1, "teste de título", "teste de descrição")
                );
                _context.Tarefas.Add(
                    new Tarefa(1, "teste de título 2", "teste de descrição 2")
                );

                _context.SaveChanges();
            }
        }
    }
}
