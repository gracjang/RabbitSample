using System.Collections.Generic;
using RabbitSample.Banking.Domain.Models;
using RabbitSample.Banking.Domain.Repositories.Interfaces;
using RabbitSimple.Banking.Data.Context;

namespace RabbitSimple.Banking.Data.Repository
{
  public class AccountRepository : IAccountRepository
  {
    private readonly BankingDbContext _bankingDbContext;

    public AccountRepository(BankingDbContext bankingDbContext)
    {
      _bankingDbContext = bankingDbContext;
    }

    public IEnumerable<Account> Get() => _bankingDbContext.Accounts;
  }
}