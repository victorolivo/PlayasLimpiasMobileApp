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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var route = $"{nameof(NewEventPage)}";
            await Shell.Current.GoToAsync(route);
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var route = $"{nameof(EventsPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}