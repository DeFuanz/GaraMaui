using Gara.Auth0;
using Gara.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara.ViewModels
{
    public class CreateVehicleViewModel : BaseViewModel
    {
        private readonly IUserService userService;
        public Command AddVehicleCommand { get; }
        public CreateVehicleViewModel(INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService) : base(navigationService, restService, client)
        {
            this.restService = restService;
            this.navigationService = navigationService;
            this.client = client;
            this.userService = userService;
        }

        public async Task AddVehicleAsync()
        {
            
            await navigationService.NavigateToAsync("//AddVehiclePage");
        }
    }
}
