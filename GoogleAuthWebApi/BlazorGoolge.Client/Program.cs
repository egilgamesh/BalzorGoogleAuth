using BlazorGoolge.Client;
using BlazorGoolgeClient;
using BlazorGoolgeClient.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddOidcAuthentication(options =>
//{
//	// Configure your authentication provider options here.
//	// For more information, see https://aka.ms/blazor-standalone-auth
//	builder.Configuration.Bind("Local", options.ProviderOptions);
//});

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient<IProfileViewModel, ProfileViewModel>("DeltaEngine", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddHttpClient<IContactsViewModel, ContactsViewModel>("DeltaEngine", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddHttpClient<ISettingsViewModel, SettingsViewModel>("DeltaEngine", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddHttpClient<ILoginViewModel, LoginViewModel>("DeltaEngine", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
await builder.Build().RunAsync();



await builder.Build().RunAsync();
