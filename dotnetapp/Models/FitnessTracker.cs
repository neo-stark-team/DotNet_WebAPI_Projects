using System;

namespace dotnetapp.Models
{
    public class FitnessTracker
    {
        public int Id { get; set; }
        public DateTime Workout_Date { get; set; }
        public int Steps { get; set; }
        public double Distance_km { get; set; }
        public int CaloriesBurned { get; set; }
        public int HeartRate { get; set; }
        public double SleepDuration { get; set; }


    }
}
