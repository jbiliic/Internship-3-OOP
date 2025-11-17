using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.enums;

namespace ConsoleApp1.Classes
{
    public class Flight
    {
        private static int idCounter = 1;
        private int id { get; }
        private int distance { get; }
        private string origin { get; }
        private string destination { get; }
        private string name => $"{origin}-{destination}";
        private DateTime departureTime { get; set; }
        private DateTime arrivalTime { get; set; }
        private DateTime createdAt { get; }
        private DateTime updatedAt { get; }
        public TimeSpan duration => arrivalTime - departureTime;
        private List<User> passengers = new List<User>() ;
        private Plane assignedPlane { get; set; }
        private CabinCrew assignedCrew { get; set; }


        public Flight(string origin, string destination,int distance , DateTime departureTime, DateTime arrivalTime,Plane assignedPlane , CabinCrew assignedCrew)
        {
            this.origin = origin;
            this.destination = destination;
            this.distance = distance;
            this.departureTime = departureTime;
            this.arrivalTime = arrivalTime;
            this.assignedPlane = assignedPlane;
            this.assignedCrew = assignedCrew;
            this.createdAt = DateTime.Now;
            this.updatedAt = DateTime.Now;
            this.id = idCounter++;
        }
        public void printFlightInfo()
        {
            Console.WriteLine($"{id} - {name} - {departureTime} - {arrivalTime} - {distance}Kms - {duration}");
        }
        public List<User> getPassengers() => passengers;
        public Plane getPlane() => assignedPlane;
        public CabinCrew getCrew() => assignedCrew;
        public DateTime getDepartureTime() => departureTime;
        public DateTime getArrvalTime() => arrivalTime;
        public int getId() => id; 
        public void addPassenger(User user)
        {
            passengers.Add(user);
        }
        public string getName() => name;
        public void removePassenger(User user)
        {
            passengers.Remove(user);
        }
        public void addCabinCrew(CabinCrew crew)
        {
            assignedCrew = crew;
        }
        public void addPlane(Plane plane)
        {
            assignedPlane = plane;
        }
        
    }
}
