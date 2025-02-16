using System.Windows;

namespace TinasLabb03
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            // Ingen sparning behövs här, eftersom vi uppdaterar direkt i MongoDB.
            base.OnExit(e);
        }
    }
}