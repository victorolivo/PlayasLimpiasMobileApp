using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;

namespace PlayasLimpiasApp.Models
{
    public class Event
    {
        

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; } = "db1";
        public string Location { get; set; }
        public int NumVolunteersReq { get; set; }
        public DateTime Deadline { get; set; }
        public bool AmIvolunteer { get; set; } = false;
    }
}
