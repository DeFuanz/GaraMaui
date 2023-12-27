using Gara.Auth0;
using Gara.Services;
using Gara.ViewModels;
using System;

namespace Gara
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(Auth0Client client, INavigationService navigationService)
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(client, navigationService);

#if WINDOWS
            client.Browser = new WebViewBrowserAuthenticator(WebViewInstance);
#endif
        }
    }
}
