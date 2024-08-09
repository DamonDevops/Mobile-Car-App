
using MobileCarApp;
using MobileCarApp.ViewModels;

namespace MobileCarApp;

public partial class MainPage : ContentPage
{
    private readonly CarListViewModel _carListViewModel;
    public MainPage(CarListViewModel carListViewModel)
    {
        InitializeComponent();
        BindingContext = carListViewModel;
        _carListViewModel = carListViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _carListViewModel.GetCarList();
    }
}