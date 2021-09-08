using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Repositories;
using ToDoApp.Domain.Entities;
using Xunit;

namespace ToDoApp.Data.Tests.Repositories
{
    public class TarefaRepositoryTests
    {
        private readonly TarefaRepository _tarefaRepository;

        public TarefaRepositoryTests()
        {
            var dbInMemory = new DBInMemory();
            var context = dbInMemory.GetContext();
            _tarefaRepository = new TarefaRepository(context);
        }

        [Fact]
        public async Task ObterTodos()
        {
            //Act
            var tarefas = await _tarefaRepository.ObterTodos();

            //Assert
            Assert.Equal(2, tarefas.Count());
        }

        [Fact]
        public async Task ObterPorId()
        {
            //Act
            var tarefa = await _tarefaRepository.ObterPorId(1);

            //Assert
            Assert.NotNull(tarefa);
        }

        [Fact]
        public async Task ObterPorTitulo()
        {
            //Act
            var tarefa = await _tarefaRepository.ObterPorTitulo("teste de título");

            //Assert
            Assert.NotNull(tarefa);
        }

        [Fact]
        public async Task Adicionar()
        {
            //Arrange
            var tarefa = new Tarefa(1, "Novo teste", "teste de descrição");

            //Act
            var sucesso = await _tarefaRepository.Adicionar(tarefa);

            //Assert
            Assert.True(sucesso);
        }

    }
}
