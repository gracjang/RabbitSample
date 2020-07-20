using System.Threading.Tasks;
using RabbitSample.Domain.Core.Events;

namespace RabbitSample.Domain.Core.Bus
{
  public interface IEventHandler<in TEvent> where TEvent : IEvent
  {
    Task Handle(TEvent @event);
  }
}