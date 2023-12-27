using CommunityToolkit.Mvvm.ComponentModel;
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
        protected BaseViewModel(INavigationService navigationService, IRestService restService)
        {
            this.navigationService = navigationService;
            this.restService = restService;
        }
    }
}
