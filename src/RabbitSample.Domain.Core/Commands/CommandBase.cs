using System;
using MediatR;

namespace RabbitSample.Domain.Core.Commands
{
  public abstract class CommandBase : IRequest<bool>
  {
    public string MessageType => GetType().Name;

    public DateTime Timestamp => DateTime.Now;
  }
}