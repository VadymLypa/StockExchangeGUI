using Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Services
{
    public class NotificationService : INotificationService
    {
        private const string ErrorWrongInput = "Please choose the symbol";

        public static void ShowSuccessfulMessageBox(string message)
        {
            MessageBox.Show(message, "Congratulation", MessageBoxButton.OK, MessageBoxImage.Information,
                MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
        }

        public static void ShowErrorMessageBox(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error,
             MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
        }
        
        public void WrongInput() => ShowErrorMessageBox(ErrorWrongInput);

        public void SocketError(string message) => ShowErrorMessageBox(message);
    }
}
