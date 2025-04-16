namespace Hirosy
{
    public class Dwarf : Character
    {
        public override void Special(Character enemy)
        {
            if (CheckStamina(5)) return;
            armor = (int)(armor * 1.5);
            Console.WriteLine($" > {name} used 'Rock and Stone'! Their skin is tough as a rock now!");
            specialUsed = true;
        }
        public override string GetRace() => "Dwarf";
        public Dwarf(string name) : base(name)
        {
            Hp = 150;
            MaxHp = hp;
            Armor = 40;
            Strength = 20;
            Dodge = 5;
            Crit = 5;
            MaxStamina = 9;
            Stamina = MaxStamina;
        }
    }
}
