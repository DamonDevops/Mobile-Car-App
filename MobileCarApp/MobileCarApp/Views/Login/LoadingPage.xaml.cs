using MobileCarApp.ViewModels.Login;

namespace MobileCarApp.Views.Login;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingViewModel loadingViewModel)
	{
		InitializeComponent();
		this.BindingContext = loadingViewModel;
	}
}