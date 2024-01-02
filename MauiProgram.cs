using Gara.Auth0;
using Gara.Services;
using Gara.ViewModels;
using Gara.Views;
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


            builder.Services.AddSingleton<INavigationService , NavigationService>();

            builder.Services.AddSingleton<IRestService, RestService>();

            builder.Services.AddSingleton<IUserService, UserService>();

            builder.Services.AddSingleton<INavigationDataService, NavigationDataService>();

            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddTransient<LoginPage>();

            builder.Services.AddTransient<HomeViewModel>();

            builder.Services.AddTransient<HomePage>();

            builder.Services.AddTransient<CreateVehicleViewModel>();

            builder.Services.AddTransient<CreateVehiclePage>();

            builder.Services.AddTransient<VehicleDetailsViewModel>();

            builder.Services.AddTransient<VehicleDetailsPage>();

            builder.Services.AddTransient<AddRefuelViewModel>();

            builder.Services.AddScoped<AddRefuelPage>();


            return builder.Build();
        }
    }
}
