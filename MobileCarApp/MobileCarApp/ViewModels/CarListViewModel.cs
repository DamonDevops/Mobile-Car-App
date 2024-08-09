using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MobileCarApp.Models;
using MobileCarApp.Services;
using MobileCarApp.ViewModels.Common;
using MobileCarApp.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MobileCarApp.ViewModels;

public partial class CarListViewModel : BaseViewModel
{
    const string editButtonText = "Update Car";
    const string createButtonText = "Add Car";
    private readonly CarApiServices _carApiServices;
    NetworkAccess accessType = Connectivity.Current.NetworkAccess;
    string message = string.Empty;

    public ObservableCollection<Car> Cars { get; private set; } = [];

    [ObservableProperty]
    bool isRefreshing;
    [ObservableProperty]
    string make = string.Empty;
    [ObservableProperty]
    string model = string.Empty;
    [ObservableProperty]
    string vin = string.Empty;
    [ObservableProperty]
    string addEditButtonText;
    [ObservableProperty]
    int carId;

    public CarListViewModel(CarApiServices carApiServices)
    {
        _carApiServices = carApiServices;
        Title = "Car List";
        AddEditButtonText = createButtonText;
    }

    [RelayCommand]
    public async Task GetCarList()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            if (Cars.Any())
            {
                Cars.Clear();
            }

            var cars = new List<Car>();
            if(accessType == NetworkAccess.Internet)
            {
                cars = await _carApiServices.GetCars();
            }
            else
            {
                cars = App.CarServices.GetCars();
            }
            
            foreach (var car in cars)
            {
                Cars.Add(car);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Unable to get cars: [{e.Message}]");
            await ShowAlert("Failed to retrieve data");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    async Task GetCarDetails(int id)
    {
        if (id == 0) return;

        await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}", true);
    }

    [RelayCommand]
    async Task SaveCar()
    {
        if (string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Vin))
        {
            await ShowAlert("Please insert valid data and try again");
            return;
        };

        var car = new Car { Id = CarId, Make = Make, Model = Model, Vin = Vin };

        if(CarId != 0)
        {
            if(accessType == NetworkAccess.Internet)
            {
                await _carApiServices.UpdateCar(CarId, car);
                message = _carApiServices.StatusMessage;
            }
            else
            {
                App.CarServices.UpdateCar(car);
                message = App.CarServices.StatusMessage;
            }
        }
        else
        {
            if (accessType == NetworkAccess.Internet)
            {
                await _carApiServices.AddCar(car);
                message = _carApiServices.StatusMessage;
            }
            else
            {
                App.CarServices.AddCar(car);
                message = App.CarServices.StatusMessage;
            }
        }
        await ShowAlert(message);
        await GetCarList();
        await ClearForm();
    }

    [RelayCommand]
    async Task DeleteCar(int id)
    {
        if(id == 0)
        {
            await ShowAlert("Invalid item, Please try again");
            return;
        }
        if(accessType == NetworkAccess.Internet)
        {
            await _carApiServices.DeleteCar(id);
            message = _carApiServices.StatusMessage;
        }
        else
        {
            var result = App.CarServices.DeleteCar(id);
            message = App.CarServices.StatusMessage;
        }
        await ShowAlert(message);
        await GetCarList();
    }

    [RelayCommand]
    async Task SetEditMode(int id)
    {
        AddEditButtonText = editButtonText;
        CarId = id;
        Car car;
        if(accessType == NetworkAccess.Internet)
        {
            car = await _carApiServices.GetCar(CarId);
        }
        else
        {
            car = App.CarServices.GetCar(CarId);
        }
        
        Make = car.Make;
        Model = car.Model;
        Vin = car.Vin;
    }

    [RelayCommand]
    async Task ClearForm()
    {
        AddEditButtonText = createButtonText;
        CarId = 0;
        Make = string.Empty;
        Model = string.Empty;
        Vin = string.Empty;
        await Task.CompletedTask;
    }

    private async Task ShowAlert(string message)
    {
        await Shell.Current.DisplayAlert("INFO", message, "OK");
    }
}
