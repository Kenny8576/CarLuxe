using AuctionService.Consumers;
using AuctionService.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<AuctionDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddMassTransit( x => {

        x.AddEntityFrameworkOutbox<AuctionDbContext>(o => {
            o.QueryDelay = TimeSpan.FromSeconds(10);

            o.UsePostgres();
            o.UseBusOutbox();
        });
        
        x.AddConsumersFromNamespaceContaining<AuctionCreatedFaultConsumer>();
        x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("aution", false));
        x.UsingRabbitMq((Context, Cfg) => {
        Cfg.ConfigureEndpoints(Context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline


app.UseAuthorization();
app.UseRouting();

app.MapControllers();

try
{
    DbInitializer.InitDb(app);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

app.Run();

