using Gara.Auth0;
using Gara.Models;
using Gara.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara.ViewModels
{
    class VehicleDetailsViewModel : BaseViewModel
    {
        public UserVehicle? UserVehicle { get; set; }
        public ObservableCollection<GasFillUp>? GasFillUps { get; } = [];

        private readonly INavigationDataService NavigationDataService;

        public Command BackCommand { get; }
        public Command NavigateToAddRefuelCommand { get; }

        public VehicleDetailsViewModel(INavigationDataService navigationDataService ,INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService) : base(navigationService, restService, client, userService)
        {
            this.restService = restService;
            this.navigationService = navigationService;
            this.client = client;
            this.userService = userService;
            NavigationDataService = navigationDataService;

            UserVehicle = NavigationDataService.GetData("CurrentVehicle") as UserVehicle;
            BackCommand = new Command(async () => await navigationService.NavigateToAsync("//HomePage"));
            NavigateToAddRefuelCommand = new Command(async () => await navigationService.NavigateToAsync("//AddRefuelPage"));

        }

        public async Task InitializeAsync()
        {
            await GetGasRefuelingsAsync(UserVehicle!.UserVehicleId);
        }

        private async Task GetGasRefuelingsAsync(int userVehicleId)
        {
            try
            {
                var vehicleGasFills = await restService.GetGasFillUps(userVehicleId);
                foreach (var gasFill in vehicleGasFills)
                {
                    GasFillUps!.Add(gasFill);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

    }
}
