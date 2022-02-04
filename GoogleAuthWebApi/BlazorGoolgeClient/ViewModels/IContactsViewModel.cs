namespace BlazorGoolgeClient.ViewModels;

public interface IContactsViewModel
{
	public List<Contact> Contacts { get; set; }
	public Task GetContacts();
}