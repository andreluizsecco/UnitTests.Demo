using System.Threading.Tasks;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Domain.Interfaces.Repositories
{
    public interface ITarefaService
    {
        Task<bool> CriarTarefa(Tarefa tarefa);
        Task<bool> AtualizarTarefa(Tarefa tarefa);
        Task<bool> RemoverTarefa(Tarefa tarefa);
        Task<bool> ConcluirTarefa(Tarefa tarefa);
    }
}
