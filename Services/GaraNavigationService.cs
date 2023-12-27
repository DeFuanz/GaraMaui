using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara.Services
{
    public interface INavigationService
    {
        Task InitializeAsync();
        Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null!);
        Task PopAsync();
    }
    public class GaraNavigationService : INavigationService
    {
        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null!)
        {
            return
                routeParameters != null
                    ? Shell.Current.GoToAsync(route, routeParameters)
                    : Shell.Current.GoToAsync(route);
        }        

        public Task PopAsync()
        {
            return Shell.Current.GoToAsync("..");
        }
    }
}
