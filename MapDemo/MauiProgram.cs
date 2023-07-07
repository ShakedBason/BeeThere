﻿using BeThere.Views;
using BeThere.Services;
using BeThere.ViewModels;
using Microsoft.Extensions.Logging;
using Xamarin.Essentials;

namespace  BeThere;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiMaps()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
            
        
//.ConfigureMauiHandlers(handlers =>
//{
//    handlers.AddHandler(typeof(Microsoft.Maui.Controls.Maps.Map), typeof(MapHandler));
//});

#if DEBUG
        //builder.Logging.AddDebug();
#endif
        builder.Services.AddTransient<ChatPage>();
        builder.Services.AddTransient<MapPage>();
        builder.Services.AddSingleton<BaseService>();
        builder.Services.AddSingleton<ConnectToAppService>();
        builder.Services.AddSingleton<ConnectToAppViewModle>();
        builder.Services.AddSingleton<LoginPage>();

        return builder.Build();
    }
}
