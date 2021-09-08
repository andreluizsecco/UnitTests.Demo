using System;

namespace ToDoApp.Domain.Entities
{
    public class Tarefa
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }

        public Categoria Categoria { get; set; }

        public Tarefa() =>
            DataCriacao = DateTime.Now;

        public Tarefa(int categoriaId, string titulo, string descricao) : this()
        {
            CategoriaId = categoriaId;
            Titulo = titulo;
            Descricao = descricao;
        }

        public void Concluir()
        {
            if (DataConclusao == null)
                DataConclusao = DateTime.Now;
            else
                throw new Exception("Tarefa já está concluída");
        }
    }
}
