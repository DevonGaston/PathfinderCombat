using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PathfinderCombat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Character pclass = new Fighter("Fighter", new Longsword(), 1);
        Character living_dead = new Monster("Living Dead", 3, new D4(), new Claws(), 4);
        int reduce;
        public MainWindow()
        {
            InitializeComponent();
        }
        void Battle(object sender, RoutedEventArgs e)
        {
            GUI.Text = pclass.name + " has " + pclass.health + " health\n";
            GUI.Text += living_dead.name + " has " + living_dead.health + " health\n";
            GUI.Text += pclass.name + " attacks with " + pclass.w1.name + "..." + pclass.attack() + " is rolled \n";
            reduce = pclass.damage();
            GUI.Text += pclass.name + " Hits! Deals " + reduce + " damage!\n";
            living_dead.reduce_health(reduce);
            GUI.Text += living_dead.name + " attacks with " + living_dead.w1.name + "..." + living_dead.attack() + " is rolled\n";
            reduce = living_dead.w1.damage();
            GUI.Text += living_dead.name + " Hits!  Deals " + reduce + " damage!\n";
            pclass.reduce_health(reduce);

        }
    }
}
