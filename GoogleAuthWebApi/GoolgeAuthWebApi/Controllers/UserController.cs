using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoolgeAuthWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private IConfiguration? configuration;

		[HttpGet("GoogleSignIn")]
		public async Task GoogleSignIn()
		{
			configuration = new ConfigurationManager();
			await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, 
				new AuthenticationProperties { RedirectUri = configuration.GetSection("Google:RedirectUri").Value});
		}
	}

}
