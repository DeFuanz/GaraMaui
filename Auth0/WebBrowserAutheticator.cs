using IdentityModel.Client;
using IdentityModel.OidcClient.Browser;


namespace Gara.Auth0
{
    public class WebBrowserAutheticator : IdentityModel.OidcClient.Browser.IBrowser
    {
        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            try
            {
                WebAuthenticatorResult result = await WebAuthenticator.Default.AuthenticateAsync(
                    
                    new Uri(options.StartUrl),
                    new Uri(options.EndUrl)
                    );

                var url = new RequestUrl(options.EndUrl)
                    .Create(new Parameters(result.Properties));

                return new BrowserResult
                {
                    Response = url,
                    ResultType = BrowserResultType.Success
                };
            }
            catch(TaskCanceledException)
            {
                return new BrowserResult
                {
                    ResultType = BrowserResultType.UserCancel,
                    ErrorDescription = "Login canceled by user."
                };
            }
        }
    }
}
