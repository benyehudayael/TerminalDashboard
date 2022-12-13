
using TerminalDashboard.Common.Configuration;
using TerminalDashboard.Services;
using WorkerService;

using DAL;
using Microsoft.EntityFrameworkCore;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContextFactory<TerminalContext>(options =>
        {
            options.UseSqlServer(
                context.Configuration.GetConnectionString("DefaultConnection")//, x => x.MigrationsAssembly("TerminalDashboard.Migrations")
                );
        });
        services.AddScoped<DataService>();
        services.AddHostedService<Worker>();
        services.Configure<MyAirport>(context.Configuration.GetSection(nameof(MyAirport)));
    })
    .Build();

await host.RunAsync();
