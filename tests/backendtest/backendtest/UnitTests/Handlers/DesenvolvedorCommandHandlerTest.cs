using System;
using System.Threading;
using System.Threading.Tasks;
using backendtest.Domain.Application.Commands.Desenvolvedor;
using backendtest.Domain.Application.Commands.Handlers;
using backendtest.Domain.Application.DTOs;
using backendtest.Domain.Data.Repositories;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Communication;
using backendtest.Shared.Messages;
using FluentValidation.Results;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.AutoMock;

namespace backendtest.Tests.UnitTests.Handlers
{
    [TestClass]
    public class DesenvolvedorCommandHandlerTest
    {
        private readonly AutoMocker _mocker;
        private readonly DesenvolvedorCommandHandler _desenvolvedorCommandHandler;

        public DesenvolvedorCommandHandlerTest()
        {
            _mocker = new AutoMocker();
            _desenvolvedorCommandHandler = _mocker.CreateInstance<DesenvolvedorCommandHandler>();

        }
        [TestMethod]
        public async Task Dado_Um_CpfJaCadastrado_Retorna_Erro()
        {
            var desenvolvedorCommand = DesenvolvedorDto.ParaDesenvolvedorDto(new Desenvolvedor( "Marcelo", "08272217627", "marcelo@gmail.com"));
            var desenvolvedorCommandMesmoCpf = new RegistrarDesenvolvedorCommand(Guid.Empty, "Teste", "08272217627", "teste@gmail.com");
             
            _mocker.GetMock<IDesenvolvedorRepository>().Setup(d =>d.ObterPorCpf("08272217627")).Returns(Task.FromResult(desenvolvedorCommand));
            _mocker.GetMock<IDesenvolvedorRepository>().Setup(d =>d.UnitOfWork.Commit()).Returns(Task.FromResult(false)); 
            var result = await _desenvolvedorCommandHandler.Handle(desenvolvedorCommandMesmoCpf, CancellationToken.None);
            
            Assert.IsNotNull(result);
            _mocker.GetMock<IDesenvolvedorRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
            _mocker.GetMock<ICommandResult>().Verify(r => r.AddFluentValidation(It.IsAny<ValidationResult>()), Times.Once);

        }
    }
}