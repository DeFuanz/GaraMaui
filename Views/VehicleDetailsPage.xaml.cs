using Gara.Auth0;
using Gara.Models;
using Gara.Services;
using Gara.ViewModels;

namespace Gara.Views;

public partial class VehicleDetailsPage : ContentPage
{
	public VehicleDetailsPage(INavigationDataService navigationDataService, INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService)
	{
		InitializeComponent();
        BindingContext = new VehicleDetailsViewModel(navigationDataService,navigationService, restService, client, userService);
    }
}