using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Classes;

namespace ConsoleApp1.screens
{
    internal class PlaneScreen
    {
        public static void planeScreen() {
            while (true) { 
                Console.Clear();
                Console.Write("1 - Prikaz aviona\n2 - Dodavanje aviona\n3 - Pretrazivanje aviona\n4 - Brisanje aviona\n5 - Sortirani prikaz\n0 - Povratak\nUnos: ");
                switch (Console.ReadKey().KeyChar) {
                    case '1':
                        Console.Clear();
                        if (GlobalVariables.planeDataBase.Count() != 0){
                            foreach(var plane in GlobalVariables.planeDataBase)
                                plane.printPlaneInfo();
                            Console.ReadKey();
                        }
                        else 
                                Helper.clearDisplAndDisplMessage("Nema dostupnih aviona u bazi podataka");
                            break;
                    case '2':
                        addPlaneScreen();
                        break;
                    case '3':
                        if (GlobalVariables.planeDataBase.Count() != 0)
                            searchPlane();
                        else
                            Helper.clearDisplAndDisplMessage("Nema dostupnih aviona u bazi podataka");
                        break;
                    case '4':
                        Console.Clear();
                        if (GlobalVariables.planeDataBase.Count() != 0)
                            deletePlaneScreen();
                        else
                            Helper.clearDisplAndDisplMessage("Nema dostupnih aviona u bazi podataka");
                        break;
                    case '5':
                        Console.Clear();
                        if (GlobalVariables.planeDataBase.Count() != 0)
                        {
                            sortedPlaneDisplay();
                            Console.ReadKey();
                        }
                        else
                            Helper.clearDisplAndDisplMessage("Nema dostupnih aviona u bazi podataka");
                        break;
                    case '0':
                        
                        return;
                    default:
                        Helper.clearDisplAndDisplMessage("Neispravan unos pokusajte ponovno");
                        break;

                }
            }
        }
        public static void addPlaneScreen(){ 
           var planeModel = Helper.getAndValidatePlaneName("model aviona: ");
           var planeCapacity = Helper.getAndValidateInputInt("kapacitet aviona: ");
           var planeMakeYear = Helper.getAndValidatePlaneMakeYear("godinu proizvodnje aviona: ");
           var newPlane = new Plane(planeModel, planeCapacity, planeMakeYear);
           GlobalVariables.planeDataBase.Add(newPlane);
           Helper.clearDisplAndDisplMessage("Avion uspijesno dodan");
           return;
        }
        public static void searchPlane() {
            while (true) { 
                Console.Clear();
                Console.Write("1 - Pretrazi po id\n2 - Pretrazi po imenu\n 0 - Povratak\nUnos:");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        while (true)
                        {
                            var inputId = Helper.getAndValidateInputInt("id aviona za pretragu ili 0 za povratak");
                            if (inputId == 0) break;
                            var selectedPlane = GlobalVariables.planeDataBase.Find(p => p.getId() == inputId);
                            if (selectedPlane == null)
                            {
                                Helper.clearDisplAndDisplMessage("Avion sa unesenim id-em ne postoji u bazi podataka");
                                continue;
                            }
                            else
                            {
                                selectedPlane.printPlaneInfo();
                                Console.ReadKey();
                                break;
                            }
                        }
                            break;
                    case '2':
                        while (true)
                        {
                            var inputName = Helper.getAndValidatePlaneName("ime aviona");
                            var selectedPlane = GlobalVariables.planeDataBase.Find(p => p.getModel() == inputName);
                            if (selectedPlane == null)
                            {
                                Console.WriteLine("Navedeni model ne postoji u nasoj floti, unesite 0 za povratak ili bilo sto za ponovni pokusaj\nUnos:");
                                if (Console.ReadKey().KeyChar == '0') break;
                                continue;
                            }
                            else
                            {
                                selectedPlane.printPlaneInfo();
                                Console.ReadKey();
                                break;
                            }
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
        public static void deletePlaneScreen()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("1 - Brisi po id\n2 - Brisi po imenu\n0 - Povratak\nUnos:");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        while (true)
                        {
                            var inputId = Helper.getAndValidateInputInt("id aviona za brisanje ili 0 za povratak");
                            if (inputId == 0) break;
                            var selectedPlane = GlobalVariables.planeDataBase.Find(p => p.getId() == inputId);
                            if (selectedPlane == null)
                            {
                                Helper.clearDisplAndDisplMessage("Avion sa unesenim id-em ne postoji u bazi podataka");
                                continue;
                            }
                            else
                            {
                                if (Helper.waitForConfirmation())
                                {
                                    deletePlane(selectedPlane);
                                    Console.WriteLine("Avion uspijesno izbrisan");
                                }
                                else {
                                    Console.WriteLine("Radnja otkazana");
                                }
                                Console.ReadKey();
                                break;
                            }
                        }
                        break;
                    case '2':
                        while (true)
                        {
                            var inputName = Helper.getAndValidatePlaneName("ime aviona");
                            var selectedPlane = GlobalVariables.planeDataBase.Find(p => p.getModel() == inputName);
                            if (selectedPlane == null)
                            {
                                Console.WriteLine("Navedeni model ne postoji u nasoj floti, unesite 0 za povratak ili bilo sto za ponovni pokusaj\nUnos:");
                                if (Console.ReadKey().KeyChar == '0') break;
                                continue;
                            }
                            else
                            {
                                if (Helper.waitForConfirmation())
                                {
                                    deletePlane(selectedPlane);
                                    Console.WriteLine("Avion uspijesno izbrisan");
                                }
                                else
                                {
                                    Console.WriteLine("Radnja otkazana");
                                }
                                Console.ReadKey();
                                break;
                            }
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
        public static void deletePlane(Plane plane)
        {
            foreach (var flight in plane.GetFlights().ToList()) {
                foreach (var user in flight.getPassengers().ToList())
                {
                    user.getFlights().Remove(flight);
                }
                flight.getPlane().GetFlights().Remove(flight);
                flight.getCrew().getFlights().Remove(flight);
                GlobalVariables.flightDataBase.Remove(flight);
            }
            
            GlobalVariables.planeDataBase.Remove(plane);
        }
        public static void sortedPlaneDisplay()
        {
            Console.Clear();
            while (true)
            {
                Console.Clear();
                Console.Write("1 - Po vremenu dodavanja\r\n2 - Godina proizvodnje uzlazno\r\n3 - Godina proizvodnje silazno\r\n");
                Console.Write("4 - Broj odrađenih letova uzlazno\r\n5 - Broj odrađenih letova silazno\r\n0 - Povratak\nUnos:");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        Helper.printPlaneList(GlobalVariables.planeDataBase.OrderBy(f => f.getCreatedAt()).ToList());
                        Console.ReadKey();
                        break;
                    case '2':
                        Console.Clear();
                        Helper.printPlaneList(GlobalVariables.planeDataBase.OrderBy(f => f.getMakeYear()).ToList());
                        Console.ReadKey();
                        break;
                    case '3':
                        Console.Clear();
                        Helper.printPlaneList(GlobalVariables.planeDataBase.OrderByDescending(f => f.getMakeYear()).ToList());
                        Console.ReadKey();
                        break;
                    case '4':
                        Console.Clear();
                        Helper.printPlaneList(GlobalVariables.planeDataBase.OrderBy(f => f.GetFlights().Count()).ToList());
                        Console.ReadKey();
                        break;
                    case '5':
                        Console.Clear();
                        Helper.printPlaneList(GlobalVariables.planeDataBase.OrderByDescending(f => f.GetFlights().Count()).ToList());
                        Console.ReadKey();
                        break;
                    case '0':
                        return;
                    default:
                        Helper.clearDisplAndDisplMessage("Neispravan unos pokusajte ponovno");
                        break;
                }
            }
        }
    }
}
