using Gara.Auth0;
using Gara.Models;
using Gara.Services;
using Gara.ViewModels;

namespace Gara.Views;

public partial class CreateVehiclePage : ContentPage
{
	public CreateVehiclePage(INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService)
	{
		InitializeComponent();
        BindingContext = new CreateVehicleViewModel(navigationService, restService, client, userService);

    }
}