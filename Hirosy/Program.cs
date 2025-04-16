using Hirosy;

public class Program
{
    static void Main()
    {
        // Queue<Fight> fightStack = PresetTournament();
        Queue<Fight> fightStack = CustomTournament();

        while (fightStack.Count > 1)
        {
            foreach (var fight in fightStack)
                if (fight.winner == null)
                    fight.Start();

            List<Fight> newTurnFights = new();
            while (fightStack.Count >= 2)
            {
                newTurnFights.Add(new Fight(fightStack.Dequeue(), fightStack.Dequeue()));
            }

            foreach (var fight in newTurnFights)
                fightStack.Enqueue(fight);
        }
        Fight final = fightStack.Dequeue();
        Console.Clear();
        Console.WriteLine($" Final battle ({final.AttackerName} vs {final.DefenderName}) is just about to happen!" +
            "\n >>>------------------>");
        Console.WriteLine("\n >> Press 'Enter' to continue... ");
        while (Console.ReadKey(true).Key != ConsoleKey.Enter) ;
        final.Start();
    }
    private static Queue<Fight> CustomTournament()
    {
        Menu menu = new();
        return new(menu.GetFights());
    }
    private static Queue<Fight> PresetTournament()
    {
        var queue = new Queue<Fight>();

        Human fin = new("Fin");
        Dwarf gimli = new("Gimli");
        Orc durak = new("Durak");
        Elf iorweth = new("Iorweth");

        Fight f1 = new(fin, durak);
        Fight f2 = new(gimli, iorweth);

        queue.Enqueue(f1);
        queue.Enqueue(f2);

        return queue;
    }
}
