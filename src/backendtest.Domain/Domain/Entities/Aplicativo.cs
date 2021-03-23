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

        public void AtualizarNome(string nome)
        {
            Nome = nome;
        }

        public void AtualizarDataLancamento(DateTime dataLancameto)
        {
            DataLancamento = dataLancameto;
        }

        public void AtualizarPlataforma(ETipoPlataforma tipoPlataforma)
        {
            Plataforma = tipoPlataforma;
        }

        public void TornarResponsavel(Desenvolvedor responsavel)
        {
            IdDesenvolvedorResponsavel = responsavel.Id;
            Responsavel = responsavel;
        }

        public void RemoverResponsavel()
        {
            IdDesenvolvedorResponsavel = null;
            Responsavel = null;
        }

        public void VincularDesenvolvedor(Desenvolvedor desenvolvedor)
        {
            _desenvolvedores.Add(desenvolvedor);
        }  
        public void DesvincularDesenvolvedor(Desenvolvedor desenvolvedor)
        {
            _desenvolvedores.Remove(desenvolvedor);
        }
    }
}
