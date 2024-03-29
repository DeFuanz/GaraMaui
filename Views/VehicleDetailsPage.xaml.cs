using Gara.Auth0;
using Gara.Models;
using Gara.Services;
using Gara.ViewModels;

namespace Gara.Views;

public partial class VehicleDetailsPage : ContentPage
{
    private VehicleDetailsViewModel viewModel;
    public VehicleDetailsPage(INavigationDataService navigationDataService, INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService)
	{
		InitializeComponent();
        viewModel = new VehicleDetailsViewModel(navigationDataService,navigationService, restService, client, userService);
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing(); 
        await viewModel.InitializeAsync();
    }
}