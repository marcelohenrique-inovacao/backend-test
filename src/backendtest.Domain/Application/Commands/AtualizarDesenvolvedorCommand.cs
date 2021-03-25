using System;
using backendtest.Shared.Messages;
using FluentValidation;

namespace backendtest.Domain.Application.Commands
{
    public class AtualizarDesenvolvedorCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }

        public AtualizarDesenvolvedorCommand(Guid id, string nome, string cpf, string email)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Email = email;
        }
        public override bool Valido()
        {
            var validationReturn = new AtualizarDesenvolvedorValidation().Validate(this);
            ValidationResult = validationReturn;
            return validationReturn.IsValid;
        }
        //REVIEW: CÓDIGO DUPLICADO
        public class AtualizarDesenvolvedorValidation : AbstractValidator<AtualizarDesenvolvedorCommand>
        {
            public AtualizarDesenvolvedorValidation()
            {
                this.CascadeMode = CascadeMode.Stop;

                RuleFor(d => d.Id)
                    .NotNull()
                    .WithMessage("O Id não pode ser nulo.")

                    .NotEmpty()
                    .WithMessage("O Id não pode ser vazio.");

                RuleFor(d => d.Nome)
                    .NotNull()
                    .WithMessage("O Nome não pode ser nulo.")

                    .NotEmpty()
                    .WithMessage("O Nome não pode ser vazio.");


                RuleFor(c => c.Cpf)
                    .NotNull()
                    .WithMessage("O CPF não pode ser nulo.")

                    .NotEmpty()
                    .WithMessage("O CPF não pode ser vazio.")

                    .Must(TerCpfValido)
                    .WithMessage("O CPF informado não é válido.");

                RuleFor(c => c.Email)
                    .NotNull()
                    .WithMessage("O Email não pode ser nulo.")

                    .NotEmpty()
                    .WithMessage("O Email não pode ser vazio.")

                    .Must(TerEmailValido)
                    .WithMessage("O e-mail informado não é válido.");

                RuleFor(d => d.Nome.Length)
                    .LessThanOrEqualTo(255)
                    .WithMessage("O nome não pode ser maior que 255 caracteres.");

                RuleFor(d => d.Email.Length)
                    .LessThanOrEqualTo(100)
                    .WithMessage("O nome não pode ser maior que 100 caracteres.");
            }
        }
        protected static bool TerCpfValido(string cpf)
        {
            return Domain.ValueObjects.CPF.Validar(cpf);
        }

        protected static bool TerEmailValido(string email)
        {
            return Domain.ValueObjects.Email.Validar(email);
        }
    }
}