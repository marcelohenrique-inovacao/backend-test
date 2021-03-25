using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Data;

namespace backendtest.Domain.Data.Repositories
{
    public interface IDesenvolvedorRepository : IRepository<Desenvolvedor>
    {
        void Adicionar(Desenvolvedor desenvolvedor);
        void Update(Desenvolvedor desenvolvedor);
        Task<bool> Excluir(Desenvolvedor desenvolvedor);
        Task<IEnumerable<Desenvolvedor>> ObterTodos();
        Task<Desenvolvedor> ObterPorCpf(string cpf);
        Task<Desenvolvedor> ObterPorId(Guid idDesenvolvedor);
        Task<Desenvolvedor> ObterPorIdComTracking(Guid id);
        Task<IEnumerable<DesenvolvedorAplicativo>> ObterAplicativosRelacionados(Guid idDesenvolvedor);
    }
}