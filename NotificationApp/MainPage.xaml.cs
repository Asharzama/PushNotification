namespace NotificationApp
{
    public partial class MainPage : ContentPage
    {
        private readonly INotificationManagerService _notificationManager;

        public MainPage()
        {
            InitializeComponent();

            // Resolve notification manager
            _notificationManager = App.Current?.Windows[0].Handler?.MauiContext?.Services.GetService<INotificationManagerService>();

            if (_notificationManager == null)
            {
                System.Diagnostics.Debug.WriteLine("NotificationManagerService could not be resolved.");
            }

            // Subscribe to the notification received event
            _notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var eventData = (NotificationEventArgs)eventArgs;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    // Handle the received notification
                    System.Diagnostics.Debug.WriteLine($"Notification received: {eventData.Title} - {eventData.Message}");
                });
            };
        }

        private void OnSendNotificationButtonClicked(object sender, EventArgs e)
        {
            if (_notificationManager != null)
            {
                // Send notification immediately
                _notificationManager.SendNotification("Hello", "This is a test notification!");

                // Or schedule a notification to be sent in 10 seconds
                _notificationManager.SendNotification("Scheduled Notification", "This will show after 10 seconds.", DateTime.Now.AddSeconds(10));
            }
        }
    }

}
