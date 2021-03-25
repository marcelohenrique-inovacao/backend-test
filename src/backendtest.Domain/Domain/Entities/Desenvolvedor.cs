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
        public virtual IReadOnlyCollection<Aplicativo> Aplicativos { get; set; }
        public virtual IReadOnlyCollection<DesenvolvedorAplicativo> DesenvolvedorAplicativos { get; set; }


        protected Desenvolvedor()
        {
            Aplicativos = new List<Aplicativo>();
            DesenvolvedorAplicativos = new HashSet<DesenvolvedorAplicativo>(); 
        }
        public Desenvolvedor(string nome, string cpf, string email)
        {
            Nome = nome;
            Cpf = new CPF(cpf);
            Email = new Email(email);
            Aplicativos = new List<Aplicativo>();
        }
        public void AtualizarNome(string nome)
        {
            Nome = nome;
        }
        public void AtualizarEmail(string email)
        {
            Email = new Email(email);
        }

        public void AtualizarCpf(string cpf)
        {
            Cpf = new CPF(cpf);
        }
    }
}
