using Gara.Auth0;
using Gara.Services;
using Gara.ViewModels;

namespace Gara;

public partial class HomePage : ContentPage
{
	public HomePage(INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService)
	{
        InitializeComponent();
        BindingContext = new HomeViewModel(navigationService, restService, client, userService);
    }
}