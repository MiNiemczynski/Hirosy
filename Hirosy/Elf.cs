namespace Hirosy
{
    public class Elf : Character
    {
        public override void Special(Character enemy)
        {
            if (CheckStamina(5)) return;
            crit = (int)(crit * 1.5);
            dodge = (int)(dodge * 1.5);
            Console.WriteLine($" > {name} used 'Focus'! Luck based stats buffed!");
            specialUsed = true;
        }
        public override string GetRace() => "Elf";
        public Elf(string name) : base(name)
        {
            Hp = 75;
            MaxHp = hp;
            Armor = 10;
            Strength = 15;
            Dodge = 40;
            Crit = 20;
            MaxStamina = 13;
            Stamina = MaxStamina;
        }
    }
}
