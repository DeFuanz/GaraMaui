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
        private readonly IUserService userService;
        public Command TestApiCommand { get; }

        public Command RefreshCommand { get; }

        private string auth0UserName;
        public string Auth0UserName
        {
            get => auth0UserName;
            set => SetProperty(ref auth0UserName, value);
        }

        public ObservableCollection<Vehicle?> Vehicles { get; } = new();


        public HomeViewModel(INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService) : base(navigationService, restService, client)
        {
            this.restService = restService;
            this.navigationService = navigationService;
            this.client = client;
            this.userService = userService;


            TestApiCommand = new Command(async () => await TestApiAsync());

            RefreshCommand = new Command(async () => await LoadVehiclesAsync());

            Auth0UserName = userService.Auth0UserName;


        }

        //Test Api connection
        private async Task<string> TestApiAsync()
        {
            var result = await restService.TestApiConnection();
            return result;
        }

        //Load all vehicles (probably refactor to create page once built)
        private async Task LoadVehiclesAsync()
        {
            Vehicles.Clear();
            var vehicles = await GetUserVehiclesAsync();
            foreach (var vehicle in vehicles)
            {
                Vehicles.Add(vehicle);
            }
        }

        //Get user vehicles
        private async Task<List<Vehicle>> GetUserVehiclesAsync()
        {
            var vehicles = await restService.GetUserVehicles(userService.Auth0UserId);
            return vehicles;
        }

    }
}
