using Counter;
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
        services.AddScoped<TerminalContext>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
