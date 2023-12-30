using Gara.Auth0;
using Gara.Models;
using Gara.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara.ViewModels
{
    public class CreateVehicleViewModel : BaseViewModel
    {
        private int selectedVehicleId;
        private Vehicle selectedVehicle;

        public int SelectedVehicleId
        {
            get => selectedVehicleId;
            set => SetProperty(ref selectedVehicleId, value);
        }

        public Vehicle SelectedVehicle
        {
            get => selectedVehicle;
            set
            {
                if (SetProperty(ref selectedVehicle, value))
                {
                    SelectedVehicleId = selectedVehicle.VehicleId;
                }
            }
        }

        public List<Vehicle> Vehicles { get; } = new List<Vehicle>();

        public Command AddVehicleCommand { get; }
        public CreateVehicleViewModel(INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService) : base(navigationService, restService, client, userService)
        {
            this.restService = restService;
            this.navigationService = navigationService;
            this.client = client;
            this.userService = userService;

            Vehicles = Task.Run(GetAllVehicles).Result;

            AddVehicleCommand = new Command(async () => await AddVehicleAsync());
        }

        public async Task AddVehicleAsync()
        {
            var userVehicle = new UserVehicle
            {
                UserId = userService.Auth0UserId,
                VehicleId = selectedVehicleId
            };
            try
            {
                await restService.AddVehicle(userVehicle);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            await navigationService.NavigateToAsync("//HomePage");
        }

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            var vehicles = await restService.GetVehicles();
            return vehicles;
        }
    }
}
