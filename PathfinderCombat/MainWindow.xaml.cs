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
        int reduce = 0, turn = 0;
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
                foreach(Character c in queue)
                {
                    GUI.Text += c.name + " has " + c.health + "health!\n";
                }
                attackButton.Click -= Battle;
                attackButton.Click += Attack;
                attackButton.Content = "Attack";
                clearButton.Click -= BattleInit;
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
                clearButton.Click -= BattleInit;
                attackButton.Click -= Attack;
                attackButton.Click += Battle;
                attackButton.Content = "Battle";
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
        void BattleInit(object sender, RoutedEventArgs e)
        {
            if (order.Count >= 2)
            {
                GUI.Text = "Sorry, reached maximum capacity of combatants.";
            }
            else
            {
                pclass = new Fighter("Fighter", new Longsword(), 1);
                order.Add(pclass);
                living_dead = new Monster("Living Dead", 3, new D4(), new Claws(), 4);
                order.Add(living_dead);
                GUI.Text = "Characters rolling for initiative!\n";
                foreach (Character c in order)
                {
                    c.setInitiative();
                    GUI.Text += c.name + " rolled " + c.Initiative + "\n";
                }
                order.Sort(delegate (Character x, Character y)
                {
                    return x.Initiative.CompareTo(y.Initiative);
                });
                queue = order.ToArray();
                int i = 1;
                foreach (Character c in queue)
                {
                    GUI.Text += c.name + "'s order is " + i + "\n";
                    i++;
                }
            }
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
    }
}

