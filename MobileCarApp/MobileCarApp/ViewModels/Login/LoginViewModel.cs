using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MobileCarApp.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCarApp.ViewModels.Login
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        string username;
        [ObservableProperty]
        string password;

        [RelayCommand]
        async Task Login()
        {
            if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await DisplayLoginError();
            }
            else
            {
                //Call the API to attempt a Login
                var loginSuccess = true;

                if (loginSuccess)
                {
                    //Display welcome message

                    //Build a menu on the fly, based on role

                    //Navigate to app's main page
                }

                await DisplayLoginError();
            }
        }

        async Task DisplayLoginError()
        {
            await Shell.Current.DisplayAlert("Login Invalid", "Invalid Username or Password", "OK");
            Password = string.Empty;
        }
    }
}