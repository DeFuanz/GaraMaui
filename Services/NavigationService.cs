using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (Shell.Current.Navigation.NavigationStack.Count > 1)
            {
                return Shell.Current.GoToAsync("..");
            }
            else
            {
                Debug.WriteLine("There's only one page on the stack, can't pop");
                // There's only one page on the stack, can't pop, handle accordingly
                // Maybe ignore or alert the user
                return Task.CompletedTask;
            }
        }
    }
}
