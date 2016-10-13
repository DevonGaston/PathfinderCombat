using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderCombat
{
    public abstract class Monster
    {
        protected Dice d20, hitDie;
        public Weapons w1;
        public string name;
        public int health;
        protected int BaseAttackBonus;
        public abstract int strike();
        public abstract int damage();
        public abstract void reduce_health(int damage);
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
            hitDie = new D8();
            health = 3;
            for(int i = 0; i < 2; i++)
            {
                health += hitDie.roll();
            }
        }

        public override int strike()
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
    }
}
