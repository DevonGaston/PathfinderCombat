using System.Collections.Generic;
using System.Windows;

namespace PathfinderCombat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Character> order = new List<Character>();
        Armor a;
        Character c;
        Weapons w;
        Dice d;
        Character[] queue;
        int reduce, turn = 0, qcap = 0, select = 1, attack, level;
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
                attackButton.Click -= Battle;
                attackButton.Click += Attack;
                attackButton.Content = "Attack";
                clearButton.Visibility = Visibility.Hidden;
                createButton.Click -= create;
                createButton.Click += selectTarget;
                createButton.Content = "Select Target";
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
                attackButton.Click -= Attack;
                attackButton.Click += Battle;
                attackButton.Content = "Battle";
                clearButton.Visibility = Visibility.Visible;
                Stats.Visibility = Visibility.Hidden;
                createButton.Click -= selectTarget;
                createButton.Click += create;
                createButton.Content = "Create Characters";
                reduce = 0;
                qcap = 0;
                return;
            }
            GUI.Text += queue[turn].name + " attacks with " + queue[turn].w1.name + "...";
            attack = queue[turn].attack();
            GUI.Text += attack + " is rolled\n";
            if (attack >= queue[select].AC)
            {
                reduce = queue[turn].damage();
                GUI.Text += queue[turn].name + " Hits! Deals " + reduce + " damage!\n";
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
            GUI.Text = "Please select a character to create\n";
            createButton.Click -= create;
            createButton.Content = "Create Fighter";
            createButton.Click += createFighter;
            attackButton.Click -= Battle;
            attackButton.Click += mainMenu;
            attackButton.Content = "Return to Main Menu";
            clearButton.Click -= clear;
            clearButton.Click += createRogue;
            clearButton.Content = "Create Rogue";
            classButton1.Visibility = Visibility.Visible;
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

        void mainMenu(object sender, RoutedEventArgs e)
        {
            createButton.Click -= createFighter;
            createButton.Content = "Create Characters";
            createButton.Click += create;
            attackButton.Content = "Battle";
            attackButton.Click -= mainMenu;
            attackButton.Click += Battle;
            clearButton.Click -= createRogue;
            clearButton.Click += clear;
            clearButton.Content = "Clear Battle Queue";
            classButton1.Visibility = Visibility.Hidden;

        }
    }

}

