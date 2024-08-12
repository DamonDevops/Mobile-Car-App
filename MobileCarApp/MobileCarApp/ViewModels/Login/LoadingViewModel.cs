using MobileCarApp.ViewModels.Common;
using MobileCarApp.Views.Login;

namespace MobileCarApp.ViewModels.Login
{
    public partial class LoadingViewModel : BaseViewModel
    {
        public LoadingViewModel()
        {
            CheckUserLoginDetails();
        }

        private async void CheckUserLoginDetails()
        {
            //Retrieve token from internal storage
            var token = await SecureStorage.GetAsync("token");

            if (string.IsNullOrEmpty(token))
            {
                GoToLogin();
            }

            //Evaluate token and decide if valid
        }

        private async void GoToLogin()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }
        private async void GoToMain()
        {
            await Shell.Current.GoToAsync($"{nameof(MainPage)}");
        }
    }
}
