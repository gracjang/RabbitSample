using Microsoft.EntityFrameworkCore;
using RabbitSample.Banking.Domain.Models;

namespace RabbitSimple.Banking.Data.Context
{
  public class BankingDbContext : DbContext
  {
    public BankingDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
  }
}