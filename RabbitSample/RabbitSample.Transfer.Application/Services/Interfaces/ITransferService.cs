using System.Collections.Generic;
using RabbitSample.Transfer.Domain.Models;

namespace RabbitSample.Transfer.Application.Services.Interfaces
{
  public interface ITransferService
  {
    IEnumerable<TransferLog> GeTransferLogs();
  }
}