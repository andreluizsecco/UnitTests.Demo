using System.Collections.Generic;
using System.Linq;

namespace ToDoApp.Domain.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<Tarefa> Tarefas { get; set; }

        public Categoria() =>
            Tarefas = new List<Tarefa>();

        public Categoria(int id, string nome) : this()
        {
            Id = id;
            Nome = nome;
        }

        public int ObterQuantidadeTarefasConcluidas() =>
            Tarefas.Count(t => t.DataConclusao.HasValue);

        public int ObterQuantidadeTarefasNaoConcluidas() =>
            Tarefas.Count(t => !t.DataConclusao.HasValue);
    }
}
