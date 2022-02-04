using System.Net.Http.Json;
using System.Security.Claims;
using BlazorGoolgeClient.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorGoolgeClient;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
	private readonly HttpClient httpClient;
	public CustomAuthenticationStateProvider(HttpClient httpClient) => this.httpClient = httpClient;

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		var currentUser = await httpClient.GetFromJsonAsync<User>("user/getcurrentuser");
		if (currentUser != null && currentUser.EmailAddress != null)
		{
			//create a claims
			var claimEmailAddress = new Claim(ClaimTypes.Name, currentUser.EmailAddress);
			var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier,
				Convert.ToString(currentUser.UserId));
			//create claimsIdentity
			var claimsIdentity =
				new ClaimsIdentity(new[] { claimEmailAddress, claimNameIdentifier }, "serverAuth");
			//create claimsPrincipal
			var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
			return new AuthenticationState(claimsPrincipal);
		}
		return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
	}
}