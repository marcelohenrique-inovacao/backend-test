using System;
using backendtest.Shared.Messages;
using FluentValidation;

namespace backendtest.Domain.Application.Commands.Aplicativo
{
    public class ExcluirAplicativoCommand : CommandGenerico
    {
        public Guid Id { get; set; }

        public ExcluirAplicativoCommand(Guid id)
        {
            Id = id;
        }

        public override bool Valido()
        {
            var validationReturn = new ExcluirAplicativoCommand.ExcluirAplicativoValidation().Validate(this);
            ValidationResult = validationReturn;
            return validationReturn.IsValid;
        }

        public class ExcluirAplicativoValidation : AbstractValidator<ExcluirAplicativoCommand>
        {
            public ExcluirAplicativoValidation()
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