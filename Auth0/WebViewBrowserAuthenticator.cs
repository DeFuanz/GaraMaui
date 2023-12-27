using IdentityModel.OidcClient.Browser;


namespace Gara.Auth0
{
    public class WebViewBrowserAuthenticator(WebView webView) : IdentityModel.OidcClient.Browser.IBrowser
    {
        private readonly WebView webView = webView;

        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken =  default)
        {
            var taskCompletionSource = new TaskCompletionSource<BrowserResult>();

            webView.Navigated += (sender, e) =>
            {
                if (e.Url.StartsWith(options.EndUrl))
                {
                    webView.WidthRequest = 0;
                    webView.HeightRequest = 0;

                    if (taskCompletionSource.Task.Status != TaskStatus.RanToCompletion)
                    {
                        taskCompletionSource.SetResult(new BrowserResult
                        {
                            ResultType = BrowserResultType.Success,
                            Response = e.Url.ToString(),
                        });
                    }
                }
            };

            webView.WidthRequest = 600;
            webView.HeightRequest = 600;
            webView.Source = new UrlWebViewSource { Url = options.StartUrl };

            return await taskCompletionSource.Task;
        }
    }
}
