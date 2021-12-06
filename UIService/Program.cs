using Microsoft.AspNetCore.ResponseCompression;
using MqttService;
using MqttService.Configuration;
using MudBlazor.Services;
using Serilog;
using SignalRHubs.Hubs;
using UIService.Data;
using static MqttService.Bootstrapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddSingleton<WeatherForecastService>();
MqttConfiguration mqttServiceConfiguration = new();
builder.Configuration.GetSection("MqttService").Bind(mqttServiceConfiguration);
builder.Services.AddSingleton(_ => new Bootstrapper(mqttServiceConfiguration, "MqttService"));
builder.Services.AddSingleton<IHostedService>(p => p.GetRequiredService<Bootstrapper>());
builder.Services.AddTransient<ILoggerService, LoggerService>();
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console());

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<MessageHub>("/chathub");
app.MapFallbackToPage("/_Host");

app.Run();
