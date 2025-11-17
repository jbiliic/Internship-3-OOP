using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.enums;

namespace ConsoleApp1.Classes
{
    internal class CabinCrew
    {
        private static int idCounter = 1;
        private int id { get; }
        private int numOfPilots { get; set; }
        private int numOfCopilots { get; set; }
        private int nomOfStewardesses { get; set; }
        private string name { get; }
        private List<CabinMember> crew = new List<CabinMember>();
        private List<Flight> assignedFlights = new List<Flight>();
        private DateTime createdAt { get; }
        public CabinCrew(string name)
        {
            this.name = name;
            this.createdAt = DateTime.Now;
            this.id = idCounter++;
            this.numOfPilots = 0;
            this.numOfCopilots = 0;
            this.nomOfStewardesses = 0;
        }
        public void addMember(CabinMember member)
        {
            crew.Add(member);
            switch (member.getRole())
            {
                case cabinCrewRoles.Pilot:
                    numOfPilots++;
                    break;
                case cabinCrewRoles.Copilot:
                    numOfCopilots++;
                    break;
                case cabinCrewRoles.Stewardess:
                    nomOfStewardesses++;
                    break;
            }
        }
    }
}
