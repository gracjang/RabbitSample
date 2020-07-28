using System.Threading.Tasks;
using RabbitSample.Domain.Core.Bus;
using RabbitSample.Domain.Core.Events;
using RabbitSample.Transfer.Domain.Events;
using RabbitSample.Transfer.Domain.Interfaces;
using RabbitSample.Transfer.Domain.Models;

namespace RabbitSample.Transfer.Domain.Handlers
{
  public class TransferCreatedEventHandler : IEventHandler<TransferCreatedEvent>
  {
    private readonly ITransferRepository _transferRepository;

    public TransferCreatedEventHandler(ITransferRepository transferRepository)
    {
      _transferRepository = transferRepository;
    }

    public Task Handle(TransferCreatedEvent @event)
    {
      _transferRepository.Add(new TransferLog
      {
        FromAccount = @event.From, 
        ToAccount = @event.To, 
        TransferAmount = @event.Amount,
      });

      return Task.CompletedTask;
    }
  }
}