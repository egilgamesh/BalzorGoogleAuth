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
	public async Task<string> GetCurrentUser()
	{
		if (User.Identity!.IsAuthenticated)
		{
			return await Task.FromResult(User.FindFirstValue(ClaimTypes.Name));
		}
		return null;
	}

	[Authorize]
	[HttpGet("UserProfileInformation")]
	public async Task<int> UserProfileInformation() => 5;
}