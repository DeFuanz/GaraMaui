using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gara.Auth0;
using Gara.Services;
using System;
using System.Threading.Tasks;

namespace Gara.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private readonly Auth0Client client;
        private readonly INavigationService navigationService;

        public IRelayCommand LoginCommand { get; }

        public LoginViewModel(Auth0Client auth0Client, INavigationService navigationService)
        {
            this.client = auth0Client;
            this.navigationService = navigationService;
            LoginCommand = new RelayCommand(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            try
            {
                var loginResult = await client.LoginAsync();

                if (!loginResult.IsError)
                {
                    await navigationService.NavigateToAsync("//HomePage");
                }

            }
            catch (Exception)
            {
                
            }
        }
    }
}
