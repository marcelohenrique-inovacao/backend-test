using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Data;

namespace backendtest.Domain.Data.Repositories
{
    public interface IAplicativoRepository : IRepository<Aplicativo>
    {
        void Adicionar(Aplicativo aplicativo);
        void Update(Aplicativo aplicativo);

        Task<IEnumerable<Aplicativo>> ObterTodos();
        Task<Aplicativo> ObterPorNome(string nome);
        Task<Aplicativo> ObterPorId(Guid id);
        //Task<bool> VincularDesenvolvedor(Desenvolvedor desenvolvedor);
        //Task<bool> DesvincularDesenvolvedor(Desenvolvedor desenvolvedor);
        Task<bool> DesenvolvedorResponsavelPorAplicativo(Guid desenvolvedor);
    }
}