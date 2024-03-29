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
        public ObservableCollection<GasFillUp>? GasFillUps { get; set; } = [];

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

            BackCommand = new Command(async () => await navigationService.NavigateToAsync("//HomePage"));
            NavigateToAddRefuelCommand = new Command(async () => await navigationService.NavigateToAsync("//AddRefuelPage"));

        }

        public async Task InitializeAsync()
        {
            UserVehicle = NavigationDataService.GetData("CurrentVehicle") as UserVehicle;
            await GetGasRefuelingsAsync(UserVehicle!.UserVehicleId);
        }

        private async Task GetGasRefuelingsAsync(int userVehicleId)
        {
            try
            {
                GasFillUps!.Clear();
                var vehicleGasFills = await restService.GetGasFillUps(userVehicleId);
                foreach (var gasFill in vehicleGasFills.OrderByDescending(x => x.FillUpDate))
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
