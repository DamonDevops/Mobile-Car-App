using MobileCarApp.Models;
using MobileCarApp.Services;

namespace MobileCarApp
{
    public partial class App : Application
    {
        public static UserInfo UserInfo;
        public static CarServices CarServices { get; private set; }
        public App(CarServices carServices)
        {
            InitializeComponent();

            MainPage = new AppShell();
            CarServices = carServices;
        }
    }
}
