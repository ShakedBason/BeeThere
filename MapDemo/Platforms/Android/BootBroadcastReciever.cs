using Android.App;
using Android.Content;
using System;

namespace BeThere.Platforms.Android
{
    [BroadcastReceiver(Name="com.locationservice.app.BootBroadcastReceiver",Enabled=true)]
    [IntentFilter(new[] {Intent.ActionBootCompleted})]
    public class BootBroadcastReciever:BroadcastReceiver
    {
        public override void OnReceive(Context? context, Intent? intent)
        {
            if (intent.Action.Equals(Intent.ActionBootCompleted)) 
            {
                Intent main=new Intent(context,typeof(MainActivity));
                main.AddFlags(ActivityFlags.NewTask);
                context.StartActivity(main);
            }
        }
    }
}
