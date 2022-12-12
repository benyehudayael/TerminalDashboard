
using TerminalDashboard.Common.Configuration;
using TerminalDashboard.Services;
using WorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddScoped<DataService>();
        services.AddHostedService<Worker>();
        services.Configure<MyAirport>(context.Configuration.GetSection(nameof(MyAirport)));
    })
    .Build();

await host.RunAsync();
