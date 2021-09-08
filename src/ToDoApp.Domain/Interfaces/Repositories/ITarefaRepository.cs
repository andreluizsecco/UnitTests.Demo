using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Domain.Interfaces.Repositories
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<Tarefa>> ObterTodos();
        Task<Tarefa> ObterPorId(int id);
        Task<Tarefa> ObterPorTitulo(string titulo);
        Task<bool> Adicionar(Tarefa tarefa);
        Task<bool> Atualizar(Tarefa tarefa);
        Task<bool> Remover(Tarefa tarefa);
    }
}
