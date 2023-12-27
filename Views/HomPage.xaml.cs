using Gara.Services;
using Gara.ViewModels;

namespace Gara;

public partial class HomePage : ContentPage
{
	public HomePage(INavigationService navigationService)
	{
        InitializeComponent();
        BindingContext = new HomeViewModel(navigationService);
    }
}