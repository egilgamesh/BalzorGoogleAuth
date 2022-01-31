using System.Net.Http.Json;
using GoogleAuthClient.Models;

namespace GoogleAuthClient.ViewModels;

public class LoginViewModel
{
	public string EmailAddress { get; set; }
	public string Password { get; set; }

	private HttpClient _httpClient;
	public LoginViewModel()
	{

	}
	public LoginViewModel(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task LoginUser()
	{
		await _httpClient.PostAsJsonAsync<User>("api/user/GoogleSignIn", this);
	}

	public static implicit operator LoginViewModel(User user)
	{
		return new LoginViewModel
		{
			EmailAddress = user.EmailAddress,
			Password = user.Password
		};
	}

	public static implicit operator User(LoginViewModel loginViewModel)
	{
		return new User
		{
			EmailAddress = loginViewModel.EmailAddress,
			Password = loginViewModel.Password
		};
	}
}