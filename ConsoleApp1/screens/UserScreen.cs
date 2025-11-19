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
                Console.Write("1 - Registracija\r\n2 - Prijava\r\n0 - Izlaz iz programa\r\nUnos:");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        var registratedUser = registrationScreen();
                        if (registratedUser == null) break;
                        postRegScreen(registratedUser);
                        break;
                    case '2':
                        var loggedInUser = loginScreen();
                        if (loggedInUser == null) break;
                        postRegScreen(loggedInUser);
                        break;
                    case '0':
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
                Console.Write("1 - Pregled mojih letova\r\n2 - Odabir leta\r\n3 - Pretrazivanje letova \n4 - Otkazivanje leta \n0 - Povratak \nUnos:");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        if (loggedUser.getFlights().Count() == 0)
                        {
                            Helper.clearDisplAndDisplMessage("Nemate odabranih letova. Pritisnite bilo koju tipku za nastavak");
                            break;
                        }
                        loggedUser.printAllFlights();
                        Console.ReadKey();
                        break;
                    case '2':
                        flightSelectionScreen(loggedUser);
                        break;
                    case '3':
                        if (loggedUser.getFlights().Count() == 0)
                        {
                            Helper.clearDisplAndDisplMessage("Nemate odabranih letova. Pritisnite bilo koju tipku za nastavak");
                            break;
                        }
                        flightSearchScreen(loggedUser);
                        break;
                    case '4':
                        if (loggedUser.getFlights().Count() == 0)
                        {
                            Helper.clearDisplAndDisplMessage("Nemate odabranih letova. Pritisnite bilo koju tipku za nastavak");
                            break;
                        }
                        flightCancellationScreen(loggedUser);
                        break;
                    case '0':
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
            var availableFlightsIds = new Dictionary<int,Flight>();
            foreach (var flight in GlobalVariables.flightDataBase)
            {
                bool doesFlightOverlap = false;
                if (!currentUser.getFlights().Keys.Contains(flight) && flight.getPassengers().Count() < flight.getPlane().getCapacity())
                {
                    foreach (var userFlight in currentUser.getFlights().Keys)
                    {
                        if (Helper.DoTimeRangesOverlap(userFlight.getDepartureTime(), userFlight.getArrvalTime(), flight.getDepartureTime(), flight.getArrvalTime()))
                        {
                            doesFlightOverlap = true;
                            break;
                        }
                    }
                    if (!doesFlightOverlap)
                    {
                        
                        availableFlightsIds.Add(flight.getId(),flight);
                    }
                }
            }
            
            while (true) {
                Console.Clear();
                foreach (var flight in availableFlightsIds.Values) {
                    flight.printFlightInfo();
                    
                }
                Console.ReadKey();
                var selectedFlightIdInput = Helper.getAndValidateInputInt("ID leta koji zelite odabrati ili 0 za povratak");
                if (selectedFlightIdInput == 0) return;
                if (availableFlightsIds.Keys.Contains(selectedFlightIdInput)) { 
                    var selectedFlight = GlobalVariables.flightDataBase.Find(f => f.getId() == selectedFlightIdInput);
                    var flightClassInput = Helper.getAndValidateEnum("Odaberite klasu leta:\n1-Economy\n2-Buissnes\n3-Vip\nUnos: ", 1, 3);
                    currentUser.getFlights().Add(selectedFlight,(flightClasses)flightClassInput);
                    selectedFlight.addPassenger(currentUser);
                    Helper.clearDisplAndDisplMessage("Uspjesno ste odabrali let! Pritisnite bilo koju tipku za nastavak");
                    return;
                } else
                {
                    Console.WriteLine("Let s navedenim id-em nije dostupan");
                    Console.ReadKey();
                    continue;
                }
            }
        }
        public static void flightSearchScreen(User currentUser)
        {
            Console.Clear();
            while (true)
            {
                Console.Clear();
                Console.Write("1 - Pregled po id\r\n2 - Pregled po imenu\r\n0 - Povratak \nUnos:");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        var idInput = Helper.getAndValidateInputInt("Unesite ID leta za pretragu ili 0 za povratak");
                        if (idInput == 0) break;
                        var searchedFlight = currentUser.getFlights().Keys.ToList().Find(f => f.getId() == idInput);
                        if (searchedFlight != null)
                        {
                            searchedFlight.printFlightInfo();
                            Console.ReadKey();
                        }
                        else
                        {
                            Helper.clearDisplAndDisplMessage("Let sa unesenim ID-em ne postoji. Pritisnite bilo koju tipku za nastavak");
                        }
                        break;
                    case '2':
                        var departureInput = Helper.getAndValidateName("polaziste leta za pretragu ili 0 za povratak");
                        if (departureInput == "0") break;
                        var arrivalInput = Helper.getAndValidateName("slijetaliste leta za pretragu ili 0 za povratak");
                        if (arrivalInput == "0") break;
                        var selectedFlights = currentUser.getFlights().Keys.ToList().Where(f => f.getName().ToLower() == $"{departureInput}-{arrivalInput}".ToLower()).ToList();
                        if (selectedFlights.Count() == 0)
                        {
                            Helper.clearDisplAndDisplMessage("Let sa unesenim podacima ne postoji. Pritisnite bilo koju tipku za nastavak");
                        }
                        else
                        {
                            foreach (var flight in selectedFlights)
                            {
                                flight.printFlightInfo();
                            }
                            Console.ReadKey();
                        }
                        break;
                    case '0':
                        return;
                    default:
                        Helper.clearDisplAndDisplMessage("Neispravan unos pokusajte ponovno");
                        break;
                }
            }
        }
        public static void flightCancellationScreen(User currentUser)
        {
            while (true)
            {
                Console.Clear();
                currentUser.printAllFlights();
                Console.ReadKey();
                var flightIdInput = Helper.getAndValidateInputInt("ID leta koji zelite otkazati ili 0 za povratak");
                if (flightIdInput == 0) return;
                var selectedFlight = currentUser.getFlights().Keys.ToList().Find(f => f.getId() == flightIdInput);
                if (selectedFlight != null)
                {
                    if (selectedFlight.getDepartureTime() > DateTime.Now.AddHours(24))
                    {
                        if (Helper.waitForConfirmation())
                        {
                            currentUser.getFlights().Remove(selectedFlight);
                            selectedFlight.removePassenger(currentUser);
                            Helper.clearDisplAndDisplMessage("Uspjesno ste otkazali let! Pritisnite bilo koju tipku za nastavak");
                            return;
                        }
                        else {
                            Console.WriteLine("Radnja otkazana");
                            Console.ReadKey();
                            return;
                        }
                    }
                    else
                    {
                        Helper.clearDisplAndDisplMessage("Let je moguce otkazati najkasnije 24h prije polaska. Pritisnite bilo koju tipku za nastavak");
                    }
                }
                else
                {
                    Helper.clearDisplAndDisplMessage("Let sa unesenim ID-em ne postoji. Pritisnite bilo koju tipku za nastavak");
                }
            }
        }
    }
}
