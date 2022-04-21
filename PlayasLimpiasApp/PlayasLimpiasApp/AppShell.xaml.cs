using Xamarin.Forms;
using PlayasLimpiasApp.Views;

namespace PlayasLimpiasApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(NewEventPage), typeof(NewEventPage));
        }
    }
}
