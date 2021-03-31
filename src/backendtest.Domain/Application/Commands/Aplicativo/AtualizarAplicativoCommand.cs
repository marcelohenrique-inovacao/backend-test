using System;
using backendtest.Domain.Domain.Enums;
using backendtest.Shared.Messages;
using FluentValidation;

namespace backendtest.Domain.Application.Commands.Aplicativo
{
    public class AtualizarAplicativoCommand : CommandGenerico
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string DataLancamento { get; private set; }
        public ETipoPlataforma TipoPlataforma { get; private set; }
        public Domain.Entities.Desenvolvedor? DesenvolvedorResponsavel { get; private set; }

        public AtualizarAplicativoCommand(Guid id, string nome, string dataLancamento, int tipoPlataforma,
            Domain.Entities.Desenvolvedor desenvolvedorResponsavel)
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
            var validationReturn = new AtualizarAplicativoCommand.AtualizarAplicativoValidation().Validate(this);
            ValidationResult = validationReturn;
            return validationReturn.IsValid;
        }

        public class AtualizarAplicativoValidation : AbstractValidator<AtualizarAplicativoCommand>
        {
            public AtualizarAplicativoValidation()
            {
                RuleFor(d => d.Nome)
                    .NotNull()
                    .WithMessage("O Nome não pode ser nulo.")
                    .NotEmpty()
                    .WithMessage("O Nome não pode ser vazio.");

                RuleFor(d => d.Nome.Length)
                    .LessThanOrEqualTo(255)
                    .WithMessage("O Nome não pode ser maior que 255 caracteres.");

                RuleFor(d => d.DataLancamento)
                    .NotNull()
                    .WithMessage("A Data de lançamento não pode ser nulo.")
                    .NotEmpty()
                    .WithMessage("A Data de lançamento não pode ser vazio.")
                    .Must(ValidaDataLancamento)
                    .WithMessage("Formato de data inválido. Formato aceito: YYYY-MM-DD");

                RuleFor(d => d.TipoPlataforma)
                    .IsInEnum()
                    .WithMessage("Valores aceitos para Tipo de Plataforma: 1-Desktop, 2-Web ou 3-Mobile.");
            }

            public bool ValidaDataLancamento(string data)
            {
                DateTime result;
                return DateTime.TryParse(data, out result);
            }
        }
    }
}