using System.Collections.Generic;

namespace backendtest.Shared.Communication
{
    public class GenericCommandResult
    {
        public string Status { get; set; }

        public object Result { get; set; }
        public List<Validacoes> Erros { get; set; }

        public GenericCommandResult()
        {
        }

        public GenericCommandResult(string status, object result, List<Validacoes> erros)
        {
            Status = status;
            Result = result;
            Erros = erros;
        }

        public class Validacoes
        {
            public string campo;
            public string erro;
        }

    }
}
