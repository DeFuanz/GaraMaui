
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
    public class HomeViewModel : BaseViewModel 
    {
        public Command RefreshCommand { get; }
        public Command NavigateToAddVehicleCommand { get; }

        private string auth0UserName;
        public string Auth0UserName
        {
            get => auth0UserName;
            set => SetProperty(ref auth0UserName, value);
        }
        

        public ObservableCollection<Vehicle> Vehicles { get; } = new();


        public HomeViewModel(INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService) : base(navigationService, restService, client, userService)
        {
            this.restService = restService;
            this.navigationService = navigationService;
            this.client = client;
            this.userService = userService;

            RefreshCommand = new Command(async () => await GetUserVehiclesAsync());
            NavigateToAddVehicleCommand = new Command(async () => await NavigateToAddVehicle());

            
            Auth0UserName = userService.Auth0UserName;

        }

        //Initialze vehicles on load to make sure refreshed during navigation. (check HomePage.xaml.cs)
        public async Task InitializeAsync()
        {
            await GetUserVehiclesAsync();
        }


        //Get user vehicles
        private async Task GetUserVehiclesAsync()
        {
            var userVehicles = await restService.GetUserVehicles(userService.Auth0UserId);

            // Clear the existing collection
            Vehicles.Clear();

            // Repopulate the collection with the new data
            foreach (var vehicle in userVehicles)
            {
                Vehicles.Add(vehicle);
            }
        }

        private async Task NavigateToAddVehicle()
        {
            try
            {
                await navigationService.NavigateToAsync("//CreateVehiclePage");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

    }
}
