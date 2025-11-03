namespace NewsProject.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string? ErrorMessage)> LoginAsync(string email, string password, bool rememberMe);
        Task LogoutAsync();
        Task<bool> IsAuthenticatedAsync();
        Task<(bool Success, string? ErrorMessage)> RegisterAsync(string email, string password);
    }
}


