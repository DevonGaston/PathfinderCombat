//Name: Kenneth Devon Gaston
//Date Last Modified: 11/11/2016
//File Name: Armor.cs
//Purpose: Contains definitions and implementations of the Armor class
namespace PathfinderCombat
{
    //Contains defintions and data for Armor class
    public abstract class Armor
    {
        public int armor_bonus; //The amount added to a Character's AC
        public string name;  //Holds the name of the armor

        //Base constructor allowing the use of the armor_bonus integer
        public Armor()
        {

        }
    }

    //All objects inheriting from Armor function the same way but with different values.  Light armor will be fully documented to show how each object works
    public class LightArmor : Armor
    {
        //Constructor: Sets armor_bonus to some arbitrary value
        public LightArmor() : base()
        {
            name = "Light Armor";
            armor_bonus = 2;
        }
    }

    public class MediumArmor : Armor
    {
        public MediumArmor() : base()
        {
            name = "Medium Armor";
            armor_bonus = 4;
        }
    }

    public class HeavyArmor : Armor
    {
        public HeavyArmor() : base()
        {
            name = "Heavy Armor";
            armor_bonus = 6;
        }
    }
}
