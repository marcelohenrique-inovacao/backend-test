using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Data;
using backendtest.Shared.DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendtest.Domain.Application.DTOs;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace backendtest.Domain.Data.Repositories
{
    public class DesenvolvedorRepository : IDesenvolvedorRepository, IAggregateRoot
    {
        private readonly DatabaseContext _context;

        public DesenvolvedorRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(Desenvolvedor desenvolvedor)
        {
            _context.Desenvolvedores.Add(desenvolvedor);
        }

        public void Update(Desenvolvedor desenvolvedor)
        {
            //REVIEW: Modificar para as alterações de CPF e EMAIL passar pela camado do repositório para somente setar a entidade correta como modificada.
            _context.Entry(desenvolvedor).State = EntityState.Modified;
            _context.Entry(desenvolvedor.Cpf).State = EntityState.Modified;
            _context.Entry(desenvolvedor.Email).State = EntityState.Modified;
        }

        public async Task<DesenvolvedorDto> ObterPorCpf(string cpf)
        {
            var desenvolvedor = await _context.Desenvolvedores
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Cpf.Numero == cpf);

            return DesenvolvedorDto.ParaDesenvolvedorDto(desenvolvedor);
        }

        public async Task<DesenvolvedorDto> ObterPorId(Guid idDesenvolvedor)
        {
            var desenvolvedor = await _context.Set<Desenvolvedor>()
                .Include(d => d.Aplicativo)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == idDesenvolvedor);

            return DesenvolvedorDto.ParaDesenvolvedorDto(desenvolvedor);
        }
        public async Task<Desenvolvedor> ObterPorIdComTracking(Guid idDesenvolvedor)
        {
            return await _context.Desenvolvedores
                .Include(d => d.desenvolvedorAplicativo)
                .Include(d => d.Cpf)
                .Include(d => d.Email)
                .FirstOrDefaultAsync(d => d.Id == idDesenvolvedor);
        }

        public async Task<IEnumerable<DesenvolvedorDto>> ObterTodos()
        {
            var desenvolvedores = await _context.Desenvolvedores
                .Include(d => d.Aplicativo)
                .AsNoTracking().ToListAsync();
            return desenvolvedores.Select(DesenvolvedorDto.ParaDesenvolvedorDto);
        }

        public async Task<IEnumerable<AplicativoDto>> ObterAplicativosRelacionados(Guid idDesenvolvedor)
        {
            var desenvolvedorAplicativos = await _context.DesenvolvedorAplicativo
                .Where(d => d.FkDesenvolvedor == idDesenvolvedor)
                .Include(d => d.FkAplicativoNavigation)
                .Include(d => d.FkAplicativoNavigation.Responsavel)
                .AsNoTracking()
                .ToListAsync();

            return desenvolvedorAplicativos.Select(x => AplicativoDto.ParaAplicativoDto(x.FkAplicativoNavigation));
        }

        public async Task<bool> Excluir(Desenvolvedor desenvolvedor)
        {
            desenvolvedor.RemoverCpf();
            desenvolvedor.RemoverEmail();
            _context.Desenvolvedores.RemoveRange(desenvolvedor);

            var sucesso = await _context.SaveChangesAsync();

            return sucesso > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}