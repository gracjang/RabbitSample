using System.Threading.Tasks;
using RabbitSample.Domain.Core.Bus;
using RabbitSample.Domain.Core.Events;
using RabbitSample.Transfer.Domain.Events;

namespace RabbitSample.Transfer.Domain.Handlers
{
  public class TransferCreatedEventHandler : IEventHandler<TransferCreatedEvent>
  {
    public Task Handle(TransferCreatedEvent @event)
    {
      return Task.CompletedTask;
    }
  }
}