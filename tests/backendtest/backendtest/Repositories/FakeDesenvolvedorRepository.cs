using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backendtest.Domain.Data.Repositories;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Data;

namespace backendtest.Tests.Repositories
{
    public class FakeDesenvolvedorRepository : IDesenvolvedorRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        public void Adicionar(Desenvolvedor desenvolvedor)
        {
            
        }

        public void Update(Desenvolvedor desenvolvedor)
        {
            
        }

        public Task<bool> Excluir(Desenvolvedor desenvolvedor)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Desenvolvedor>> ObterTodos()
        {
            return null;
        }

        public async Task<Desenvolvedor> ObterPorCpf(string cpf)
        {
            var desenvolvedor = new Desenvolvedor("Pedro", "08272217627", "teste@gmail.com");
            return desenvolvedor;
        }

        public Task<Desenvolvedor> ObterPorId(Guid id)
        {
            return null;

        }

        public Task<Desenvolvedor> ObterPorIdComTracking(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DesenvolvedorAplicativo>> ObterAplicativosRelacionados(Guid idDesenvolvedor)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DesenvolvedorAplicativo>> ObterAplicativosDesenvolvedorFazParte(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}