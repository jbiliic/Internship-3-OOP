using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.enums;

namespace ConsoleApp1.Classes
{
    public class CabinMember
    {
        private static int idCounter = 1;
        private int id { get; }
        private string name { get; }
        private string lastName { get; }
        private cabinCrewRoles role { get; }
        private gender memberGender { get; }
        private DateTime createdAt { get; }
        private DateTime dateOfBirth { get; }
        private CabinCrew currentCrew { get; set; }

        public CabinMember(string name, string lastName, cabinCrewRoles role, DateTime dateOfBirth,gender userGender)
        {
            this.name = name;
            this.lastName = lastName;
            this.role = role;
            this.dateOfBirth = dateOfBirth;
            this.memberGender = userGender;
            this.createdAt = DateTime.Now;
            this.id = idCounter++;
            this.currentCrew = null;
        }
        public cabinCrewRoles getRole() => role;
        public void printInfo()
        {
            Console.WriteLine($"ID: {id} - Ime: {name} {lastName} - Uloga: {role} - Datum rodjenja: {dateOfBirth}");
        }
        public CabinCrew getCrew() => currentCrew;
        public int getId() => id;
        public void setCrew(CabinCrew crew)
        {
            this.currentCrew = crew;
        }
    }
}
