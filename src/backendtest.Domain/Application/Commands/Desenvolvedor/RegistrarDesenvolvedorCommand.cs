using System;
using backendtest.Shared.Messages;
using FluentValidation;

namespace backendtest.Domain.Application.Commands.Desenvolvedor
{
    public class RegistrarDesenvolvedorCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
         
        public RegistrarDesenvolvedorCommand(Guid id, string nome, string cpf, string email)
        {
            Id = id;
            AggregateId = id;
            Nome = nome;
            Cpf = cpf;
            Email = email;
        }

        public override bool Valido()
        {
            var validationReturn = new RegistrarDesenvolvedorValidation().Validate(this);
            ValidationResult = validationReturn;
            return validationReturn.IsValid;
        }

        public class RegistrarDesenvolvedorValidation : AbstractValidator<RegistrarDesenvolvedorCommand>
        {
            public RegistrarDesenvolvedorValidation()
            {
                this.CascadeMode = CascadeMode.Stop;
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