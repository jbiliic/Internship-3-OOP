using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes
{
    internal class CabinCrew
    {
        private static int idCounter = 1;
        private int id { get; }
        private string name { get; }
        private List<CabinMember> crew = new List<CabinMember>();
        private List<Flight> assignedFlights = new List<Flight>();
        private DateTime createdAt { get; }
        CabinCrew(string name)
        {
            this.name = name;
            this.createdAt = DateTime.Now;
            this.id = idCounter++;
        }
    }
}
