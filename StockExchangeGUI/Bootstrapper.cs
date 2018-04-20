using Prism.Unity;
using Microsoft.Practices.Unity;
using Extensibility;
using Services;
using System.Windows;
using StockExchangeGUI.Views;

namespace StockExchangeGUI
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            Container.RegisterType<INotificationService, NotificationService>();

            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.Show();
        }
    }
}
