using MediatR;

namespace RabbitSample.Domain.Core.Events
{
  public interface IEvent : IRequest<bool>
  {
  }
}