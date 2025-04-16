using System.Globalization;

namespace Hirosy
{
    public class Fight
    {
        readonly NumberFormatInfo precision = new();

        private readonly Character attacker;
        private readonly Character defender;
        public Character winner;
        public string AttackerName => attacker.Name;
        public string DefenderName => defender.Name;
        public Fight(Character attacking, Character defending)
        {
            this.winner = default!;
            this.attacker = attacking;
            this.defender = defending;
            precision.NumberDecimalDigits = 2;
        }
        public Fight(Fight f1, Fight f2)
        {
            this.winner = default!;
            this.attacker = f1.winner;
            this.defender = f2.winner;
            precision.NumberDecimalDigits = 2;
        }
        private void Win(Character winner)
        {
            Console.WriteLine($"\n >> {winner.Name} has won this fight!");

            string race = winner.GetRace();
            switch (race)
            {
                case "Orc":
                    this.winner = new Orc(winner.Name);
                    break;
                case "Human":
                    this.winner = new Human(winner.Name);
                    break;
                case "Dwarf":
                    this.winner = new Dwarf(winner.Name);
                    break;
                case "Elf":
                    this.winner = new Elf(winner.Name);
                    break;
            }
            WaitForEnter();
        }
        private void PresentCharacter(Character character)
        {
            Console.WriteLine($" \n      > {character.GetRace()} {character.Name}:\n        >>>------------------>");
            Console.WriteLine($"" +
                $"\tHP: \t\t{character.Hp.ToString("N", precision)}" +
                $"\n\tArmor: \t\t{character.Armor}" +
                $"\n\tStrength: \t{character.Strength}" +
                $"\n\tDodge: \t\t{character.Dodge} %" +
                $"\n\tCrit: \t\t{character.Crit} %");
            Console.Write("\tStamina: ");
            int i;
            for (int space = 0; space < 13 - character.MaxStamina; space++) Console.Write(" ");
            for (i = 0; i < character.Stamina; i++) Console.Write("#");
            while (i < character.MaxStamina)
            {
                Console.Write("-");
                i++;
            }
            Console.WriteLine("\n        >>>------------------>");
        }
        private void Present()
        {
            PresentCharacter(this.attacker);
            PresentCharacter(this.defender);
        }
        private static void Turn(Character hasTurn, Character enemy)
        {
            Console.WriteLine($"\n >> Pick your action, {hasTurn.Name}: " +
                $"\n\t1. Quick attack (2 stamina) - little stamina consumption - decent damage value" +
                $"\n\t2. Heavy attack (3 stamina) - moderate stamina consumption - higher damage value" +
                $"\n\t3. Heal \t(4 stamina) - heals character based on maximum HP value" +
                $"\n\t4. Recover \t(stamina++) - regenerates character's stamina based on maximum stamina value" +
                (hasTurn.SpecialUsed ? "" : $"\n\t5. Special \t(5 stamina) - character's special ability"));
            Console.Write(" >> Action number: ");
            try
            {
                int? choice = Console.ReadLine()?[0];

                switch (choice)
                {
                    case '1':
                        hasTurn.QuickAttack(enemy);
                        break;
                    case '2':
                        hasTurn.HeavyAttack(enemy);
                        break;
                    case '3':
                        hasTurn.Heal();
                        break;
                    case '4':
                        hasTurn.Recover();
                        break;
                    case '5':
                        if (!hasTurn.SpecialUsed)
                            hasTurn.Special(enemy);
                        else
                            Console.WriteLine(" > Special power has already been used! Enemy turn! ");
                        break;
                    default:
                        Console.WriteLine("Wrong key! Enemy turn!");
                        break;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Wrong key! Enemy turn!");
            }
        }
        public void Start()
        {
            Console.Clear();
            Console.WriteLine($" Battle! {attacker.Name} attacked {defender.Name}!");
            while (true)
            {
                Console.WriteLine($" > {attacker.Name}'s turn!");
                Present();
                Turn(attacker, defender);
                if (defender.Hp <= 0) { Win(attacker); break; }
                WaitForEnter();
                Console.Clear();

                Console.WriteLine($" > {defender.Name} turn!");
                Present();
                Turn(defender, attacker);
                if (attacker.Hp <= 0) { Win(defender); break; }
                WaitForEnter();
                Console.Clear();
            }
        }
        private static void WaitForEnter()
        {
            Console.WriteLine(" >> Press 'Enter' to continue... ");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) ;
        }
    }
}
