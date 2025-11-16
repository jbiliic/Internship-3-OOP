using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.enums;

namespace ConsoleApp1.Classes
{
    internal class CabinMember
    {
        private static int idCounter = 0;
        private int id { get; }
        private string name { get; }
        private string lastName { get; }
        private cabinCrewRoles role { get; }
        private gender memberGender { get; }
        private DateTime createdAt { get; }
        private DateTime dateOfBirth { get; }
        private CabinCrew currentCrew { get; set; }

        CabinMember(string name, string lastName, cabinCrewRoles role, DateTime dateOfBirth,gender userGender)
        {
            this.name = name;
            this.lastName = lastName;
            this.role = role;
            this.dateOfBirth = dateOfBirth;
            this.memberGender = userGender;
            this.createdAt = DateTime.Now;
            this.id = idCounter++;
        }
    }
}
