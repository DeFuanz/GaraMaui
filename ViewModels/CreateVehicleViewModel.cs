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
        private string enteredVehicleId;

        public string EnteredVehicleId
        {
            get => enteredVehicleId;
            set => SetProperty(ref enteredVehicleId, value);
        }


        public Command AddVehicleCommand { get; }
        public CreateVehicleViewModel(INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService) : base(navigationService, restService, client, userService)
        {
            this.restService = restService;
            this.navigationService = navigationService;
            this.client = client;
            this.userService = userService;

            AddVehicleCommand = new Command(async () => await AddVehicleAsync());
        }

        public async Task AddVehicleAsync()
        {
            var userVehicle = new UserVehicle
            {
                UserId = userService.Auth0UserId,
                VehicleId = Convert.ToInt32(enteredVehicleId)
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
    }
}
