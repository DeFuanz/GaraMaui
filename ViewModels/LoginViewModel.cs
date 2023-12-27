using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gara.Auth0;
using Gara.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Gara.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        public IRelayCommand LoginCommand { get; }
        private readonly IUserService userService;

        public LoginViewModel(INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService) : base(navigationService, restService, client)
        {
            this.client = client;
            this.navigationService = navigationService;
            this.restService = restService;
            LoginCommand = new RelayCommand(async () => await LoginAsync());
            this.userService = userService;
        }

        private async Task LoginAsync()
        {
            try
            {
                var loginResult = await client.LoginAsync();

                

                if (!loginResult.IsError && loginResult.User != null)
                {
                    userService.Auth0UserName = loginResult.User.Identity.Name;

                    await navigationService.NavigateToAsync("//HomePage");
                }

            }
            catch (Exception)
            {
                
            }
        }
    }
}
