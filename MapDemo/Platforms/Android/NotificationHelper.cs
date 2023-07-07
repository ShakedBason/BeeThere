
using Android.Content;
using Android.App;
using AndroidX.Core.App;
using Android.OS;

namespace BeThere.Platforms.Android
{
    internal class NotificationHelper
    {
        private static string foregroundChannelId = "9001";
        private static Context context = global::Android.App.Application.Context;

        public Notification GetServiceStartedNotification()
        {
            Intent intent = new Intent(context, typeof(MainActivity)); //action or request
            intent.AddFlags(ActivityFlags.SingleTop);
            intent.PutExtra("Title", "Message");

            var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.UpdateCurrent);//to execute the intent later
            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(context, foregroundChannelId)
            .SetContentTitle("Xamarin.Forms Background Tracking Example")
            .SetContentText("Your location is being tracked")
            //.SetSmallIcon(Resource.Drawable.location)
            .SetOngoing(true)
            .SetContentIntent(pendingIntent);

            if (global::Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                NotificationChannel notificationChannel = new NotificationChannel(foregroundChannelId, "Title", NotificationImportance.High);
                notificationChannel.Importance = NotificationImportance.High;
                notificationChannel.EnableLights(true);
                notificationChannel.EnableVibration(true);
                notificationChannel.SetShowBadge(true);
                notificationChannel.SetVibrationPattern(new long[] { 100, 300, 300 });

                NotificationManager? notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;
                if (notificationManager != null)
                {
                    notificationBuilder.SetChannelId(foregroundChannelId);
                    notificationManager.CreateNotificationChannel(notificationChannel);
                }
            }

            return notificationBuilder.Build();
        }
    }
}
