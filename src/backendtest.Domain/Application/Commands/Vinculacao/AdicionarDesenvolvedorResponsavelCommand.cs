using System;
using backendtest.Shared.Messages;
using FluentValidation;

namespace backendtest.Domain.Application.Commands.Vinculacao
{
    public class AdicionarDesenvolvedorResponsavelCommand : CommandGenerico
    {
        public Guid IdAplicativo { get; private set; }
        public Guid IdDesenvolvedor { get; private set; }

        public AdicionarDesenvolvedorResponsavelCommand(Guid idAplicativo, Guid idDesenvolvedor)
        {
            IdAplicativo = idAplicativo;
            IdDesenvolvedor = idDesenvolvedor;
        }

        public override bool Valido()
        {
            var validationResult = new AdicionarDesenvolvedorResponsavelValidation().Validate(this);
            return validationResult.IsValid;
        }
    }

    public class AdicionarDesenvolvedorResponsavelValidation : AbstractValidator<AdicionarDesenvolvedorResponsavelCommand>
    {
        public AdicionarDesenvolvedorResponsavelValidation()
        {
            this.CascadeMode = CascadeMode.Stop;

            RuleFor(a => a.IdAplicativo)
                .NotNull()
                .WithMessage("O Id do Aplicativo não pode ser nulo.")
                .NotEmpty()
                .WithMessage("O Id do Aplicativo não pode ser vazio.");

            RuleFor(a => a.IdDesenvolvedor)
                .NotNull()
                .WithMessage("O Id do Desenvolvedor não pode ser nulo.")
                .NotEmpty()
                .WithMessage("O Id do Desenvolvedor não pode ser vazio.");
        }
    }
}