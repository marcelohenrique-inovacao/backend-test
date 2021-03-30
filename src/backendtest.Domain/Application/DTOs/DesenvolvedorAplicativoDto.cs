using System;
using System.Collections.Generic;
using backendtest.Domain.Domain.Entities;

namespace backendtest.Domain.Application.DTOs
{
    public class DesenvolvedorAplicativoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataLancamento { get; set; }
        public string Plataforma { get; set; }
        public Guid? IdResponsavel { get; set; }
        public string NomeResponsavel { get; set; }

        public List<DesenvolvedorDto> Desenvolvedores { get; set; }

        public static DesenvolvedorAplicativoDto ParaDesenvolvedorAplicativoDto(Aplicativo aplicativo)
        {
            var desenvolvedorAplicativoDto = new DesenvolvedorAplicativoDto
            {
                Id = aplicativo.Id,
                Nome = aplicativo.Nome,
                DataLancamento = aplicativo.DataLancamento,
                Plataforma = aplicativo.Plataforma.ToString(),
                IdResponsavel = aplicativo.IdDesenvolvedorResponsavel,
                NomeResponsavel = aplicativo.Responsavel.Nome
            };

            foreach (var item in aplicativo.desenvolvedorAplicativo)
            {
                desenvolvedorAplicativoDto.Desenvolvedores.Add(new DesenvolvedorDto
                {
                    Id = item.FkDesenvolvedorNavigation.Id,
                    Nome = item.FkDesenvolvedorNavigation.Nome,
                    Cpf = item.FkDesenvolvedorNavigation.Cpf.Numero,
                    Email = item.FkDesenvolvedorNavigation.Email.Endereco
                });
            }

            return desenvolvedorAplicativoDto;
        }
    }
}