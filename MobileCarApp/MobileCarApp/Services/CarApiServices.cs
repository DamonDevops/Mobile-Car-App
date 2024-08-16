using MobileCarApp.Models;
using MobileCarApp.Models.Login;
using MobileCarApp.Models.LoginModel;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MobileCarApp.Services;

public class CarApiServices
{
    HttpClient _httpClient;
    public static string baseAdress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8099" : "http://localhost:8099";
    public string StatusMessage = string.Empty;

    public CarApiServices()
    {
        _httpClient = new() { BaseAddress = new Uri(baseAdress) };
    }

    public async Task<List<Car>?> GetCars()
    {
        try
        {
            await SetAuthToken();
            var response = await _httpClient.GetStringAsync("/cars");
            StatusMessage = "Retrieved successfully";
            return JsonConvert.DeserializeObject<List<Car>>(response);
        }
        catch(Exception)
        {
            StatusMessage = "Failed to retrieve data.";
        }

        return null;
    }

    public async Task<Car?> GetCar(int id)
    {
        try
        {
            var response = await _httpClient.GetStringAsync($"/cars/{id}");
            StatusMessage = "Retrieved successfully";
            return JsonConvert.DeserializeObject<Car>(response);
        }
        catch (Exception)
        {
            StatusMessage = "Failed to retrieve data.";
        }

        return null;
    }

    public async Task AddCar(Car car)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/cars/", car);
            response.EnsureSuccessStatusCode();
            StatusMessage = "Added successfully";
        }
        catch
        {
            StatusMessage = "Failed to upload data.";
        }
    }

    public async Task UpdateCar(int id, Car car)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"/cars/{id}", car);
            response.EnsureSuccessStatusCode();
            StatusMessage = "Updated successfully";
        }
        catch
        {
            StatusMessage = "Failed to update data.";
        }
    }

    public async Task DeleteCar(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/cars/{id}");
            response.EnsureSuccessStatusCode();
            StatusMessage = "Deleted successfully";
        }
        catch
        {
            StatusMessage = "Failed to delete data.";
        }
    }

    public async Task<AuthResponse> Login(LoginModel login)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/login", login);
            response.EnsureSuccessStatusCode();
            StatusMessage = "Login Successfull";

            return JsonConvert.DeserializeObject<AuthResponse>(await response.Content.ReadAsStringAsync());                                                                                                                                                                                                                                                                                                                                                                                  
        }
        catch
        {
            StatusMessage = "Failed to Login";
            return new AuthResponse();
        }
    }

    public async Task SetAuthToken()
    {
        var token = await SecureStorage.GetAsync("token");
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

    }
}