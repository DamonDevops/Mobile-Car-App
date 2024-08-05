using CommunityToolkit.Mvvm.ComponentModel;
using MobileCarApp.Models;
using MobileCarApp.ViewModels.Common;

namespace MobileCarApp.ViewModels;

[QueryProperty(nameof(Car), "Car")]
public partial class CarDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    Car car;

    public CarDetailsViewModel()
    {
    }
}
