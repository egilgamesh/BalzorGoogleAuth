namespace BalzorClientGoolgeAuth.ModelView;

public interface ILoginViewModel
{
	public string EmailAddress { get; set; }
	public string Password { get; set; }

	public Task LoginUser();
}