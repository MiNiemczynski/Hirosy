using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hirosy
{
    public class Menu
    {
        private readonly List<Fight> fights;
        public List<Fight> GetFights() => fights;
        public Menu()
        {
            fights = new List<Fight>();
            Init();
        }
        private void Init()
        {
            Console.Clear();
            Console.WriteLine(" Hello! Welcome, to " +
                "\n >>>------HIROSY------>" +
                "\n\n It is a simple fantasy turn-based fighting game" +
                "\n Good luck and have fun!");
            Console.Write("\n >> Enter number of duels you want to arrange (default = 2, maximum = 10): ");

            int numberOfFights = ReadIntFromConsole();
            if (numberOfFights == 0)
                numberOfFights = 2;

            for (int i = 1; i <= numberOfFights; i++)
            {
                fights.Add(CreateFight(i));
            }
        }
        private static Fight CreateFight(int fightNumber)
        {
            var attacker = GetCharacterInput(fightNumber, "attacker");
            var defender = GetCharacterInput(fightNumber, "defender");
            return new Fight(attacker, defender);
        }

        private static Character GetCharacterInput(int fightNumber, string role)
        {
            int race = GetRaceFromConsole(fightNumber, role);
            string name = GetNameFromConsole(fightNumber, role);
            return CreateCharacter(race, name);
        }

        private static int GetRaceFromConsole(int fightNumber, string role)
        {
            int raceNumber;
            bool firstTry = true;
            do
            {
                Console.Clear();
                Console.WriteLine($" Fight number: {fightNumber} \n >>>------------>");
                Console.WriteLine($"\n >>{(firstTry ? "" : " Wrong value!")} Choose {role}'s race: " +
                    "\n\t1. Human - strong and resistant, can weaken their enemy" +
                    "\n\t2. Elf - fragile but agile, can increase their luck" +
                    "\n\t3. Dwarf - tough but unwieldy, can increase their defense" +
                    "\n\t4. Orc - potent and aggressive, can increase their offense");
                Console.Write(" >> Race number: ");

                raceNumber = ReadIntFromConsole(4);
                firstTry = false;
            }
            while (raceNumber == 0);
            return raceNumber;
        }

        private static string GetNameFromConsole(int fightNumber, string role)
        {
            string name;
            bool firstTry = true;
            do
            {
                Console.Clear();
                Console.WriteLine($" Fight number: {fightNumber} \n >>>------------>");
                Console.WriteLine($"\n >>{(firstTry ? "" : " Incorrect name!")} Enter {role}'s name:");
                Console.Write(" >> Name: ");
                name = ReadStringFromConsole(50);
                firstTry = false;
            }
            while (string.IsNullOrWhiteSpace(name));
            return name;
        }

        private static Character CreateCharacter(int raceNumber, string name)
        {
            return raceNumber switch
            {
                2 => new Elf(name),
                3 => new Dwarf(name),
                4 => new Orc(name),
                _ => new Human(name),
            };
        }

        private static int ReadIntFromConsole(int maxValue = 10)
        {
            string? input = Console.ReadLine();
            return int.TryParse(input, out int actionNumber) && actionNumber <= maxValue && actionNumber > 0 ? actionNumber : 0;
        }
        private static string ReadStringFromConsole(int? maxLength = null)
        {
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input)) return "";
            if (maxLength.HasValue && input.Length > maxLength.Value) return "";

            return input;
        }
    }
}
