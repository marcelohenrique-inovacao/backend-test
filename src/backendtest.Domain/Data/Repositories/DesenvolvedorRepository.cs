using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendtest.Domain.Domain.Entities;
using backendtest.Domain.Domain.ValueObjects;
using backendtest.Shared.Data;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace backendtest.Domain.Data.Repositories
{
    public class DesenvolvedorRepository : IDesenvolvedorRepository
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
            _context.Entry(desenvolvedor).State = EntityState.Modified;
        }

        public async Task<Desenvolvedor> ObterPorCpf(string cpf)
        {
            return await _context.Desenvolvedores
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Cpf.Numero == cpf);
        }

        public async Task<Desenvolvedor> ObterPorId(Guid idDesenvolvedor)
        {
            return await _context.Set<Desenvolvedor>()
                .Include(d => d.Aplicativo)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == idDesenvolvedor);
        }
        public async Task<Desenvolvedor> ObterPorIdComTracking(Guid idDesenvolvedor)
        {
            return await _context.Desenvolvedores
                .Include(d=>d.desenvolvedorAplicativo)
                .Include(d=>d.Cpf)
                .Include(d=> d.Email)
                .FirstOrDefaultAsync(d => d.Id == idDesenvolvedor);
        }

        public async Task<IEnumerable<Desenvolvedor>> ObterTodos()
        {
            return await _context.Desenvolvedores
                .Include(d=> d.Aplicativo)
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<DesenvolvedorAplicativo>> ObterAplicativosRelacionados(Guid idDesenvolvedor)
        {
            return await _context.DesenvolvedorAplicativo
                .Where(d=> d.FkDesenvolvedor == idDesenvolvedor)
                .Include(d=> d.FkAplicativoNavigation)
                .AsNoTracking()
                .ToListAsync();
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