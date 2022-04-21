using PlayasLimpiasApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PlayasLimpiasApp.Services
{
    internal static class PlayasLimpiasDB
    {
        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Events.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Event>();
        }

        public static async Task AddEvent(Event e)
        {
            await Init();
            await db.InsertAsync(e);
        }

        public static async Task RemoveEvent(int id)
        {
            await Init();
            await db.DeleteAsync<Event>(id);
        }

        public static async Task<IEnumerable<Event>> GetAllEvents()
        {
            await Init();
            var events = await db.Table<Event>().ToListAsync();
            return events;
        }

        public static async Task<IEnumerable<Event>> GetMyEvents()
        {
            await Init();
            var events = await (from e in db.Table<Event>()
                               where e.AmIvolunteer == true
                               select e).ToListAsync();
            return events;
        }

        public static async Task UpdateEvent(Event e)
        {
            await Init();
            await db.UpdateAsync(e);
        }
    }
}
