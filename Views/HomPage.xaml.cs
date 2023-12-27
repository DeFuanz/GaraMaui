using Gara.Services;
using Gara.ViewModels;

namespace Gara;

public partial class HomePage : ContentPage
{
	public HomePage(INavigationService navigationService, IRestService restService)
	{
        InitializeComponent();
        BindingContext = new HomeViewModel(navigationService, restService);
    }
}