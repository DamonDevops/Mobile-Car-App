using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MobileCarApp.Models;
using MobileCarApp.Models.LoginModel;
using MobileCarApp.Services;
using MobileCarApp.ViewModels.Common;
using System.Security.Claims;

namespace MobileCarApp.ViewModels.Login
{
    public partial class LoginViewModel : BaseViewModel
    {
        private CarApiServices _carApiServices;

        [ObservableProperty]
        string username;
        [ObservableProperty]
        string password;

        public LoginViewModel(CarApiServices carApiServices)
        {
            _carApiServices = carApiServices;
        }

        [RelayCommand]
        async Task Login()
        {
            if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await DisplayLoginMessage("Invalid Login Attempt");
            }
            else
            {
                //Call the API to attempt a Login
                var loginModel = new LoginModel(username, password);
                var response = await _carApiServices.Login(loginModel);

                //Display welcome message / or not
                await DisplayLoginMessage(_carApiServices.StatusMessage);

                if (!string.IsNullOrEmpty(response.Token))
                {
                    //Store token in SecureStorage
                    await SecureStorage.SetAsync("token", response.Token);
                    //Build a menu on the fly, based on role
                    var jsonToken = new JsonWebTokenHandler().ReadToken(response.Token) as JsonWebToken;

                    var role = jsonToken.Claims.FirstOrDefault(q => q.Type.Equals(ClaimTypes.Role))?.Value;

                    App.UserInfo = new UserInfo
                    {
                        Username = Username,
                        Role = role
                    };
                    //Navigate to app's main page
                    await Shell.Current.GoToAsync($"{nameof(MainPage)}");
                }
                else
                {
                    await DisplayLoginMessage("Invalid Login Attempt");
                }
            }
        }

        async Task DisplayLoginMessage(string message)
        {
            await Shell.Current.DisplayAlert("Login Attempt", message, "OK");
            Password = string.Empty;
        }
    }
}