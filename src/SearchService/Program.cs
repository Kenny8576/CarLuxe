using System.Net;
using Microsoft.AspNetCore.Server.HttpSys;
using MongoDB.Driver;
using MongoDB.Entities;
using Polly;
using Polly.Extensions.Http;
using SearchService.Data;
using SearchService.Model;
using SearchService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient<AuctionSvcHttpClient>().AddPolicyHandler(GetPolicy());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

app.Lifetime.ApplicationStarted.Register(async () =>{
        try
    {
          await DbInitilizer.InitDb(app);
        } catch(Exception e)
    {
           Console.WriteLine(e);
    }
    });



app.Run();

static IAsyncPolicy<HttpResponseMessage> GetPolicy()
       => HttpPolicyExtensions
          .HandleTransientHttpError()
          .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
          .WaitAndRetryForeverAsync(_=> TimeSpan.FromSeconds(3)); 
