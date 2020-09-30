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

namespace Pentago
{
    /// <summary>
    /// Logika interakcji dla klasy Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
            withComputer = false;
        }
        public bool withComputer { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RulesButton_Click(object sender, RoutedEventArgs e)
        {
           NavigationService.Navigate(new Rules());
        }

        public void PlayerButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(MainWindow.play);
            MainWindow.board.IsEnabledTrue();
            if((sender as Button).Content.ToString() == "Single player")
            {
                withComputer = true;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
