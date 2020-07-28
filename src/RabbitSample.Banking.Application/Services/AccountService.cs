using System.Collections.Generic;
using RabbitSample.Banking.Application.Models;
using RabbitSample.Banking.Application.Services.Interfaces;
using RabbitSample.Banking.Domain.Commands;
using RabbitSample.Banking.Domain.Models;
using RabbitSample.Banking.Domain.Repositories.Interfaces;
using RabbitSample.Domain.Core.Bus;

namespace RabbitSample.Banking.Application.Services
{
  public class AccountService : IAccountService
  {
    private readonly IAccountRepository _accountRepository;
    private readonly IBus _bus;

    public AccountService(IAccountRepository accountRepository, IBus bus)
    {
      _accountRepository = accountRepository;
      _bus = bus;
    }

    public IEnumerable<Account> GetAccounts() => _accountRepository.Get();
    public void Transfer(AccountTransfer accountTransfer)
    {
      var command = new CreateTransferCommand
      {
        From = accountTransfer.AccountSource,
        To = accountTransfer.AccountDestination,
        Amount = accountTransfer.TransferAmount
      };

      _bus.SendCommand(command);
    }
  }
}