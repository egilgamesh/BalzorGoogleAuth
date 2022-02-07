using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie().AddGoogle(googleOptions =>
{
	googleOptions.ClientId = builder.Configuration.GetSection("Google:ClientId").Value;
	googleOptions.ClientSecret = builder.Configuration.GetSection("Google:ClientSecret").Value;
	googleOptions.CallbackPath = "/profile";
	googleOptions.Scope.Add("profile");
	googleOptions.Events.OnCreatingTicket = context =>
	{
		string? pictureUrl = context.User.GetProperty("picture").GetString();
		context.Identity!.AddClaim(new Claim("picture", pictureUrl!));
		return Task.CompletedTask;
	};

	googleOptions.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
	googleOptions.ClaimActions.MapJsonKey("urn:google:locale", "locale", "string");
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
	endpoints.MapRazorPages();
	endpoints.MapControllers();
	endpoints.MapFallbackToFile("index.html");
});
//app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
app.Run();