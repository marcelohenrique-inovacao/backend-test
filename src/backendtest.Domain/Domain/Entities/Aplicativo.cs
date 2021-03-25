using backendtest.Domain.Domain.Enums;
using backendtest.Shared.DomainObjects;
using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;

namespace backendtest.Domain.Domain.Entities
{
    public class Aplicativo : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public DateTime DataLancamento { get; private set; }
        public ETipoPlataforma Plataforma { get; private set; }
        public Guid? IdDesenvolvedorResponsavel { get; private set; }
        public virtual Desenvolvedor Responsavel { get; private set; }

        private List<DesenvolvedorAplicativo> _desenvolvedorAplicativos;

        public virtual IReadOnlyCollection<DesenvolvedorAplicativo> desenvolvedorAplicativo =>
            _desenvolvedorAplicativos;

        public Aplicativo()
        {
            _desenvolvedorAplicativos = new List<DesenvolvedorAplicativo>();
        }
        public Aplicativo(string nome, DateTime dataLancamento, ETipoPlataforma plataforma)
        {
            Nome = nome;
            DataLancamento = dataLancamento;
            Plataforma = plataforma;
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

            var linkagem = new DesenvolvedorAplicativo(this, responsavel);
            _desenvolvedorAplicativos.Add(linkagem);
        }

        public void RemoverResponsavel(Desenvolvedor responsavel)
        {
            var linkagem = new DesenvolvedorAplicativo(this, responsavel);
            _desenvolvedorAplicativos.Remove(linkagem);

            IdDesenvolvedorResponsavel = null;
            Responsavel = null;
        }

        public void VincularDesenvolvedor(Desenvolvedor desenvolvedor)
        {
            _desenvolvedorAplicativos.Add(new DesenvolvedorAplicativo(this, desenvolvedor));
        }
        public void DesvincularDesenvolvedor(Desenvolvedor desenvolvedor)
        {
            // REVIEW: isso não vai funcionar. 
            _desenvolvedorAplicativos.Remove(new DesenvolvedorAplicativo(this, desenvolvedor));
        }

        public bool PermiteExcluir()
        {
            return _desenvolvedorAplicativos.Count <= 0;
        }
    }
}
