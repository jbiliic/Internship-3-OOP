using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.enums;

namespace ConsoleApp1.Classes
{
    public class User
    {
        private static int idCounter = 1;
        private int id { get; }
        private string name { get; }
        private string lastName { get; }
        private string email { get; }
        private string password { get; }
        private gender userGender { get; }
        private DateTime createdAt { get; }
        private DateTime dateOfBirth { get; }

        private Dictionary<Flight, flightClasses> flights = new Dictionary<Flight, flightClasses>();
        private Dictionary<Flight, flightClasses> favourites = new Dictionary<Flight, flightClasses>();

        public User(string name, string lastName, string email, string password, DateTime dateOfBirth, gender userGender)
        {
            this.name = name;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
            this.dateOfBirth = dateOfBirth;
            this.userGender = userGender;
            this.createdAt = DateTime.Now;
            this.id = idCounter++;
        }
        public void printAllFlights()
        {
            Console.WriteLine($"Letovi za korisnika {name} {lastName}:");
            foreach (var flight in flights)
            {
                flight.Key.printFlightInfo();
            }
        }
        public string getEmail()=> email;
        public string getPassword() => password;
        public Dictionary<Flight, flightClasses> getFlights() => flights;
        public Dictionary<Flight, flightClasses> getFavourites() => favourites;
        public void addToFavourites(Flight flight, flightClasses flightClass)
        {
            favourites.Add(flight, flightClass);
        }

    }
}
