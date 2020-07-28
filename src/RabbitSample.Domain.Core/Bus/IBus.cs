using System.Threading.Tasks;
using RabbitSample.Domain.Core.Commands;
using RabbitSample.Domain.Core.Events;

namespace RabbitSample.Domain.Core.Bus
{
  public interface IBus
  {
    Task SendCommand<T>(T command) where T : CommandBase;

    void Publish<TEvent>(TEvent @event) where TEvent : EventBase;

    void Subscribe<TEvent, TEventHandler>() 
      where TEvent : EventBase
      where TEventHandler : IEventHandler<TEvent>;
  }
}