using CslaBlazorApp.Client;
using Csla.Configuration;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//builder.Services.AddAuthorizationCore();
builder.Services.AddOptions();

builder.Services.AddCsla(o => o
  .AddBlazorWebAssembly()
  .DataPortal(dpo => dpo
	.UseHttpProxy(options => options.DataPortalUrl = "/api/DataPortal")));

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

CultureInfo culture;
var js = builder.Build().Services.GetRequiredService<IJSRuntime>();
var result = await js.InvokeAsync<string>("blazorCulture.get");

if (result != null) {
	culture = new CultureInfo(result);
} else {
	culture = new CultureInfo("en-US");
	await js.InvokeVoidAsync("blazorCulture.set", "en-US");
}

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await builder.Build().RunAsync();