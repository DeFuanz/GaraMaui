using Gara.Auth0;
using Gara.Services;
using Gara.ViewModels;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

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


            builder.Services.AddSingleton<INavigationService , GaraNavigationService>();

            builder.Services.AddSingleton<IRestService, GaraRestService>();

            builder.Services.AddSingleton<IUserService, UserService>();

            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddTransient<LoginPage>();

            builder.Services.AddTransient<HomeViewModel>();

            builder.Services.AddTransient<HomePage>();

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    // Log the exception or handle it as needed
                    Debug.WriteLine(ex.Message);
                }
            };

            return builder.Build();
        }
    }
}
