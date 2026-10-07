using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Application.Events
{
    public class ClienteRegistradoEventHandler : INotificationHandler<ClienteRegistradoEvent>
    {
        public Task Handle(ClienteRegistradoEvent notification, CancellationToken cancellationToken)
        {
            // Enviar evento de confirmação
            return Task.CompletedTask;
        }
    }
}
