
using MobileCarApp;
using MobileCarApp.ViewModels;

namespace MobileCarApp;

public partial class MainPage : ContentPage
{
    public MainPage(CarListViewModel carListViewModel)
    {
        InitializeComponent();
        BindingContext = carListViewModel;
    }
}