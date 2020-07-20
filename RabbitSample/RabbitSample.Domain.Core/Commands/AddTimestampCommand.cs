using System;
using MediatR;

namespace RabbitSample.Domain.Core.Commands
{
  public class AddTimestampCommand : CommandBase
  {
    public DateTime Timestamp => DateTime.Now;
  }
}