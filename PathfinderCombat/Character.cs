
namespace PathfinderCombat
{
    abstract public class Character
    {
        protected Dice d20, hitDie;
        public Weapons w1;
        public Armor block;
        public string name;
        public int BaseAttackBonus;
        public int health = 0, Initiative, AC;
        public Character(string n)
        {
            name = n;
            d20 = new D(20);
            AC = 10;
        }
        public abstract int attack();
        public abstract int damage();
        public abstract void reduce_health(int damage);
        public abstract int rollInitiative();
        public abstract void setInitiative();
    }
    public class Monster : Character
    {
        public Monster(string n, int attack, Dice hd, Weapons w, int level) : base(n)
        {
            BaseAttackBonus = attack;
            hitDie = hd;
            w1 = w;
            AC += 5;
            for (int i = 0; i < level; i++)
            {
                health += hitDie.roll();
            }
        }

        public override int attack()
        {
            return BaseAttackBonus + d20.roll();
        }

        public override int damage()
        {
            return w1.damage();
        }
        public override void reduce_health(int damage)
        {
            health -= damage;
        }

        public override int rollInitiative()
        {
            return d20.roll();
        }

        public override void setInitiative()
        {
            Initiative = rollInitiative();
        }
    }

    public class Rogue : Character
    {
        public Rogue(string n, Weapons w, Armor a, int level) : base(n)
        {
            BaseAttackBonus = level * (3 / 4);
            hitDie = new D(6);
            w1 = w;
            block = a;
            AC += block.armor_bonus;
            health += 6;
            for (int i = 1; i < level; i++)
            {
                health += hitDie.roll();
            }
        }
        public override int attack()
        {
            return BaseAttackBonus + d20.roll();
        }

        public override int damage()
        {
            return w1.damage();
        }
        public override void reduce_health(int damage)
        {
            health -= damage;
        }
        public override int rollInitiative()
        {
            return d20.roll();
        }

        public override void setInitiative()
        {
            Initiative = rollInitiative();
        }
    }
    public class Fighter : Character
    {
        public Fighter(string n, Weapons w, Armor a, int level) : base(n)
        {
            BaseAttackBonus = level;
            hitDie = new D(10);
            w1 = w;
            block = a;
            AC += block.armor_bonus;
            health += 10;
            for (int i = 1; i < level; i++)
            {
                health += hitDie.roll();
            }
        }
        public override int attack()
        {
            return BaseAttackBonus + d20.roll();
        }

        public override int damage()
        {
            return w1.damage();
        }
        public override void reduce_health(int damage)
        {
            health -= damage;
        }
        public override int rollInitiative()
        {
            return d20.roll();
        }

        public override void setInitiative()
        {
            Initiative = rollInitiative();
        }
    }
    public class Wizard : Character
    {
        public Wizard(string n, Weapons w, Armor a, int level) : base(n)
        {
            BaseAttackBonus = level/2;
            hitDie = new D(4);
            w1 = w;
            block = a;
            AC += block.armor_bonus;
            health += 4;
            for (int i = 1; i < level; i++)
            {
                health += hitDie.roll();
            }
        }
        public override int attack()
        {
            return BaseAttackBonus + d20.roll();
        }

        public override int damage()
        {
            return w1.damage();
        }
        public override void reduce_health(int damage)
        {
            health -= damage;
        }
        public override int rollInitiative()
        {
            return d20.roll();
        }

        public override void setInitiative()
        {
            Initiative = rollInitiative();
        }
    }
}