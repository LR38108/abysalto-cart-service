using Cart.API.Data;
using Cart.API.Middleware;
using Cart.API.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CartDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse(
        builder.Configuration.GetConnectionString("Redis")!);
    configuration.AbortOnConnectFail = false;
    return ConnectionMultiplexer.ConnectAsync(configuration).GetAwaiter().GetResult();
});

builder.Services.AddScoped<ICartCacheService, CartCacheService>();
builder.Services.AddScoped<ICartService, CartService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
