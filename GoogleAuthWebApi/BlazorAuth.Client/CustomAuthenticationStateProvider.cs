using System.Net.Http.Json;
using System.Security.Claims;
using BlazorGoolgeClient.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorAuth.Client;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
	private readonly HttpClient httpClient;

	public CustomAuthenticationStateProvider(HttpClient httpClient) =>
		this.httpClient = httpClient;

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		try
		{
			return await TryGetAuthenticationState();
		}
		catch (Exception ex)
		{
			throw new AuthenticationFailed(
				"User Authentication not valid, please login using your google account ", ex);
		}
	}

	private async Task<AuthenticationState> TryGetAuthenticationState()
	{
		var currentUser = await httpClient.GetFromJsonAsync<User?>("user/getcurrentuser");
		if (string.IsNullOrEmpty(currentUser?.Name))
			return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
		var claimUsername = new Claim(ClaimTypes.Name, currentUser.Name);
		var claimProfilePicture = new Claim(ClaimTypes.Uri, currentUser.ProfilePictureUrl!);
		var claimsPrincipal =
			new ClaimsPrincipal(new ClaimsIdentity(new[] { claimUsername, claimProfilePicture }, "serverAuth"));
		return new AuthenticationState(claimsPrincipal);
	}

	private sealed class AuthenticationFailed : Exception
	{
		public AuthenticationFailed(string message, Exception innerException) : base(message,
			innerException) { }
	}
}