using System.Threading.RateLimiting;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.RateLimiting;
using RentCarServer.Application;
using RentCarServer.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddRateLimiter(cfr =>
{
    cfr.AddFixedWindowLimiter("fixed", options =>
    {
        options.PermitLimit = 100;
        options.QueueLimit = 100;
        options.Window = TimeSpan.FromSeconds(1);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;

    });
});

builder.Services
    .AddControllers()
    .AddOData(opt =>
    {
        opt.Select()
            .Filter()
            .Count()
            .Expand()
            .OrderBy()
            .SetMaxTop(null);
    });

builder.Services.AddCors();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();
app.UseHttpsRedirection(); //https ile çalışması için
app.UseCors(cfr =>
{
    cfr.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .SetPreflightMaxAge(TimeSpan.FromMinutes(10)); // aynı yerden 10 dk içinde tekrar istek alıyorsa tekrar denetlemesin
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers().RequireRateLimiting("fixed");
app.Run();

