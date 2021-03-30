using System;
using System.Reflection.Metadata.Ecma335;
using backendtest.Domain.Domain.Entities;

namespace backendtest.Domain.Application.DTOs
{
    public class DesenvolvedorDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }


        public static DesenvolvedorDto ParaDesenvolvedorDto(Desenvolvedor desenvolvedor)
        {
            if (desenvolvedor == null) return null;

            return new DesenvolvedorDto
            {
                Id = desenvolvedor.Id,
                Nome = desenvolvedor.Nome,
                Cpf = desenvolvedor.Cpf.Numero,
                Email = desenvolvedor.Email.Endereco
            };
        }
    }
}