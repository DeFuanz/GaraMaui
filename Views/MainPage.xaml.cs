using Gara.Auth0;

namespace Gara
{
    public partial class MainPage : ContentPage
    {
        private readonly Auth0Client client;

        public MainPage(Auth0Client auth0Client)
        {
            InitializeComponent();
            client = auth0Client;

            #if WINDOWS
            auth0Client.Browser = new WebViewBrowserAuthenticator(WebViewInstance);
            #endif
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var loginResult = await client.LoginAsync();

            if (!loginResult.IsError)
            {
                LoginView.IsVisible = false;
                HomeView.IsVisible = true;
            }
            else
            {
                await DisplayAlert("Error", loginResult.ErrorDescription, "Ok");
            }
        }

        
    }

}
