using MediatR;
using RabbitSample.Domain.Core.Commands;

namespace RabbitSample.Domain.Core.Events
{
  public abstract class EventBase : ICommand, IRequest<bool>
  {
    public string MessageType => GetType().Name;
  }
}