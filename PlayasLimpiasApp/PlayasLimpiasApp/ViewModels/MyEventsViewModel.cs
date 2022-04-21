using PlayasLimpiasApp.Models;
using PlayasLimpiasApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace PlayasLimpiasApp.ViewModels
{
    public class MyEventsViewModel : BindableObject
    {
        //Properties and commands
        public ObservableCollection<Event> MyEvents { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }
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
        public MyEventsViewModel()
        {
             MyEvents = new ObservableCollection<Event>();
            //loadSample();

            //Initialize commands
            RefreshCommand = new AsyncCommand(Refresh);
            SelectedCommand = new AsyncCommand<object>(Selected);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Event>(Remove);
            Refresh();
        }

        private async Task Remove(Event e)
        {
            e.AmIvolunteer = false;
            await PlayasLimpiasDB.UpdateEvent(e);
            await Refresh();
        }


        //private async Task loadSample()
        //{
        //    Event e = new Event("Playa Sucia", "db1", "Fajardo", 3, DateTime.Now);
        //    Event ev = new Event("Playa Sucia", "db2", "Fajardo", 3, DateTime.Now);
        //    Events.Add(e);
        //    Events.Add(ev);
        //    Events.Add(e);
        //    Events.Add(ev);
        //    Events.Add(e);
        //    Events.Add(ev);
        //    Events.Add(e);
        //    Events.Add(ev);
        //}

        private async Task Add()
        {
            var name = await App.Current.MainPage.DisplayPromptAsync("Name of Event", "Name:", "Ok");
            var image = await App.Current.MainPage.DisplayPromptAsync("Image", "Image:", "Ok");
            var location = await App.Current.MainPage.DisplayPromptAsync("Loaction", "Beach Loaction:", "Ok");
            var numVol = await App.Current.MainPage.DisplayPromptAsync("Volunteers", "Volunteers Required for event: ", "Ok");
            //var deadline = await App.Current.MainPage.DisplayPromptAsync("Deadline", "Event Date: ", "Ok");
            int numVolInt = Convert.ToInt32(numVol);
            //DateTime deadlineDate = Convert.ToDateTime(deadline);

            Event e = new Event
            {
                Name = name,
                Image = image,
                Location = location,
                NumVolunteersReq = numVolInt,
                Deadline = DateTime.Now
            };
            await PlayasLimpiasDB.AddEvent(e);
            await Refresh();
        }

        private async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            MyEvents.Clear();

            IEnumerable<Event> updatedEventsList = await PlayasLimpiasDB.GetMyEvents();

            foreach (var e in updatedEventsList)
            {
                MyEvents.Add(e);
            }

            IsBusy = false;
        }

        private async Task Selected(object o)
        {
            var e = o as Event;
            if (e == null)
                return;

            SelectedEvent = null;

            await Application.Current.MainPage.DisplayAlert("Selection TEST", $"You have selected {e.Name} event.", "Ok");


        }
    }
}
