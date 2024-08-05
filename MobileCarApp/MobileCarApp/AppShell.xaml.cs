using MobileCarApp.Views;

namespace MobileCarApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CarDetailsPage), typeof(CarDetailsPage));
        }
    }
}
