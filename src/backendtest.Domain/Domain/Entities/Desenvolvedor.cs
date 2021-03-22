using backendtest.Domain.Domain.ValueObjects;
using backendtest.Shared.DomainObjects;
using FluentValidation;
using System.Collections.Generic;

namespace backendtest.Domain.Domain.Entities
{
    public class Desenvolvedor : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public CPF Cpf { get; private set; }
        public Email Email { get; private set; }
        private readonly List<Aplicativo> _aplicativos;
        public Aplicativo ResponsavelAplicativo { get; private set; }
        public IReadOnlyCollection<Aplicativo> Aplicativos => _aplicativos;


        protected Desenvolvedor() { _aplicativos = new List<Aplicativo>(); }
        public Desenvolvedor(string nome, string cpf, string email)
        {
            Nome = nome;
            Cpf = new CPF(cpf);
            Email = new Email(email);
            _aplicativos = new List<Aplicativo>();
        }

        public void TrocarEmail(string email)
        {
            Email = new Email(email);
        }

        public void TrocarCpf(string cpf)
        {
            Cpf = new CPF(cpf);
        }
        #region Command

        public class DesenvolvedorCadastroValido : AbstractValidator<Desenvolvedor>
        {
            public DesenvolvedorCadastroValido()
            {
                RuleFor(d => d.Nome)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("O Nome não pode ser vazio ou nulo.");

                RuleFor(d => d.Nome.Length)
                    .LessThanOrEqualTo(255)
                    .WithMessage("O nome não pode ser maior que 255 caracteres.");
                //RuleFor(c => c.Cpf)
                //    .Must(TerCpfValido)
                //    .WithMessage("O CPF informado não é válido.");

                //RuleFor(c => c.Email)
                //    .Must(TerEmailValido)
                //    .WithMessage("O e-mail informado não é válido.");
            }
        }
        protected static bool TerCpfValido(string cpf)
        {
            return CPF.Validar(cpf);
        }

        protected static bool TerEmailValido(string email)
        {
            return Email.Validar(email);
        }
        public override bool Valido()
        {
            var validationResult = new DesenvolvedorCadastroValido().Validate(this);
            return validationResult.IsValid;
        }
        #endregion
    }
}
