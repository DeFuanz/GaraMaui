using CommunityToolkit.Mvvm.ComponentModel;
using Gara.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara.ViewModels
{
    public class BaseViewModel(INavigationService navigationService) : ObservableObject
    {
        public INavigationService NavigationService = navigationService;
    }
}
