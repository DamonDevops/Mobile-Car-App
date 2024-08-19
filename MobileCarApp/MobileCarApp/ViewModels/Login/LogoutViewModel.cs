using CommunityToolkit.Mvvm.Input;
using MobileCarApp.ViewModels.Common;
using MobileCarApp.Views.Login;

namespace MobileCarApp.ViewModels.Login;

public partial class LogoutViewModel : BaseViewModel
{
    public LogoutViewModel()
    {
        Logout();
    }

    [RelayCommand]
    async void Logout()
    {
        SecureStorage.Remove("token");
        App.UserInfo = null;
        await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
    }
}
