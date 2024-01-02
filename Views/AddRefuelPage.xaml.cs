using Gara.Auth0;
using Gara.Services;
using Gara.ViewModels;

namespace Gara.Views;

public partial class AddRefuelPage : ContentPage
{
    private readonly AddRefuelViewModel viewModel;
    public AddRefuelPage(INavigationDataService navigationDataService ,INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService)
    {
        InitializeComponent();
        viewModel = new AddRefuelViewModel(navigationDataService ,navigationService, restService, client, userService);
        BindingContext = viewModel;
    }
}