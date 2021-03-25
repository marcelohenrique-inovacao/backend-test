using backendtest.Domain.Domain.Enums;
using backendtest.Shared.Messages;
using FluentValidation;
using System;
using backendtest.Domain.Domain.Entities;

namespace backendtest.Domain.Application.Commands
{
    public class RegistrarAplicativoCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataLancamento { get; private set; }
        public ETipoPlataforma TipoPlataforma { get; private set; }
        public Desenvolvedor? DesenvolvedorResponsavel { get; private set; }

        public RegistrarAplicativoCommand(Guid id, string nome, DateTime dataLancamento, int tipoPlataforma, Desenvolvedor desenvolvedorResponsavel)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
            DataLancamento = dataLancamento;
            TipoPlataforma = (ETipoPlataforma)tipoPlataforma;
            DesenvolvedorResponsavel = desenvolvedorResponsavel;
        }

        public override bool Valido()
        {
            var validationReturn = new RegistrarAplicativoValidation().Validate(this);
            ValidationResult = validationReturn;
            return validationReturn.IsValid;
        }
        public class RegistrarAplicativoValidation : AbstractValidator<RegistrarAplicativoCommand>
        {
            public RegistrarAplicativoValidation()
            {
                RuleFor(d => d.Nome)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("O Nome não pode ser vazio ou nulo.");

                RuleFor(d => d.Nome.Length)
                    .LessThanOrEqualTo(255)
                    .WithMessage("O nome não pode ser maior que 255 caracteres.");

                RuleFor(d => d.DataLancamento)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("A Data de lançamento não pode ser vazio ou nulo.");

                RuleFor(d => d.TipoPlataforma)
                    .IsInEnum()
                    .WithMessage("Valores aceitos para Tipo de Plataforma: 1-Desktop, 2-Web ou 3-Mobile.");
            }
        }
    }
}