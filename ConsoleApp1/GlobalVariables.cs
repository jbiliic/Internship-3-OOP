using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Classes;

namespace ConsoleApp1
{
    internal static class GlobalVariables
    {
        public static List<User> userDataBase { get; set; } = new List<User>();
        public static List<Flight> flightDataBase { get; set; } = new List<Flight>();
        public static List<Plane> planeDataBase { get; set; } = new List<Plane>();
        public static List<CabinCrew> cabinCrewDataBase { get; set; } = new List<CabinCrew>();
        public static List<CabinMember> cabinMemberDataBase { get; set; } = new List<CabinMember>();
    }
}
