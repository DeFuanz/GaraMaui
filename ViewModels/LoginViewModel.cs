using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gara.Auth0;
using Gara.Services;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gara.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        public IRelayCommand LoginCommand { get; }

        public LoginViewModel(INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService) : base(navigationService, restService, client, userService)
        {
            this.client = client;
            this.navigationService = navigationService;
            this.restService = restService;
            LoginCommand = new RelayCommand(async () => await LoginAsync());
            this.userService = userService;
        }

        //Login user. if success, navigate to home page and save user info
        private async Task LoginAsync()
        {
            try
            {
                var loginResult = await client.LoginAsync();

                

                if (!loginResult.IsError && loginResult.User != null)
                {
                    userService.Auth0UserName = loginResult.User.Identity!.Name!;
                    foreach (var claim in loginResult.User.Claims)
                    {
                        Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
                    }
                    var userId = loginResult.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                    var splitId = userId!.Split("|");
                    if (splitId.Length == 2)
                    {
                        userService.Auth0UserId = splitId[1];
                    }

                    Debug.WriteLine($"Auth0UserId: {userService.Auth0UserId}");
                    Debug.WriteLine($"Auth0UserName: {userService.Auth0UserName}");

                    await navigationService.NavigateToAsync("//HomePage");
                }

            }
            catch (Exception)
            {
                
            }
        }
    }
}
