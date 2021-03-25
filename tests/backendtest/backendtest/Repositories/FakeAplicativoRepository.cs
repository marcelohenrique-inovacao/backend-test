using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backendtest.Domain.Data.Repositories;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Data;

namespace backendtest.Tests.Repositories
{
    public class FakeAplicativoRepository : IAplicativoRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        public void Adicionar(Aplicativo aplicativo)
        {
            throw new NotImplementedException();
        }

        public void Update(Aplicativo aplicativo)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Aplicativo>> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public Task<Aplicativo> ObterPorNome(string nome)
        {
            throw new NotImplementedException();
        }

        public Task<Aplicativo> ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DesenvolvedorResponsavelPorAplicativo(Guid desenvolvedor)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}