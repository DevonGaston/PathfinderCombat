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
        Armor a;
        Character c;
        Weapons w;
        Dice d;
        Character[] queue;
        int reduce, turn = 0, qcap = 0, select = 1, attack, level;
        string name;
        public MainWindow()
        {
            InitializeComponent();
        }
        void Battle(object sender, RoutedEventArgs e)
        {
            if (order.Count < 2)
            {
                GUI.Text = "Sorry, not enough combatants.";
            }
            else
            {
                GUI.Text = "Battle Start!\n";
                GUI.Text += "Characters rolling for initiative!\n";
                foreach (Character c in order)
                {
                    c.setInitiative();
                    GUI.Text += c.name + " rolled " + c.Initiative + "\n";
                }
                order.Sort(delegate (Character x, Character y)
                {
                    return y.Initiative.CompareTo(x.Initiative);
                });
                queue = order.ToArray();
                int i = 1;
                foreach (Character c in queue)
                {
                    GUI.Text += c.name + "'s order is " + i + "\n";
                    GUI.Text += c.name + " has " + c.health + " health!\n";
                    GUI.Text += c.name + "'s AC is " + c.AC + "\n";
                    i++;
                    qcap++;
                }
                GUI.Text += "\n";
                GUI.Text += "It is " + queue[turn].name + "'s turn. " + queue[turn].name + " has " + queue[turn].health + " health\n";
                Stats.Visibility = Visibility.Visible;
                Stats.Text = queue[select].name + " is the intended target\n";
                Button1.Click -= Battle;
                Button1.Click += Attack;
                Button1.Content = "Attack";
                Button3.Visibility = Visibility.Hidden;
                Button2.Click -= create;
                Button2.Click += selectTarget;
                Button2.Content = "Select Target";
            }
        }

        void Attack(object sender, RoutedEventArgs e)
        {
            if (qcap < 2)
            {
                turn = 0;
                GUI.Text += queue[turn].name + " won the battle!\n";
                order.Clear();
                queue = null;
                Button1.Click -= Attack;
                Button1.Click += Battle;
                Button1.Content = "Battle";
                Button3.Visibility = Visibility.Visible;
                Stats.Visibility = Visibility.Hidden;
                Button2.Click -= selectTarget;
                Button2.Click += create;
                Button2.Content = "Create Characters";
                reduce = 0;
                qcap = 0;
                return;
            }
            GUI.Text += queue[turn].name + " attacks with " + queue[turn].w1.name + "...";
            attack = queue[turn].attack();
            GUI.Text += attack + " is rolled\n";
            if (attack >= queue[select].AC)
            {
                GUI.Text += queue[turn].name + " Hits!\n";
                reduce = queue[turn].damage();
                if((attack - queue[turn].BaseAttackBonus) == 20)
                {
                    GUI.Text += "Natural 20 rolled! Rolling to confirm critical...\n";
                    attack = queue[turn].attack();
                    if (attack >= queue[select].AC)
                    {
                        reduce *= queue[turn].w1.crit;
                        GUI.Text += "Critical hit confirmed.  Damage will be multiplied by " + queue[turn].w1.crit + "\n";
                    }
                    else
                    {
                        GUI.Text += "Critical hit failed.  Damage will be dealt normally\n";
                    }
                }
                GUI.Text += "Attack deals " + reduce + " damage!\n";
                queue[select].reduce_health(reduce);
                GUI.Text += queue[select].name + " now has " + queue[select].health + " health\n";
                GUI.Text += "\n";
            }

            else
            {
                GUI.Text += "Attack missed!\n";
            }
            if (queue[select].health < 1)
            {
                GUI.Text += queue[select].name + " has been slain!\n";
                order.Remove(queue[select]);
                order.Sort(delegate (Character x, Character y)
                {
                    return y.Initiative.CompareTo(x.Initiative);
                });
                queue = order.ToArray();
                int i = 1;
                foreach (Character c in queue)
                {
                    GUI.Text += c.name + "'s order is " + i + "\n";
                    i++;
                }
                qcap--;
            }
            turn++;
            if (turn >= qcap)
            {
                turn = 0;
            }
            GUI.Text += "\n";
            GUI.Text += "It is " + queue[turn].name + "'s turn. " + queue[turn].name + " has " + queue[turn].health + " health\n";
            select++;
            if (select >= qcap)
            {
                select = 0;
            }
            Stats.Text = queue[select].name + " is selected target\n";
        }

        void selectTarget(object sender, RoutedEventArgs e)
        {
            if (qcap < 2)
            {
                Stats.Text = "No more targets to select\n";
                return;
            }
            select++;
            if (select >= qcap)
            {
                select = 0;
            }
            Stats.Text = queue[select].name + " is selected target\n";
        }

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
        void create(object sender, RoutedEventArgs e)
        {
            GUI.Text = "Please selext your prefered creation method\n";
            Button1.Content = "Quick Create";
            Button1.Click -= Battle;
            Button1.Click += quickCreate;
            Button4.Visibility = Visibility.Visible;
            Button2.Visibility = Visibility.Hidden;
            Button3.Visibility = Visibility.Hidden;
        }
        void quickCreate(object sender, RoutedEventArgs e)
        {
            GUI.Text = "Please select a character to create\n";
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

        void buildCharacter(object sender, RoutedEventArgs e)
        {
            return;
        }

        void createFighter(object sender, RoutedEventArgs e)
        {
            w = new Longsword();
            a = new HeavyArmor();
            c = new Fighter("Fighter", w, a, 1);
            order.Add(c);
            GUI.Text += "Fighter has been added to queue\n";
        }

        void createUndead(object sender, RoutedEventArgs e)
        {
            d = new D(4);
            w = new Claws();
            c = new Monster("Living Dead", 3, d, w, 3);
            order.Add(c);
            GUI.Text += "Living Dead has been added to queue\n";
        }

        void createRogue(object sender, RoutedEventArgs e)
        {
            w = new Claws();
            a = new LightArmor();
            c = new Rogue("Rogue", w, a, 1);
            order.Add(c);
            GUI.Text += "Rogue has been added to queue\n";
        }

        void createWizard(object sender, RoutedEventArgs e)
        {
            w = new Club();
            a = new LightArmor();
            c = new Wizard("Wizard", w, a, 1);
            order.Add(c);
            GUI.Text += "Wizard has been added to queue\n";
        }

        void mainMenu(object sender, RoutedEventArgs e)
        {
            Button2.Click -= createFighter;
            Button2.Content = "Create Characters";
            Button2.Click += create;
            Button1.Content = "Battle";
            Button1.Click -= mainMenu;
            Button1.Click += Battle;
            Button3.Click -= createRogue;
            Button3.Click += clear;
            Button3.Content = "Clear Battle Queue";
            Button4.Click -= createUndead;
            Button4.Content = "Build Character";
            Button4.Click += buildCharacter;
            Button4.Visibility = Visibility.Hidden;
            Button5.Visibility = Visibility.Hidden;

        }
    }

}

