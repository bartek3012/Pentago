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
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            menu = new Menu();
            play = new Play();
            MenuFrame.NavigationService.Navigate(menu);
            squareButon1 = new Button[9];
            squareButon1[0] = Button11; squareButon1[1] = Button12; squareButon1[2] = Button13; squareButon1[3] = Button14; squareButon1[4] = Button15;
            squareButon1[5] = Button16; squareButon1[6] = Button17; squareButon1[7] = Button18; squareButon1[8] = Button19;
            squareButon2 = new Button[9];
            squareButon2[0] = Button21; squareButon2[1] = Button22; squareButon2[2] = Button23; squareButon2[3] = Button24; squareButon2[4] = Button25;
            squareButon2[5] = Button26; squareButon2[6] = Button27; squareButon2[7] = Button28; squareButon2[8] = Button29;
            squareButon3 = new Button[9];
            squareButon3[0] = Button31; squareButon3[1] = Button32; squareButon3[2] = Button33; squareButon3[3] = Button34; squareButon3[4] = Button35;
            squareButon3[5] = Button36; squareButon3[6] = Button37; squareButon3[7] = Button38; squareButon3[8] = Button39;
            squareButon4 = new Button[9];
            squareButon4[0] = Button41; squareButon4[1] = Button42; squareButon4[2] = Button43; squareButon4[3] = Button44; squareButon4[4] = Button45;
            squareButon4[5] = Button46; squareButon4[6] = Button47; squareButon4[7] = Button48; squareButon4[8] = Button49;
            arrowsButton = new Button[8];
            arrowsButton[0] = ButtonArrow1P; arrowsButton[1] = ButtonArrow1N; arrowsButton[2] = ButtonArrow2P; arrowsButton[3] = ButtonArrow2N;
            arrowsButton[4] = ButtonArrow3P; arrowsButton[5] = ButtonArrow3N; arrowsButton[6] = ButtonArrow4P; arrowsButton[7] = ButtonArrow4N;
            board = new Board(squareButon1, squareButon2, squareButon3, squareButon4);
            HideArrows();
            board.IsEnabledFalse();
            blackMovement = false;
        }
        private Button[] squareButon1;
        private Button[] squareButon2;
        private Button[] squareButon3;
        private Button[] squareButon4;
        private static Button[] arrowsButton;
        public static Board board { get; private set; }
        public static Menu menu { get; private set; }
        public static Play play { get; private set; }
        public static bool blackMovement { get; set; }
        private void ButtonBall_Click(object sender, RoutedEventArgs e)
        {
            if(blackMovement) (sender as Button).Background = Brushes.Black;
            else (sender as Button).Background = Brushes.White;
            blackMovement = !blackMovement;            
            board.IsEnabledFalse();
            if(!board.ShowWiner())
            { 
                foreach (Button arrow in arrowsButton)
                {
                arrow.Visibility = Visibility.Visible;
                }
            }

        }

        private void ButtonArrow_Click(object sender, RoutedEventArgs e)
        {
            HideArrows();
            board.IsEnabledTrue();
            string nameButton = (sender as Button).Name;
            char direction = nameButton[12];
            char NumberSquare = nameButton[11];
            board.Rotation(direction, NumberSquare-49); //decrease number by 48 because of ASCI and decrese by 1 because of numeration from 0 in array
            board.ShowWiner();
            play.ChangeMove();
        }

        public static void HideArrows()
        {
            foreach (Button arrow in arrowsButton)
            {
                arrow.Visibility = Visibility.Collapsed;
            }
        }
    }
}
