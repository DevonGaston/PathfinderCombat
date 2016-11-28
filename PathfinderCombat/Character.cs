//Name: Kenneth Devon Gaston
//Date Last Modified: 11/11/2016
//File Name: Character.cs
//Purpose: Contains definitions and implementations of the Character class
namespace PathfinderCombat
{
    //Contains definitions and data for Character classes
    abstract public class Character
    {
        protected Dice d20;  //Dice object used to determine results of attack and rollInitiative functions
        public Dice hitDie; //Used to determine total hitpoint for Monster and all others with levels > 1
        public Weapons w1;  //Weapon object used by Characters to attack each other
        public Armor block; //Armor used to increase Character's AC value 
        public string name;  //Name of the Character
        public int BaseAttackBonus; //Value added to d20.roll() in attack
        public int health = 0; //Overall hitpoints of Character (determined by personal constructor)
        public int Initiative; //Value found in rollInitiative(); used to determine order in combat (who goes first, etc.)
        public int AC; //Value that opposing Characters returned attack() value must at least equal in order to successfully hit

        //Base constructor: Takes in string n and sets n to it.  Also assigns d20 to a D(20) object and AC to 10 (as determined by Pathfinder system) 
        public Character(string n)
        {
            name = n;
            d20 = new D(20);
            AC = 10;
        }

        //Rolls the d20, adds the BaseAtackBonus for it, and returns the resulting value
        public abstract int attack();

        //Accesses the damage function in w1 and returns the value returned from that function
        public abstract int damage();

        //Takes in an integer representing damage the character has taken and deducts that much from their total health value
        public abstract void reduce_health(int damage);

        //Rolls the d20 and returns the resulting value to determine order in combat
        public abstract void rollInitiative();
    }

    //Contains implementations of Character class for any Monster
    public class Monster : Character
    {
        //Constructor: Takes string, two ints, a Dice object, and a Weapons object
        public Monster(string n, int attack, Dice hd, Weapons w, int level) : base(n)
        {
            //BaseAttackBonus set to passed in attack integer
            BaseAttackBonus = attack;

            //hitDie set to passed in hd Dice object
            hitDie = hd;

            //w1 set to passed in w Weapons object
            w1 = w;

            //AC increased by 5 (arbitrarily: subject to change in the future)
            AC += 5;

            //Rolls the hitDie object and adds result to health for every level the creature has (i.e., until the loop reaches the passed-in level value)
            for (int i = 0; i < level; i++)
            {
                health += hitDie.roll();
            }
        }

        //Rolls the d20, adds the BaseAtackBonus for it, and returns the resulting value
        public override int attack()
        {
            return BaseAttackBonus + d20.roll();
        }

        //Accesses the damage function in w1 and returns the value returned from that function
        public override int damage()
        {
            return w1.damage();
        }

        //Takes in an integer representing damage the character has taken and deducts that much from their total health value
        public override void reduce_health(int damage)
        {
            health -= damage;
        }

        //Rolls the d20 and returns the resulting value to determine order in combat
        public override void rollInitiative()
        {
            Initiative = d20.roll();
        }
    }

    public class Rogue : Character
    {
        public Rogue(string n, Weapons w, Armor a, int level) : base(n)
        {
            BaseAttackBonus = level * (3 / 4);
            hitDie = new D(6);
            w1 = w;
            block = a;
            AC += block.armor_bonus;
            health += 6;
            for (int i = 1; i < level; i++)
            {
                health += hitDie.roll();
            }
        }
        public override int attack()
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
        public override void rollInitiative()
        {
            Initiative = d20.roll();
        }
    }
    public class Fighter : Character
    {
        public Fighter(string n, Weapons w, Armor a, int level) : base(n)
        {
            BaseAttackBonus = level;
            hitDie = new D(10);
            w1 = w;
            block = a;
            AC += block.armor_bonus;
            health += 10;
            for (int i = 1; i < level; i++)
            {
                health += hitDie.roll();
            }
        }
        public override int attack()
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
        public override void rollInitiative()
        {
            Initiative = d20.roll();
        }
    }
    public class Wizard : Character
    {
        public Wizard(string n, Weapons w, Armor a, int level) : base(n)
        {
            BaseAttackBonus = level / 2;
            hitDie = new D(4);
            w1 = w;
            block = a;
            AC += block.armor_bonus;
            health += 4;
            for (int i = 1; i < level; i++)
            {
                health += hitDie.roll();
            }
        }
        public override int attack()
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
        public override void rollInitiative()
        {
            Initiative = d20.roll();
        }
    }
}