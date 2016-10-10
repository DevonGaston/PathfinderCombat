using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderCombat
{
    public abstract class CombatClasses
    {
        public abstract int attack();
    }

    public class Fighter : CombatClasses
    {
        protected int BaseAttackBonus;
        protected int strike;
        Dice d20 = new D20();
        public Fighter(int level)
        {
            BaseAttackBonus = level;
        }

        public override int attack()
        {
            strike = d20.roll() + BaseAttackBonus;
            return strike;
        }
    }
}
