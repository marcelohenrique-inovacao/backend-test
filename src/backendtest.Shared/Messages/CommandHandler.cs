using System;
using System.Threading.Tasks;
using backendtest.Shared.Data;
using FluentValidation.Results;

namespace backendtest.Shared.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AdicionarErro(string propriedade, string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(propriedade, mensagem));
        }

        protected async Task<ValidationResult> PersistirDados(IUnitOfWork uow)
        { 
            if (!await uow.Commit()) AdicionarErro("Banco de Dados","Houve um erro ao persistir os dados");

            return ValidationResult;
        }
    }
}