using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoogleAuthWebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private IConfiguration? configuration;

	[HttpGet("logoutuser")]
	public async Task<ActionResult<string>> LogOutUser()
	{
		await HttpContext.SignOutAsync();
		return "Success";
	}

	[HttpGet("GoogleSignIn")]
	public async Task GoogleSignIn()
	{
		configuration = new ConfigurationManager();
		await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
			new AuthenticationProperties { RedirectUri = "https://localhost:44327/signin-google" });
	}

	[Authorize]
	[HttpGet("getcurrentuser")]
	public async Task<Dictionary<string, string>> GetCurrentUser()
	{
		Dictionary<string,string> result = new Dictionary<string,string>();
		if (User.Identity!.IsAuthenticated)
			result.Add("Username",User.FindFirstValue(ClaimTypes.Name));
		return await Task.FromResult(result);
		return null;
	}

	[Authorize]
	[HttpGet("UserProfileInformation")]
	public async Task<int> UserProfileInformation() => 5;
}