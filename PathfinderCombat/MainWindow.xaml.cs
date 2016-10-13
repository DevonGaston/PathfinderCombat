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
        CombatClasses fighter = new Fighter(1);
        Monster living_dead = new Living_Dead(3);
        Weapons sword = new Longsword();
        public MainWindow()
        {
            InitializeComponent();
        }
        void Battle(object sender, RoutedEventArgs e)
        {
            GUI.Text = fighter.name + " attacks with " + sword.name + "..." + fighter.attack() + " is rolled \n";
            GUI.Text += fighter.name + " Hits! Deals " + sword.damage() + " damage!\n";
            GUI.Text += living_dead.name + " attacks with " + living_dead.w1.name + "..." + living_dead.strike() + " is rolled\n";
            GUI.Text += living_dead.name + " Hits!  Deals " + living_dead.w1.damage() + " damage!\n";

        }
    }
}
