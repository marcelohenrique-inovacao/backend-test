using backendtest.Domain.Domain.Enums;
using backendtest.Shared.DomainObjects;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace backendtest.Domain.Domain.Entities
{
    public class Aplicativo : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public DateTime DataLancamento { get; private set; }
        public ETipoPlataforma Plataforma { get; private set; }
        public Guid? IdDesenvolvedorResponsavel { get; private set; }
        public Desenvolvedor Responsavel { get; private set; }


        public readonly List<Desenvolvedor> _desenvolvedores;
        public IReadOnlyCollection<Desenvolvedor> Desenvolvedores => _desenvolvedores;

        protected Aplicativo() { _desenvolvedores = new List<Desenvolvedor>(); }
        public Aplicativo(string nome, DateTime dataLancamento, ETipoPlataforma plataforma)
        {
            Nome = nome;
            DataLancamento = dataLancamento;
            Plataforma = plataforma;
            _desenvolvedores = new List<Desenvolvedor>();
        }

        public void TrocarNome(string nome)
        {
            Nome = nome;
        }

        public void TrocarDataLancamento(DateTime dataLancameto)
        {
            DataLancamento = dataLancameto;
        }

        public void TornarResponsavel(Desenvolvedor responsavel)
        {
            IdDesenvolvedorResponsavel = responsavel.Id;
            Responsavel = responsavel;
        }

        public void RemoverResponsavel(Desenvolvedor responsavel)
        {
            IdDesenvolvedorResponsavel = null;
            Responsavel = null;
        }

        #region Command
        public class AplicativoCadastroValido : AbstractValidator<Aplicativo>
        {
            public AplicativoCadastroValido()
            {
                RuleFor(d => d.Nome)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("O Nome não pode ser vazio ou nulo.");

                RuleFor(d => d.Nome.Length)
                    .LessThanOrEqualTo(255)
                    .WithMessage("O nome não pode ser maior que 255 caracteres.");

                RuleFor(d => d.DataLancamento)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("A Data de lançamento não pode ser vazio ou nulo.");

                RuleFor(d => d.Plataforma)
                    .IsInEnum()
                    .WithMessage("Valores aceitos para Tipo de Plataforma: 1-Desktop, 2-Web ou 3-Mobile.");
            }
        }

        public override bool Valido()
        {
            var validationResult = new AplicativoCadastroValido().Validate(this);
            return validationResult.IsValid;
        }
        #endregion
    }
}
