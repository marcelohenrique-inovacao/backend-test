using System;
using backendtest.Shared.Messages;
using FluentValidation;

namespace backendtest.Domain.Application.Commands.Vinculacao
{
    public class DesvincularAplicativoDesenvolvedorCommand : Command

    {
        public Guid IdAplicativo { get; private set; }
        public Guid IdDesenvolvedor { get; private set; }

        public DesvincularAplicativoDesenvolvedorCommand(Guid idAplicativo, Guid idDesenvolvedor)
        {
            IdAplicativo = idAplicativo;
            IdDesenvolvedor = idDesenvolvedor;
        }

        public override bool Valido()
        {
            var validationResult = new DesvincularAplicativoDesenvolvedorValidation().Validate(this);
            return validationResult.IsValid;
        }
    }
    public class DesvincularAplicativoDesenvolvedorValidation : AbstractValidator<DesvincularAplicativoDesenvolvedorCommand>
    {
        public DesvincularAplicativoDesenvolvedorValidation()
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