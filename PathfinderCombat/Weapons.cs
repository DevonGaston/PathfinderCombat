using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderCombat
{
    public abstract class Weapons
    {
        protected Dice dam;
        public string name;
        public Weapons()
        {
        }
        public abstract int damage();
    }

    public class Longsword : Weapons
    {
        public Longsword() : base()
        {
            dam = new D(10);
            name = "Longsword";
        }
        public override int damage()
        {
            return dam.roll();
        }
    }

    public class Claws : Weapons
    {
        public Claws() : base()
        {
            name = "Claws";
            dam = new D(4);
        }
        public override int damage()
        {
            return dam.roll();
        }
    }
}
