using System.Globalization;

namespace Hirosy
{

    public abstract class Character
    {
        readonly NumberFormatInfo precision = new();
        readonly Random r = new();

        protected string name;
        protected decimal hp;
        protected decimal maxHp;
        protected int armor;
        protected int strength;
        protected int dodge;
        protected int crit;
        protected int stamina;
        protected int maxStamina;

        protected bool specialUsed;

        public string Name { get { return name; } set { this.name = value; } }
        public decimal Hp
        {
            get { return hp; }
            set
            {
                if (hp > value)
                {
                    decimal damage = this.hp - value;
                    damage -= (damage * ((decimal)armor / 100));

                    bool dodged = r.NextInt64(0, 100) <= dodge;
                    if (dodged)
                    {
                        Console.WriteLine($" [!] {name} dodged!");
                        return;
                    }
                    this.hp -= damage;
                    Console.WriteLine($" > {name} got hit for {damage.ToString("N", precision)} hp, armor prevented {armor}% of dmg!");
                }
                else if (value > maxHp && hp != 0)
                {
                    this.hp = maxHp;
                    Console.WriteLine($" > {name} has full hp!");
                }
                else
                {
                    hp = value;
                }
            }
        }
        public decimal MaxHp { get { return maxHp; } set { this.maxHp = value; } }
        public int Armor { get { return armor; } set { this.armor = value; } }
        public int Strength { get { return strength; } set { this.strength = value; } }
        public int Dodge { get { return dodge; } set { this.dodge = value; } }
        public int Crit { get { return crit; } set { this.crit = value; } }
        public bool SpecialUsed { get { return specialUsed; } set { this.specialUsed = value; } }
        public int Stamina
        {
            get { return stamina; }
            set
            {
                if (value > stamina)
                {
                    if (value < maxStamina)
                    {
                        Console.WriteLine($" > {name} recovered {value - stamina} stamina!");
                        this.stamina = value;
                    }
                    else if (value > maxStamina)
                    {
                        this.stamina = maxStamina;
                        Console.WriteLine($" > {name} recovered to full stamina!");
                    }
                    else
                    {
                        this.stamina = value;
                    }
                }
                else
                {
                    Console.WriteLine($" > {stamina - value} stamina drained!");
                    this.stamina = value;
                }
            }
        }
        public int MaxStamina { get { return maxStamina; } set { this.maxStamina = value; } }

        public bool CheckStamina(int s)
        {
            if (stamina < s)
            {
                Console.WriteLine(" > Too low on stamina to use that! Enemy turn!");
                return true;
            }
            Stamina -= s;
            return false;
        }
        public void DealDmg(Character enemy, decimal dmg)
        {

            bool critical = r.NextInt64(0, 100) <= crit;
            int tmpArmor = enemy.armor;
            if (critical)
            {
                enemy.Armor = 0;
                Console.WriteLine(" [!] Critical hit - armor ignored!");
            }
            enemy.Hp -= dmg * 10;
            enemy.Armor = tmpArmor;
        }
        public abstract void Special(Character enemy);
        public void QuickAttack(Character enemy)
        {
            if (CheckStamina(2)) return;
            int rawDmg = (int)r.NextInt64(15, 20);
            decimal dmg = (decimal)rawDmg + (rawDmg * (decimal)strength / 100);
            Console.WriteLine($" > {name} used 'Quick attack' on {enemy.name} trying to deal {dmg.ToString("N", precision)} dmg!");
            DealDmg(enemy, dmg);
        }
        public void HeavyAttack(Character enemy)
        {
            if (CheckStamina(3)) return;
            int rawDmg = (int)r.NextInt64(15, 30);
            decimal dmg = (decimal)rawDmg + (rawDmg * (decimal)strength * 2 / 100);
            Console.WriteLine($" > {name} used 'Heavy attack' on {enemy.name} trying to deal {dmg.ToString("N", precision)}{/*/{rawDmg}*/""} dmg!");
            DealDmg(enemy, dmg);
        }
        public void Heal()
        {
            if (CheckStamina(4)) return;
            decimal h = r.NextInt64(5, 25);
            decimal healed = h + h * ((decimal)(maxHp / 100) / 2);
            Console.WriteLine($" > {name} healed for {healed.ToString("N", precision)} hp!");
            Hp += healed;
        }
        public void Recover()
        {
            int recovered = (int)r.NextInt64(maxStamina / 4, (maxStamina / 2) + 1);
            Stamina += recovered;
        }
        public abstract string GetRace();
        public Character()
        {
            this.name ??= "";
            precision.NumberDecimalDigits = 2;
        }
        public Character(string name) : this()
        {
            this.name = name;
            this.specialUsed = false;
        }
    }
}
