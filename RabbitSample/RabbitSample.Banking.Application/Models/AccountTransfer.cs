namespace RabbitSample.Banking.Application.Models
{
  public class AccountTransfer
  {
    public int AccountSource { get; set; }

    public int AccountDestination { get; set; }

    public decimal TransferAmount { get; set; }

  }
}