using System.Collections.Generic;
using RabbitSample.Banking.Application.Models;
using RabbitSample.Banking.Domain.Models;

namespace RabbitSample.Banking.Application.Services.Interfaces
{
  public interface IAccountService
  {
    IEnumerable<Account> GetAccounts();

    void Transfer(AccountTransfer accountTransfer);
  }
}