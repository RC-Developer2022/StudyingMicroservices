using AccessApplication.Endpoints;
using AccessApplication.Context;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("AccessDb"))
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (!await context.Database.CanConnectAsync())
        {
            await context.Database.EnsureCreatedAsync();
        }
        await context.Database.MigrateAsync();    
    }
}


app.UseHttpsRedirection();

app.MapAccess();

app.Run();