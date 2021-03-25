using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Data;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Desenvolvedores.AsNoTracking().FirstOrDefaultAsync(d => d.Cpf.Numero == cpf);
        }

        public async Task<Desenvolvedor> ObterPorId(Guid id)
        {
            return await _context.Set<Desenvolvedor>() 
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id); 
        }

        public async Task<IEnumerable<Desenvolvedor>> ObterTodos()
        {
            return await _context.Desenvolvedores.AsNoTracking().ToListAsync();
        } 

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}