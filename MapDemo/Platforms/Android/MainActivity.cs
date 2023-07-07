﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Service.Voice;
using Javax.Security.Auth;
using BeThere.Platforms.Android;
using BeThere.Models;

namespace BeThere;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    Intent serviceIntent;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        Platform.Init(this, savedInstanceState);
        serviceIntent = new Intent(this, typeof(AndroidLocationService));
        setServiceMethods();
    }

    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
    {

        //Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    }

    void setServiceMethods()
    {
        MessagingCenter.Subscribe<StartServiceMessage>(this, "ServiceStarted", message =>
        {
            if (!IsServiceRunning(typeof(AndroidLocationService)))
            {
                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
                {
                    StartForegroundService(serviceIntent);
                }
                else
                {
                    StartService(serviceIntent);
                }
            }
        });

        MessagingCenter.Subscribe<StopServiceMessage>(this, "serviceStopped", message =>
        {
            if (IsServiceRunning(typeof(AndroidLocationService)))
                StopService(serviceIntent);
        });

    }
    private bool IsServiceRunning(Type cls)
    {

        ActivityManager manager = (ActivityManager)GetSystemService(Context.ActivityService);//the active ones
        foreach (var service in manager.GetRunningServices(int.MaxValue))
        {
            if (service.Service.ClassName.Equals(Java.Lang.Class.FromType(cls).CanonicalName))
                return true;
        }

        return false;

    }
}
