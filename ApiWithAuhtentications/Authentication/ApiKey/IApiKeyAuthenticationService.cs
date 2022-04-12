namespace ApiWithAuhtentications.Authentication.ApiKey
{
    public interface IApiKeyAuthenticationService
	{
		Task<bool> IsValidApiKey(string apiKey);
	}
}