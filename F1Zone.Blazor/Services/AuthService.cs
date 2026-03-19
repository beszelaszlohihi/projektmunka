namespace F1Zone.Blazor.Services;
using F1ZoneLibrary.Dto;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

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

            ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(response.Token);

            return null;
        }

        return "Hibás email vagy jelszó!";
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
    }
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
}