using PlayasLimpiasApp.Models;
using PlayasLimpiasApp.Services;
using PlayasLimpiasApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PlayasLimpiasApp.ViewModels
{
    public class EventsViewModel : BindableObject
    {   
        //Properties and commands
        public ObservableCollection<Event> Events { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }
        public AsyncCommand<Event> VolunteerCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Event> RemoveCommand { get; }
        bool isBusy;
        public bool IsBusy 
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }
        Event selectedEvent;
        public Event SelectedEvent 
        {
            get => selectedEvent;
            set
            {
                selectedEvent = value;
                OnPropertyChanged();
            }
        }
        public EventsViewModel()
        {
            Events = new ObservableCollection<Event>();
            //loadSample();

            //Initialize commands
            RefreshCommand = new AsyncCommand(Refresh);
            SelectedCommand = new AsyncCommand<object>(Selected);
            VolunteerCommand = new AsyncCommand<Event>(Volunteer);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Event>(Remove);
            Refresh();
        }

        private async Task Remove(Event e)
        {
            await PlayasLimpiasDB.RemoveEvent(e.Id);
            await Refresh();
        }

        public async Task Volunteer(Event e)
        {
            if (e.AmIvolunteer == true)
            {
                await Application.Current.MainPage.DisplayAlert("Slowdown cowboy", $"You are alredy a volunteer for this event.", "Ok");
            }    
            else if (e != null)
            {
                await Application.Current.MainPage.DisplayAlert("Thanks for Volunteering!", $"You are now a volunteer for '{e.Name}' event.", "Ok");

                e.AmIvolunteer = true;
                await PlayasLimpiasDB.UpdateEvent(e);
            }
        }

        //private async Task loadSample()
        //{
        //    Event e = new Event { 
        //        Name = "Playa en Fajardo necesita ayuda",
        //        Image = "db1",
        //        Location = "Fajardo, PR",
        //        NumVolunteersReq = 18,
        //        Deadline = DateTime.Now

        //    };

        //    await PlayasLimpiasDB.AddEvent(e);
            
        //}

        private async Task Add()
        {
            var route = $"{nameof(NewEventPage)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Events.Clear();

            IEnumerable<Event> updatedEventsList = await PlayasLimpiasDB.GetAllEvents();

            foreach(var e in updatedEventsList)
            {
                Events.Add(e);
            }

            IsBusy = false;
        }

        private async Task Selected(object o)
        {
            var e = o as Event;
            if (e == null)
                return;

            SelectedEvent = null;


        }
    }
}
