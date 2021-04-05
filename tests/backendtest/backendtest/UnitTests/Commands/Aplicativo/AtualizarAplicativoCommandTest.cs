using System;
using backendtest.Domain.Application.Commands.Aplicativo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace backendtest.Tests.UnitTests.Commands.Aplicativo
{
    [TestClass]
    public class AtualizarAplicativoCommandTest
    {
        private AtualizarAplicativoCommand _atualizarAplicativoCommand;
        private readonly Domain.Domain.Entities.Desenvolvedor _desenvolvedor =
            new("Marcelo", "08272217627", "marcelo@gmail.com");

        [TestMethod]
        public void Dado_um_IdVazio_Retorna_Erro()
        {
            _atualizarAplicativoCommand =
                new AtualizarAplicativoCommand(Guid.Empty, null, "2021-01-01", 3, _desenvolvedor);
            _atualizarAplicativoCommand.Valido();
            Assert.AreEqual(_atualizarAplicativoCommand.ValidationResult.Errors[0].ErrorMessage, "O Id não pode ser vazio.");
        }

        [TestMethod]
        public void Dado_um_NomeNulo_Retorna_Erro()
        {
            _atualizarAplicativoCommand =
                new AtualizarAplicativoCommand(Guid.NewGuid(), null, "2021-01-01", 3, _desenvolvedor);
            _atualizarAplicativoCommand.Valido();
            Assert.AreEqual(_atualizarAplicativoCommand.ValidationResult.Errors[0].ErrorMessage, "O Nome não pode ser nulo.");
        }

        [TestMethod]
        public void Dado_um_NomeVazio_Retorna_Erro()
        {
            _atualizarAplicativoCommand =
                new AtualizarAplicativoCommand(Guid.NewGuid(), "", "2021-01-01", 3, _desenvolvedor);
            _atualizarAplicativoCommand.Valido();
            Assert.AreEqual(_atualizarAplicativoCommand.ValidationResult.Errors[0].ErrorMessage, "O Nome não pode ser vazio.");
        }

        [TestMethod]
        public void Dado_um_NomeExtenso_Retorna_Erro()
        {
            _atualizarAplicativoCommand =
                new AtualizarAplicativoCommand(Guid.NewGuid(),
                    "marcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.com",
                    "2021-01-01", 3, _desenvolvedor);
            _atualizarAplicativoCommand.Valido();
            Assert.AreEqual(_atualizarAplicativoCommand.ValidationResult.Errors[0].ErrorMessage, "O Nome não pode ser maior que 255 caracteres.");
        }

        [TestMethod]
        public void Dado_uma_DataNula_Retorna_Erro()
        {
            _atualizarAplicativoCommand =
                new AtualizarAplicativoCommand(Guid.NewGuid(), "BradescoApp", null, 3, _desenvolvedor);
            _atualizarAplicativoCommand.Valido();
            Assert.AreEqual(_atualizarAplicativoCommand.ValidationResult.Errors[0].ErrorMessage, "A Data de lançamento não pode ser nula.");
        }
        [TestMethod]
        public void Dado_uma_DataVazia_Retorna_Erro()
        {
            _atualizarAplicativoCommand =
                new AtualizarAplicativoCommand(Guid.NewGuid(), "BradescoApp", "", 3, _desenvolvedor);
            _atualizarAplicativoCommand.Valido();
            Assert.AreEqual(_atualizarAplicativoCommand.ValidationResult.Errors[0].ErrorMessage, "A Data de lançamento não pode ser vazia.");
        }
        [TestMethod]
        public void Dado_uma_DataInvalida_Retorna_Erro()
        {
            _atualizarAplicativoCommand =
                new AtualizarAplicativoCommand(Guid.NewGuid(), "BradescoApp", "2021-55-55", 3, _desenvolvedor);
            _atualizarAplicativoCommand.Valido();
            Assert.AreEqual(_atualizarAplicativoCommand.ValidationResult.Errors[0].ErrorMessage, "Formato de data inválida. Formato aceito: YYYY-MM-DD.");
        }

        [TestMethod]
        public void Dado_um_ValorDiferenteDoEnumerator_TipoPlataforma_Retorna_Erro()
        {
            _atualizarAplicativoCommand =
    new AtualizarAplicativoCommand(Guid.NewGuid(), "BradescoApp", "2021-01-01", 5, _desenvolvedor);
            _atualizarAplicativoCommand.Valido();
            Assert.AreEqual(_atualizarAplicativoCommand.ValidationResult.Errors[0].ErrorMessage, "Valores aceitos para Tipo de Plataforma: 1-Desktop, 2-Web ou 3-Mobile.");
        }
    }
}