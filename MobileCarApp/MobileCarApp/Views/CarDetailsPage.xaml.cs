using MobileCarApp.ViewModels;

namespace MobileCarApp.Views;

public partial class CarDetailsPage : ContentPage
{
	public CarDetailsPage(CarDetailsViewModel carDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = carDetailsViewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
		//Do things when the user arrive to the page
        base.OnNavigatedTo(args);
    }
}