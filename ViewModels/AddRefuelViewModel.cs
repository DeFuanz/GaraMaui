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
    public class AddRefuelViewModel : BaseViewModel
    {
        public Command BackCommand { get; }
        public Command AddRefuelCommand { get; }

        private readonly INavigationDataService navigationData;

        private bool isNotBusy = true;
        public bool IsNotBusy
        {
            get => isNotBusy;
            set => SetProperty(ref isNotBusy, value);
        }

        private DateTime fillDate;
        public DateTime FillDate
        {
            get => fillDate;
            set => SetProperty(ref fillDate, value);
        }

        private string filledGallons;
        public string FilledGallons
        {
            get => filledGallons;
            set => SetProperty(ref filledGallons, value);
        }

        private string pricePerGallon;
        public string PricePerGallon
        {
            get => pricePerGallon;
            set => SetProperty(ref pricePerGallon, value);
        }

        private string milesDriven;
        public string MilesDriven
        {
            get => milesDriven;
            set => SetProperty(ref milesDriven, value);
        }



        public AddRefuelViewModel(INavigationDataService navigationData ,INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService) : base(navigationService, restService, client, userService)
        {
            this.restService = restService;
            this.navigationService = navigationService;
            this.client = client;
            this.userService = userService;
            this.navigationData = navigationData;

            BackCommand = new Command(async () => await navigationService.NavigateToAsync("//HomePage"));
            AddRefuelCommand = new Command(async () => await AddRefuelAsync());
        }

        // TODO: Add Total Mileage addidtion updated when refuel added.
        public async Task AddRefuelAsync()
        {
            isNotBusy = false;
            var userVehicle = navigationData.GetData("CurrentVehicle") as UserVehicle;
            var refuel = new GasFillUp
            {
                FillUpDate = FillDate.ToUniversalTime(),
                GallonsFilled = Convert.ToDecimal(FilledGallons),
                PricePerGallon = Convert.ToDecimal(PricePerGallon),
                UserVehicleId = userVehicle!.UserVehicleId,
                MilesDriven = Convert.ToDecimal(MilesDriven)
            };
            try
            {
                await restService.AddGassFillUp(refuel);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            await navigationService.NavigateToAsync("//HomePage");
        }
    }
}
