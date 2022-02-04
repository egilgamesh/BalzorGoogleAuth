using System.Net.Http.Json;
using BlazorGoolgeClient.Models;

namespace BlazorGoolgeClient.ViewModels;

public class SettingsViewModel : ISettingsViewModel
{
	//properties
	public long UserId { get; set; }
	public bool Notifications { get; set; }
	public bool DarkTheme { get; set; }
	private readonly HttpClient _httpClient;

	//methods
	public SettingsViewModel() { }
	public SettingsViewModel(HttpClient httpClient) => _httpClient = httpClient;

	public async Task GetProfile()
	{
		var user = await _httpClient.GetFromJsonAsync<User>($"user/getprofile/{UserId}");
		LoadCurrentObject(user);
	}

	public async Task Save()
	{
		await _httpClient.GetFromJsonAsync<User>(
			$"user/updatetheme?userId={UserId}&value={DarkTheme.ToString()}");
		await _httpClient.GetFromJsonAsync<User>(
			$"user/updatenotifications?userId={UserId}&value={Notifications.ToString()}");
	}

	private void LoadCurrentObject(SettingsViewModel settingsViewModel)
	{
		DarkTheme = settingsViewModel.DarkTheme;
		Notifications = settingsViewModel.Notifications;
	}

	//operators
	public static implicit operator SettingsViewModel(User user) =>
		new SettingsViewModel
		{
			Notifications = user.Notifications == null || (long)user.Notifications == 0
				? false
				: true,
			DarkTheme = user.DarkTheme == null || (long)user.DarkTheme == 0
				? false
				: true
		};

	public static implicit operator User(SettingsViewModel settingsViewModel) =>
		new User
		{
			Notifications = settingsViewModel.Notifications
				? 1
				: 0,
			DarkTheme = settingsViewModel.DarkTheme
				? 1
				: 0
		};
}