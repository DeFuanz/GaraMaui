using Gara.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara.ViewModels
{
    public class HomeViewModel : BaseViewModel 
    {
        

        public Command TestApiCommand { get; }
        public HomeViewModel(INavigationService navigationService, IRestService restService) : base(navigationService, restService)
        {
            this.restService = restService;
            TestApiCommand = new Command(async () => await TestApiAsync());
        }

        private async Task<string> TestApiAsync()
        {
            var result = await restService.TestApiConnection();
            return result;
        }
    }
}
