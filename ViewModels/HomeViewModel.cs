using Gara.Auth0;
using Gara.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara.ViewModels
{
    public class HomeViewModel : BaseViewModel 
    {
        private readonly IUserService userService;
        public Command TestApiCommand { get; }

        private string auth0UserName;
        public string Auth0UserName
        {
            get => auth0UserName;
            set => SetProperty(ref auth0UserName, value);
        }
        

        public HomeViewModel(INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService) : base(navigationService, restService, client)
        {
            this.restService = restService;
            this.navigationService = navigationService;
            this.client = client;
            this.userService = userService;
            TestApiCommand = new Command(async () => await TestApiAsync());

            Auth0UserName = userService.Auth0UserName;

        }

        private async Task<string> TestApiAsync()
        {
            var result = await restService.TestApiConnection();
            return result;
        }
    }
}
