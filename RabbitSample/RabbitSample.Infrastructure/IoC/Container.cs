using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RabbitSample.Banking.Application.Services;
using RabbitSample.Banking.Application.Services.Interfaces;
using RabbitSample.Banking.Domain.Commands;
using RabbitSample.Banking.Domain.Handlers;
using RabbitSample.Banking.Domain.Repositories.Interfaces;
using RabbitSample.Domain.Core.Bus;
using RabbitSimple.Banking.Data.Context;
using RabbitSimple.Banking.Data.Repository;

namespace RabbitSample.Infrastructure.IoC
{
  public static class Container
  {
    public static void RegisterServices(this IServiceCollection services)
    {
      services.AddTransient<IBus, RabbitMQBus>();
      services.AddTransient<IAccountService, AccountService>();
      services.AddTransient<IAccountRepository, AccountRepository>();
      services.AddTransient<BankingDbContext>();
      services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, CreateTransferCommandHandler>();
    }
  }
}