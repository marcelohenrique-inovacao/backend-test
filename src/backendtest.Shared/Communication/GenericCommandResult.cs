using System.Collections.Generic;
using FluentValidation.Results;
using MediatR;

namespace backendtest.Shared.Communication
{
    public sealed class GenericCommandResult : ICommandResult
    {
        public string Status { get; set; }
        public List<object> Result { get; set; } = new();
        public List<Validacao> Erros { get; set; } = new();

        public GenericCommandResult()
        {
            Status = SetStatus();
        }

        public GenericCommandResult(List<object> result, List<Validacao> erros)
        {
            Result = result;
            Erros = erros;
            Status = SetStatus();
        }

        internal string SetStatus()
        {
            if (Erros.Count > 0)
                return "400 Bad Request";

            return Result.Count > 0 ? "200 Ok" : "404 Not Found";
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
            if (obj != null)
                Result.Add(obj);

            Status = SetStatus();
        }

        public void AddErro(string campo, string mensagem)
        {
            Erros.Add(new Validacao(campo, mensagem));
            Status = SetStatus();
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
