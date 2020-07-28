using System.Text.Encodings.Web;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RabbitSample.Domain.Core.Bus;
using RabbitSample.Infrastructure.IoC;
using RabbitSample.Transfer.Data.Context;
using RabbitSample.Transfer.Domain.Events;
using RabbitSample.Transfer.Domain.Handlers;

namespace RabbitSample.Transfer.API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<TransferDbContext>(options =>
      {
        options.UseSqlServer(Configuration.GetConnectionString("TransferDbConnectionString"));
      });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Transfer Microservice", Version = "v1" });
      });

      services.AddControllers().AddJsonOptions(options =>
      {
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All);
      });

      services.RegisterServices();
      services.AddMediatR(typeof(Startup));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transfer.API");
      });

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      ConfigureEventBus(app);
    }

    private void ConfigureEventBus(IApplicationBuilder app)
    {
      var eventBus = app.ApplicationServices.GetRequiredService<IBus>();
      eventBus.Subscribe<TransferCreatedEvent, TransferCreatedEventHandler>();
    }
  }
}
