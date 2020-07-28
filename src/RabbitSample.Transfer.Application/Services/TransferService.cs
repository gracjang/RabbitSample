using System.Collections.Generic;
using RabbitSample.Transfer.Application.Services.Interfaces;
using RabbitSample.Transfer.Domain.Models;
using RabbitSample.Transfer.Domain.Interfaces;

namespace RabbitSample.Transfer.Application.Services
{
  public class TransferService : ITransferService
  {
    private readonly ITransferRepository _transferRepository;
    

    public TransferService(ITransferRepository transferRepository)
    {
      _transferRepository = transferRepository;
    }

    public IEnumerable<TransferLog> GeTransferLogs()
    {
      return _transferRepository.GetTransferLogs();
    }
  }
}