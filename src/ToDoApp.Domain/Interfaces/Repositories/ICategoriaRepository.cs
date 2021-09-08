using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Domain.Interfaces.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> ObterTodos();
    }
}
