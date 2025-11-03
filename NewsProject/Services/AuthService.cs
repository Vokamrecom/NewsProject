using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using NewsProject.Interfaces;

namespace NewsProject.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<(bool Success, string? ErrorMessage)> LoginAsync(string email, string password, bool rememberMe)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    return (false, "Email и пароль обязательны");
                }

                var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return (true, null);
                }

                return (false, "Неверный email или пароль");
            }
            catch (Exception ex)
            {
                return (false, $"Ошибка при входе: {ex.Message}");
            }
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            // Этот метод будет использоваться для проверки авторизации в контроллере
            return await Task.FromResult(true);
        }

        public async Task<(bool Success, string? ErrorMessage)> RegisterAsync(string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    return (false, "Email и пароль обязательны");
                }

                if (!IsValidEmail(email))
                {
                    return (false, "Неверный формат email");
                }

                if (password.Length < 6)
                {
                    return (false, "Пароль должен содержать минимум 6 символов");
                }

                var user = new IdentityUser 
                { 
                    UserName = email, 
                    Email = email 
                };

                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return (true, null);
                }

                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                return (false, errorMessages);
            }
            catch (Exception ex)
            {
                return (false, $"Ошибка при регистрации: {ex.Message}");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}


