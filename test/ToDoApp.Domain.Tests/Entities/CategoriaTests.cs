using FluentAssertions;
using System;
using System.Collections.Generic;
using ToDoApp.Domain.Entities;
using Xunit;

namespace ToDoApp.Domain.Tests.Entities
{
    public class CategoriaTests
    {
        private readonly Categoria _categoria;
        
        public CategoriaTests()
        {
            _categoria = new Categoria(1, "Pessoal");
            var tarefas = new List<Tarefa>
            {
                new Tarefa(1, "teste", "teste de descrição"),
                new Tarefa(1, "teste 2", "teste de descrição 2"),
                new Tarefa(1, "teste 3", "teste de descrição 3")
            };
            tarefas[0].Concluir();
            tarefas[1].Concluir();

            (_categoria.Tarefas as List<Tarefa>).AddRange(tarefas);
        }

        [Fact]
        public void ObterQuantidadeTarefasConcluidasDaCategoriaRetorna2()
        {
            //Act
            var quantidadeTarefasConcluidas = _categoria.ObterQuantidadeTarefasConcluidas();

            //Assert
            Assert.Equal(2, quantidadeTarefasConcluidas);
            
            //Assert - alternativa com Fluent Assertions
            quantidadeTarefasConcluidas.Should().Be(2);
        }

        [Fact]
        public void ObterQuantidadeTarefasNaoConcluidasDaCategoriaRetorna1()
        {
            //Act
            var quantidadeTarefasNaoConcluidas = _categoria.ObterQuantidadeTarefasNaoConcluidas();

            //Assert
            Assert.Equal(1, quantidadeTarefasNaoConcluidas);
        }
    }
}
