using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RabbitSample.Banking.Domain.Commands;
using RabbitSample.Banking.Domain.Events;
using RabbitSample.Domain.Core.Bus;

namespace RabbitSample.Banking.Domain.Handlers
{
  public class CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
  {
    private readonly IBus _bus;

    public CreateTransferCommandHandler(IBus bus)
    {
      _bus = bus;
    }

    public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
    {
      _bus.Publish(new TransferCreatedEvent(request.From, request.To, request.Amount));
      return Task.FromResult(true);
    }
  }
}