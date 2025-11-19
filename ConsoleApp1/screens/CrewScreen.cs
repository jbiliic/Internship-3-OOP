using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Classes;

namespace ConsoleApp1.screens
{
    internal class CrewScreen
    {
        public static void crewScreen()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("1 - Prikaz posade\n2 - Dodavanje nove posade\n3 - Dodavanje nove osobe\n0 - Povratak\nUnos: ");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        if (GlobalVariables.cabinCrewDataBase.Count() != 0)
                        {
                            foreach (var crew in GlobalVariables.cabinCrewDataBase)
                                crew.printCrewInfo();
                            Console.ReadKey();
                        }
                        else
                            Helper.clearDisplAndDisplMessage("Nema timova u bazi podataka");
                        break;
                    case '2':
                        addCrewScreen();
                        break;
                    case '3':
                        break;
                    case '0':
                        return;
                    default:
                        Helper.clearDisplAndDisplMessage("Neispravan unos pokusajte ponovno");
                        break;
                }
            }
        }
        public static void addCrewScreen()
        
        {
            CabinMember selectedPilot, selectedCopilot, selectedStewardess1, selectedStewardess2;
            var crewName = Helper.getAndValidateName("naziv posade: ");
            while (true) {
                var availablePilots = GlobalVariables.cabinMemberDataBase.FindAll(p => (p.getRole() == enums.cabinCrewRoles.Pilot) && (p.getCrew() == null));
                if (availablePilots.Count() == 0) {
                    Helper.clearDisplAndDisplMessage("Nema dostupnih pilota, prvo dodajte pilota pa stvorite posadu");
                    return;
                }
                Console.WriteLine("Dostupni piloti:");
                foreach (var crew in availablePilots) {
                    crew.printInfo();
                }
                Console.ReadKey();
                var pilotId = Helper.getAndValidateInputInt("Id pilota kojeg zelite ili 0 za povratak");
                if (pilotId == 0) return;
                selectedPilot = availablePilots.First(p => p.getId() == pilotId);
                if (selectedPilot == null) {
                    Helper.clearDisplAndDisplMessage("Pilot s unesenim id-em ne postoji");
                    continue;
                }
                break;
            }
            while (true)
            {
                var availableCopilots = GlobalVariables.cabinMemberDataBase.FindAll(p => (p.getRole() == enums.cabinCrewRoles.Copilot) && (p.getCrew() == null));
                if (availableCopilots.Count() == 0)
                {
                    Helper.clearDisplAndDisplMessage("Nema dostupnih kopilota, prvo dodajte kopilota pa stvorite posadu");
                    return;
                }
                Console.WriteLine("Dostupni kopiloti:");
                foreach (var crew in availableCopilots)
                {
                    crew.printInfo();
                }
                Console.ReadKey();
                var copilotId = Helper.getAndValidateInputInt("Id kopilota kojeg zelite ili 0 za povratak");
                if (copilotId == 0) return;
                selectedCopilot = availableCopilots.First(p => p.getId() == copilotId);
                if (selectedCopilot == null)
                {
                    Helper.clearDisplAndDisplMessage("Kopilot s unesenim id-em ne postoji");
                    continue;
                }
                break;
            }
            while (true)
            {
                var availableStewardesses1 = GlobalVariables.cabinMemberDataBase.FindAll(p => (p.getRole() == enums.cabinCrewRoles.Stewardess) && (p.getCrew() == null));
                if (availableStewardesses1.Count() < 2)
                {
                    Helper.clearDisplAndDisplMessage("Nema dovoljno dostupnih stewardessa, prvo dodajte stewardessu pa stvorite posadu");
                    return;
                }
                Console.WriteLine("Dostupne stewardesse:");
                foreach (var crew in availableStewardesses1)
                {
                    crew.printInfo();
                }
                Console.ReadKey();
                var stewardess1Id = Helper.getAndValidateInputInt("Id stewardesse kojeg zelite ili 0 za povratak");
                if (stewardess1Id == 0) return;
                selectedStewardess1 = availableStewardesses1.First(p => p.getId() == stewardess1Id);
                if (selectedStewardess1 == null)
                {
                    Helper.clearDisplAndDisplMessage("Stewardessa s unesenim id-em ne postoji");
                    continue;
                }
                break;
            }
            while (true)
            {
                var availableStewardesses2 = GlobalVariables.cabinMemberDataBase.FindAll(p => (p.getRole() == enums.cabinCrewRoles.Stewardess) && (p.getCrew() == null) && (p!= selectedStewardess1));
                if (availableStewardesses2.Count() < 2)
                {
                    Helper.clearDisplAndDisplMessage("Nema dovoljno dostupnih stewardessa, prvo dodajte stewardessu pa stvorite posadu");
                    return;
                }
                Console.WriteLine("Dostupne stewardesse:");
                foreach (var crew in availableStewardesses2)
                {
                    crew.printInfo();
                }
                Console.ReadKey();
                var stewardess2Id = Helper.getAndValidateInputInt("Id stewardesse kojeg zelite ili 0 za povratak");
                if (stewardess2Id == 0) return;
                selectedStewardess2 = availableStewardesses2.First(p => (p.getId() == stewardess2Id) && (p != selectedStewardess1));
                if (selectedStewardess2 == null)
                {
                    Helper.clearDisplAndDisplMessage("Stewardessa s unesenim id-em ne postoji");
                    continue;
                }
                break;
            }
            CabinCrew newCrew = new CabinCrew(crewName);
            newCrew.addMember(selectedPilot);
            newCrew.addMember(selectedCopilot);
            newCrew.addMember(selectedStewardess1);
            newCrew.addMember(selectedStewardess2);
            selectedStewardess1.setCrew(newCrew);
            selectedStewardess2.setCrew(newCrew);
            selectedCopilot.setCrew(newCrew);
            selectedPilot.setCrew(newCrew);
            GlobalVariables.cabinCrewDataBase.Add(newCrew);
            Helper.clearDisplAndDisplMessage("Uspjesno dodana nova posada");
        }
    }
}
