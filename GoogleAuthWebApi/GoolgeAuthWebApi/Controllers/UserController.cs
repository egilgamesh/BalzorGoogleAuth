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
	private IConfiguration? configuration;

	[HttpGet("logoutuser")]
	public async Task<ActionResult<string>> LogOutUser()
	{
		await HttpContext.SignOutAsync();
		return "Success";
	}


	////Authentication Methods
	//[HttpPost("loginuser")]
	//public async Task<ActionResult<User>> LoginUser(User user)
	//{
	//	user.Password = Utility.Encrypt(user.Password);
	//	User loggedInUser = await _context.Users.Where(u => u.EmailAddress == user.EmailAddress && u.Password == user.Password).FirstOrDefaultAsync();

	//	if (loggedInUser != null)
	//	{
	//		//create a claim
	//		var claim = new Claim(ClaimTypes.Email, loggedInUser.EmailAddress);
	//		//create claimsIdentity
	//		var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");
	//		//create claimsPrincipal
	//		var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
	//		//Sign In User
	//		await HttpContext.SignInAsync(claimsPrincipal);
	//	}
	//	return await Task.FromResult(loggedInUser);
	//}

	[HttpGet("GoogleSignIn")]
	public async Task GoogleSignIn()
	{
		configuration = new ConfigurationManager();
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
			currentUser.ProfilePictureUrl =User.FindFirstValue(ClaimTypes.Uri);
		}
		return await Task.FromResult(currentUser);
	}

	[Authorize]
	[HttpGet("UserProfileInformation")]
	public async Task<int> UserProfileInformation() => 5;
}