using System.Collections.Generic;
using FluentValidation.Results;

namespace backendtest.Shared.Communication
{
    public interface ICommandResult
    {
        string Status { get; set; }
        public List<object> Result { get; set; }
        public List<GenericCommandResult.Validacao> Erros { get; set; }
        void AddFluentValidation(ValidationResult validationResult);
        void AddResult(object obj);
        void AddErro(string campo, string mensagem);
    }
}