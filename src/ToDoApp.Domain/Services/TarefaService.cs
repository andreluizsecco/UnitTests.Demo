using System;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces.Repositories;

namespace ToDoApp.Domain.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaService(ITarefaRepository tarefaRepository) =>
            _tarefaRepository = tarefaRepository;

        public async Task<bool> CriarTarefa(Tarefa tarefa)
        {
            // Simulando regra de negócio que não permite criação de duas tarefas com o mesmo título
            if (await _tarefaRepository.ObterPorTitulo(tarefa.Titulo) != null)
                throw new Exception("Já existe uma tarefa com esse título");

            if (await _tarefaRepository.ObterPorId(tarefa.Id) == null)
                return await _tarefaRepository.Adicionar(tarefa);

            return false;
        }

        public async Task<bool> AtualizarTarefa(Tarefa tarefa)
        {
            // Simulando regra de negócio que permite alterações em até 3 minutos depois da criação
            if ((DateTime.Now - tarefa.DataCriacao).Minutes > 3)
                return false;

            if (await _tarefaRepository.ObterPorId(tarefa.Id) != null)
                return await _tarefaRepository.Atualizar(tarefa);

            return false;
        }

        public async Task<bool> RemoverTarefa(Tarefa tarefa)
        {
            if (await _tarefaRepository.ObterPorId(tarefa.Id) != null)
                return await _tarefaRepository.Remover(tarefa);

            return false;
        }

        public async Task<bool> ConcluirTarefa(Tarefa tarefa)
        {
            if (tarefa != null)
            {
                tarefa.Concluir();
                return await _tarefaRepository.Atualizar(tarefa);
            }

            return false;
        }
    }
}
