using Gara.Auth0;
using Gara.Services;
using Gara.ViewModels;

namespace Gara.Views;

public partial class HomePage : ContentPage
{
    private HomeViewModel viewModel;
    public HomePage(INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService)
	{
        InitializeComponent();
        viewModel = new HomeViewModel(navigationService, restService, client, userService);
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.InitializeAsync();
    }
}