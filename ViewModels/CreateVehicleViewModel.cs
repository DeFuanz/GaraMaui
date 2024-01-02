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
        public List<Vehicle> Vehicles { get; set; }


        private List<string> makes;
        public List<string> Makes
        {
            get => makes;
            set => SetProperty(ref makes, value);
        }

        private List<string> models;
        public List<string> Models
        {
            get => models;
            set => SetProperty(ref models, value);
        }

        private List<int> years;
        public List<int> Years
        {
            get => years;
            set => SetProperty(ref years, value);
        }


        private string selectedMake;
        public string SelectedMake
        {
            get => selectedMake;
            set
            {
                SetProperty(ref selectedMake, value);
                GetModelsByMake(value);
            }
        }

        private string selectedModel;
        public string SelectedModel
        {
            get => selectedModel;
            set
            {
                SetProperty(ref selectedModel, value);
                GetYearsByModel(value);
            }
        }

        private int selectedYear;
        public int SelectedYear
        {
            get => selectedYear;
            set
            {
                SetProperty(ref selectedYear, value);
            }
        }



        public Command AddVehicleCommand { get; }
        public CreateVehicleViewModel(INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService) : base(navigationService, restService, client, userService)
        {
            this.restService = restService;
            this.navigationService = navigationService;
            this.client = client;
            this.userService = userService;

            InitializeAsync();

            AddVehicleCommand = new Command(async () => await AddVehicleAsync());
        }

        private async Task InitializeAsync()
        {
            try
            {
                Vehicles = await GetAllVehicles();
                Makes = Vehicles.Select(v => v.Make).Distinct().ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task AddVehicleAsync()
        {
            var vehicleIdforBuild = Vehicles.Where(v => v.Make == selectedMake && v.Model == selectedModel && v.Year == selectedYear).FirstOrDefault().VehicleId;
            
            var userVehicle = new UserVehicle
            {
                UserId = userService.Auth0UserId,
                VehicleId = vehicleIdforBuild
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

        public void GetModelsByMake(string? make)
        {
            try
            {
                Models = Vehicles.Where(m => m.Make == make).Select(m => m.Model).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            Debug.Assert(models != null);
        }

        public void GetYearsByModel(string? model)
        {
            try
            {
                Years = Vehicles.Where(m => m.Model == model).Select(m => m.Year).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            Debug.Assert(years != null);
        }
    }
}
