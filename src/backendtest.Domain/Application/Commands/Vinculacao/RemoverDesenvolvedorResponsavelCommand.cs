using System;
using backendtest.Shared.Messages;
using FluentValidation;

namespace backendtest.Domain.Application.Commands.Vinculacao
{
    public class RemoverDesenvolvedorResponsavelCommand : CommandGenerico
    {
        public Guid IdAplicativo { get; private set; } 

        public RemoverDesenvolvedorResponsavelCommand(Guid idAplicativo, Guid idDesenvolvedor)
        {
            IdAplicativo = idAplicativo; 
        }

        public override bool Valido()
        {
            var validationResult = new RemoverDesenvolvedorResponsavelValidation().Validate(this);
            return validationResult.IsValid;
        }
    }
    public class RemoverDesenvolvedorResponsavelValidation : AbstractValidator<RemoverDesenvolvedorResponsavelCommand>
    {
        public RemoverDesenvolvedorResponsavelValidation()
        {
            this.CascadeMode = CascadeMode.Stop;

            RuleFor(a => a.IdAplicativo)
                .NotNull()
                .WithMessage("O Id do Aplicativo não pode ser nulo.")
                .NotEmpty()
                .WithMessage("O Id do Aplicativo não pode ser vazio."); 
        }
    }
}