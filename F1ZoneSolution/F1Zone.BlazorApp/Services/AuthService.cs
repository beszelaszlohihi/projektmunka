using Blazored.LocalStorage;
using F1ZoneLibrary.Dto;
using Microsoft.AspNetCore.Components.Authorization;
using F1Zone.BlazorApp.Services;
using System.Net.Http.Json;

namespace F1Zone.BlazorApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<string?> Login(LoginDto loginDto)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Auth/login", loginDto);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
                await _localStorage.SetItemAsync("authToken", response!.Token);

                // Biztonságos átalakítás 'as' használatával
                var customProvider = _authStateProvider as CustomAuthStateProvider;
                if (customProvider != null)
                {
                    customProvider.NotifyUserAuthentication(response.Token);
                }

                return null;
            }

            return "Hibás email vagy jelszó!";
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");

            // Biztonságos átalakítás 'as' használatával
            var customProvider = _authStateProvider as CustomAuthStateProvider;
            if (customProvider != null)
            {
                customProvider.NotifyUserLogout();
            }
        }
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}