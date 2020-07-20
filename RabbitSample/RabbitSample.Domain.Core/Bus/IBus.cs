using System.ComponentModel.Design;
using System.Threading.Tasks;
using System.Windows.Input;
using MediatR;
using RabbitSample.Domain.Core.Events;

namespace RabbitSample.Domain.Core.Bus
{
  public interface IBus
  {
    Task SendCommand<T>(T command) where T : ICommand;

    void Publish<TEvent>(TEvent @event) where TEvent : IEvent;

    void Subscribe<TEvent, TEventHandler>() where TEvent : IEvent
      where TEventHandler : IEventHandler<TEvent>;
  }
}