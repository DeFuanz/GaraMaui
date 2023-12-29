using Gara.Auth0;
using Gara.Services;
using Gara.ViewModels;

namespace Gara.Views;

public partial class CreateVehiclePage : ContentPage
{
	public CreateVehiclePage(INavigationService navigationService, IRestService restService, Auth0Client client, UserService userService)
	{
		InitializeComponent();
        BindingContext = new CreateVehicleViewModel(navigationService, restService, client, userService);
    }
}