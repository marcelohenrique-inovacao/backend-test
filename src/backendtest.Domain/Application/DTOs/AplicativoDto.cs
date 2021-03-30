using System;
using backendtest.Domain.Domain.Entities;

namespace backendtest.Domain.Application.DTOs
{
    public class AplicativoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataLancamento { get; set; }
        public string Plataforma { get; set; }
        public Guid? IdResponsavel { get; set; }
        public string NomeResponsavel { get; set; }

        public static AplicativoDto ParaAplicativoDto(Aplicativo aplicativo)
        {
            return new AplicativoDto
            {
                Id = aplicativo.Id,
                Nome = aplicativo.Nome,
                DataLancamento = aplicativo.DataLancamento,
                Plataforma = aplicativo.Plataforma.ToString(),
                IdResponsavel = aplicativo.IdDesenvolvedorResponsavel,
                NomeResponsavel = aplicativo.Responsavel?.Nome 
            };
        }
    }
}