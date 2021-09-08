using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Data.Context;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ToDoApp.Data.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly DataContext _context;

        public TarefaRepository(DataContext context) =>
            _context = context;

        public async Task<IEnumerable<Tarefa>> ObterTodos()
        {
            return await _context.Tarefas
                .Include(p => p.Categoria)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Tarefa> ObterPorId(int id)
        {
            return await _context.Tarefas
                .Include(p => p.Categoria)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Tarefa> ObterPorTitulo(string titulo)
        {
            return await _context.Tarefas
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Titulo.Equals(titulo));
        }

        public async Task<bool> Adicionar(Tarefa tarefa)
        {
            _context.Add(tarefa);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Atualizar(Tarefa tarefa)
        {
            _context.Update(tarefa);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Remover(Tarefa tarefa)
        {
            _context.Remove(tarefa);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
