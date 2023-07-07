using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using BeThere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeThere.Platforms.Android
{
    [Service]
    public class AndroidLocationService : Service
    {
        CancellationTokenSource cts;
        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10001;

        public override IBinder? OnBind(Intent? intent)
        {
            return null;
        }


        public override StartCommandResult OnStartCommand(Intent? intent, StartCommandFlags flags, int startId)
        {
            cts = new CancellationTokenSource();
            Notification notification = new NotificationHelper().GetServiceStartedNotification();
            StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);

            Task.Run(() =>
            {
                try
                {
                    GetLocationService locShared = new GetLocationService();
                    locShared.Run(cts.Token).Wait();
                }
                catch (Exception ex) { } //need to be changed
                finally
                {
                    if (cts.IsCancellationRequested)
                    {
                        StopServiceMessage message = new StopServiceMessage();
                        Device.BeginInvokeOnMainThread(
                            () => MessagingCenter.Send(message, "Service stopped"));

                    }
                }
            }, cts.Token);

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            if (cts != null)
            {
                cts.Token.ThrowIfCancellationRequested();
                cts.Cancel();
            }

            base.OnDestroy();
        }
    }
}
