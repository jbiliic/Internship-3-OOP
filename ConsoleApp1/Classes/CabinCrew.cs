using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.enums;

namespace ConsoleApp1.Classes
{
    public class CabinCrew
    {
        private static int idCounter = 1;
        private int id { get; }
        private int numOfPilots { get; set; }
        private int numOfCopilots { get; set; }
        private int numOfStewardesses { get; set; }
        private string name { get; }
        private List<CabinMember> crew = new List<CabinMember>();
        private List<Flight> assignedFlights = new List<Flight>();
        private DateTime createdAt { get; }
        public CabinCrew(string name)
        {
            this.name = name;
            this.createdAt = DateTime.Now;
            this.id = idCounter++;
            
        }
        public void addMember(CabinMember member)
        {
            crew.Add(member);
            
        }
        public List<Flight> getFlights() => assignedFlights;
        public int getId() => id;
        public List<CabinMember> getCrew() => crew;
        public void printCrewInfo() { 
            Console.WriteLine($"ID: {id} - Naziv posade: {name}");
            foreach (var member in crew) {
                Console.Write("\t");
                member.printInfo();
                
            }
            Console.Write("\n");
        }
        public void addFlight(Flight flight)
        {
            assignedFlights.Add(flight);
        }
        public string getName() => name;
    }
}
