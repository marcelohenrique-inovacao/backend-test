using backendtest.Shared.Messages;
using FluentValidation;
using System;

namespace backendtest.Domain.Application.Commands
{
    public class RegistrarDesenvolvedorCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }

        public RegistrarDesenvolvedorCommand(Guid id, string nome, string cpf, string email)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Email = email;
        }

        public override bool Valido()
        {
            return new RegistrarDesenvolvedorValidation().Validate(this).IsValid;
        }

        public class RegistrarDesenvolvedorValidation : AbstractValidator<RegistrarDesenvolvedorCommand>
        {
            public RegistrarDesenvolvedorValidation()
            {
                RuleFor(d => d.Nome)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("O Nome não pode ser vazio ou nulo.");

                RuleFor(d => d.Nome.Length)
                    .LessThanOrEqualTo(255)
                    .WithMessage("O nome não pode ser maior que 255 caracteres.");

                RuleFor(c => c.Cpf)
                    .Must(TerCpfValido)
                    .WithMessage("O CPF informado não é válido.");

                RuleFor(c => c.Email)
                    .Must(TerEmailValido)
                    .WithMessage("O e-mail informado não é válido.");

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