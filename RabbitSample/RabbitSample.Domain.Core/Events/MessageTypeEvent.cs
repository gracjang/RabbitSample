using System;
using MediatR;

namespace RabbitSample.Domain.Core.Events
{
  public class MessageTypeEvent : IEvent
  {
    public DateTime Timestamp => DateTime.Now;

    public string MessageType => GetType().Name;
  }
}