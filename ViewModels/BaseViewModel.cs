using CommunityToolkit.Mvvm.ComponentModel;
using Gara.Auth0;
using Gara.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara.ViewModels
{
    public abstract class BaseViewModel : ObservableObject
    {
        protected INavigationService navigationService;
        protected IRestService restService;
        protected Auth0Client client;
        protected IUserService userService;
        protected BaseViewModel(INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService)
        {
            this.navigationService = navigationService;
            this.restService = restService;
            this.client = client;
            this.userService = userService;
        }
    }
}
