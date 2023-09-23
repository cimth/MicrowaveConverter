using Microsoft.Extensions.Logging;
using MicrowaveConverter.Views;
using CommunityToolkit.Maui;

namespace MicrowaveConverter;

public static class MauiProgram
{
    // ==============
    // CreateMauiApp()
    // ==============
    
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            // Use dependency injection.
            // See: https://learn.microsoft.com/en-us/dotnet/architecture/maui/dependency-injection
            .RegisterViews()
            .RegisterViewModels()
            // Use the .NET MAUI Community Toolkit.
            // See:
            // https://github.com/CommunityToolkit/Maui
            // https://learn.microsoft.com/en-us/dotnet/communitytoolkit/maui/get-started?tabs=CommunityToolkitMaui
            .UseMauiCommunityToolkit();

        #if DEBUG
        builder.Logging.AddDebug();
        #endif

        return builder.Build();
    }
    
    // ==============
    // Dependency injection
    // ==============

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<MainPage>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<ViewModels.MainViewModel>();
        return mauiAppBuilder;
    }
}