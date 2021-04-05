using System;
using backendtest.Domain.Application.Commands.Vinculacao;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace backendtest.Tests.UnitTests.Commands.Vinculacao
{
    [TestClass]
    public class DesvincularAplicativoDesenvolvedorCommandTest
    {
        private AdicionarDesenvolvedorResponsavelCommand _adicionarDesenvolvedorResponsavelCommand;

        [TestMethod]
        public void Dado_Um_IdDesenvolvedorVazio_Retorna_Erro()
        {
            _adicionarDesenvolvedorResponsavelCommand =
                new AdicionarDesenvolvedorResponsavelCommand(Guid.NewGuid(), Guid.Empty);
            _adicionarDesenvolvedorResponsavelCommand.Valido();
            Assert.AreEqual(_adicionarDesenvolvedorResponsavelCommand.ValidationResult.Errors[0].ErrorMessage, "O Id do Desenvolvedor não pode ser vazio.");
        }  
        [TestMethod]
        public void Dado_Um_IdAplicativoVazio_Retorna_Erro()
        {
            _adicionarDesenvolvedorResponsavelCommand =
                new AdicionarDesenvolvedorResponsavelCommand(Guid.Empty, Guid.NewGuid());
            _adicionarDesenvolvedorResponsavelCommand.Valido();
            Assert.AreEqual(_adicionarDesenvolvedorResponsavelCommand.ValidationResult.Errors[0].ErrorMessage, "O Id do Aplicativo não pode ser vazio.");

        }
    }
}