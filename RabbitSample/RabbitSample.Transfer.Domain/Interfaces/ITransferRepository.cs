using System.Collections.Generic;
using RabbitSample.Transfer.Domain.Models;

namespace RabbitSample.Transfer.Domain.Interfaces
{
  public interface ITransferRepository
  {
    IEnumerable<TransferLog> GetTransferLogs();
  }
}