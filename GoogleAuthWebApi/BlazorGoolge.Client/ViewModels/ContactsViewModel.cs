using System.Net.Http.Json;
using BlazorGoolgeClient.Models;

namespace BlazorGoolgeClient.ViewModels;

public class ContactsViewModel : IContactsViewModel
{
	//properties
	public List<Contact> Contacts { get; set; }
	private readonly HttpClient _httpClient;

	//methods
	public ContactsViewModel() { }
	public ContactsViewModel(HttpClient httpClient) => _httpClient = httpClient;

	public async Task GetContacts()
	{
		var users = await _httpClient.GetFromJsonAsync<List<User>>("user/getcontacts");
		LoadCurrentObject(users);
	}

	private void LoadCurrentObject(List<User> users)
	{
		Contacts = new List<Contact>();
		foreach (var user in users)
			Contacts.Add(user);
	}
}