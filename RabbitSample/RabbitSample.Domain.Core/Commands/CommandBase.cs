using MediatR;

namespace RabbitSample.Domain.Core.Commands
{
  public abstract class CommandBase : ICommand, IRequest<bool>
  {
    public string MessageType => GetType().Name;
  }
}