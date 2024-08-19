using MobileCarApp.ViewModels.Login;

namespace MobileCarApp.Views.Login;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
		this.BindingContext = loginViewModel;
	}
}