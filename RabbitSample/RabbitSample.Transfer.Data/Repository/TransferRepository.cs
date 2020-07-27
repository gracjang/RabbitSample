using System.Collections.Generic;
using RabbitSample.Transfer.Data.Context;
using RabbitSample.Transfer.Domain.Interfaces;
using RabbitSample.Transfer.Domain.Models;

namespace RabbitSample.Transfer.Data.Repository
{
  public class TransferRepository : ITransferRepository
  {
    private readonly TransferDbContext _transferDbContext;

    public TransferRepository(TransferDbContext transferDbContext)
    {
      _transferDbContext = transferDbContext;
    }

    public IEnumerable<TransferLog> GetTransferLogs()
      => _transferDbContext.TransferLogs;
  }
}