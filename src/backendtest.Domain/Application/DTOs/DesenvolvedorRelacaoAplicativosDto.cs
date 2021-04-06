using System;
using System.Collections.Generic;

namespace backendtest.Domain.Application.DTOs
{
    public class DesenvolvedorRelacaoAplicativosDto
    {
        public Guid IdDesenvolvedor { get; set; }
        public string NomeDesenvolvedor { get; set; }
        public AplicativoInfo AplicativoResponsavel { get; set; }
        public List<AplicativoInfo> AplicativosVinculados { get; set; }

        public DesenvolvedorRelacaoAplicativosDto()
        {
            AplicativosVinculados = new List<AplicativoInfo>();
        }
    }

    public class AplicativoInfo
    {
        public Guid IdAplicativo { get; set; }
        public string NomeAplicativo { get; set; }
    }

}