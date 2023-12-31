
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
        private INavigationDataService NavigationDataService;


        public Command RefreshCommand { get; }
        public Command NavigateToAddVehicleCommand { get; }
        public Command NavigateToVehicleDetailsCommand { get; } 

        private string auth0UserName;
        public string Auth0UserName
        {
            get => auth0UserName;
            set => SetProperty(ref auth0UserName, value);
        }
        

        public ObservableCollection<UserVehicle> Vehicles { get; } = new();


        public HomeViewModel(INavigationDataService navigationData,INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService) : base(navigationService, restService, client, userService)
        {
            this.restService = restService;
            this.navigationService = navigationService;
            this.client = client;
            this.userService = userService;

            NavigationDataService = navigationData;

            RefreshCommand = new Command(async () => await GetUserVehiclesAsync());
            NavigateToAddVehicleCommand = new Command(async () => await NavigateToAddVehicle());
            NavigateToVehicleDetailsCommand = new Command<UserVehicle>(async (userVehicle) => await NavigateToVehicleDetails(userVehicle));


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
            try
            {
                var userVehicles = await restService.GetUserVehicles(userService.Auth0UserId);
                Vehicles.Clear();
                foreach (var userVehicle in userVehicles)
                {
                    Vehicles.Add(userVehicle);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
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

        private async Task NavigateToVehicleDetails(UserVehicle userVehicle)
        {
            try
            {
                INavigationDataService navigationDataService = NavigationDataService;
                navigationDataService.SetData("CurrentVehicle", userVehicle);
                await navigationService.NavigateToAsync($"//VehicleDetailsPage");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

    }
}
