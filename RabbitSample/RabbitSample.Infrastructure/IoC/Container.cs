using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RabbitSample.Banking.Application.Services;
using RabbitSample.Banking.Application.Services.Interfaces;
using RabbitSample.Banking.Domain.Commands;
using RabbitSample.Banking.Domain.Handlers;
using RabbitSample.Banking.Domain.Repositories.Interfaces;
using RabbitSample.Domain.Core.Bus;
using RabbitSample.Transfer.Application.Services;
using RabbitSample.Transfer.Application.Services.Interfaces;
using RabbitSample.Transfer.Data.Context;
using RabbitSample.Transfer.Data.Repository;
using RabbitSample.Transfer.Domain.Events;
using RabbitSample.Transfer.Domain.Handlers;
using RabbitSample.Transfer.Domain.Interfaces;
using RabbitSimple.Banking.Data.Context;
using RabbitSimple.Banking.Data.Repository;

namespace RabbitSample.Infrastructure.IoC
{
  public static class Container
  {
    public static void RegisterServices(this IServiceCollection services)
    {
      services.AddSingleton<IBus, RabbitMQBus>(sp =>
      {
        var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();

        return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);
      });
      services.AddTransient<TransferCreatedEventHandler>();

      services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, CreateTransferCommandHandler>();

      services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferCreatedEventHandler>();

      services.AddTransient<ITransferService, TransferService>();
      services.AddTransient<IAccountService, AccountService>();

      services.AddTransient<ITransferRepository, TransferRepository>();
      services.AddTransient<IAccountRepository, AccountRepository>();

      services.AddTransient<TransferDbContext>();
      services.AddTransient<BankingDbContext>();
    }
  }
}