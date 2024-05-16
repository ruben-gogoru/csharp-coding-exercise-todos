using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WealthKernel.Todos.Data;
using WealthKernel.Todos.Service;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup();


builder.Services.AddSingleton<InMemoryTodosRepository>();

startup.ConfigureServices(builder.Services);

var app = builder.Build();

    

startup.Configure(app);

app.Run();
