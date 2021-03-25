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
        }

        public Task<bool> Excluir(Aplicativo aplicativo)
        {
            throw new NotImplementedException();
        }

        public async Task<Aplicativo> ObterPorNome(string nome)
        {
            return await _context.Aplicativos.AsNoTracking()
                .Include(a => a.desenvolvedorAplicativo).FirstOrDefaultAsync(a => a.Nome == nome);
        }

        public async Task<Aplicativo> ObterPorId(Guid idAplicativo)
        {
            return await _context.Aplicativos.AsNoTracking()
                .Include(a => a.desenvolvedorAplicativo)
                .FirstOrDefaultAsync(a => a.Id == idAplicativo);
        }

        public Task<Aplicativo> ObterPorIdComTracking(Guid idAplicativo)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DesenvolvedorAplicativo>> ObterDesenvolvedoresRelacionados(Guid idAplicativo)
        {
            return await _context.DesenvolvedorAplicativo
                .Where(d=> d.FkAplicativo == idAplicativo)
                .Include(d=> d.FkDesenvolvedorNavigation)
                .AsNoTracking()
                .ToListAsync();
        } 

        //public async Task<bool> VincularDesenvolvedor(Guid aplicativoId, Desenvolvedor desenvolvedor)
        //{ 
        //    var aplicativo = await ObterPorId(aplicativoId); 
        //    aplicativo.VincularDesenvolvedor(desenvolvedor);
        //}

        //public Task<bool> DesvincularDesenvolvedor(Desenvolvedor desenvolvedor)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<Aplicativo> ObterAplicativoResponsavel(Guid idDesenvolvedor)
        {
            throw new NotImplementedException();

            //if (idAplicativo == Guid.Empty) return ;

            //var aplicativo = await _context.Aplicativos.AsNoTracking()
            //    .FirstOrDefaultAsync(a => a.IdDesenvolvedorResponsavel == idAplicativo);

            //if (aplicativo != null)
            //    return true;

            //return false;
        }

        public async Task<IEnumerable<Aplicativo>> ObterTodos()
        {
            return await _context.Aplicativos.AsNoTracking()
                .Include(a => a.desenvolvedorAplicativo).ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}