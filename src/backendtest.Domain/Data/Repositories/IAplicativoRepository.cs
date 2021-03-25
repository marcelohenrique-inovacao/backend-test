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
        Task<bool> Excluir(Aplicativo aplicativo);

        Task<IEnumerable<Aplicativo>> ObterTodos();
        Task<Aplicativo> ObterPorNome(string nome);
        Task<Aplicativo> ObterPorId(Guid idAplicativo);
        Task<Aplicativo> ObterPorIdComTracking(Guid idAplicativo);

        //Task<bool> VincularDesenvolvedor(Desenvolvedor desenvolvedor);
        //Task<bool> DesvincularDesenvolvedor(Desenvolvedor desenvolvedor);
        Task<IEnumerable<DesenvolvedorAplicativo>> ObterDesenvolvedoresRelacionados(Guid idAplicativo);
        Task<Aplicativo> ObterAplicativoResponsavel(Guid idDesenvolvedor);
    }
}