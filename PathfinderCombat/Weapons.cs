//Name: Kenneth Devon Gaston
//Date Last Modified: 11/11/2016
//File Name: Weapons.cs
//Purpose: Contains definitions and implementations of the Weapons class
namespace PathfinderCombat
{

    //Contains definitions for Weapons class
    public abstract class Weapons
    {
        public Dice dam; //The range of damage the weapon can do as determined by a D object
        public string name; //The name of the weapon
        public int crit;  //The multiplier applied to damage on a confirmed critical hit

        //Calculates and returns total damage dealt
        public abstract int damage();
    }

    //All remaining objects in file inherit from Weapons and function similarly outside of damage and crit values and their names
    //Longsword will be fully documented, and all other inheriters will follow it's basic functionality
    public class Longsword : Weapons
    {
        //Constructor: sets name, dam, and crit (at this point, values are derived from Pathfinder)
        public Longsword()
        {
            dam = new D(10);
            name = "Longsword";
            crit = 3;
        }

        //Calculates and returns damage dealt
        public override int damage()
        {
            //Damage dice rolled and value returned from roll function is returned
            return dam.roll();
        }
    }

    public class Claws : Weapons
    {
        public Claws()
        {
            name = "Claws";
            dam = new D(3);
            crit = 4;
        }
        public override int damage()
        {
            return dam.roll();
        }
    }

    public class Club : Weapons
    {
        public Club()
        {
            name = "Club";
            dam = new D(6);
            crit = 2;
        }
        public override int damage()
        {
            return dam.roll();
        }
    }

    public class Dagger : Weapons
    {
        public Dagger()
        {
            name = "Dagger";
            dam = new D(4);
            crit = 3;
        }
        public override int damage()
        {
            return dam.roll();
        }
    }
}
