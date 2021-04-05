using System;
using backendtest.Domain.Application.Commands.Desenvolvedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace backendtest.Tests.UnitTests.Commands.Desenvolvedor
{
    [TestClass]
    public class AtualizarDesenvolvedorCommandTest
    {
        private AtualizarDesenvolvedorCommand _atualizarDesenvolvedorCommandTest;

        [TestMethod]
        public void Dado_um_IdVazio_Retorna_Erro()
        {
            _atualizarDesenvolvedorCommandTest =
                new AtualizarDesenvolvedorCommand(Guid.Empty, "Spotify", "08272217627", "marcelo@gmail.com");
            _atualizarDesenvolvedorCommandTest.Valido();
            Assert.AreEqual(_atualizarDesenvolvedorCommandTest.ValidationResult.Errors[0].ErrorMessage, "O Id não pode ser vazio.");
        } 

        [TestMethod]
        public void Dado_um_NomeNulo_Retorna_Erro()
        {
            _atualizarDesenvolvedorCommandTest =
                new AtualizarDesenvolvedorCommand(Guid.NewGuid(), null, "08272217627", "marcelo@gmail.com");
            _atualizarDesenvolvedorCommandTest.Valido();
            Assert.AreEqual(_atualizarDesenvolvedorCommandTest.ValidationResult.Errors[0].ErrorMessage, "O Nome não pode ser nulo.");
        }

        [TestMethod]
        public void Dado_um_NomeVazio_Retorna_Erro()
        {
            _atualizarDesenvolvedorCommandTest =
                new AtualizarDesenvolvedorCommand(Guid.NewGuid(), "", "08272217627", "marcelo@gmail.com");
            _atualizarDesenvolvedorCommandTest.Valido();
            Assert.AreEqual(_atualizarDesenvolvedorCommandTest.ValidationResult.Errors[0].ErrorMessage, "O Nome não pode ser vazio.");
        }

        [TestMethod]
        public void Dado_um_NomeExtenso_Retorna_Erro()
        {
            _atualizarDesenvolvedorCommandTest =
                new AtualizarDesenvolvedorCommand(Guid.NewGuid(),
                    "marcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.com",
                    "08272217627", "marcelo@gmail.com");
            _atualizarDesenvolvedorCommandTest.Valido();
            Assert.AreEqual(_atualizarDesenvolvedorCommandTest.ValidationResult.Errors[0].ErrorMessage, "O nome não pode ser maior que 255 caracteres.");
        }

        [TestMethod]
        public void Dado_um_CpfNulo_Retorna_Erro()
        {
            _atualizarDesenvolvedorCommandTest =
                new AtualizarDesenvolvedorCommand(Guid.NewGuid(), "MArcelo", null, "marcelo@gmail.com");
            _atualizarDesenvolvedorCommandTest.Valido();
            Assert.AreEqual(_atualizarDesenvolvedorCommandTest.ValidationResult.Errors[0].ErrorMessage, "O CPF não pode ser nulo.");
        }

        [TestMethod]
        public void Dado_um_CpfVazio_Retorna_Erro()
        {
            _atualizarDesenvolvedorCommandTest =
                new AtualizarDesenvolvedorCommand(Guid.NewGuid(), "MArcelo", "", "marcelo@gmail.com");
            _atualizarDesenvolvedorCommandTest.Valido();
            Assert.AreEqual(_atualizarDesenvolvedorCommandTest.ValidationResult.Errors[0].ErrorMessage, "O CPF não pode ser vazio.");
        }

        [TestMethod]
        public void Dado_um_CpfInvalido_Retorna_Erro()
        {
            _atualizarDesenvolvedorCommandTest =
                new AtualizarDesenvolvedorCommand(Guid.NewGuid(), "MArcelo", "fewfw123", "marcelo@gmail.com");
            _atualizarDesenvolvedorCommandTest.Valido();
            Assert.AreEqual(_atualizarDesenvolvedorCommandTest.ValidationResult.Errors[0].ErrorMessage, "O CPF informado não é válido.");
        }

        [TestMethod]
        public void Dado_um_EmailNulo_Retorna_Erro()
        {
            _atualizarDesenvolvedorCommandTest =
                new AtualizarDesenvolvedorCommand(Guid.NewGuid(), "MArcelo", "08272217627", null);
            _atualizarDesenvolvedorCommandTest.Valido();
            Assert.AreEqual(_atualizarDesenvolvedorCommandTest.ValidationResult.Errors[0].ErrorMessage, "O Email não pode ser nulo.");
        }

        [TestMethod]
        public void Dado_um_EmailVazio_Retorna_Erro()
        {
            _atualizarDesenvolvedorCommandTest =
                new AtualizarDesenvolvedorCommand(Guid.NewGuid(), "MArcelo", "08272217627", "");
            _atualizarDesenvolvedorCommandTest.Valido();
            Assert.AreEqual(_atualizarDesenvolvedorCommandTest.ValidationResult.Errors[0].ErrorMessage, "O Email não pode ser vazio.");
        }

        [TestMethod]
        public void Dado_um_EmailInvalido_Retorna_Erro()
        {
            _atualizarDesenvolvedorCommandTest =
                new AtualizarDesenvolvedorCommand(Guid.NewGuid(), "MArcelo", "08272217627", "marcelogmail.com");
            _atualizarDesenvolvedorCommandTest.Valido();
            Assert.AreEqual(_atualizarDesenvolvedorCommandTest.ValidationResult.Errors[0].ErrorMessage, "O e-mail informado não é válido.");
        }

        [TestMethod]
        public void Dado_um_EmailExtenso_Retorna_Erro()
        {
            _atualizarDesenvolvedorCommandTest =
                new AtualizarDesenvolvedorCommand(Guid.NewGuid(), "MArcelo", "08272217627",
                    "marceloaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@gmail.com");
            _atualizarDesenvolvedorCommandTest.Valido();
            Assert.AreEqual(_atualizarDesenvolvedorCommandTest.ValidationResult.Errors[0].ErrorMessage, "O e-mail não pode ser maior que 100 caracteres.");
        }

        [TestMethod]
        public void Dado_TodasInformacoesCorretas_Retorna_IsValidTrue()
        {
            _atualizarDesenvolvedorCommandTest = new AtualizarDesenvolvedorCommand(Guid.NewGuid(), "Marcelo", "08272217627", "marcelo@gmail.com");
            Assert.IsTrue(_atualizarDesenvolvedorCommandTest.Valido());
        }
    }
}