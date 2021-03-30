using FluentValidation.Results;

namespace backendtest.Shared.Communication
{
    public interface ICommandResult
    {
        void AddFluentValidation(ValidationResult validationResult);
        void AddResult(object obj);
        void AddErro(string campo, string mensagem);
    }
}