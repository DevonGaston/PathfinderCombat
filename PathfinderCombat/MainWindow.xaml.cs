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
        Character[] queue;
        Character pclass, living_dead;
        int reduce = 0, turn = 0, qcap = 0;
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
                    i++;
                }
                attackButton.Click -= Battle;
                attackButton.Click += Attack;
                attackButton.Content = "Attack";
                clearButton.Visibility = Visibility.Hidden;
                createButton.Visibility = Visibility.Hidden;
            }
        }

        void Attack(object sender, RoutedEventArgs e)
        {
            if (turn == 2)
            {
                turn = 0;
            }
            queue[turn].reduce_health(reduce);

            if (queue[turn].health < 1)
            {
                GUI.Text = queue[turn].name + " has been slain!\n";
                queue = null;
                order.Clear();
                attackButton.Click -= Attack;
                attackButton.Click += Battle;
                attackButton.Content = "Battle";
                clearButton.Visibility = Visibility.Visible;
                createButton.Visibility = Visibility.Visible;
                reduce = 0;
                return;
            }

            else
            {
                GUI.Text = queue[turn].name + " has " + queue[turn].health + " health\n";
                GUI.Text += queue[turn].name + " attacks with " + queue[turn].w1.name + "...";
                GUI.Text += queue[turn].attack() + " is rolled\n";
                reduce = queue[turn].damage();
                GUI.Text += queue[turn].name + " Hits! Deals " + reduce + " damage!\n";
            }
            turn++;
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
            createButton.Content = "Fighter";
            createButton.Click += createFighter;
            attackButton.Click -= Battle;
            attackButton.Click += mainMenu;
            attackButton.Content = "Return to Main Menu";
            classButton1.Visibility = Visibility.Visible;
        }

        void createFighter(object sender, RoutedEventArgs e)
        {
            if (order.Count >= 2)
            {
                GUI.Text = "Sorry, queue is too full\n";
            }
            else
            {
                order.Add(new Fighter("Fighter", new Longsword(), 1));
                GUI.Text += "Fighter has been added to queue\n";
            }
        }

        void createUndead(object sender, RoutedEventArgs e)
        {
            if(order.Count >= 2)
            {
                GUI.Text = "Sorry, Queue is too full\n";
            }
            else
            {
                order.Add(new Monster("Living Dead", 3, new D(4), new Claws(), 3));
                GUI.Text += "Living Dead has been added to queue\n";
            }
        }

        void mainMenu(object sender, RoutedEventArgs e)
        {
            createButton.Click -= createFighter;
            createButton.Content = "Create Characters";
            createButton.Click += clear;
            attackButton.Content = "Battle";
            attackButton.Click -= mainMenu;
            attackButton.Click += Battle;

        }
    }

}

