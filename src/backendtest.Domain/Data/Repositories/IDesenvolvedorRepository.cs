using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using backendtest.Domain.Application.DTOs;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Data;

namespace backendtest.Domain.Data.Repositories
{
    public interface IDesenvolvedorRepository : IRepository<Desenvolvedor>
    {
        void Adicionar(Desenvolvedor desenvolvedor);
        void Update(Desenvolvedor desenvolvedor);
        Task<bool> Excluir(Desenvolvedor desenvolvedor);
        Task<IEnumerable<DesenvolvedorDto>> ObterTodos();
        Task<DesenvolvedorDto> ObterPorCpf(string cpf);
        Task<DesenvolvedorDto> ObterPorId(Guid idDesenvolvedor);
        Task<Desenvolvedor> ObterPorIdComTracking(Guid id);
        //REVIEW: Retornar AplicativoDTO
        Task<IEnumerable<AplicativoDto>> ObterAplicativosRelacionados(Guid idDesenvolvedor);

        DbConnection ObterConexao();
    }
}