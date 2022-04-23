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
            Routing.RegisterRoute(nameof(EventDetailsPage), typeof(EventDetailsPage));
            Routing.RegisterRoute(nameof(EventsPage), typeof(EventsPage));
            Routing.RegisterRoute(nameof(MyEventsPage), typeof(MyEventsPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        }
    }
}
