using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Microsoft.Maui.Controls;
using NotificationApp.Platforms.Android;

namespace NotificationApp
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected async override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            CreateNotificationFromIntent(Intent);
            // Resolve notification manager
            INotificationManagerService notificationManager =
                App.Current?.Windows[0].Handler?.MauiContext?.Services.GetService<INotificationManagerService>();



            // Request Notification Permission
            PermissionStatus status = await Permissions.RequestAsync<NotificationPermission>();

            if (status != PermissionStatus.Granted)
            {
                // Handle the case when the user denies the permission
                System.Diagnostics.Debug.WriteLine("Notification permission denied.");
            }
        }
        protected async void RequestNotificationPermission()
        {
            PermissionStatus status = await Permissions.RequestAsync<NotificationPermission>();

            if (status != PermissionStatus.Granted)
            {
                System.Diagnostics.Debug.WriteLine("Notification permission denied.");
            }
        }
        protected override void OnNewIntent(Intent? intent)
        {
            base.OnNewIntent(intent);

            CreateNotificationFromIntent(intent);
        }

        static void CreateNotificationFromIntent(Intent intent)
        {
            if (intent?.Extras != null)
            {
                string title = intent.GetStringExtra(NotificationApp.Platforms.Android.NotificationManagerService.TitleKey);
                string message = intent.GetStringExtra(NotificationApp.Platforms.Android.NotificationManagerService.MessageKey);

                var service = IPlatformApplication.Current.Services.GetService<INotificationManagerService>();
                service.ReceiveNotification(title, message);
            }
        }
    }
}
