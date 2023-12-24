using Gara.Auth0;
using Microsoft.Extensions.Logging;

namespace Gara
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddSingleton(new Auth0Client(new()
            {
                Domain = "dev-zhjessp8n653h4m3.us.auth0.com",
                ClientId = "eCSNoa9fYq5dLdyYY2YOEdq7JrtpEPcL",
                Scope = "openid profile",

#if WINDOWS
                RedirectUri = "https://localhost/callback"
#else
                RedirectUri = "myapp://callback"
#endif
            }));

            return builder.Build();
        }
    }
}
