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
                        c.sleep(999999);
                        GUI.Text += c.attack() + " is rolled\n";
                        c.sleep(999999);
                        reduce = c.damage();
                        GUI.Text += c.name + " Hits! Deals " + reduce + "damage!\n";
                        c.sleep(9999999);
                    }
                }
            }
            else
            {
                GUI.Text = "Battle is won.  Please create new characters.";
            }
        }
        void BattleInit(object sender, RoutedEventArgs e)
        {
            order.Add(pclass);
            order.Add(living_dead);
            GUI.Text = "Characters rolling for initiative!\n";
            foreach (Character c in order)
            {
                c.setInitiative();
                GUI.Text += c.name + " rolled " + c.Initiative + "\n";
                c.sleep(9999999);
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
}
