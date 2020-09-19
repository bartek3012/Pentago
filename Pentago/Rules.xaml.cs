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
    /// Logika interakcji dla klasy Rules.xaml
    /// </summary>
    public partial class Rules : Page
    {
        private Menu menu;
        private string polandRules;
        private string englisRules;
        public Rules(Menu m)
        {   
            InitializeComponent();
            UKFlagButton.Visibility = Visibility.Collapsed;
            menu = m;
            englisRules = "The game board consists of the four separate " +
                "boards. The starting player place a ball in a socket of his or her choice. After placing a marble the" +
                " player turns any one of the four boards one notch (90 degrees) clock - or clockwise. A board, not " +
                "necessarily the one on witch the ball has been placed, must be turned each move. Then the second player" +
                " does the same, i.e. place ball and turns board. So on and forth. The first player to get five balls in " +
                "a row wins! The row can be horizontal, vertical or diagonal and run over two or three boards. If a player" +
                "gets five in a row when placing a ball he or she doesn't need to turn a board. If all the sockets have" +
                "been filled without any player getting five in a row the games is a draw. If both players get five in a " +
                "row as a player turns a board the game is also draw.";
            RulesTextBox.Text = englisRules;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(menu);
        }
    }
}
