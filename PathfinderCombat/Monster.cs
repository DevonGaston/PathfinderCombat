using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderCombat
{
    public abstract class Monster
    {
        protected Dice d20;
        public Weapons w1;
        public string name;
        protected int BaseAttackBonus;
        public abstract int strike();
        public abstract int damage();
        public Monster(int attack)
        {
            BaseAttackBonus = attack;
            d20 = new D20();
        }
    }

    public class Living_Dead : Monster
    {
        public Living_Dead(int attack) : base(attack)
        {
            w1 = new Claws();
            name = "Living Dead";
        }

        public override int strike()
        {
            return BaseAttackBonus + d20.roll();
        }

        public override int damage()
        {
            return w1.damage();
        }
    }
}
