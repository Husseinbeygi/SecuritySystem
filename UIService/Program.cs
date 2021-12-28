using _0_Framework.Helper;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.StaticFiles;
using MqttService;
using MqttService.Actions;
using MqttService.Handlers;
using MudBlazor.Services;
using SecuritySystem.Infrastructre;
using Serilog;
using UIService.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console());

SecuritySystemBootstrapper.Configure(builder.Services, "Data Source=Database.db");

#region MqttServices
builder.Services.AddTransient<IMqttActions, MqttActions>();
builder.Services.AddHostedService<MqttBootstrapper>();
#endregion

#region UIKitServices
builder.Services.AddMudServices();
builder.Services.AddBlazorise(options =>
       {
           options.ChangeTextOnKeyPress = true; // optional
       })
      .AddBootstrapProviders()
      .AddFontAwesomeIcons();
#endregion


builder.Services.AddTransient<MessageHandler>();
builder.Services.AddTransient<IRtspUrlGenerator, RtspUrlGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".m3u8"] = "application/x-mpegURL";


app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true
});

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<ClientHub>("/clienthub");
app.MapFallbackToPage("/_Host");

app.Run();
