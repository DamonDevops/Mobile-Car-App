using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MobileCarApp.Models;
using MobileCarApp.Services;
using MobileCarApp.ViewModels.Common;
using System.Web;

namespace MobileCarApp.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class CarDetailsViewModel : BaseViewModel, IQueryAttributable
{
    private readonly CarApiServices _carApiServices;
    [ObservableProperty]
    Car car;
    [ObservableProperty]
    int id;
    NetworkAccess accessType = Connectivity.Current.NetworkAccess;

    public CarDetailsViewModel(CarApiServices carApiServices)
    {
        _carApiServices = carApiServices;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Id = Convert.ToInt32(HttpUtility.UrlDecode(query[nameof(Id)].ToString()));
        Car = App.CarServices.GetCar(Id);
    }

    [RelayCommand]
    public async Task GetCarDetails()
    {
        if(accessType == NetworkAccess.Internet)
        {
            Car = await _carApiServices.GetCar(Id);
        }
        else
        {
            Car = App.CarServices.GetCar(Id);
        }
    }
}
