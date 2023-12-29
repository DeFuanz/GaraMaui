using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara.Services
{
    
    public class NavigationService : INavigationService
    {
        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        //Route to a new route
        public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null!)
        {
            return
                routeParameters != null
                    ? Shell.Current.GoToAsync(route, routeParameters)
                    : Shell.Current.GoToAsync(route);
        }        

        //Pop navigation to previous page
        public Task PopAsync()
        {
            return Shell.Current.GoToAsync("..");
        }
    }
}
