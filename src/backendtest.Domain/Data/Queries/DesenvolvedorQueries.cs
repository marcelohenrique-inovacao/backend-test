using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backendtest.Domain.Application.DTOs;
using backendtest.Domain.Data.Repositories;
using Dapper;

namespace backendtest.Domain.Data.Queries
{
    public interface IDesenvolvedorQueries
    {
        Task<DesenvolvedorRelacaoAplicativosDto> ObterDesenvolvedorRelacaoAplicativos(Guid idDesenvolvedor);
    }

    public class DesenvolvedorQueries : IDesenvolvedorQueries
    {
        private readonly IDesenvolvedorRepository _desenvolvedorRepository;

        public DesenvolvedorQueries(IDesenvolvedorRepository desenvolvedorRepository)
        {
            _desenvolvedorRepository = desenvolvedorRepository;
        }

        public async Task<DesenvolvedorRelacaoAplicativosDto> ObterDesenvolvedorRelacaoAplicativos(Guid idDesenvolvedor)
        {
            const string sql = @"
                        Select D.Id IdDesenvolvedor, D.Nome NomeDesenvolvedor, A.Id IdAplicativoResponsavel, 
                        A.Nome NomeAplicativoResponsavel, Da.Id_Aplicativo IdAplicativoVinculado, Av.Nome NomeAplicativoVinculado
                        From Desenvolvedor D
                        Left Join Aplicativo A on (A.Id_DesenvolvedorResponsavel = D.Id)
                        Left Join DesenvolvedorAplicativo Da on (Da.Id_Desenvolvedor = D.Id)
                        Left Join Aplicativo Av on (Av.Id = Da.Id_Aplicativo)
                        Where D.Id = @idDesenvolvedor
                         ";
            var resultQuery = await _desenvolvedorRepository.ObterConexao()
                .QueryAsync<dynamic>(sql, new { idDesenvolvedor });

            return MapearDesenvolvedorRelacaoAplicativos(resultQuery);
        }

        private DesenvolvedorRelacaoAplicativosDto MapearDesenvolvedorRelacaoAplicativos(dynamic result)
        {
            var desenvolvedorRelacaoAplicativo = new DesenvolvedorRelacaoAplicativosDto
            {
                IdDesenvolvedor = result[0].IdDesenvolvedor,
                NomeDesenvolvedor = result[0].NomeDesenvolvedor,
            };

            if (result[0].IdAplicativoResponsavel != null)
            {
                desenvolvedorRelacaoAplicativo.AplicativoResponsavel = new AplicativoInfo
                {
                    IdAplicativo = result[0].IdAplicativoResponsavel,
                    NomeAplicativo = result[0].NomeAplicativoResponsavel
                };
            }

            if (result[0].IdAplicativoVinculado == null)
                return desenvolvedorRelacaoAplicativo;

            foreach (var item in result)
            {
                var aplicativoInfo = new AplicativoInfo
                {
                    IdAplicativo = item.IdAplicativoVinculado,
                    NomeAplicativo = item.NomeAplicativoVinculado
                };

                desenvolvedorRelacaoAplicativo.AplicativosVinculados.Add(aplicativoInfo);
            }


            return desenvolvedorRelacaoAplicativo;
        }
    }

}