using backendtest.Domain.Application.Commands.Desenvolvedor;
using backendtest.Domain.Application.Commands.Handlers;
using backendtest.Shared.Communication.Mediator;
using backendtest.Tests.Repositories;
using FluentAssertions;
using FluentValidation.Results;
using Moq;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace backendtest.Tests.Steps
{
    [Binding]
    public class DesenvolvedorSteps
    {
        private static readonly Mock<IMediatorHandler> _mediatorHandler = new(); 
        private DesenvolvedorCommandHandler _handler = 
            new(new FakeDesenvolvedorRepository(), _mediatorHandler.Object);

        private RegistrarDesenvolvedorCommand _command; 
        private readonly ScenarioContext _scenarioContext; 
        private ValidationResult retornoCommand;
        public DesenvolvedorSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"que todos os dados do desenvolvedor estão corretos")]
        public void DadoQueTodosOsDadosDoDesenvolvedorEstaoCorretos()
        { 
            _command = new RegistrarDesenvolvedorCommand(
                Guid.Empty, "Marcelo", "08272217627", "marcelo@gmail.com");
        }

        [Given(@"que alguma informação do desenvolvedor está inválida")]
        public void DadoQueAlgumaInformacaoDoDesenvolvedorEstaInvalida()
        {
            _command = new RegistrarDesenvolvedorCommand(
                Guid.Empty, "", "08272217627", "marcelo@gmail.com");
        }

        [Given(@"que ele estiver vinculado à um Aplicativo")]
        public void DadoQueEleEstiverVinculadoAUmAplicativo()
        {
            
        }

        [Given(@"que o CPF do desenvolvedor está inválido")]
        public void DadoQueOCPFDoDesenvolvedorEstaInvalido()
        {
            _command = new RegistrarDesenvolvedorCommand(
                Guid.Empty,"Marcelo", "123", "marcelo@gmail.com");
        }

        [Given(@"que o Email do desenvolvedor está inválido")]
        public void DadoQueOEmailDoDesenvolvedorEstaInvalido()
        {
            _command = new RegistrarDesenvolvedorCommand(
                Guid.Empty,"Marcelo", "08272217627", "marcelogmail.com");
        }

        [When(@"eu tento cadastrar")]
        public async void QuandoEuTentoCadastrar()
        {
            retornoCommand = await _handler.Handle(_command, new CancellationToken());
        }

        [When(@"eu tento alterar")]
        public void QuandoEuTentoAlterar()
        { 
        }

        [When(@"eu tento excluir")]
        public void QuandoEuTentoExcluir()
        { 
        }

        [Then(@"retorna Ok")]
        public void EntaoRetornaOk()
        {
            retornoCommand.Errors.Count.Should().Be(null);
        }

        [Then(@"retorna BadRequest")]
        public void EntaoRetornaBadRequest()
        {
            retornoCommand.Errors.Count.Should().BeGreaterThan(0); 
        }
    }
}
