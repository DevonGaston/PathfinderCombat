//Name: Kenneth Devon Gaston
//Date Last Modified: 11/11/2016
//File Name: MainWindow.xaml.cs
//Purpose: Contains functions attatched to various objects in MainWindow.xaml; effectively contains code that runs program
using System.Collections.Generic;
using System.Windows;

namespace PathfinderCombat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        List<Character> order = new List<Character>();  //List holds all characters created in program
        Armor a;  //Armor object to be defined by functions or user
        Character c; //Character object to be defined by functions or user
        Weapons w; //Weapons object to be defined by functions or user
        Dice d;  //Dice object to be defined by functions or user
        Character[] queue;  //List of characters used to arrange in order by Initiative variable in Character object before being pushed into an array
        int reduce;  //The amount of damage to be passed off to a Character's reduce_health function if hit
        int turn = 0; //Used to determine which Character acts at any point in the battle; a turn ends when a character acts
        int qcap = 0; //Keeps track of how many Characters are in the array at any point
        int select = 1;  //Used to determine what Character will be affected by the actions of the Character whose turn it is
        int attack; //Stores result of Character's attack function to determine whether or not the Character they attack takes damage
        int level;  //Used to store user input determining level of the Character object created (not implemented yet)
        string name;  //Used to store user input determining name of the Character object created (not implemented yet)
        public MainWindow()
        {
            InitializeComponent();
        }

        //Initiates Battle
        void Battle(object sender, RoutedEventArgs e)
        {
            //Prevents user from starting combat with less than 2 Characters created
            if (order.Count < 2)
            {
                GUI.Text = "Sorry, not enough combatants.";
            }
            else
            {
                GUI.Text = "Battle Start!\n";
                GUI.Text += "Characters rolling for initiative!\n";

                //rollInitiative function for each character in the list is accessed
                foreach (Character c in order)
                {
                    c.rollInitiative();
                    GUI.Text += c.name + " rolled " + c.Initiative + "\n";
                }

                //List reordered from highest Initiative value to lowest
                order.Sort(delegate (Character x, Character y)
                {
                    return y.Initiative.CompareTo(x.Initiative);
                });

                //Characters in list are sent to an array
                queue = order.ToArray();
                int i = 1;

                //Characters order and base statistics listed off
                foreach (Character c in queue)
                {
                    GUI.Text += c.name + "'s order is " + i + "\n";
                    GUI.Text += c.name + " has " + c.health + " health!\n";
                    GUI.Text += c.name + "'s AC is " + c.AC + "\n";
                    i++;
                    qcap++;
                }
                GUI.Text += "\n";

                //Informs player who goes first
                GUI.Text += "It is " + queue[turn].name + "'s turn. " + queue[turn].name + " has " + queue[turn].health + " health\n";

                //Stats TextBlock made visible to show what Character will be affected by another Characters's actions
                Stats.Visibility = Visibility.Visible;
                Stats.Text = queue[select].name + " is the intended target\n";

                //Buttons Hidden and given different functionality as needed
                Button1.Click -= Battle;
                Button1.Click += Attack;
                Button1.Content = "Attack";
                Button3.Visibility = Visibility.Hidden;
                Button2.Click -= create;
                Button2.Click += selectTarget;
                Button2.Content = "Select Target";
            }
        }

        //One Character attacks another
        void Attack(object sender, RoutedEventArgs e)
        {

            GUI.Text += queue[turn].name + " attacks " + queue[select].name + " with " + queue[turn].w1.name + "...";

            //Attack value is determined
            attack = queue[turn].attack();
            GUI.Text += attack + " is rolled\n";

            //Case 1: attack value is >= Character.AC value
            if (attack >= queue[select].AC)
            {
                GUI.Text += queue[turn].name + " Hits!\n";

                //Damage is calculated
                reduce = queue[turn].damage();

                //Check to see if the attack value is a natural 20 (i.e., the randomly determined number unaltered is 20)
                if ((attack - queue[turn].BaseAttackBonus) == 20)
                {
                    GUI.Text += "Natural 20 rolled! Rolling to confirm critical...\n";

                    //Attack value is determined again
                    attack = queue[turn].attack();

                    //Special Case 1: attack value is >= Character.AC value
                    if (attack >= queue[select].AC)
                    {
                        //Damage is multipled by Weapons' crit value
                        reduce *= queue[turn].w1.crit;
                        GUI.Text += "Critical hit confirmed.  Damage will be multiplied by " + queue[turn].w1.crit + "\n";
                    }

                    //Special Case 2: attack value is < Character.AC value
                    else
                    {
                        GUI.Text += "Critical hit failed.  Damage will be dealt normally\n";
                    }
                }

                //Affected Character's health is reduced by amount in reduce
                GUI.Text += "Attack deals " + reduce + " damage!\n";
                queue[select].reduce_health(reduce);
                GUI.Text += queue[select].name + " now has " + queue[select].health + " health\n";
                GUI.Text += "\n";
            }

            //Case2: attack value is < Character.AC value
            else
            {
                GUI.Text += "Attack missed!\n";
            }

            //Check to see if damaged Character's health is < 1
            //Case 1: Character's health is < 1
            if (queue[select].health < 1)
            {
                GUI.Text += queue[select].name + " has been slain!\n";

                //Character is removed from the list
                order.Remove(queue[select]);

                //Decrement qcap to reflect that a Character is dead
                qcap--;

                //Check to see if only one Character remains in array
                if (qcap < 2)
                {
                    GUI.Text += queue[turn].name + " won the battle!\n";

                    //Variables reset
                    turn = 0;
                    reduce = 0;
                    qcap = 0;
                    select = 1;
                    order.Clear();
                    queue = null;

                    //Functionality of buttons returned to default state
                    Button1.Click -= Attack;
                    Button1.Click += Battle;
                    Button1.Content = "Battle";
                    Button3.Visibility = Visibility.Visible;
                    Button2.Click -= selectTarget;
                    Button2.Click += create;
                    Button2.Content = "Create Characters";

                    //Stats TextBlock is hidden until the next battle
                    Stats.Visibility = Visibility.Hidden;

                    //Exit function
                    return;
                }

                //Order is redetermined
                order.Sort(delegate (Character x, Character y)
                {
                    return y.Initiative.CompareTo(x.Initiative);
                });

                //queue is assigned to new order in list
                queue = order.ToArray();
                int i = 1;
                foreach (Character c in queue)
                {
                    GUI.Text += c.name + "'s order is " + i + "\n";
                    i++;
                }
            }

            //Increment turn to allow next Character in queue to act when function is accessed again
            turn++;

            //Reset turn variable to beginning if capacity is exceeded
            if (turn >= qcap)
            {
                turn = 0;
            }
            GUI.Text += "\n";
            GUI.Text += "It is " + queue[turn].name + "'s turn. " + queue[turn].name + " has " + queue[turn].health + " health\n";

            //Increment select varaible to ensure that next target isn't the next Character whose turn it is
            select++;
            if (select > (qcap - 1))
            {
                select = 0;
            }

            //Prevents Character taking their turn from targeting themselves
            if (queue[select].name == queue[turn].name)
            {
                select++;
                //Bounds checking
                if (select >= qcap)
                {
                    select = 0;
                }
            }
            Stats.Text = queue[select].name + " is selected target\n";
        }

        //Determines which Character will be affected by Character whose turn it is during Attack function
        void selectTarget(object sender, RoutedEventArgs e)
        {
            //Next Character in queue is selected
            select++;

            //Character at beginning is selected if qcap is exceeded
            if (select >= qcap)
            {
                select = 0;
            }

            //Prevents Character taking their turn from targeting themselves
            if (queue[select].name == queue[turn].name)
            {
                select++;

                //Bounds checking
                if (select >= qcap)
                {
                    select = 0;
                }
            }

            Stats.Text = queue[select].name + " is selected target\n";
        }

        //Removes all Characters in queue
        void clear(object sender, RoutedEventArgs e)
        {
            if (order.Count == 0)
            {
                GUI.Text = "Queue Already Empty.";
            }
            else
            {
                order.Clear();
                GUI.Text = "Queue Cleared.";
            }
        }

        //Sends user to Character Creation menu
        void create(object sender, RoutedEventArgs e)
        {
            GUI.Text = "Please selext your prefered creation method\n";

            //Functionality of Button1 is changed
            Button1.Content = "Quick Create";
            Button1.Click -= Battle;
            Button1.Click += quickCreate;

            //Button 4 is made visible and usable
            Button4.Visibility = Visibility.Visible;

            //Remaining buttons hidden
            Button2.Visibility = Visibility.Hidden;
            Button3.Visibility = Visibility.Hidden;
        }

        //Creates Characters with predetermined variables and places them in queue
        void quickCreate(object sender, RoutedEventArgs e)
        {
            GUI.Text = "Please select a character to create\n";
            GUI.Text += "\n";
            //Buttons made visible and functionality changed
            Button2.Visibility = Visibility.Visible;
            Button3.Visibility = Visibility.Visible;
            Button5.Visibility = Visibility.Visible;
            Button2.Click -= create;
            Button2.Content = "Create Fighter";
            Button2.Click += createFighter;
            Button1.Click -= quickCreate;
            Button1.Click += mainMenu;
            Button1.Content = "Return to Main Menu";
            Button3.Click -= clear;
            Button3.Click += createRogue;
            Button3.Content = "Create Rogue";
            Button4.Click -= buildCharacter;
            Button4.Content = "Create Living Dead";
            Button4.Click += createUndead;
        }

        //Allows user to create a Character on their own (not implemented yet)
        void buildCharacter(object sender, RoutedEventArgs e)
        {
            return;
        }

        //Creates a Fighter with pre-determined variables
        void createFighter(object sender, RoutedEventArgs e)
        {
            w = new Longsword();
            a = new HeavyArmor();
            level = 2;
            c = new Fighter("Fighter", w, a, level);
            order.Add(c);
            GUI.Text += "Fighter has been added to queue\n";
            GUI.Text += "Weapon equipped is " + c.w1.name + ".  Deals " + c.w1.dam.name + " damage.\n";
            GUI.Text += "Armor equipped is " + c.block.name + ".  Increases AC by " + c.block.armor_bonus + ".\n";
            GUI.Text += "Level is " + level + ".  Health increased by " + c.hitDie.name + " per level.\n";
            GUI.Text += "Attack bonus is " + c.BaseAttackBonus + ".\n";
            GUI.Text += "\n";
            Button2.Visibility = Visibility.Hidden;
        }

        //Creates a Living Dead with pre-determined variables
        void createUndead(object sender, RoutedEventArgs e)
        {
            d = new D(4);
            w = new Claws();
            level = 3;
            c = new Monster("Living Dead", level, d, w, level);
            order.Add(c);
            GUI.Text += "Living Dead has been added to queue\n";
            GUI.Text += "Weapon equipped is " + c.w1.name + ".  Deals " + c.w1.dam.name + " damage.\n";
            GUI.Text += "Level is " + level + ".  Health increased by " + c.hitDie.name + " per level.\n";
            GUI.Text += "Attack bonus is " + c.BaseAttackBonus + ".\n";
            GUI.Text += "\n";
            Button4.Visibility = Visibility.Hidden;
        }

        //Creates a Rogue with pre-determined variables
        void createRogue(object sender, RoutedEventArgs e)
        {
            w = new Dagger();
            a = new MediumArmor();
            level = 2;
            c = new Rogue("Rogue", w, a, level);
            order.Add(c);
            GUI.Text += "Rogue has been added to queue\n";
            GUI.Text += "Weapon equipped is " + c.w1.name + ".  Deals " + c.w1.dam.name + " damage.\n";
            GUI.Text += "Armor equipped is " + c.block.name + ".  Increases AC by " + c.block.armor_bonus + ".\n";
            GUI.Text += "Level is " + level + ".  Health increased by " + c.hitDie.name + " per level.\n";
            GUI.Text += "Attack bonus is " + c.BaseAttackBonus + ".\n";
            GUI.Text += "\n";
            Button3.Visibility = Visibility.Hidden;
        }

        //Creates a Wizard with pre-determined variables
        void createWizard(object sender, RoutedEventArgs e)
        {
            w = new Club();
            a = new LightArmor();
            level = 2;
            c = new Wizard("Wizard", w, a, level);
            order.Add(c);
            GUI.Text += "Wizard has been added to queue\n";
            GUI.Text += "Weapon equipped is " + c.w1.name + ".  Deals " + c.w1.dam.name + " damage.\n";
            GUI.Text += "Armor equipped is " + c.block.name + ".  Increases AC by " + c.block.armor_bonus + ".\n";
            GUI.Text += "Level is " + level + ".  Health increased by " + c.hitDie.name + " per level.\n";
            GUI.Text += "Attack bonus is " + c.BaseAttackBonus + ".\n";
            GUI.Text += "\n";
            Button5.Visibility = Visibility.Hidden;
        }

        //Resets Buttons to original states
        void mainMenu(object sender, RoutedEventArgs e)
        {
            Button2.Click -= createFighter;
            Button2.Content = "Create Characters";
            Button2.Click += create;
            Button2.Visibility = Visibility.Visible;
            Button1.Content = "Battle";
            Button1.Click -= mainMenu;
            Button1.Click += Battle;
            Button3.Click -= createRogue;
            Button3.Click += clear;
            Button3.Content = "Clear Battle Queue";
            Button3.Visibility = Visibility.Visible;
            Button4.Click -= createUndead;
            Button4.Content = "Build Character";
            Button4.Click += buildCharacter;
            Button4.Visibility = Visibility.Hidden;
            Button5.Visibility = Visibility.Hidden;

        }
    }
}