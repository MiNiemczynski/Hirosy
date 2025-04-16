namespace Hirosy
{
    public class Orc : Character
    {
        public override void Special(Character enemy)
        {
            if (CheckStamina(5)) return;
            strength = (int)(strength * 1.5);
            crit = (int)(crit * 1.5);

            dodge = (int)(dodge * 0.5);
            armor = (int)(armor * 0.5);
            Console.WriteLine($" > {name} used 'Rage'! Offensive stats buffed, deffensive stast reduced!");
            specialUsed = true;
        }
        public override string GetRace() => "Orc";
        public Orc(string name) : base(name)
        {
            Hp = 125;
            MaxHp = hp;
            Armor = 10;
            Strength = 30;
            Dodge = 10;
            Crit = 20;
            MaxStamina = 11;
            Stamina = MaxStamina;
        }
    }
}
