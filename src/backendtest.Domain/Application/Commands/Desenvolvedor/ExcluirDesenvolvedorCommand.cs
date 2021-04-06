using System;
using backendtest.Shared.Messages;
using FluentValidation;

namespace backendtest.Domain.Application.Commands.Desenvolvedor
{
    public class ExcluirDesenvolvedorCommand : CommandGenerico
    {
        public Guid Id { get; set; }

        public ExcluirDesenvolvedorCommand(Guid id)
        {
            Id = id;
        }

        public override bool Valido()
        {
            var validationReturn = new ExcluirDesenvolvedorValidation().Validate(this);
            ValidationResult = validationReturn;
            return validationReturn.IsValid;
        }

        public class ExcluirDesenvolvedorValidation : AbstractValidator<ExcluirDesenvolvedorCommand>
        {
            public ExcluirDesenvolvedorValidation()
            {
                this.CascadeMode = CascadeMode.Stop;

                RuleFor(e => e.Id)
                    .NotNull()
                    .WithMessage("O Id não pode ser nulo.")
                    .NotEmpty()
                    .WithMessage("O Id não pode ser vazio.");
            }
        }
    }
}