using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderCombat
{
    public abstract class CombatClasses
    {
        protected int BaseAttackBonus;
        public string name;
        public abstract int attack();
    }

    public class Fighter : CombatClasses
    {
        Dice d20 = new D20();
        public Fighter(int level)
        {
            BaseAttackBonus = level;
            name = "Fighter";
        }

        public override int attack()
        {
            return BaseAttackBonus + d20.roll();
        }
    }

    public class Rogue : CombatClasses
    {
        Dice d20 = new D20();
        public Rogue(int level)
        {
            BaseAttackBonus = (level * 3) / 4;
            name = "Rogue";
        }

        public override int attack()
        {
            return BaseAttackBonus + d20.roll();
        }
    }

    public class Wizard : CombatClasses
    {
        Dice d20 = new D20();
        public Wizard(int level)
        {
            BaseAttackBonus = level / 2;
            name = "Wizard";
        }

        public override int attack()
        {
            return BaseAttackBonus + d20.roll();
        }
    }
}

