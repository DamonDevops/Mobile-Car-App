﻿using MobileCarApp.Views;
using MobileCarApp.Views.Login;

namespace MobileCarApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CarDetailsPage), typeof(CarDetailsPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
        }
    }
}
