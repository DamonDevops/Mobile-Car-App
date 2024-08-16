using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Logging;
using MobileCarApp.ViewModels.Common;
using MobileCarApp.Views.Login;
using Serilog.Core;

namespace MobileCarApp.ViewModels.Login
{
    public partial class LoadingViewModel : BaseViewModel
    {
        private readonly ILogger<LoadingViewModel> _logger;
        public LoadingViewModel(ILogger<LoadingViewModel> logger)
        {
            CheckUserLoginDetails();
            _logger = logger;
        }

        private async void CheckUserLoginDetails()
        {
            //Retrieve token from internal storage
            //SecureStorage.Remove("token");
            var token = await SecureStorage.GetAsync("token");

            if (string.IsNullOrEmpty(token))
            {
                GoToLogin();
            }
            else
            {
                IdentityModelEventSource.ShowPII = true;
                IdentityModelEventSource.LogCompleteSecurityArtifact = true;
                try
                {
                    if (new JsonWebTokenHandler().CanReadToken(token))
                    {
                        var jsonToken = new JsonWebTokenHandler().ReadToken(token); //as JsonWebToken;
                        _logger.LogInformation(jsonToken.ToString());

                        if (jsonToken.ValidTo < DateTime.UtcNow)
                        {
                            SecureStorage.Remove("token");
                            GoToLogin();
                        }
                        else
                        {
                            GoToMain();
                        }
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Authentication error", "Unreadable Token", "OK");
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                        _logger.LogError(ex.Message);
                    }

                    await Shell.Current.DisplayAlert("Authentication error", "There is a problem with your authentification token, please contact an administrator", "OK");
                }
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
