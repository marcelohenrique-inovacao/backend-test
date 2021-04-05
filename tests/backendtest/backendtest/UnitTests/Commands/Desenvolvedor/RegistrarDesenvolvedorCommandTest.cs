using System;
using backendtest.Domain.Application.Commands.Desenvolvedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace backendtest.Tests.UnitTests.Commands.Desenvolvedor
{
    [TestClass]
    public class RegistrarDesenvolvedorCommandTest
    {
        private RegistrarDesenvolvedorCommand _registrarDesenvolvedorCommand;

        [TestMethod]
        public void Dado_um_NomeNulo_Retorna_Erro()
        {
            _registrarDesenvolvedorCommand =
                new RegistrarDesenvolvedorCommand(Guid.Empty, null, "08272217627", "marcelo@gmail.com");
            _registrarDesenvolvedorCommand.Valido();
            Assert.AreEqual(_registrarDesenvolvedorCommand.ValidationResult.Errors[0].ErrorMessage, "O Nome não pode ser nulo.");
        }

        [TestMethod]
        public void Dado_um_NomeVazio_Retorna_Erro()
        {
            _registrarDesenvolvedorCommand =
                new RegistrarDesenvolvedorCommand(Guid.Empty, "", "08272217627", "marcelo@gmail.com");
            _registrarDesenvolvedorCommand.Valido();
            Assert.AreEqual(_registrarDesenvolvedorCommand.ValidationResult.Errors[0].ErrorMessage, "O Nome não pode ser vazio.");
        }

        [TestMethod]
        public void Dado_um_NomeExtenso_Retorna_Erro()
        {
            _registrarDesenvolvedorCommand =
                new RegistrarDesenvolvedorCommand(Guid.Empty, 
                    "marcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.commarcelo@gmail.com", 
                    "08272217627", "marcelo@gmail.com");
            _registrarDesenvolvedorCommand.Valido();
            Assert.AreEqual(_registrarDesenvolvedorCommand.ValidationResult.Errors[0].ErrorMessage, "O nome não pode ser maior que 255 caracteres.");
        }

        [TestMethod]
        public void Dado_um_CpfNulo_Retorna_Erro()
        {
            _registrarDesenvolvedorCommand =
                new RegistrarDesenvolvedorCommand(Guid.Empty, "MArcelo", null, "marcelo@gmail.com");
            _registrarDesenvolvedorCommand.Valido();
            Assert.AreEqual(_registrarDesenvolvedorCommand.ValidationResult.Errors[0].ErrorMessage, "O CPF não pode ser nulo.");
        }

        [TestMethod]
        public void Dado_um_CpfVazio_Retorna_Erro()
        {
            _registrarDesenvolvedorCommand =
                new RegistrarDesenvolvedorCommand(Guid.Empty, "MArcelo", "", "marcelo@gmail.com");
            _registrarDesenvolvedorCommand.Valido();
            Assert.AreEqual(_registrarDesenvolvedorCommand.ValidationResult.Errors[0].ErrorMessage, "O CPF não pode ser vazio.");
        }

        [TestMethod]
        public void Dado_um_CpfInvalido_Retorna_Erro()
        {
            _registrarDesenvolvedorCommand =
                new RegistrarDesenvolvedorCommand(Guid.Empty, "MArcelo", "fewfw123", "marcelo@gmail.com");
            _registrarDesenvolvedorCommand.Valido();
            Assert.AreEqual(_registrarDesenvolvedorCommand.ValidationResult.Errors[0].ErrorMessage, "O CPF informado não é válido.");
        }

        [TestMethod]
        public void Dado_um_EmailNulo_Retorna_Erro()
        {
            _registrarDesenvolvedorCommand =
                new RegistrarDesenvolvedorCommand(Guid.Empty, "MArcelo", "08272217627", null);
            _registrarDesenvolvedorCommand.Valido();
            Assert.AreEqual(_registrarDesenvolvedorCommand.ValidationResult.Errors[0].ErrorMessage, "O Email não pode ser nulo.");
        }

        [TestMethod]
        public void Dado_um_EmailVazio_Retorna_Erro()
        {
            _registrarDesenvolvedorCommand =
                new RegistrarDesenvolvedorCommand(Guid.Empty, "MArcelo", "08272217627", "");
            _registrarDesenvolvedorCommand.Valido();
            Assert.AreEqual(_registrarDesenvolvedorCommand.ValidationResult.Errors[0].ErrorMessage, "O Email não pode ser vazio.");
        }

        [TestMethod]
        public void Dado_um_EmailInvalido_Retorna_Erro()
        {
            _registrarDesenvolvedorCommand =
                new RegistrarDesenvolvedorCommand(Guid.Empty, "MArcelo", "08272217627", "marcelogmail.com");
            _registrarDesenvolvedorCommand.Valido();
            Assert.AreEqual(_registrarDesenvolvedorCommand.ValidationResult.Errors[0].ErrorMessage, "O e-mail informado não é válido.");
        }

        [TestMethod]
        public void Dado_um_EmailExtenso_Retorna_Erro()
        {
            _registrarDesenvolvedorCommand =
                new RegistrarDesenvolvedorCommand(Guid.Empty, "MArcelo", "08272217627",
                    "marceloaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@gmail.com");
            _registrarDesenvolvedorCommand.Valido();
            Assert.AreEqual(_registrarDesenvolvedorCommand.ValidationResult.Errors[0].ErrorMessage, "O e-mail não pode ser maior que 100 caracteres.");
        }

        [TestMethod]
        public void Dado_TodasInformacoesCorretas_Retorna_IsValidTrue()
        {
            _registrarDesenvolvedorCommand = new RegistrarDesenvolvedorCommand(Guid.Empty, "Marcelo", "08272217627", "marcelo@gmail.com");
            Assert.IsTrue(_registrarDesenvolvedorCommand.Valido());
        }
    }
}