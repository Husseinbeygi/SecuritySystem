using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.ResponseCompression;
using MqttService;
using MqttService.Handlers;
using SecuritySystem.Infrastructre;
using Serilog;
using UIService.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<IMqttActions, MqttActions>();
builder.Services.AddHostedService<MqttBootstrapper>();
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console());
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

SecuritySystemBootstrapper.Configure(builder.Services, "Data Source=Database.db");

builder.Services.AddBlazorise(options =>
       {
           options.ChangeTextOnKeyPress = true; // optional
       })
      .AddBootstrapProviders()
      .AddFontAwesomeIcons();
builder.Services.AddTransient<MessageHandler>();

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
app.MapHub<ClientHub>("/clienthub");
app.MapFallbackToPage("/_Host");

app.Run();
