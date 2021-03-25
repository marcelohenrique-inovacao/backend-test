using System;
using backendtest.Shared.DomainObjects;

namespace backendtest.Domain.Domain.Entities
{
    public partial class DesenvolvedorAplicativo : Entity
    {  
            public Guid FkDesenvolvedor { get; set; }
            public Guid FkAplicativo { get; set; }

            public virtual Aplicativo FkAplicativoNavigation { get; set; }
            public virtual Desenvolvedor FkDesenvolvedorNavigation { get; set; }

            protected DesenvolvedorAplicativo()
            {
                
            }
            public DesenvolvedorAplicativo(Aplicativo aplicativo, Desenvolvedor desenvolvedor)
            { 
                FkDesenvolvedor = desenvolvedor.Id;
                FkAplicativo = aplicativo.Id;
                FkAplicativoNavigation = aplicativo;
                FkDesenvolvedorNavigation = desenvolvedor;
            } 
    }
}