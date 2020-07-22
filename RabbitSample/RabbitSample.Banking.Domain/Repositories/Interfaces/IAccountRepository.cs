using System.Collections.Generic;
using RabbitSample.Banking.Domain.Models;

namespace RabbitSample.Banking.Domain.Repositories.Interfaces
{
  public interface IAccountRepository
  {
    IEnumerable<Account> Get();
  }
}