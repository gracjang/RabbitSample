using System;
using MediatR;
using RabbitSample.Domain.Core.Commands;

namespace RabbitSample.Domain.Core.Events
{
  public abstract class EventBase : IRequest<bool>
  {
    public string MessageType => GetType().Name;
    public DateTime Timestamp => DateTime.Now;
  }
}