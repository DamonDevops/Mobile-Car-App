using CommunityToolkit.Mvvm.ComponentModel;
using MobileCarApp.Models;
using MobileCarApp.ViewModels.Common;
using System.Web;

namespace MobileCarApp.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class CarDetailsViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty]
    Car car;
    [ObservableProperty]
    int id;

    public CarDetailsViewModel()
    {
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Id = Convert.ToInt32(HttpUtility.UrlDecode(query[nameof(Id)].ToString()));
        Car = App.CarServices.GetCar(Id);
    }
}
