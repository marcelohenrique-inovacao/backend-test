using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }

        public async Task<bool> Excluir(Aplicativo aplicativo)
        {
            _context.Aplicativos.Remove(aplicativo);

            var sucesso = await _context.SaveChangesAsync();

            return sucesso > 0;
        }

        public async Task<Aplicativo> ObterPorNome(string nome)
        {
            return await _context.Aplicativos.AsNoTracking()
                .Include(a => a.desenvolvedorAplicativo)
                .Include(a => a.desenvolvedorAplicativo)
                .FirstOrDefaultAsync(a => a.Nome == nome);
        }

        public async Task<Aplicativo> ObterPorId(Guid idAplicativo)
        {
            return await _context.Aplicativos.AsNoTracking()
                .Include(a => a.desenvolvedorAplicativo)
                .Include(a => a.Responsavel)
                .FirstOrDefaultAsync(a => a.Id == idAplicativo);
        }

        public async Task<Aplicativo> ObterPorIdComTracking(Guid idAplicativo)
        {
            return await _context.Aplicativos
                .Include(a => a.desenvolvedorAplicativo)
                .Include(a=> a.Responsavel)
                .FirstOrDefaultAsync(a => a.Id == idAplicativo);
        }

        public async Task<IEnumerable<DesenvolvedorAplicativo>> ObterDesenvolvedoresRelacionados(Guid idAplicativo)
        {
            return await _context.DesenvolvedorAplicativo
                .Where(d => d.FkAplicativo == idAplicativo)
                .Include(d => d.FkDesenvolvedorNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Aplicativo> ObterAplicativoResponsavel(Guid idDesenvolvedor)
        {
            if (idDesenvolvedor == Guid.Empty) return null;

            var aplicativo = await _context.Aplicativos.AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdDesenvolvedorResponsavel == idDesenvolvedor);

            return aplicativo != null ? aplicativo : null;
        }

        public async Task<bool> PermiteVincularDesenvolvedor(Guid idDesenvolvedor)
        {
            if (idDesenvolvedor == Guid.Empty) return false;

            var quantidadeAplicativosVinculado =
                await _context.DesenvolvedorAplicativo.AsNoTracking()
                    .Where(d => d.FkDesenvolvedor == idDesenvolvedor)
                    .CountAsync();
            return quantidadeAplicativosVinculado < 3;
        }

        public async Task<IEnumerable<Aplicativo>> ObterTodos()
        {
            return await _context.Aplicativos.AsNoTracking()
                .ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}