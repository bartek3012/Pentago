using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pentago
{
   public class Board
    {
        private Button[][] squaresButton;
        private Button[] allSquares;
        static int SizeOfSquare = 9;
        public Board(Button[] squareButton1, Button[] squareButton2, Button[] squareButton3, Button[] squareButton4)
        {

            squaresButton = new Button[4][];
            squaresButton[0] = squareButton1;
            squaresButton[1] = squareButton2;
            squaresButton[2] = squareButton3;
            squaresButton[3] = squareButton4;
            allSquares = new Button[SizeOfSquare * 4];
            for (int k = 0; k < 2; k++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        allSquares[i + (j * 6) + (18 * k)] = squaresButton[2*k][i + (j * 3)];
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        allSquares[i + 3 + (j * 6) + (18 * k)] = squaresButton[2*k+1][i + (j * 3)];

                    }
                }
            }
        }
        public void IsEnabledFalse()
        {

            foreach(Button[] squares in squaresButton)
            {
                foreach(Button ball in squares)
                {
                    ball.IsEnabled = false;
                }
            }
        }
        public void IsEnabledTrue()
        {

            foreach (Button[] squares in squaresButton)
            {
                foreach (Button ball in squares)
                {
                    ball.IsEnabled = true;
                }
            }
        }

        public void Rotation(char direction, int square)
        {
            Brush[] copySquare = new Brush[SizeOfSquare];
            for (int i = 0; i < SizeOfSquare; i++)
            {
                copySquare[i] = squaresButton[square][i].Background;
            }
            
            int index = 0;
            if(direction == 'N') //'N' is oposit direction to clock move
            {            
                for(int j=0; j<3; j++)
            { 
                for(int i=6; i>=0; i-=3)
                {
                squaresButton[square][i+j].Background = copySquare[index++];
                }
            }

            }
            else { //direction like clock move
                for (int j = 0; j < 3; j++)
                {
                    for (int i = 6; i >= 0; i -= 3)
                    {
                        squaresButton[square][index++].Background = copySquare[j+i];
                    }
                }
            }
        }
        public bool WinCheck(Brush color)
        {
            int ballsInLine = 0;
            for (int i = 0; i < 36; i += 6) //move about 6 balls, because we move by all line
            {
                for (int j = 0; j < 2; j++) //we can make 5 ball in line whan we will start from first or second ball
                {
                    for (int k = 0; k < 5; k++) //if all 5 ball have wanted color
                    {
                        if (allSquares[i + j + k].Background == color)
                        {
                            ballsInLine++;
                        }
                        else break;
                    }
                    if (ballsInLine == 5) return true;
                    else ballsInLine = 0;
                }
            }
                for (int i = 0; i <= 6; i++) //move about 6 balls, because we move by all line
                {
                    for (int j = 0; j <= 6; j+=6) //we can make 5 ball in line whan we will start from first or second ball
                    {
                        for (int k = 0; k <= 24; k+=6) //if all 5 ball have wanted color
                        {
                            if (allSquares[i + j + k].Background == color)
                            {
                                ballsInLine++;
                            }
                            else break;
                        }
                        if (ballsInLine == 5) return true;
                        else ballsInLine = 0;
                    }
                }
            for(int i=0; i<8; i++)
            {
                if (i == 2) i = 6;
                for(int j=0; j<=28; j+=7)
                {
                    if (allSquares[i + j].Background == color)
                    {
                        ballsInLine++;
                    }
                    else break;
                }
                if (ballsInLine == 5) return true;
                else ballsInLine = 0;
            }
            for (int i = 4; i <=11; i++)
            {
                if (i == 6) i = 10;
                for (int j = 0; j <= 24; j += 5)
                {
                    if (allSquares[i + j].Background == color)
                    {
                        ballsInLine++;
                    }
                    else break;
                }
                if (ballsInLine == 5) return true;
                else ballsInLine = 0;
            }
            return false;
        }
        public bool FullBoard()
        {
            foreach(Button ball in allSquares)
            {
                if (ball.Background == Brushes.Transparent) return false;
            }
            return true;
        }

        public bool ShowWiner()
        {
            bool whiteWin = WinCheck(Brushes.White);
            bool blackWin = WinCheck(Brushes.Black);
            if(whiteWin == true && blackWin == true)
            {
                MainWindow.play.ShowWiner("Draw!");
                IsEnabledFalse();
                return true;
            }
            else if(whiteWin)
            {
                MainWindow.play.ShowWiner("White won!");
                IsEnabledFalse();
                return true;
            }
            else if (blackWin)
            {
                MainWindow.play.ShowWiner("Black won!");
                IsEnabledFalse();
                return true;
            }
            else if(FullBoard())
            {
                MainWindow.play.ShowWiner("Draw!");
                IsEnabledFalse();
                return true;
            }
            return false;
        }

        public void RestartGame(bool regame)
        {
           MainWindow.blackMovement = false;
            foreach(Button ball in allSquares)
            {
                ball.Background = Brushes.Transparent;
            }
            MainWindow.HideArrows();
            if (regame)
            {
                IsEnabledTrue();
            }
            else IsEnabledFalse(); 
        }
    }
}
