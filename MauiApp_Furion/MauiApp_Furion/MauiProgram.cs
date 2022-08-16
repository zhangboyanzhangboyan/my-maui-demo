using MauiApp_Furion.Services.DingdingRobotServices;
using System.Text.Encodings.Web;

namespace MauiApp_Furion;

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

        //builder.Services.AddSingleton<MainPage,MainPage>();

        builder.Services.AddHttpClient();

        builder.Services.AddTransient<ISendMessage, SendMessage>();
        builder.Services.AddSingleton<MainPage, MainPage>();

        return builder.Build();
    }
}
