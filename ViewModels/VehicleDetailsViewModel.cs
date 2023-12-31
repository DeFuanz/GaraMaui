using Gara.Auth0;
using Gara.Models;
using Gara.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara.ViewModels
{
    class VehicleDetailsViewModel : BaseViewModel
    {
        public UserVehicle UserVehicle { get; set; }

        private INavigationDataService NavigationDataService;

        public VehicleDetailsViewModel(INavigationDataService navigationDataService ,INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService) : base(navigationService, restService, client, userService)
        {
            this.restService = restService;
            this.navigationService = navigationService;
            this.client = client;
            this.userService = userService;
            NavigationDataService = navigationDataService;
            UserVehicle = NavigationDataService.GetData("CurrentVehicle") as UserVehicle;
        }
    }
}
