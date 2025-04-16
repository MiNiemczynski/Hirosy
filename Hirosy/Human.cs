namespace Hirosy
{
    public class Human : Character
    {
        public override void Special(Character enemy)
        {
            if (CheckStamina(5)) return;
            enemy.Armor = (int)(enemy.Armor * 0.8);
            enemy.Strength = (int)(enemy.Strength * 0.8);
            enemy.Dodge = (int)(enemy.Dodge * 0.8);
            enemy.Crit = (int)(enemy.Crit * 0.8);
            Console.WriteLine($" > {name} used 'Battle Cry' on {enemy.Name}! Enemy stats reduced!");
            specialUsed = true;
        }
        public override string GetRace() => "Human";
        public Human(string name) : base(name)
        {
            Hp = 100;
            MaxHp = hp;
            Armor = 50;
            Strength = 10;
            Dodge = 10;
            Crit = 10;
            MaxStamina = 10;
            Stamina = MaxStamina;
        }
    }
}
