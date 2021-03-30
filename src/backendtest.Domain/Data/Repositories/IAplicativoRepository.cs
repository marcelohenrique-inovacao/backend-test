using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backendtest.Domain.Application.DTOs;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Data;

namespace backendtest.Domain.Data.Repositories
{
    public interface IAplicativoRepository : IRepository<Aplicativo>
    {
        void Adicionar(Aplicativo aplicativo);
        void Update(Aplicativo aplicativo);
        Task<bool> Excluir(Aplicativo aplicativo);

        Task<IEnumerable<AplicativoDto>> ObterTodos();
        Task<AplicativoDto> ObterPorNome(string nome);
        Task<AplicativoDto> ObterPorId(Guid idAplicativo);
        Task<Aplicativo> ObterPorIdComTracking(Guid idAplicativo);
        Task<IEnumerable<DesenvolvedorDto>> ObterDesenvolvedoresRelacionados(Guid idAplicativo);
        Task<Aplicativo> ObterAplicativoResponsavel(Guid idDesenvolvedor);
        Task<bool> PermiteVincularDesenvolvedor(Guid idDesenvolvedor);
        void VincularDesenvolvedor(Aplicativo aplicativo, Desenvolvedor desenvolvedor);
        Task<bool> DesvincularDesenvolvedor(Aplicativo aplicativo, Desenvolvedor desenvolvedor);
    }
}