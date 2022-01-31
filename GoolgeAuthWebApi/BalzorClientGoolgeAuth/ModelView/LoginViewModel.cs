﻿using System.Net.Http.Json;
using BalzorClientGoolgeAuth.Models;

namespace BalzorClientGoolgeAuth.ModelView;

public class LoginViewModel: ILoginViewModel
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
		await _httpClient.PostAsJsonAsync<User>("user/loginuser", this);
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