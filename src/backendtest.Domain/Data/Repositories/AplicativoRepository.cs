using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace backendtest.Domain.Data.Repositories
{
    public class AplicativoRepository : IAplicativoRepository
    {
        private readonly DatabaseContext _context;

        public AplicativoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(Aplicativo aplicativo)
        {
            _context.Aplicativos.Add(aplicativo);
        }

        public void Update(Aplicativo aplicativo)
        {
            _context.Entry(aplicativo).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<Aplicativo> ObterPorNome(string nome)
        {
            return await _context.Aplicativos.Include(a => a.Desenvolvedores).FirstOrDefaultAsync(a => a.Nome == nome);
        }

        public async Task<Aplicativo> ObterPorId(Guid id)
        {
            return await _context.Aplicativos.Include(a => a.Desenvolvedores).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Aplicativo>> ObterTodos()
        {
            return await _context.Aplicativos.Include(a => a.Desenvolvedores).ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}