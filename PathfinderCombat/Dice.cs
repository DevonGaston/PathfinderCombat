using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PathfinderCombat
{
    public abstract class Dice
    {
        public Random shake = new Random();
        public int result, max;
        public abstract int roll();
        public abstract int rattle(int max);
    }

    public class D : Dice
    {
        public D(int m)
        {
            Thread.Sleep(3);
            max = m;
        }
        public override int roll()
        {
            return rattle(max);
        }

        public override int rattle(int m)
        {
            result = shake.Next(1, m);
            return result;
        }
    }
}
