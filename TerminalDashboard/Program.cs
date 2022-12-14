using DAL;
using Microsoft.EntityFrameworkCore;
using TerminalDashboard.Common.Configuration;
using TerminalDashboard.Services;


try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContextFactory<TerminalContext>(options =>
    {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            x => x.MigrationsAssembly("TerminalDashboard.Migrations")
            );
    });
    var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          builder =>
                          {
                              builder.WithOrigins("http://localhost:4200");
                          });
    });
    builder.Services.AddControllers();
    builder.Services.AddScoped<DataService>();
    builder.Services.Configure<MyAirport>(builder.Configuration.GetSection(nameof(MyAirport)));

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.MapControllers();
    app.UseHttpsRedirection();
    app.UseCors(MyAllowSpecificOrigins);

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<TerminalContext>();

        context.Database.Migrate();
    }

    app.MapGet("/", () => "Hello World!");
    app.Run();
}
catch(Exception e)
{
    throw e;
}