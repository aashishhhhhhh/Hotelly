using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

public class UserService
{
    private readonly HttpClient _client;

    public UserService()
    {
        _client = new HttpClient { BaseAddress = new Uri("http://localhost:{port}/api/") };
    }

    public async Task<string> RegisterUser(string username, string email, string password)
    {
        var user = new { Username = username, Email = email, PasswordHash = password };
        var response = await _client.PostAsJsonAsync("users/register", user);

        return response.IsSuccessStatusCode ? "Registration successful" : await response.Content.ReadAsStringAsync();
    }

    public async Task<string> LoginUser(string email, string password)
    {
        var user = new { Email = email, PasswordHash = password };
        var response = await _client.PostAsJsonAsync("users/login", user);

        return response.IsSuccessStatusCode ? "Login successful" : await response.Content.ReadAsStringAsync();
    }
}
