using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using MobileCarApp.Services;
using MobileCarApp.ViewModels;
using MobileCarApp.Views;

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
            
            //Pages
            builder.Services.AddTransient<CarDetailsPage>();
            builder.Services.AddSingleton<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
