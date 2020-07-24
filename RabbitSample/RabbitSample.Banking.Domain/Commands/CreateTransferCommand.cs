using RabbitSample.Domain.Core.Commands;

namespace RabbitSample.Banking.Domain.Commands
{
  public class CreateTransferCommand : CommandBase
  {
    public int From { get; set; }

    public int To { get; set; }

    public decimal Amount { get; set; }

  }
}