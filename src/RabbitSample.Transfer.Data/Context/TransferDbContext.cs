using Microsoft.EntityFrameworkCore;
using RabbitSample.Transfer.Domain.Models;

namespace RabbitSample.Transfer.Data.Context
{
  public class TransferDbContext : DbContext
  {
    public TransferDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<TransferLog> TransferLogs { get; set; }
  }
}