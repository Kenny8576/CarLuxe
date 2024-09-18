using MassTransit;
using NotificationService;
using NotificationService.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit( x => {
    
        x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();

        x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("nt", false));

        x.UsingRabbitMq((Context, Cfg) => {
            Cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
            {
                host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
                host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
            });
            
        Cfg.ConfigureEndpoints(Context);
    });
});

builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<NotificationHub>("/notifications");

app.Run();
