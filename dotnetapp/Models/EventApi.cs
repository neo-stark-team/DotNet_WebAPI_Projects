using System;

namespace dotnetapp.Models
{
    public class EventApi
    {
        public int Id { get; set; }
        public string Event_Name { get; set; }
        public string Event_Type { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string Location { get; set; }

    }
}
