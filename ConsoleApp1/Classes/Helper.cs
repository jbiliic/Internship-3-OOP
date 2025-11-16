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
                if (string.IsNullOrWhiteSpace(toReturn) || toReturn.Length < 2 || !ContainsSpecialCharacters(toReturn))
                { Console.Write($"\n{inputType} nesmije biti prazno, te mora biti duze od 6 slova i sadrzavati posebne znakove!!! Pritisnite enter te pokusajte ponovno"); Console.ReadKey(); continue; }
                return toReturn;
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
    }
}
