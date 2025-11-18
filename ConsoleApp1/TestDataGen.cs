using System;
using System.Collections.Generic;
using ConsoleApp1.Classes;
using ConsoleApp1.enums;

namespace ConsoleApp1.TestData
{
    internal static class TestDataGenerator
    {
        // ------------------------
        // 1. PLANES
        // ------------------------
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

        // ------------------------
        // 2. CABIN MEMBERS
        // ------------------------
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
                new CabinMember("Nancy", "Clark", cabinCrewRoles.Stewardess, new DateTime(1997, 3, 22), gender.Female),
                new CabinMember("Elizabeth", "Lewis", cabinCrewRoles.Stewardess, new DateTime(1994, 7, 8), gender.Female),
                new CabinMember("Karen", "Walker", cabinCrewRoles.Stewardess, new DateTime(1996, 11, 19), gender.Female)
            };
        }

        // ------------------------
        // 3. CABIN CREWS (1 pilot + 1 copilot + 2 stjuardese)
        // ------------------------
        public static List<CabinCrew> GenerateCabinCrews(List<CabinMember> members)
        {
            var crews = new List<CabinCrew>
            {
                new CabinCrew("Alpha Crew"),
                new CabinCrew("Bravo Crew"),
                new CabinCrew("Charlie Crew"),
                new CabinCrew("Delta Crew")
            };

            // Separate groups
            var pilots = members.FindAll(x => x.getRole() == cabinCrewRoles.Pilot);
            var copilots = members.FindAll(x => x.getRole() == cabinCrewRoles.Copilot);
            var stjs = members.FindAll(x => x.getRole() == cabinCrewRoles.Stewardess);

            // Assign
            for (int i = 0; i < crews.Count; i++)
            {
                crews[i].addMember(pilots[i]);               // 1 pilot
                crews[i].addMember(copilots[i]);            // 1 copilot
                crews[i].addMember(stjs[i * 2]);            // 2 stjuardese
                crews[i].addMember(stjs[i * 2 + 1]);
            }

            return crews;
        }
        // ------------------------
        // 4. FLIGHTS (complete linking)
        // ------------------------
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
                // choose route
                var route = routes[random.Next(routes.Length)];

                // choose plane & crew
                var plane = planes[random.Next(planes.Count)];
                var crew = crews[random.Next(crews.Count)];

                // departure in next 1–30 days
                var departureTime = DateTime.Now
                    .AddDays(random.Next(1, 30))
                    .AddHours(random.Next(0, 24))
                    .AddMinutes(random.Next(0, 60));

                // arrival 2–12 hours later
                var arrivalTime = departureTime
                    .AddHours(random.Next(2, 12))
                    .AddMinutes(random.Next(0, 60));

                // create flight
                var flight = new Flight(
                    route.Origin,
                    route.Destination,
                    route.Distance,
                    departureTime,
                    arrivalTime,
                    plane,
                    crew
                );

                // ADD FLIGHT TO GLOBAL LIST
                flights.Add(flight);

                // -----------------------
                // BI-DIRECTIONAL LINKING
                // -----------------------

                // 1. Link plane → flight
                plane.addFlight(flight);

                // 2. Link crew → flight
                crew.addFlight(flight);

                // 3. Link flight → crew members (if needed)
                //    Only if your CabinMember has addFlight()
                
            }

            return flights;
        }
        // ------------------------
        // 5. USERS
        // ------------------------
        public static List<User> GenerateUsers()
        {
            return new List<User>
            {
                new User("James", "Wilson", "james.wilson@gmail.com", "pass123!", new DateTime(1985, 3, 10), gender.Male),
                new User("Mary", "Johnson", "mary.johnson@gmail.com", "pass123!", new DateTime(1990, 7, 22), gender.Female),
                new User("Christopher", "Brown", "chris.brown@gmail.com", "pass123!", new DateTime(1988, 11, 5), gender.Male),
                new User("Patricia", "Davis", "patricia.davis@gmail.com", "pass123!", new DateTime(1992, 2, 18), gender.Female),
                new User("Daniel", "Miller", "daniel.miller@gmail.com", "pass123!", new DateTime(1987, 9, 30), gender.Male),
                new User("Jennifer", "Wilson", "jennifer.wilson@gmail.com", "pass123!", new DateTime(1995, 4, 15), gender.Female),
                new User("Matthew", "Taylor", "matthew.taylor@gmail.com", "pass123!", new DateTime(1983, 12, 8), gender.Male),
                new User("Linda", "Anderson", "linda.anderson@gmail.com", "pass123!", new DateTime(1991, 6, 25), gender.Female),
                new User("Anthony", "Thomas", "anthony.thomas@gmail.com", "pass123!", new DateTime(1989, 8, 12), gender.Male),
                new User("Barbara", "Jackson", "barbara.jackson@gmail.com", "pass123!", new DateTime(1993, 1, 7), gender.Female)
            };
        }
        // ------------------------
        // 6. ASSIGN PASSENGERS TO FLIGHTS
        // ------------------------
        public static void AssignPassengersToFlights(List<User> users, List<Flight> flights)
        {
            var random = new Random();

            foreach (var user in users)
            {
                // Each user gets 1–3 random flights
                int count = random.Next(1, 4);
                var selectedFlights = new HashSet<Flight>();

                while (selectedFlights.Count < count)
                {
                    var f = flights[random.Next(flights.Count)];

                    if (!selectedFlights.Contains(f))
                    {
                        var cls = (flightClasses)random.Next(0, 3);

                        // USER → FLIGHT
                        user.getFlights().Add(f, cls);

                        // FLIGHT → USER
                        f.addPassenger(user);

                        selectedFlights.Add(f);
                    }
                }
            }
        }
        // ------------------------
        // 7. MAIN GENERATOR
        // ------------------------
        public static void GenerateAndDisplayTestData()
        {
            Console.WriteLine("=== GENERATING TEST DATA ===");

            var planes = GeneratePlanes();
            var members = GenerateCabinMembers();
            var crews = GenerateCabinCrews(members);
            var users = GenerateUsers();
            var flights = GenerateFlights(planes, crews);

            AssignPassengersToFlights(users, flights);

            // Save globally
            GlobalVariables.planeDataBase = planes;
            GlobalVariables.cabinMemberDataBase = members;
            GlobalVariables.cabinCrewDataBase = crews;
            GlobalVariables.userDataBase = users;
            GlobalVariables.flightDataBase = flights;

            // Simple output
            Console.WriteLine($"\nPlanes: {planes.Count}");
            Console.WriteLine($"CabinMembers: {members.Count}");
            Console.WriteLine($"CabinCrews: {crews.Count}");
            Console.WriteLine($"Users: {users.Count}");
            Console.WriteLine($"Flights: {flights.Count}");

            Console.WriteLine("\n=== CREW STRUCTURE ===");
            foreach (var crew in crews)
            {
                Console.WriteLine($"{crew.getName()} → {crew.getCrew().Count} members, {crew.getFlights().Count} flights");
            }

            Console.WriteLine("\n=== SAMPLE FLIGHTS ===");
            for (int i = 0; i < Math.Min(3, flights.Count); i++)
                flights[i].printFlightInfo();

            Console.WriteLine("\n=== SAMPLE USERS ===");
            for (int i = 0; i < Math.Min(2, users.Count); i++)
            {
                Console.WriteLine(users[i].getEmail());
                users[i].printAllFlights();
            }

            Console.WriteLine("\n=== DATA READY ===");
        }
    }
}
