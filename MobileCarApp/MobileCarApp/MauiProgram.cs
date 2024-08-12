using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using MobileCarApp.Services;
using MobileCarApp.ViewModels;
using MobileCarApp.ViewModels.Login;
using MobileCarApp.Views;
using MobileCarApp.Views.Login;

namespace MobileCarApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "cars.db3");

            //Services
            builder.Services.AddSingleton<CarServices>(s => ActivatorUtilities.CreateInstance<CarServices>(s, dbPath));
            builder.Services.AddTransient<CarApiServices>();

            //ViewModels
            builder.Services.AddSingleton<CarListViewModel>();
            builder.Services.AddTransient<CarDetailsViewModel>();
            builder.Services.AddSingleton<LoadingViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();

            //Pages
            builder.Services.AddTransient<CarDetailsPage>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<LoadingPage>();
            builder.Services.AddSingleton<LoginPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
