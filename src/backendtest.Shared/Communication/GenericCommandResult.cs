using System.Collections.Generic;
using FluentValidation.Results;
using MediatR;

namespace backendtest.Shared.Communication
{
    public sealed class GenericCommandResult : ICommandResult
    {
        public string Status;
        public object Result { get; set; }
        public readonly List<Validacao> Erros = new();

        public GenericCommandResult()
        {
        }

        public GenericCommandResult(object result, List<Validacao> erros)
        {
            Result = result;
            Erros = erros;
            Status = SetStatus();
        }

        internal string SetStatus()
        {
            return Erros.Count > 0 ? "400 Bad Request" : "200 Ok";
        }

        public void AddFluentValidation(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
            {
                AddErro(item.PropertyName, item.ErrorMessage);
            }

            Status = SetStatus();
        }

        public void AddResult(object obj)
        {
            Result = obj;
        }

        public void AddErro(string campo, string mensagem)
        {

            Erros.Add(new Validacao(campo, mensagem));
        }

        public class Validacao
        {
            public string Campo;
            public string Erro;

            public Validacao(string campo, string erro)
            {
                Campo = campo;
                Erro = erro;
            }
        }

    }
}
