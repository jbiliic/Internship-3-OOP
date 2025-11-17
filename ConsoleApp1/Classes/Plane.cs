using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.enums;

namespace ConsoleApp1.Classes
{
    internal class Plane
    {
        private static int idCounter = 1;
        private int id { get; }
        private string model { get; }
        private int capacity { get; }
        private DateTime createdAt { get; }
        private DateTime makeYear { get; }
        private List<Flight> flights = new List<Flight>();
        public Plane(string model, int capacity, DateTime makeYear)
        {
            this.model = model;
            this.capacity = capacity;
            this.createdAt = DateTime.Now;
            this.makeYear = makeYear;
            this.id = idCounter++;
        }
        public int getCapacity() => capacity;
    }
}
