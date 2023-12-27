using Gara.Auth0;
using Gara.Services;
using Gara.ViewModels;
using System;

namespace Gara
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(INavigationService navigationService, IRestService restService, Auth0Client client, IUserService userService)
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(navigationService, restService, client, userService);

#if WINDOWS
            client.Browser = new WebViewBrowserAuthenticator(WebViewInstance);
#endif
        }
    }
}
