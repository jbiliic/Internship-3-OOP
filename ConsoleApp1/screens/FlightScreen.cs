using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Classes;

namespace ConsoleApp1.screens
{
    internal class FlightScreen
    {
        public static void flightScreen()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("1 - Svi letovi\n2 - Dodavanje leta\n3 - Pretrazivanje letova\n4 - Uredivanje letova\n5 - Brisanje letova\n0 - Povratak\nUnos: ");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        if (GlobalVariables.flightDataBase.Count() != 0)
                        {
                            Console.Clear();
                            foreach (var flight in GlobalVariables.flightDataBase)
                            {
                                flight.printFlightInfo();
                            }
                            Console.ReadKey();
                        }
                        else
                        {
                            Helper.clearDisplAndDisplMessage("Nema letova za ispis");
                        }
                        break;
                    case '2':
                        addFlight();
                        break;
                    case '3':
                        searchFlight();
                        break;
                    case '4':
                        editFlight();
                        break;
                    case '5':
                        deleteFlight();
                        break;
                    case '0':
                        return;
                    default:
                        Helper.clearDisplAndDisplMessage("Neispravan unos pokusajte ponovno");
                        break;
                }
            }
        }
        public static void addFlight()
        {
            var origin = Helper.getAndValidateName("lokaciju pocetka leta");
            var destination = "";
            while (true)
            {
                destination = Helper.getAndValidateName("lokaciju slijetanja");
                if (destination.ToLower().Equals(origin.ToLower()))
                {
                    Helper.clearDisplAndDisplMessage("Avion nemoze krenuti i doci na isto mijesto");
                    continue;
                }
                else { break; }

            }
            var departureTime = Helper.getAndValidateTime("Vrijeme polaska");
            var arrivalTime = DateTime.Now;
            while (true)
            {
                arrivalTime = Helper.getAndValidateTime("vrijeme slijetanja");
                if (departureTime > arrivalTime)
                {
                    Helper.clearDisplAndDisplMessage("Avion nemoze slijetjeti prije nego sto je krenuo");
                    continue;
                }
                else { break; }
            }
            var distance = Helper.getAndValidateInputInt("duljinu puta");
            var availablePlanes = Helper.getAvailablePlanes(departureTime, arrivalTime);
            var availableCrew = Helper.getAvailableCrew(departureTime, arrivalTime);
            if (availableCrew.Count() == 0)
            {
                Helper.clearDisplAndDisplMessage("Nema dostupne posade za odabrano vrijeme pa se let nemoze dodati");
                return;
            }
            if (availablePlanes.Count() == 0)
            {
                Helper.clearDisplAndDisplMessage("Nema dostupnog aviona za odabrano vrijeme pa se let nemoze dodati");
                return;
            }
            var planeId = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Dostupni avioni:");
                foreach (var plane in availablePlanes.Values)
                   plane.printPlaneInfo();
                Console.ReadKey();
                planeId = Helper.getAndValidateInputInt("ID aviona ili 0 za povratak");
                if (planeId == 0) return;
                if (!availablePlanes.ContainsKey(planeId))
                {
                    Helper.clearDisplAndDisplMessage("Unijeli ste nepostojeci ID aviona");
                    continue;
                }
                break;
            }
            var crewId = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Dostupna posada:");
                foreach (var crew in availableCrew.Values)
                   crew.printCrewInfo();
                Console.ReadKey();
                crewId = Helper.getAndValidateInputInt("ID posade ili 0 za povratak");
                if (crewId == 0) return;
                if (!availableCrew.ContainsKey(crewId))
                {
                    Helper.clearDisplAndDisplMessage("Unijeli ste nepostojeci ID");
                    continue;
                }
                break;
            }
            if (Helper.waitForConfirmation())
            {
                var newFlight = new Flight(origin, destination, distance, departureTime, arrivalTime, availablePlanes[planeId], availableCrew[crewId]);
                GlobalVariables.flightDataBase.Add(newFlight);
                availableCrew[crewId].addFlight(newFlight);
                availablePlanes[planeId].addFlight(newFlight);
                Helper.clearDisplAndDisplMessage("Uspjesno ste dodali let");
            }
        }
        public static void searchFlight()
        {
            Console.Clear();
            if (GlobalVariables.flightDataBase.Count() == 0)
            {
                Helper.clearDisplAndDisplMessage("Nema letova za pretrazivanje");
                return;
            }
            while (true)
            {
                var input = Helper.getAndValidateInputInt("id leta koji zelite pretraziti ili 0 za izlaz");
                if (input == 0) return;
                var flight = GlobalVariables.flightDataBase.Find(f => f.getId() == input);
                if (flight == null)
                {
                    Helper.clearDisplAndDisplMessage("Let s unesenim ID-em ne postoji");
                    continue;
                }
                else
                {
                    Console.Clear();
                    flight.printFlightInfo();
                    Console.ReadKey();
                    return;
                }

            }
        }
        public static void deleteFlight()
        {
            Console.Clear();
            var flights = Helper.canBeDeleted();
            if (GlobalVariables.flightDataBase.Count() == 0 || flights.Count()==0)
            {
                Helper.clearDisplAndDisplMessage("Nema letova za brisanje");
                return;
            }
            while (true)
            {
                foreach(var flight in flights.Values)
                    flight.printFlightInfo();
                Console.ReadKey();
                var input = Helper.getAndValidateInputInt("id leta koji zelite obrisati ili 0 za izlaz");
                if (input == 0) return;

                var flightToDelete = flights.Values.ToList().Find(f => f.getId() == input);
                if (flightToDelete == null)
                {
                    Helper.clearDisplAndDisplMessage("Let s unesenim ID-em ne postoji");
                    continue;
                }
                else
                {
                    if (Helper.waitForConfirmation())
                    {
                        foreach (var user in flightToDelete.getPassengers())
                        {
                            user.getFlights().Remove(flightToDelete);
                        }
                        flightToDelete.getPlane().GetFlights().Remove(flightToDelete);
                        flightToDelete.getCrew().getFlights().Remove(flightToDelete);
                        GlobalVariables.flightDataBase.Remove(flightToDelete);
                        Helper.clearDisplAndDisplMessage("Let uspijesno izbrisan");
                        return;
                    }
                    else {
                        Helper.clearDisplAndDisplMessage("Radnja otkazana");
                        return;
                    }
                }
            }
        }
        public static void editFlight() {
            Console.Clear();
            if (GlobalVariables.flightDataBase.Count() == 0)
            {
                Helper.clearDisplAndDisplMessage("Nema letova za uredivanje");
                return;
            }
            var inputId = 0;
            while (true)
            {
                inputId = Helper.getAndValidateInputInt("id leta koji zelite urediti ili 0 za izlaz");
                if (inputId == 0) return;
                var flight = GlobalVariables.flightDataBase.Find(f => f.getId() == inputId);
                if (flight == null)
                {
                    Helper.clearDisplAndDisplMessage("Let s unesenim ID-em ne postoji");
                    continue;
                }
                break;
            }
            var flightToEdit = GlobalVariables.flightDataBase.Find(f => f.getId() == inputId);
            var departureTime = Helper.getAndValidateTime("novo vrijeme polaska");
            var arrivalTime = DateTime.Now;
            while (true)
            {
                arrivalTime = Helper.getAndValidateTime("novo vrijeme slijetanja");
                if (departureTime > arrivalTime)
                {
                    Helper.clearDisplAndDisplMessage("Avion nemoze slijetjeti prije nego sto je krenuo");
                    continue;
                }
                else { break; }
            }
            var availableCrew = Helper.getAvailableCrew(departureTime, arrivalTime);
            if (availableCrew.Count() == 0)
            {
                Helper.clearDisplAndDisplMessage("Nema dostupne posade za odabrano vrijeme pa se let nemoze dodati");
                return;
            }
            foreach (var flight in flightToEdit.getPlane().GetFlights()) {
                if (flight == flightToEdit) continue;
                if (Helper.DoTimeRangesOverlap(flight.getDepartureTime(), flight.getArrvalTime(), departureTime, arrivalTime)) {
                    Helper.clearDisplAndDisplMessage("Nemoguce je dodati ovo vrijeme jer vas avion tada ima drugi let");
                    return;
                }
            }
            var crewId = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Dostupna posada:");
                foreach (var crew in availableCrew.Values)
                    crew.printCrewInfo();
                Console.ReadKey();
                crewId = Helper.getAndValidateInputInt("ID posade ili 0 za povratak");
                if (crewId == 0) return;
                if (!availableCrew.ContainsKey(crewId))
                {
                    Helper.clearDisplAndDisplMessage("Unijeli ste nepostojeci ID");
                    continue;
                }
                break;
            }
            if (Helper.waitForConfirmation())
            {

                flightToEdit.getCrew().getFlights().Remove(flightToEdit);
                availableCrew[crewId].addFlight(flightToEdit);
                flightToEdit.addCabinCrew(availableCrew[crewId]);
                flightToEdit.updateDepartureTime(departureTime);
                flightToEdit.updateArrivalTime(arrivalTime);
                Helper.clearDisplAndDisplMessage("Uspjesno ste uredili let");
                return;
            }
            else { 
                Helper.clearDisplAndDisplMessage("Radnja otkazana");
                return;
            }


        }
    }
}
