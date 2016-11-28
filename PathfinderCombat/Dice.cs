//Name: Kenneth Devon Gaston
//Date Last Modified: 11/11/2016
//File Name: Dice.cs
//Purpose: Contains definitions and implementations of the Dice class
using System;
using System.Threading;

namespace PathfinderCombat
{
    //Contans definitions of the Dice class
    public abstract class Dice
    {
        public Random shake = new Random(); //Object used to determine value returned by roll function
        public string name; //Holds the name of the created dice to verify the range of numbers it can generate
        public int max;  //Contains the maximum value that can be obtained by shake objects Next funciton
        public int result;  //Stores value returned from shake object

        //Obtains value from shake.Next(1, max) and returns that value
        public abstract int roll();
    }

    //Contains implementations of Dice class
    public class D : Dice
    {
        //Constructor: Takes in a predetermined value and sets max to that value
        public D(int m)
        {
            Thread.Sleep(3); //Ensures all D objects exist on different clock ticks (i.e., they can return different values when using the roll function)
            max = m;
            name = "d" + m;
        }

        //return is set to a value randomly generated from shake and then returned
        public override int roll()
        {
            result = shake.Next(1, max);
            return result;
        }
    }
}
