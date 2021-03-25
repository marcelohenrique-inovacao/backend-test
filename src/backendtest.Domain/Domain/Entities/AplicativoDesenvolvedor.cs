using System;

namespace backendtest.Domain.Domain.Entities
{
    public class AplicativoDesenvolvedor
    {
        public Desenvolvedor Desenvolvedor { get; set; }
        public Aplicativo Aplicativo { get; set; }
        public Guid IdDesenvolvedor { get; set; }
        public Guid IdAplicativo { get; set; }

        public AplicativoDesenvolvedor(Desenvolvedor desenvolvedor, Aplicativo aplicativo, Guid idDesenvolvedor, Guid idAplicativo)
        {
            Desenvolvedor = desenvolvedor;
            Aplicativo = aplicativo;
            IdDesenvolvedor = idDesenvolvedor;
            IdAplicativo = idAplicativo;
        }

        protected AplicativoDesenvolvedor()
        {
            
        }
    }
}