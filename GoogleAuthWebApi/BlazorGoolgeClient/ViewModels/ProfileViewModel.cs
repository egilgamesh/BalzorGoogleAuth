using System.Net.Http.Json;
using BlazorGoolgeClient.Models;

namespace BlazorGoolgeClient.ViewModels;

public class ProfileViewModel : IProfileViewModel
{
	public long UserId { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string EmailAddress { get; set; }
	public string Message { get; set; }
	public string AboutMe { get; set; }
	private readonly HttpClient _httpClient;
	public ProfileViewModel() { }
	public ProfileViewModel(HttpClient httpClient) => _httpClient = httpClient;

	public async Task UpdateProfile()
	{
		User user = this;
		await _httpClient.PutAsJsonAsync("user/updateprofile/" + UserId, user);
		Message = "Profile updated successfully";
	}

	public async Task GetProfile()
	{
		var user = await _httpClient.GetFromJsonAsync<User>("user/getprofile/" + UserId);
		LoadCurrentObject(user);
		Message = "Profile loaded successfully";
	}

	private void LoadCurrentObject(ProfileViewModel profileViewModel)
	{
		FirstName = profileViewModel.FirstName;
		LastName = profileViewModel.LastName;
		EmailAddress = profileViewModel.EmailAddress;
		AboutMe = profileViewModel.AboutMe;
		//add more fields
	}

	public static implicit operator ProfileViewModel(User user) =>
		new ProfileViewModel
		{
			FirstName = user.FirstName,
			LastName = user.LastName,
			EmailAddress = user.EmailAddress,
			UserId = user.UserId,
			AboutMe = user.AboutMe
		};

	public static implicit operator User(ProfileViewModel profileViewModel) =>
		new User
		{
			FirstName = profileViewModel.FirstName,
			LastName = profileViewModel.LastName,
			EmailAddress = profileViewModel.EmailAddress,
			UserId = profileViewModel.UserId,
			AboutMe = profileViewModel.AboutMe
		};
}