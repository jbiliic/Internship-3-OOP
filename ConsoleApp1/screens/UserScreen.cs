using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Classes;
using ConsoleApp1.enums;

namespace ConsoleApp1.screens
{
    internal class UserScreen
    {
        public static void userScreen()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("1 - Registracija\r\n2 - Prijava\r\n3 - Izlaz iz programa\r\nUnos:");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        var registratedUser = registrationScreen();
                        if (registratedUser.getFlights().Count != 0)
                        {
                            postRegScreen(registratedUser);
                        }
                        else
                        {
                            Helper.clearDisplAndDisplMessage("Nemate letova za prikaz. Pritisnite bilo koju tipku za nastavak");
                        }
                        break;
                    case '2':
                        var loggedInUser = loginScreen();
                        if (loggedInUser.getFlights().Count != 0)
                        {
                            postRegScreen(loggedInUser);
                        }
                        else
                        {
                            Helper.clearDisplAndDisplMessage("Nemate letova za prikaz. Pritisnite bilo koju tipku za nastavak");
                        }
                        break;
                    case '3':
                        return;
                    default:
                        Console.WriteLine("\n Neispravan unos pokusajte nonovno");
                        break;
                }
            }
        }
        public static User registrationScreen()
        {
            Console.Clear();

            var name = Helper.getAndValidateName("Ime");
            var lastName = Helper.getAndValidateName("Prezime");
            var email = Helper.getAndValidateEmail("Email");
            var password = Helper.getAndValidatePassword("Lozinka");
            var dateOfBirth = Helper.getAndValidateDateOfBirth("Datum rodjenja");
            var genderInput = Helper.getAndValidateEnum(" 1-Muski\n2-Zenski \n3-Drugo\nUnos: ", 1, 3);

            var newUser = new User(name, lastName, email, password, dateOfBirth, (gender)genderInput);
            GlobalVariables.userDataBase.Add(newUser);
            Helper.clearDisplAndDisplMessage("Uspjesna registracija! Pritisnite bilo koju tipku za nastavak");
            return newUser;
        }
        public static User loginScreen()
        {
            while (true)
            {
                Console.Clear();
                if (GlobalVariables.userDataBase.Count() == 0)
                {
                    Console.WriteLine("Nema korisnika pa prijava nije moguca");
                    return null;
                }
                var email = Helper.getAndValidateEmail("Email");
                var password = Helper.getAndValidatePassword("Lozinka");
                foreach (var user in GlobalVariables.userDataBase)
                {
                    if (user.getEmail() == email && user.getPassword() == password)
                    {
                        Helper.clearDisplAndDisplMessage("Uspjesna prijava! Pritisnite bilo koju tipku za nastavak");
                        return user;
                    }
                }
                Console.Clear();
                Console.WriteLine("\nPogresan email ili lozinka. Pritisnite 0 za povratak ili bilo sto drugo za ponovni pokusaj");
                var input = Console.ReadKey().KeyChar;
                if (input == '0') return null;
            }
        }
        public static void postRegScreen(User loggedUser)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("1 - Pregled mojih letova\r\n2 - Odabir leta\r\n3 - Pretrazivanje letova \n4 - Otkazivanje leta \n5 - Povratak \nUnos:");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        loggedUser.printAllFlights();
                        Console.ReadKey();
                        break;
                    case '2':
                        flightSelectionScreen(loggedUser);
                        break;
                    case '3':
                        break;
                    case '4':
                        break;
                    case '5':
                        return;
                    default:
                        Helper.clearDisplAndDisplMessage("Neispravan unos pokusajte nonovno");
                        break;
                }
            }
        }
        public static void flightSelectionScreen(User currentUser)
        {
            Console.Clear();
            bool doesFlightOverlap = false;
            var availableFlightsIds = new List<int>();
            foreach (var flight in GlobalVariables.flightDataBase)
            {
                if (!currentUser.getFlights().Values.Contains(flight) && flight.getPassengers().Count() < flight.getPlane().getCapacity())
                {
                    foreach (var userFlight in currentUser.getFlights().Values)
                    {
                        if (Helper.DoTimeRangesOverlap(userFlight.getDepartureTime(), userFlight.getArrvalTime(), flight.getDepartureTime(), flight.getArrvalTime()))
                        {
                            doesFlightOverlap = true;
                        }

                    }
                    if (!doesFlightOverlap)
                    {
                        flight.printFlightInfo();
                        availableFlightsIds.Add(flight.getId());
                    }
                }
            }
            while (true) { 
                var selectedFlightIdInput = Helper.getAndValidateInputInt("ID leta koji zelite odabrati ili 0 za povratak");
                if (selectedFlightIdInput == 0) return;
                if (availableFlightsIds.Contains(selectedFlightIdInput)) { 
                    var selectedFlight = GlobalVariables.flightDataBase.Find(f => f.getId() == selectedFlightIdInput);
                    var flightClassInput = Helper.getAndValidateEnum("Odaberite klasu leta:\n1-Economy\n2-Buissnes\n3-Vip\nUnos: ", 1, 3);
                    currentUser.getFlights().Add((flightClasses)flightClassInput, selectedFlight);
                    selectedFlight.addPassenger(currentUser);
                    Helper.clearDisplAndDisplMessage("Uspjesno ste odabrali let! Pritisnite bilo koju tipku za nastavak");
                    return;
                }
                Console.WriteLine("Neispravan unos pokusajte ponovno");
            }

        }
    }
}
