using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationApp
{
    public class NotificationEventArgs : EventArgs
    {
        public string Title { get; }
        public string Message { get; }

        public NotificationEventArgs(string title, string message)
        {
            Title = title;
            Message = message;
        }
    }
}
