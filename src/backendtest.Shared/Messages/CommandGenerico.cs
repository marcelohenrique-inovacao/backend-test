using System;
using backendtest.Shared.Communication;
using FluentValidation.Results;
using MediatR;

namespace backendtest.Shared.Messages
{
    public abstract class CommandGenerico : Message, IRequest<ICommandResult>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; } 

        protected CommandGenerico()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool Valido()
        {
            throw new NotImplementedException();
        }
    }
}