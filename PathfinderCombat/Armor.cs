namespace PathfinderCombat
{
    public abstract class Armor
    {
        public int armor_bonus;
        public Armor()
        {

        }
    }

    public class LightArmor : Armor
    {
        public LightArmor() : base()
        {
            armor_bonus = 2;
        }
    }

    public class MediumArmor : Armor
    {
        public MediumArmor() : base()
        {
            armor_bonus = 4;
        }
    }

    public class HeavyArmor : Armor
    {
        public HeavyArmor() : base()
        {
            armor_bonus = 6;
        }
    }
}
