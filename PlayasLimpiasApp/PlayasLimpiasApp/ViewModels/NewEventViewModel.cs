using PlayasLimpiasApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace PlayasLimpiasApp.ViewModels
{
    public class NewEventViewModel : BindableObject
    {
        public string Name { get; set; }
        public string Image { get; set; } = "db1";
        public string Location { get; set; }
        public int NumVolunteersReq { get; set; } = 3;
        public DateTime Deadline { get; set; } = DateTime.Now;
        public bool AmIvolunteer { get; set; } = false;

        public AsyncCommand SaveCommand { get; }

        public NewEventViewModel()
        {
            SaveCommand = new AsyncCommand(Save);
        }

        private async Task Save()
        {
            //Basic Input Validation
            if(string.IsNullOrWhiteSpace(Name)){
                await Application.Current.MainPage.DisplayAlert("More information needed", "Please enter a name for your event", "Ok");
            }
            else if (string.IsNullOrWhiteSpace(Location))
            {
                await Application.Current.MainPage.DisplayAlert("More information needed", "Please enter the location for your event", "Ok");
            }
            else if (NumVolunteersReq < 2)
            {
                await Application.Current.MainPage.DisplayAlert("Invalid Entry", "Please enter a number of volunteers required for your event\n( 2 minimum )", "Ok");
            }
            else{
                Event e = new Event
                {
                    Name = this.Name,
                    Image = this.Image,
                    Location = this.Location,
                    NumVolunteersReq = this.NumVolunteersReq,
                    Deadline = this.Deadline,
                    AmIvolunteer = this.AmIvolunteer
                };

                await Services.PlayasLimpiasDB.AddEvent(e);

                await Application.Current.MainPage.DisplayAlert("Making our world a better place!", $"Your Event '{e.Name}' have been created successfully", "Ok");

                await Shell.Current.GoToAsync("..");
            }
            
        }
    }
}
