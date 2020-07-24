using RabbitSample.Domain.Core.Events;

namespace RabbitSample.Banking.Domain.Events
{
  public class TransferCreatedEvent : EventBase
  {
    public int From { get; private set; }

    public int To { get; private set; }

    public decimal Amount { get; private set; }

    public TransferCreatedEvent(int from, int to, decimal amount)
    {
      From = from;
      To = to;
      Amount = amount;
    }
  }
}