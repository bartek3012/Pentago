using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
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
    /// Logika interakcji dla klasy Play.xaml
    /// </summary>
    public partial class Play : Page
    {
        public Play()
        {
            InitializeComponent();
        }

        private void BackMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are yosu sure? You will lost current state", "Pentago", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(MainWindow.menu);
                MainWindow.board.RestartGame(false);
                MoveButton.Background = Brushes.White;
            }

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are yosu sure? You will lost current state", "Pentago", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        public void ChangeMove()
        {
            MoveButton.Background = (MoveButton.Background == Brushes.White) ? Brushes.Black : Brushes.White;

        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.board.RestartGame(true);
            MoveButton.Visibility = Visibility.Visible;
            MoveButton.Background = Brushes.White;
            RurnTextBlock.Text = "Move of";
        }

        public void ShowWiner(string winner)
        {
            RurnTextBlock.Text = winner;
            MoveButton.Visibility = Visibility.Hidden;
        }

    }
}
