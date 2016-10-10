using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderCombat
{
    public interface Dice
    {
       int roll();
    }

    public class D2 : Dice
    {
        Random shake = new Random();
        int result;
        int Dice.roll()
        {
            result = (shake.Next(1, 2));
            return result;
        }
    }

    public class D3 : Dice
    {
        Random shake = new Random();
        int result;
        int Dice.roll()
        {
            result = (shake.Next(1, 3));
            return result;
        }
    }

    public class D4 : Dice
    {
        Random shake = new Random();
        int result;
        int Dice.roll()
        {
            result = (shake.Next(1, 4));
            return result;
        }
    }

    public class D6 : Dice
    {
        Random shake = new Random();
        int result;
        int Dice.roll()
        {
            result = (shake.Next(1, 6));
            return result;
        }
    }

    public class D8 : Dice
    {
        Random shake = new Random();
        int result;
        int Dice.roll()
        {
            result = (shake.Next(1, 8));
            return result;
        }
    }

    public class D10 : Dice
    {
        Random shake = new Random();
        int result;
        int Dice.roll()
        {
            result = (shake.Next(1, 10));
            return result;
        }
    }

    public class D12 : Dice
    {
        Random shake = new Random();
        int result;
        int Dice.roll()
        {
            result = (shake.Next(1, 12));
            return result;
        }
    }

    public class D20 : Dice
    {
        Random shake = new Random();
        int result;
        int Dice.roll()
        {
            result = (shake.Next(1, 20));
            return result;
        }
    }
}
