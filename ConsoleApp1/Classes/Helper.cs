using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes
{
    public static class Helper
    {

        public static DateTime getAndValidateDateOfBirth(string inputType)
        {
            while (true)
            {
                Console.Clear();
                Console.Write($"Unesite {inputType} u formatu dd-mm-yyyy: ");
                var input = Console.ReadLine();
                try
                {
                    DateTime toReturn = DateTime.ParseExact(input, "dd-MM-yyyy", null);
                    if (toReturn > DateTime.Now || toReturn < DateTime.Now.AddYears(-100))
                    {
                        Console.Write("\nUneseni datum je u buducnosti ili je previse u proslosti!!! Pritisnite enter te pokusajte ponovno");
                        Console.ReadKey();
                        continue;
                    }
                    Console.Clear();
                    return toReturn;
                }
                catch (Exception)
                {
                    Console.Write("\nNeispravan format datuma!!! Pritisnite enter te pokusajte ponovno");
                    Console.ReadKey();
                }
            }
        }

        public static string getAndValidateName(string inputType)
        {
            while (true)
            {
                Console.Clear();
                Console.Write($"\nUnesite {inputType}: ");
                var toReturn = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(toReturn) || toReturn.Length < 2 || toReturn.Length > 20 || ContainsSpecialCharacters(toReturn))
                { Console.Write($"\n{inputType} nesmije biti prazno, krace od 2 slova te duze od 20 i nesmije sadrzavati posebne znakove!!! Pritisnite enter te pokusajte ponovno"); Console.ReadKey(); continue; }
                Console.Clear();
                return toReturn;
            }
        }

        public static string getAndValidateEmail(string inputType)
        {
            while (true)
            {
                Console.Clear();
                Console.Write($"\nUnesite {inputType}: ");
                var toReturn = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(toReturn) || !toReturn.EndsWith("@gmail.com"))
                { Console.Write($"\n{inputType} nesmije biti prazno, te mora zavrsavati sa @gmail.com!!! Pritisnite enter te pokusajte ponovno"); Console.ReadKey(); continue; }
                Console.Clear();
                return toReturn;
            }
        }

        public static string getAndValidatePassword(string inputType)
        {
            while (true)
            {
                Console.Clear();
                Console.Write($"\nUnesite {inputType}: ");
                var toReturn = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(toReturn) || toReturn.Length < 6 || !ContainsSpecialCharacters(toReturn))
                { Console.Write($"\n{inputType} nesmije biti prazno, te mora biti duze od 6 slova i sadrzavati posebne znakove!!! Pritisnite enter te pokusajte ponovno"); Console.ReadKey(); continue; }
                Console.Clear();
                return toReturn;
            }
        }
        public static int getAndValidateEnum(string prompt, int minValue, int maxValue) { 
            while (true)
            {
                Console.Clear();
                Console.Write($"\n{prompt}: ");
                var input = Console.ReadLine();
                int toReturn;
                if (!int.TryParse(input, out toReturn) || toReturn < minValue || toReturn > maxValue)
                {
                    Console.Write($"\nUnos mora biti broj izmedju {minValue} i {maxValue}!!! Pritisnite enter te pokusajte ponovno");
                    Console.ReadKey();
                    continue;
                }
                Console.Clear();
                return toReturn;
            }
        }
        public static int getAndValidateInputInt(string inputType)
        {
            while (true)
            {
                Console.Clear();
                Console.Write($"Unesite {inputType}: ");
                var toReturn = Console.ReadLine();
                try
                {
                    var toReturnParsed = int.Parse(toReturn);
                    if (toReturnParsed < 0)
                    {
                        Console.Write("\nBroj mora biti veca od 0!!! Pritisnite enter te pokusajte ponovno");
                        Console.ReadKey();
                        continue;
                    }
                    Console.Clear();
                    return toReturnParsed;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.Write("\nNeispravan format!!! Pritisnite enter te pokusajte ponovno");
                    Console.ReadKey();
                    continue;
                }
            }
        }

        public static bool ContainsSpecialCharacters(string name)
        {
            foreach (char c in name)
            {
                if (!char.IsLetter(c))
                {
                    return true;
                }
            }
            return false;
        }
        public static void clearDisplAndDisplMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.ReadKey();
        }
        public static bool DoTimeRangesOverlap( DateTime start1, DateTime end1,DateTime start2, DateTime end2)
        {
            return start1 < end2 && start2 < end1;
        }
        public static bool waitForConfirmation() { 
            while (true)
            {
                Console.Clear();
                Console.Write("\nJeste li sigurni (da/ne): ");
                switch (Console.ReadLine()) {
                    case "da":
                    case "DA":
                    case "Da":
                        Console.Clear();
                        return true;
                        break;
                    case "ne":
                    case "NE":
                    case "Ne":
                        Console.Clear();
                        return false;
                        break;
                    default:
                        Console.Write("\nNeispravan unos!!! Pritisnite enter te pokusajte ponovno");
                        Console.ReadKey();
                        break;
                }
            }
        }
        public static DateTime getAndValidateTime(string inputType) {
            while (true)
            {
                Console.Clear();
                Console.Write($"Unesite {inputType} u formatu dd-mm-yyyy HH:mm: ");
                var input = Console.ReadLine();
                try
                {
                    DateTime toReturn = DateTime.ParseExact(input, "dd-MM-yyyy HH:mm", null);
                    if (toReturn < DateTime.Now || toReturn > DateTime.Now.AddYears(10))
                    {
                        Console.Write("\nUneseni datum je u proslosti ili previse u buducnosti!!! Pritisnite enter te pokusajte ponovno");
                        Console.ReadKey();
                        continue;
                    }
                    Console.Clear();
                    return toReturn;
                }
                catch (Exception)
                {
                    Console.Write("\nNeispravan format datuma!!! Pritisnite enter te pokusajte ponovno");
                    Console.ReadKey();
                }
            }

        }
        public static Dictionary<int, Plane> getAvailablePlanes(DateTime departureTime, DateTime arrivalTime)
        {
            var availablePlanes = new Dictionary<int, Plane>();
            foreach (var plane in GlobalVariables.planeDataBase)
            {
                var hasOverLappingFlight = false;
                foreach (var flight in plane.GetFlights())
                {
                    if (Helper.DoTimeRangesOverlap(departureTime, arrivalTime, flight.getDepartureTime(), flight.getArrvalTime()))
                    {
                        hasOverLappingFlight = true;
                        break;
                    }
                }
                if (!hasOverLappingFlight)
                    availablePlanes.Add(plane.getId(), plane);
            }
            return availablePlanes;
        }
        public static Dictionary<int, CabinCrew> getAvailableCrew(DateTime departureTime, DateTime arrivalTime)
        {
            var availableCrew = new Dictionary<int, CabinCrew>();
            foreach (var crew in GlobalVariables.cabinCrewDataBase.Where(c => c.getCrew().Count()>=4))
            {
                var hasOverLappingFlight = false;
                foreach (var flight in crew.getFlights())
                {
                    if (Helper.DoTimeRangesOverlap(departureTime, arrivalTime, flight.getDepartureTime(), flight.getArrvalTime()))
                    {
                        hasOverLappingFlight = true;
                        break;
                    }
                }
                if (!hasOverLappingFlight)
                    availableCrew.Add(crew.getId(), crew);
            }
            return availableCrew;
        }
        public static Dictionary<int, Flight> canBeDeleted() { 
            var canBeDeleted = new Dictionary<int, Flight>();
            foreach (var flight in GlobalVariables.flightDataBase) { 
                if(flight.getPassengers().Count()<0.5*flight.getPlane().getCapacity() && flight.getDepartureTime()>DateTime.Now.AddHours(24))
                    canBeDeleted.Add(flight.getId(), flight);
            }
            return canBeDeleted;
        }
        public static string getAndValidatePlaneName(string inputType)
        {
            while (true)
            {
                Console.Clear();
                Console.Write($"\nUnesite {inputType}: ");
                var toReturn = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(toReturn) || toReturn.Length < 2 || toReturn.Length > 20)
                { Console.Write($"\n{inputType} nesmije biti prazno, krace od 2 slova te duze od 20!!! Pritisnite enter te pokusajte ponovno"); Console.ReadKey(); continue; }
                Console.Clear();
                return toReturn;
            }
        }
        public static DateTime getAndValidatePlaneMakeYear(string inputType)
        {
            while (true)
            {
                Console.Clear();
                Console.Write($"Unesite {inputType} u formatu yyyy: ");
                var input = Console.ReadLine();
                try
                {
                    DateTime toReturn = DateTime.ParseExact(input, "yyyy", null);
                    if (toReturn > DateTime.Now || toReturn < DateTime.Now.AddYears(-100))
                    {
                        Console.Write("\nUneseni datum je u buducnosti ili je previse u proslosti!!! Pritisnite enter te pokusajte ponovno");
                        Console.ReadKey();
                        continue;
                    }
                    Console.Clear();
                    return toReturn;
                }
                catch (Exception)
                {
                    Console.Write("\nNeispravan format datuma!!! Pritisnite enter te pokusajte ponovno");
                    Console.ReadKey();
                }
            }
        }
        public static void printFlightList(List<Flight> flights)
        {
            foreach (var flight in flights)
            {
                flight.printFlightInfo();
            }
        }
    }
}
