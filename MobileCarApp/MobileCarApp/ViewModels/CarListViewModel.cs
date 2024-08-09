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
    async Task GetCarList()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            if (Cars.Any()) Cars.Clear();

            var cars = new List<Car>();
            //cars = App.CarServices.GetCars();
            cars = await _carApiServices.GetCars();
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
            await Shell.Current.DisplayAlert("Invalid Data", "Please insert valid data and try again", "OK");
            return;
        };

        var car = new Car { Make = Make, Model = Model, Vin = Vin };

        if(CarId != 0)
        {
            car.Id = CarId;
            //App.CarServices.UpdateCar(car);
            await _carApiServices.UpdateCar(CarId, car);
            await Shell.Current.DisplayAlert("Info", App.CarServices.StatusMessage, "Ok");
        }
        else
        {
            //App.CarServices.AddCar(car);
            await _carApiServices.AddCar(car);
            await Shell.Current.DisplayAlert("Info", App.CarServices.StatusMessage, "Ok");
        }
        await GetCarList();
        await ClearForm();
    }

    [RelayCommand]
    async Task DeleteCar(int id)
    {
        if(id == 0)
        {
            await Shell.Current.DisplayAlert("Invalid Record", "Please try again", "OK");
            return;
        }
        //var result = App.CarServices.DeleteCar(id);
        await _carApiServices.DeleteCar(id);
        //if(result == 0)
        //{
        //    await Shell.Current.DisplayAlert("Failed", App.CarServices.StatusMessage, "OK");
        //}
        //else
        //{
        //    await Shell.Current.DisplayAlert("Deletion Successful", App.CarServices.StatusMessage, "OK");
        //    await GetCarList();
        //}
    }

    [RelayCommand]
    async Task SetEditMode(int id)
    {
        AddEditButtonText = editButtonText;
        CarId = id;
        var car = App.CarServices.GetCar(id);
        Make = car.Make;
        Model = car.Model;
        Vin = car.Vin;
        await Task.CompletedTask;
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
}
