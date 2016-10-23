using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace PathfinderCombat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Character> order = new List<Character>();
        Character pclass = new Fighter("Fighter", new Longsword(), 1);
        Character living_dead = new Monster("Living Dead", 3, new D4(), new Claws(), 4);
        int reduce = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        void Battle(object sender, RoutedEventArgs e)
        {
            if (order.Count > 1)
            {
                foreach (Character c in order)
                {
                    c.reduce_health(reduce);
                    if (c.health < 1)
                    {
                        GUI.Text = c.name + " has been slain!\n";
                        order.Remove(c);
                        break;
                    }
                    else
                    {
                        GUI.Text = c.name + " has " + c.health + " health\n";
                        GUI.Text += c.name + " attacks with " + c.w1.name + "...";
                        Thread.Sleep(999);
                        GUI.Text += c.attack() + " is rolled\n";
                        Thread.Sleep(999);
                        reduce = c.damage();
                        GUI.Text += c.name + " Hits! Deals " + reduce + "damage!\n";
                        
                    }
                    Thread.Sleep(999);
                }
            }
            else
            {
                GUI.Text = "Battle is won.  Please create new characters.";
            }
        }
        void BattleInit(object sender, RoutedEventArgs e)
        {
            if(order.Count >= 2)
            {
                GUI.Text = "Sorry, reached maximum capacity of combatants.";
            }
            else { 
            order.Add(pclass);
            order.Add(living_dead);
            GUI.Text = "Characters rolling for initiative!\n";
            foreach (Character c in order)
            {
                Thread.Sleep(999);
                c.setInitiative();
                GUI.Text += c.name + " rolled " + c.Initiative + "\n";
            }
            order.Sort(delegate (Character x, Character y)
            {
                return x.Initiative.CompareTo(y.Initiative);
            });
            int i = 1;
                foreach (Character c in order)
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
