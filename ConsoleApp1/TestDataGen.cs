using System;
using System.Collections.Generic;
using ConsoleApp1.Classes;
using ConsoleApp1.enums;

namespace ConsoleApp1.TestData
{
    internal static class TestDataGenerator
    {
        public static List<Plane> GeneratePlanes()
        {
            return new List<Plane>
            {
                new Plane("Boeing 737", 180, new DateTime(2018, 3, 15)),
                new Plane("Boeing 747", 416, new DateTime(2015, 7, 22)),
                new Plane("Airbus A320", 150, new DateTime(2020, 1, 10)),
                new Plane("Airbus A380", 555, new DateTime(2019, 11, 5)),
                new Plane("Embraer E195", 120, new DateTime(2021, 6, 30)),
                new Plane("Boeing 787", 242, new DateTime(2017, 9, 18))
            };
        }

        public static List<CabinMember> GenerateCabinMembers()
        {
            return new List<CabinMember>
            {
                // Pilots
                new CabinMember("John", "Smith", cabinCrewRoles.Pilot, new DateTime(1980, 5, 15), gender.Male),
                new CabinMember("Sarah", "Johnson", cabinCrewRoles.Pilot, new DateTime(1985, 8, 22), gender.Female),
                new CabinMember("Michael", "Brown", cabinCrewRoles.Pilot, new DateTime(1978, 12, 3), gender.Male),
                new CabinMember("Emily", "Davis", cabinCrewRoles.Pilot, new DateTime(1982, 3, 30), gender.Female),
                
                // Copilots
                new CabinMember("David", "Wilson", cabinCrewRoles.Copilot, new DateTime(1990, 7, 14), gender.Male),
                new CabinMember("Jessica", "Miller", cabinCrewRoles.Copilot, new DateTime(1992, 2, 28), gender.Female),
                new CabinMember("Robert", "Taylor", cabinCrewRoles.Copilot, new DateTime(1988, 11, 10), gender.Male),
                new CabinMember("Amanda", "Anderson", cabinCrewRoles.Copilot, new DateTime(1991, 6, 5), gender.Female),
                
                // Stewardesses
                new CabinMember("Lisa", "Martinez", cabinCrewRoles.Stewardess, new DateTime(1995, 4, 18), gender.Female),
                new CabinMember("Jennifer", "Thomas", cabinCrewRoles.Stewardess, new DateTime(1993, 9, 25), gender.Female),
                new CabinMember("Maria", "Garcia", cabinCrewRoles.Stewardess, new DateTime(1994, 1, 12), gender.Female),
                new CabinMember("Susan", "Lee", cabinCrewRoles.Stewardess, new DateTime(1996, 8, 7), gender.Female),
                new CabinMember("Patricia", "Harris", cabinCrewRoles.Stewardess, new DateTime(1992, 12, 15), gender.Female),
                new CabinMember("Nancy", "Clark", cabinCrewRoles.Stewardess, new DateTime(1997, 3, 22), gender.Female)
            };
        }

        public static List<CabinCrew> GenerateCabinCrews(List<CabinMember> allMembers)
        {
            var crews = new List<CabinCrew>
            {
                new CabinCrew("Alpha Crew"),
                new CabinCrew("Bravo Crew"),
                new CabinCrew("Charlie Crew"),
                new CabinCrew("Delta Crew")
            };

            // Distribute members among crews (2 pilots, 2 copilots, 3 stewardesses per crew)
            int memberIndex = 0;

            foreach (var crew in crews)
            {
                // Add 2 pilots
                for (int i = 0; i < 2 && memberIndex < allMembers.Count; i++)
                {
                    if (allMembers[memberIndex].getRole() == cabinCrewRoles.Pilot)
                    {
                        crew.addMember(allMembers[memberIndex]);
                    }
                    memberIndex++;
                }

                // Add 2 copilots
                for (int i = 0; i < 2 && memberIndex < allMembers.Count; i++)
                {
                    if (allMembers[memberIndex].getRole() == cabinCrewRoles.Copilot)
                    {
                        crew.addMember(allMembers[memberIndex]);
                    }
                    memberIndex++;
                }

                // Add 3 stewardesses
                for (int i = 0; i < 3 && memberIndex < allMembers.Count; i++)
                {
                    if (allMembers[memberIndex].getRole() == cabinCrewRoles.Stewardess)
                    {
                        crew.addMember(allMembers[memberIndex]);
                    }
                    memberIndex++;
                }
            }

            return crews;
        }

        public static List<User> GenerateUsers()
        {
            return new List<User>
            {
                new User("James", "Wilson", "james.wilson@gmail.com", "password123", new DateTime(1985, 3, 10), gender.Male),
                new User("Mary", "Johnson", "mary.johnson@gmail.com", "password123", new DateTime(1990, 7, 22), gender.Female),
                new User("Christopher", "Brown", "chris.brown@gmail.com", "password123", new DateTime(1988, 11, 5), gender.Male),
                new User("Patricia", "Davis", "patricia.davis@gmail.com", "password123", new DateTime(1992, 2, 18), gender.Female),
                new User("Daniel", "Miller", "daniel.miller@gmail.com", "password123", new DateTime(1987, 9, 30), gender.Male),
                new User("Jennifer", "Wilson", "jennifer.wilson@gmail.com", "password123", new DateTime(1995, 4, 15), gender.Female),
                new User("Matthew", "Taylor", "matthew.taylor@gmail.com", "password123", new DateTime(1983, 12, 8), gender.Male),
                new User("Linda", "Anderson", "linda.anderson@gmail.com", "password123", new DateTime(1991, 6, 25), gender.Female),
                new User("Anthony", "Thomas", "anthony.thomas@gmail.com", "password123", new DateTime(1989, 8, 12), gender.Male),
                new User("Barbara", "Jackson", "barbara.jackson@gmail.com", "password123", new DateTime(1993, 1, 7), gender.Female)
            };
        }

        public static List<Flight> GenerateFlights(List<Plane> planes, List<CabinCrew> crews)
        {
            var flights = new List<Flight>();
            var random = new Random();

            var routes = new[]
            {
                new { Origin = "JFK", Destination = "LAX", Distance = 2475 },
                new { Origin = "LHR", Destination = "CDG", Distance = 214 },
                new { Origin = "DXB", Destination = "SIN", Distance = 3596 },
                new { Origin = "HND", Destination = "ICN", Distance = 745 },
                new { Origin = "SYD", Destination = "MEL", Distance = 443 },
                new { Origin = "FRA", Destination = "MUC", Distance = 189 },
                new { Origin = "SFO", Destination = "ORD", Distance = 1859 },
                new { Origin = "DFW", Destination = "DEN", Distance = 641 }
            };

            for (int i = 0; i < 15; i++)
            {
                var route = routes[random.Next(routes.Length)];
                var plane = planes[random.Next(planes.Count)];
                var crew = crews[random.Next(crews.Count)];

                var departureTime = DateTime.Now.AddDays(random.Next(1, 30)).AddHours(random.Next(24));
                var arrivalTime = departureTime.AddHours(random.Next(2, 12));

                var flightClass = (flightClasses)random.Next(0, 3); // Assuming 3 flight classes

                var flight = new Flight(
                    route.Origin,
                    route.Destination,
                    route.Distance,
                    departureTime,
                    arrivalTime,
                    flightClass,
                    plane,
                    crew
                );

                flights.Add(flight);
            }

            return flights;
        }

        public static void AssignPassengersToFlights(List<User> users, List<Flight> flights)
        {
            var random = new Random();

            foreach (var user in users)
            {
                // Each user gets 1-3 random flights
                int numFlights = random.Next(1, 4);
                var userFlights = new HashSet<Flight>();

                while (userFlights.Count < numFlights)
                {
                    var flight = flights[random.Next(flights.Count)];
                    if (!userFlights.Contains(flight))
                    {
                        var flightClass = (flightClasses)random.Next(0, 3);
                        user.getFlights().Add(flight, flightClass);
                        flight.addPassenger(user);
                        userFlights.Add(flight);
                    }
                }
            }
        }

        public static void GenerateAndDisplayTestData()
        {
            Console.WriteLine("=== GENERATING TEST DATA ===\n");

            // Generate all entities
            var planes = GeneratePlanes();
            var cabinMembers = GenerateCabinMembers();
            var cabinCrews = GenerateCabinCrews(cabinMembers);
            var users = GenerateUsers();
            var flights = GenerateFlights(planes, cabinCrews);

            // Assign passengers to flights
            AssignPassengersToFlights(users, flights);

            GlobalVariables.planeDataBase = planes;
            GlobalVariables.cabinMemberDataBase = cabinMembers;
            GlobalVariables.cabinCrewDataBase = cabinCrews;
            GlobalVariables.userDataBase = users;
            GlobalVariables.flightDataBase = flights;

            // Display generated data
            Console.WriteLine($"Generated {planes.Count} planes");
            Console.WriteLine($"Generated {cabinMembers.Count} cabin members");
            Console.WriteLine($"Generated {cabinCrews.Count} cabin crews");
            Console.WriteLine($"Generated {users.Count} users");
            Console.WriteLine($"Generated {flights.Count} flights\n");

            // Display sample data
            Console.WriteLine("=== SAMPLE PLANES ===");
            for (int i = 0; i < Math.Min(3, planes.Count); i++)
            {
                Console.WriteLine($"Plane: {planes[i].getCapacity()} capacity");
            }

            Console.WriteLine("\n=== SAMPLE FLIGHTS ===");
            for (int i = 0; i < Math.Min(3, flights.Count); i++)
            {
                flights[i].printFlightInfo();
            }

            Console.WriteLine("\n=== SAMPLE USERS ===");
            for (int i = 0; i < Math.Min(2, users.Count); i++)
            {
                Console.WriteLine($"User: {users[i].getEmail()}");
                users[i].printAllFlights();
                Console.WriteLine();
            }
        }
    }
}