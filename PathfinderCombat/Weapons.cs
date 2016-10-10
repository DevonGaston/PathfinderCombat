using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderCombat
{
    public abstract class Weapons
    {
        public abstract int damage();
    }

    public class Longsword : Weapons
    {
        protected int damage_dealt;
        protected Dice d10 = new D10();
        public override int damage()
        {
            damage_dealt = d10.roll();
            return damage_dealt;
        }
    }
}
