using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces.Repositories;
using ToDoApp.Domain.Services;
using Xunit;

namespace ToDoApp.Domain.Tests.Services
{
    public class TarefaServiceTests
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly TarefaService _tarefaService;
        private readonly Tarefa _tarefa;

        public TarefaServiceTests()
        {
            _tarefaRepository = Substitute.For<ITarefaRepository>();
            _tarefaService = new TarefaService(_tarefaRepository);
            _tarefa = new Tarefa(1, "teste de título", "teste de descrição");
        }

        [Fact]
        public async Task CriarTarefaComSucessoRetornaTrue()
        {
            //Arrange
            Tarefa tarefaExistente = null;

            _tarefaRepository.ObterPorTitulo(_tarefa.Titulo)
                .Returns(tarefaExistente);
            _tarefaRepository.ObterPorId(_tarefa.Id)
                .Returns(tarefaExistente);
            _tarefaRepository.Adicionar(_tarefa)
                .Returns(true);

            //Act
            var sucesso = await _tarefaService.CriarTarefa(_tarefa);

            //Assert
            Assert.True(sucesso);
        }

        [Fact]
        public async Task CriarTarefaComIdExistenteRetornaFalse()
        {
            //Arrange
            Tarefa tarefaExistente = _tarefa;
            Tarefa tarefaNula = null;

            _tarefaRepository.ObterPorTitulo(_tarefa.Titulo)
                .Returns(tarefaNula);
            _tarefaRepository.ObterPorId(_tarefa.Id)
                .Returns(tarefaExistente);

            //Act
            var sucesso = await _tarefaService.CriarTarefa(_tarefa);

            //Assert
            Assert.False(sucesso);
        }

        [Fact]
        public async Task CriarTarefaComTituloExistenteRetornaErro()
        {
            //Arrange
            Tarefa tarefaExistente = _tarefa;

            _tarefaRepository.ObterPorTitulo(_tarefa.Titulo)
                .Returns(tarefaExistente);
            _tarefaRepository.Adicionar(_tarefa)
                .Returns(false);

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _tarefaService.CriarTarefa(_tarefa));

            //Assert
            Assert.Equal("Já existe uma tarefa com esse título", exception.Message);
        }

        [Fact]
        public async Task AtualizarTarefaComSucessoRetornaTrue()
        {
            //Arrange
            Tarefa tarefaExistente = _tarefa;

            _tarefaRepository.ObterPorId(_tarefa.Id)
                .Returns(tarefaExistente);
            _tarefaRepository.Atualizar(_tarefa)
                .Returns(true);

            //Act
            var sucesso = await _tarefaService.AtualizarTarefa(_tarefa);

            //Assert
            Assert.True(sucesso);
        }

        [Fact]
        public async Task AtualizarTarefaComIdInexistenteRetornaFalse()
        {
            //Arrange
            Tarefa tarefaExistente = null;

            _tarefaRepository.ObterPorId(_tarefa.Id)
                .Returns(tarefaExistente);
            _tarefaRepository.Adicionar(_tarefa)
                .Returns(false);

            //Act
            var sucesso = await _tarefaService.AtualizarTarefa(_tarefa);

            //Assert
            Assert.False(sucesso);
        }

        [Fact]
        public async Task AtualizarTarefaComDataCriacaoMaior3MinutosRetornaFalse()
        {
            //Arrange
            _tarefa.DataCriacao = DateTime.Now.AddMinutes(-4);

            //Act
            var sucesso = await _tarefaService.AtualizarTarefa(_tarefa);

            //Assert
            Assert.False(sucesso);
        }

        [Fact]
        public async Task RemoverTarefaComSucessoRetornaTrue()
        {
            //Arrange
            Tarefa tarefaExistente = _tarefa;

            _tarefaRepository.ObterPorId(_tarefa.Id)
                .Returns(tarefaExistente);
            _tarefaRepository.Remover(_tarefa)
                .Returns(true);

            //Act
            var sucesso = await _tarefaService.RemoverTarefa(_tarefa);

            //Assert
            Assert.True(sucesso);
        }

        [Fact]
        public async Task RemoverTarefaComIdInexistenteRetornaFalse()
        {
            //Arrange
            Tarefa tarefaExistente = null;

            _tarefaRepository.ObterPorId(_tarefa.Id)
                .Returns(tarefaExistente);
            _tarefaRepository.Remover(_tarefa)
                .Returns(false);

            //Act
            var sucesso = await _tarefaService.RemoverTarefa(_tarefa);

            //Assert
            Assert.False(sucesso);
        }

        [Fact]
        public async Task ConcluirTarefaComSucessoRetornaTrue()
        {
            //Arrange
            _tarefaRepository.Atualizar(_tarefa)
                .Returns(true);

            //Act
            var sucesso = await _tarefaService.ConcluirTarefa(_tarefa);

            //Assert
            Assert.NotNull(_tarefa.DataConclusao);
            Assert.True(sucesso);
        }

        [Fact]
        public async Task ConcluirTarefaObjetoNuloRetornaFalse()
        {
            //Act
            var sucesso = await _tarefaService.ConcluirTarefa(null);

            //Assert
            Assert.False(sucesso);
        }
    }
}
