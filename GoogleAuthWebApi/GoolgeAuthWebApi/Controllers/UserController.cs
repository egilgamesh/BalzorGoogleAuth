using System.Security.Claims;
using GoogleAuthWebApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoogleAuthWebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	public IConfiguration? Configuration { get; private set; }

	[HttpGet("logoutuser")]
	public async Task<ActionResult<string>> LogOutUser()
	{
		await HttpContext.SignOutAsync();
		return "Success";
	}

	[HttpGet("GoogleSignIn")]
	public async Task GoogleSignIn()
	{
		Configuration = new ConfigurationManager();
		await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
			new AuthenticationProperties { RedirectUri = "https://localhost:44327/signin-google" });
	}

	[HttpGet("getcurrentuser")]
	public async Task<ActionResult<User>> GetCurrentUser()
	{
		var currentUser = new User();
		if (User.Identity!.IsAuthenticated)
		{
			currentUser.Name = User.FindFirstValue(ClaimTypes.Name);
			currentUser.ProfilePictureUrl =
				User.Claims.FirstOrDefault(claim => claim.Type == "picture")!.Value;
		}
		return await Task.FromResult(currentUser);
	}

	[Authorize]
	[HttpGet("UserProfileInformation")]
	public async Task<int> UserProfileInformation() => 5;
}