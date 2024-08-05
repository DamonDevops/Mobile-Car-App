using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MobileCarApp.Models;
using MobileCarApp.Services;
using MobileCarApp.ViewModels.Common;
using MobileCarApp.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;

namespace MobileCarApp.ViewModels;

public partial class CarListViewModel : BaseViewModel
{
    public ObservableCollection<Car> Cars { get; private set; } = new();

    [ObservableProperty]
    bool isRefreshing;

    public CarListViewModel()
    {
        Title = "Car List";
        GetCarList().Wait();
    }

    [RelayCommand]
    async Task GetCarList()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            if (Cars.Any()) Cars.Clear();

            var cars = App.CarServices.GetCars();
            foreach (var car in cars)
            {
                Cars.Add(car);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Unable to get cars: [{e.Message}]");
            await Shell.Current.DisplayAlert("Error", "Failed to retrieve data", "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    async Task GetCarDetails(Car car)
    {
        if (car == null) return;

        await Shell.Current.GoToAsync(nameof(CarDetailsPage), true, new Dictionary<string, object>
        {
            {nameof(Car), car}
        });
    }
}
