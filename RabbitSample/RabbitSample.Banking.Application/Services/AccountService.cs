using System.Collections.Generic;
using RabbitSample.Banking.Application.Services.Interfaces;
using RabbitSample.Banking.Domain.Models;
using RabbitSample.Banking.Domain.Repositories.Interfaces;

namespace RabbitSample.Banking.Application.Services
{
  public class AccountService : IAccountService
  {
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
      _accountRepository = accountRepository;
    }

    public IEnumerable<Account> GetAccounts() => _accountRepository.Get();
  }
}