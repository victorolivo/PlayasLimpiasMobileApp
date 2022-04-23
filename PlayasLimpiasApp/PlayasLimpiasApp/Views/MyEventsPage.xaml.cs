using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayasLimpiasApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyEventsPage : ContentPage
    {
        public MyEventsPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var route = $"{nameof(EventsPage)}";
            await Shell.Current.GoToAsync(route);
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var route = $"///{nameof(HomePage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}