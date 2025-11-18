using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.screens;
using ConsoleApp1.TestData;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestDataGenerator.GenerateAndDisplayTestData();
            while (true)
            {
                Console.Clear();
                Console.Write("1 - Putnici\r\n2 - Letovi\r\n3 – Avioni\r\n4 – Posada\r\n5 – Izlaz iz programa\r\nUnos:");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        UserScreen.userScreen();
                        break;
                    case '2':
                        FlightScreen.flightScreen();
                        break;
                    case '3':
                        PlaneScreen.planeScreen();
                        break;
                    case '4':
                        break;
                    case '5':
                        Environment.Exit(0);
                        break;


                }            
            }
        }
    }
}
