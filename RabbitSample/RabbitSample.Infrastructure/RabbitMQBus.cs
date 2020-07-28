using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitSample.Domain.Core.Bus;
using RabbitSample.Domain.Core.Commands;
using RabbitSample.Domain.Core.Events;

namespace RabbitSample.Infrastructure
{
  public sealed class RabbitMQBus : IBus
  {
    private readonly IMediator _mediator;
    private readonly Dictionary<string, List<Type>> _handlers;
    private readonly List<Type> _eventTypes;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RabbitMQBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory)
    {
      _mediator = mediator;
      _serviceScopeFactory = serviceScopeFactory;
      _handlers = new Dictionary<string, List<Type>>();
      _eventTypes = new List<Type>();
    }

    public Task SendCommand<T>(T command) where T : CommandBase
    {
      return _mediator.Send(command);
    }

    public void Publish<TEvent>(TEvent @event) where TEvent : EventBase
    {
      var factory = new ConnectionFactory {HostName = "localhost"};
      using var con = factory.CreateConnection();
      using (var channel = con.CreateModel())
      {
        var eventName = @event.GetType().Name;

        channel.QueueDeclare(eventName, false, false, false, null);

        var message = JsonConvert.SerializeObject(@event);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish("", eventName, null, body);
      }
    }

    public void Subscribe<TEvent, TEventHandler>() 
      where TEvent : EventBase 
      where TEventHandler : IEventHandler<TEvent>
    {
      void Validate(string s, Type type)
      {
        if(!_eventTypes.Contains(typeof(TEvent)))
        {
          _eventTypes.Add(typeof(TEvent));
        }

        if(!_handlers.ContainsKey(s))
        {
          _handlers.Add(s, new List<Type>());
        }

        if(_handlers[s].Any(x => x == type))
        {
          throw new ArgumentException($"Handler type {type.Name} already is registered for {s}");
        }
      }

      var eventName = typeof(TEvent).Name;
      var handlerType = typeof(TEventHandler);

      Validate(eventName, handlerType);

      _handlers[eventName].Add(handlerType);

      StartBasicConsume<TEvent>();
    }

    private void StartBasicConsume<T>() where T : EventBase
    {
      var factory = new ConnectionFactory
      {
        HostName = "localhost",
        DispatchConsumersAsync = true
      };
      using var con = factory.CreateConnection();
      var channel = con.CreateModel();
      var eventName = typeof(T).Name;

      channel.QueueDeclare(eventName, false, false, false, null);

      var consumer = new AsyncEventingBasicConsumer(channel);
      consumer.Received += Consumer_Received;
      channel.BasicConsume(eventName, true, consumer);
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
    {
      var eventName = @event.RoutingKey;
      var message = Encoding.UTF8.GetString(@event.Body.ToArray());

      try
      {
        await ProcessEvent(eventName, message).ConfigureAwait(false);
      }
      catch (Exception e)
      {
        Console.WriteLine($"Exception was occured when Consumer_Received{e}");
        throw;
      }
    }

    private async Task ProcessEvent(string eventName, string message)
    {
      if(_handlers.ContainsKey(eventName))
      {
        using var scope = _serviceScopeFactory.CreateScope();
        var subscriptions = _handlers[eventName];
        foreach (var subscription in subscriptions)
        {
          var handler = scope.ServiceProvider.GetService(subscription);
          if (handler != null)
          {
            var eventType = _eventTypes.SingleOrDefault(x => x.Name == eventName);
            var @event = JsonConvert.DeserializeObject(message, eventType);
            var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType ?? throw new InvalidOperationException());

            await (Task)concreteType.GetMethod("Handle").Invoke(handler, new[] { @event });
          }
        }
      }
    }
  }
}
