using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderCombat
{
    public abstract class CombatClasses
    {
        protected Dice d20, hitDie;
        protected int BaseAttackBonus;
        public int health;
        public string name;
        public abstract int attack();
        public abstract void reduce_health(int damage);
    }

    public class Fighter : CombatClasses
    {
        public Fighter(int level)
        {
            BaseAttackBonus = level;
            name = "Fighter";
            d20 = new D20();
            hitDie = new D10();
            health = 10;
            for(int i = 1; i < level; i++)
            {
                health += hitDie.roll();
            }
        }

        public override int attack()
        {
            return BaseAttackBonus + d20.roll();
        }

        public override void reduce_health(int damage)
        {
            health -= damage;
        }
    }

    public class Rogue : CombatClasses
    {
        public Rogue(int level)
        {
            BaseAttackBonus = (level * 3) / 4;
            name = "Rogue";
            d20 = new D20();
            hitDie = new D6();
            health = 6;
            for (int i = 1; i < level; i++)
            {
                health += hitDie.roll();
            }
        }

        public override int attack()
        {
            return BaseAttackBonus + d20.roll();
        }

        public override void reduce_health(int damage)
        {
            health -= damage;
        }
    }

    public class Wizard : CombatClasses
    {
        public Wizard(int level)
        {
            BaseAttackBonus = level / 2;
            name = "Wizard";
            d20 = new D20();
            hitDie = new D4();
            health = 4;
            for (int i = 1; i < level; i++)
            {
                health += hitDie.roll();
            }
        }

        public override int attack()
        {
            return BaseAttackBonus + d20.roll();
        }

        public override void reduce_health(int damage)
        {
            health -= damage;
        }
    }
}

