using backendtest.Domain.Domain.Entities;
using backendtest.Shared.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendtest.Domain.Application.DTOs;

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
        public async Task<IEnumerable<AplicativoDto>> ObterTodos()
        {
            var aplicativos = await _context.Aplicativos
                .Include(a=>a.Responsavel)
                .AsNoTracking()
                .ToListAsync();

            return aplicativos.Select(AplicativoDto.ParaAplicativoDto);
        }

        public async Task<AplicativoDto> ObterPorNome(string nome)
        {
            var aplicativo = await _context.Aplicativos.AsNoTracking()
                .Include(a => a.desenvolvedorAplicativo)
                .FirstOrDefaultAsync(a => a.Nome == nome);

            return AplicativoDto.ParaAplicativoDto(aplicativo);
        }

        public async Task<AplicativoDto> ObterPorId(Guid idAplicativo)
        {
            var aplicativo = await _context.Aplicativos.AsNoTracking()
                .Include(a => a.desenvolvedorAplicativo)
                .Include(a => a.Responsavel)
                .FirstOrDefaultAsync(a => a.Id == idAplicativo);

            return AplicativoDto.ParaAplicativoDto(aplicativo);
        }

        public async Task<Aplicativo> ObterPorIdComTracking(Guid idAplicativo)
        {
            return await _context.Aplicativos
                .Include(a => a.desenvolvedorAplicativo)
                .Include(a => a.Responsavel)
                .FirstOrDefaultAsync(a => a.Id == idAplicativo);
        }

        public async Task<IEnumerable<DesenvolvedorDto>> ObterDesenvolvedoresRelacionados(Guid idAplicativo)
        {
            var desenvolvedores = await _context.DesenvolvedorAplicativo
                .Where(d => d.FkAplicativo == idAplicativo)
                .Include(d => d.FkDesenvolvedorNavigation)
                .AsNoTracking()
                .ToListAsync();

            return desenvolvedores.Select(d=> DesenvolvedorDto.ParaDesenvolvedorDto(d.FkDesenvolvedorNavigation));
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

        public void VincularDesenvolvedor(Aplicativo aplicativo, Desenvolvedor desenvolvedor)
        {
            var vinculacao = new DesenvolvedorAplicativo(aplicativo, desenvolvedor);
            aplicativo.VincularDesenvolvedor(vinculacao);

            _context.Entry(vinculacao).State = EntityState.Added;
        }
        public async Task<bool> DesvincularDesenvolvedor(Aplicativo aplicativo, Desenvolvedor desenvolvedor)
        {
            var listaVinculacao = await _context.DesenvolvedorAplicativo
                .Where(a=>a.FkAplicativo == aplicativo.Id)
                .ToListAsync();

            var vinculacao = await _context.DesenvolvedorAplicativo
                .Where(a => a.FkAplicativo == aplicativo.Id && a.FkDesenvolvedor == desenvolvedor.Id)
                .FirstOrDefaultAsync();

            listaVinculacao.Remove(vinculacao);

            aplicativo.DesvincularDesenvolvedor(listaVinculacao);

            _context.Entry(vinculacao).State = EntityState.Deleted;
            return true;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}