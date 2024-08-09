using MobileCarApp.ViewModels;

namespace MobileCarApp.Views;

public partial class CarDetailsPage : ContentPage
{
	private readonly CarDetailsViewModel _carDetailsViewModel;
	public CarDetailsPage(CarDetailsViewModel carDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = carDetailsViewModel;
		_carDetailsViewModel = carDetailsViewModel;
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
		//Do things when the user arrive to the page
        base.OnNavigatedTo(args);
		await _carDetailsViewModel.GetCarDetails();
    }
}