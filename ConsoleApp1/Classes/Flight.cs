using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.enums;

namespace ConsoleApp1.Classes
{
    internal class Flight
    {
        private static int idCounter = 0;
        private int id { get; }
        private int distance { get; }
        private string origin { get; }
        private string destination { get; }
        private string name => $"{origin} to {destination}";
        private DateTime departureTime { get; set; }
        private DateTime arrivalTime { get; set; }
        private DateTime createdAt { get; }
        private DateTime updatedAt { get; }
        public TimeSpan Duration => arrivalTime - departureTime;
        private List<User> passengers = new List<User>() ;
        private flightClasses flightClass { get; }
        private Plane assignedPlane { get; set; }
        private CabinCrew assignedCrew { get; set; }


        Flight(string origin, string destination,int distance , DateTime departureTime, DateTime arrivalTime,flightClasses flightClass,Plane assignedPlane , CabinCrew assignedCrew)
        {
            this.origin = origin;
            this.destination = destination;
            this.distance = distance;
            this.departureTime = departureTime;
            this.arrivalTime = arrivalTime;
            this.flightClass = flightClass;
            this.assignedPlane = assignedPlane;
            this.assignedCrew = assignedCrew;
            this.createdAt = DateTime.Now;
            this.updatedAt = DateTime.Now;
            this.id = idCounter++;
        }
    }
}
