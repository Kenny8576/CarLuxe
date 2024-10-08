using AuctionService.Consumers;
using AuctionService.Data;
using AuctionService.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
            Cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
            {
                host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
                host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
            });
            
        Cfg.ConfigureEndpoints(Context);
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.Authority = builder.Configuration["IdentityServiceUrl"];
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters.ValidateAudience = false;
                    options.TokenValidationParameters.NameClaimType = "username";
                });

builder.Services.AddGrpc();
var app = builder.Build();

// Configure the HTTP request pipeline

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();
app.MapGrpcService<GrpcAuctionService>();

try
{
    DbInitializer.InitDb(app);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

app.Run();

