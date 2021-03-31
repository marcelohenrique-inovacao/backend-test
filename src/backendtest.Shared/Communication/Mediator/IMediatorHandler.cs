using System.Threading.Tasks;
using backendtest.Shared.Messages;
using backendtest.Shared.Messages.CommonMessages.DomainEvents;
using backendtest.Shared.Messages.CommonMessages.Notifications;
using FluentValidation.Results;

namespace backendtest.Shared.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        //Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
        Task<ICommandResult> EnviarComandoGenerico<T>(T comando) where T : CommandGenerico;
        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
        Task PublicarDomainEvent<T>(T notificacao) where T : DomainEvent;
    }
}