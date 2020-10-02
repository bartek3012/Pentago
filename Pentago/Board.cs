using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private Button blackMoveButton;
        private int []blackMoveRotation;
        static int SizeOfSquare = 9;
        public Board(Button[] squareButton1, Button[] squareButton2, Button[] squareButton3, Button[] squareButton4)
        {
            squaresButton = new Button[4][];
            squaresButton[0] = squareButton1;
            squaresButton[1] = squareButton2;
            squaresButton[2] = squareButton3;
            squaresButton[3] = squareButton4;
            blackMoveButton = new Button();
            blackMoveRotation = new int[2];
            allSquares = new Button[SizeOfSquare * 4];
            for (int k = 0; k < 2; k++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        allSquares[i + (j * 6) + (18 * k)] = squaresButton[2 * k][i + (j * 3)];
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        allSquares[i + 3 + (j * 6) + (18 * k)] = squaresButton[2 * k + 1][i + (j * 3)];

                    }
                }
            }
        }
        public void IsEnabledFalse()
        {

            foreach (Button[] squares in squaresButton)
            {
                foreach (Button ball in squares)
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
            if (direction == 'N') //'N' is oposit direction to clock move
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int i = 6; i >= 0; i -= 3)
                    {
                        squaresButton[square][i + j].Background = copySquare[index++];
                    }
                }

            }
            else { //direction like clock move
                for (int j = 0; j < 3; j++)
                {
                    for (int i = 6; i >= 0; i -= 3)
                    {
                        squaresButton[square][index++].Background = copySquare[j + i];
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
            for (int i = 0; i < 6; i++) //move about 6 balls, because we move by all line
            {
                for (int j = 0; j <= 6; j += 6) //we can make 5 ball in line whan we will start from first or second ball
                {
                    for (int k = 0; k <= 24; k += 6) //if all 5 ball have wanted color
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
            for (int i = 0; i < 8; i++)
            {
                if (i == 2) i = 6;
                for (int j = 0; j <= 28; j += 7)
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
            for (int i = 4; i <= 11; i++)
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
            foreach (Button ball in allSquares)
            {
                if (ball.Background == Brushes.Transparent) return false;
            }
            return true;
        }

        public bool ShowWiner()
        {
            bool whiteWin = WinCheck(Brushes.White);
            bool blackWin = WinCheck(Brushes.Black);
            if (whiteWin == true && blackWin == true)
            {
                MainWindow.play.ShowWiner("Draw!");
                IsEnabledFalse();
                return true;
            }
            else if (whiteWin)
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
            else if (FullBoard())
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
            foreach (Button ball in allSquares)
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

        public void ComputerMoveBalls()
        {
            if(ThreeInLine(Brushes.White))
            {
                return;
            }
            for (int i = 0; i < 4; i++)
            {
                if(squaresButton[i][4].Background == Brushes.Transparent)
                {
                    squaresButton[i][4].Background = Brushes.Black;
                    blackMoveButton = squaresButton[i][4];
                    return;
                }
            }
            Random random = new Random();
            while(true)
            { 
                int number = random.Next(36);
                if(allSquares[number].Background == Brushes.Transparent)
                {
                    allSquares[number].Background = Brushes.Black;
                    blackMoveButton = allSquares[number];
                    break;
                }
            }
            //ComputerMoveArrows();
        }

        public void ComputerMoveArrows()
        {

            Random random = new Random();
            int direction = random.Next(2);
            int square = random.Next(4);
            blackMoveRotation[0] = direction;
            blackMoveRotation[1] =  square;
            if (direction == 0) Rotation('N', square);
            else Rotation('P', square);
            IsEnabledTrue();
        }

        private bool WinWhiteCheck()
        {
            foreach (Button ball in allSquares)
            {
                if (ball.Background == Brushes.Transparent)
                {
                    ball.Background = Brushes.White;
                    for (int i = 0; i < 4; i++)
                    {
                        Rotation('N', i);
                        if (WinCheck(Brushes.White))
                        {
                            Rotation('P', i);
                            ball.Background = Brushes.Transparent;
                            return true;
                        }
                        else
                        {
                            Rotation('P', i);
                            Rotation('P', i);
                            if (WinCheck(Brushes.Black))
                            {
                                Rotation('N', i);
                                ball.Background = Brushes.Transparent;
                                return true;
                            }
                            else
                            {
                                Rotation('N', i);

                            }
                        }
                    }
                    ball.Background = Brushes.Transparent;
                }
            }
            return false;
        }

        public void IfWinWhite()
        {

            if (WinWhiteCheck() == false) return;
            blackMoveButton.Background = Brushes.Transparent;
            if(blackMoveRotation[0] == 0)
            {
                Rotation('P', blackMoveRotation[1]);
            }
            else
            {
                Rotation('N', blackMoveRotation[1]);
            }
            bool notWin;
            foreach (Button Blackball in allSquares)
            {
                if(Blackball.Background == Brushes.Transparent)
                {
                    Blackball.Background = Brushes.Black;
                    for (int j = 0; j < 4; j++)
                    {
                        notWin = true;
                        for (int k = 0; k < 2; k++)
                        {
                            if (k == 0)
                            {
                                Rotation('P', j);
                            }
                            else
                            {
                                Rotation('N', j);
                                Rotation('N', j);
                            }
                            foreach (Button Whiteball in allSquares)
                            {
                                if (Whiteball.Background == Brushes.Transparent)
                                {
                                    Whiteball.Background = Brushes.White;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Rotation('N', i);
                                        if (WinCheck(Brushes.White))
                                        {
                                            notWin = false;
                                            Rotation('P', i);
                                        }
                                        else
                                        {
                                            Rotation('P', i);
                                            Rotation('P', i);
                                            if (WinCheck(Brushes.White))
                                            {
                                                notWin = false;
                                            }
                                            Rotation('N', i);
                                        }
                                    }
                                    Whiteball.Background = Brushes.Transparent;
                                }
                            }
                            if (notWin == true)
                            {
                                return;
                            }    
                            if (k == 1)
                            {
                                Rotation('P', j);
                            }

                        }
                    }
                    Blackball.Background = Brushes.Transparent;
                }
            }
            blackMoveButton.Background = Brushes.Black;
            Rotation('P', 2);
        }
        public bool IfWinBlack()
        {
            foreach (Button ball in allSquares)
            {
                if (ball.Background == Brushes.Transparent)
                {
                    ball.Background = Brushes.Black;
                    for (int i = 0; i < 4; i++)
                    {
                        Rotation('N', i);
                        if (WinCheck(Brushes.Black))
                        {
                            return true;
                        }
                        else
                        {
                            Rotation('P', i);
                            Rotation('P', i);
                            if (WinCheck(Brushes.Black))
                            {
                                return true;
                            }
                            else
                            {
                                Rotation('N', i);
                                
                            }
                        }
                    }
                    ball.Background = Brushes.Transparent;
                }
            }
            return false;
        }

        public void ComputerMove()
        {
            IsEnabledFalse();
            MainWindow.blackMovement = !MainWindow.blackMovement;
            if(!IfWinBlack())
            { 
            ComputerMoveBalls();
            ComputerMoveArrows();
            IfWinWhite();
            }
            ShowWiner();
            MainWindow.play.ChangeMove();
        }
        public bool ThreeInLine(Brush ColorInLine)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j <= 6; j += 3)
                {
                    if (squaresButton[i][j + 1].Background == ColorInLine && squaresButton[i][j].Background == ColorInLine && squaresButton[i][j + 2].Background == Brushes.Transparent)
                    {
                        squaresButton[i][j + 2].Background = Brushes.Black;
                        blackMoveButton = squaresButton[i][j + 2];
                        return true;
                    }
                    if (squaresButton[i][j + 2].Background == ColorInLine && squaresButton[i][j].Background == Brushes.Transparent && squaresButton[i][j + 1].Background == ColorInLine)
                    {
                        squaresButton[i][j].Background = Brushes.Black;
                        blackMoveButton = squaresButton[i][j];
                        return true;
                    }
                }
                for (int j = 0; j < 3; j++)
                {
                    if (squaresButton[i][j + 3].Background == ColorInLine && squaresButton[i][j].Background == ColorInLine && squaresButton[i][j + 6].Background == Brushes.Transparent)
                    {
                        squaresButton[i][j + 6].Background = Brushes.Black;
                        blackMoveButton = squaresButton[i][j + 6];
                        return true;

                    }
                    if (squaresButton[i][j + 3].Background == ColorInLine && squaresButton[i][j + 6].Background == ColorInLine && squaresButton[i][j].Background == Brushes.Transparent)
                    {
                        squaresButton[i][j].Background = Brushes.Black;
                        blackMoveButton = squaresButton[i][j];
                        return true;

                    }
                }
                if (squaresButton[i][0].Background == ColorInLine && squaresButton[i][4].Background == ColorInLine && squaresButton[i][8].Background == Brushes.Transparent)
                {
                    squaresButton[i][8].Background = Brushes.Black;
                    blackMoveButton = squaresButton[i][8];
                    return true;
                }
                if (squaresButton[i][8].Background == ColorInLine && squaresButton[i][4].Background == ColorInLine && squaresButton[i][0].Background == Brushes.Transparent)
                {
                    squaresButton[i][0].Background = Brushes.Black;
                    blackMoveButton = squaresButton[i][0];
                    return true;
                }
                if (squaresButton[i][2].Background == ColorInLine && squaresButton[i][4].Background == ColorInLine && squaresButton[i][6].Background == Brushes.Transparent)
                {
                    squaresButton[i][6].Background = Brushes.Black;
                    blackMoveButton = squaresButton[i][6];
                    return true;
                }
                if (squaresButton[i][6].Background == ColorInLine && squaresButton[i][4].Background == ColorInLine && squaresButton[i][2].Background == Brushes.Transparent)
                {
                    squaresButton[i][2].Background = Brushes.Black;
                    blackMoveButton = squaresButton[i][2];
                    return true;
                }
                if (squaresButton[i][1].Background == ColorInLine && squaresButton[i][3].Background == ColorInLine && squaresButton[i][5].Background == Brushes.Transparent && squaresButton[i][7].Background == Brushes.Transparent)
                {
                    squaresButton[i][5].Background = Brushes.Black;
                    blackMoveButton = squaresButton[i][5];
                    return true;
                }
                if (squaresButton[i][1].Background == ColorInLine && squaresButton[i][5].Background == ColorInLine && squaresButton[i][3].Background == Brushes.Transparent && squaresButton[i][7].Background == Brushes.Transparent)
                {
                    squaresButton[i][5].Background = Brushes.Black;
                    blackMoveButton = squaresButton[i][5];
                    return true;
                }
                if (squaresButton[i][5].Background == ColorInLine && squaresButton[i][7].Background == ColorInLine && squaresButton[i][1].Background == Brushes.Transparent && squaresButton[i][3].Background == Brushes.Transparent)
                {
                    squaresButton[i][1].Background = Brushes.Black;
                    blackMoveButton = squaresButton[i][1];
                    return true;
                }
                if (squaresButton[i][3].Background == ColorInLine && squaresButton[i][7].Background == ColorInLine && squaresButton[i][1].Background == Brushes.Transparent && squaresButton[i][5].Background == Brushes.Transparent)
                {
                    squaresButton[i][5].Background = Brushes.Black;
                    blackMoveButton = squaresButton[i][5];
                    return true;
                }
                ///
                if (squaresButton[i][1].Background == ColorInLine && squaresButton[i][5].Background == ColorInLine && squaresButton[i][2].Background == Brushes.Transparent)
                {
                    squaresButton[i][2].Background = Brushes.Black;
                    blackMoveButton = squaresButton[i][2];
                    return true;
                }
                if (squaresButton[i][1].Background == ColorInLine && squaresButton[i][3].Background == ColorInLine && squaresButton[i][0].Background == Brushes.Transparent)
                {
                    squaresButton[i][0].Background = Brushes.Black;
                    blackMoveButton = squaresButton[i][0];
                    return true;
                }
                if (squaresButton[i][3].Background == ColorInLine && squaresButton[i][7].Background == ColorInLine && squaresButton[i][6].Background == Brushes.Transparent)
                {
                    squaresButton[i][6].Background = Brushes.Black;
                    blackMoveButton = squaresButton[i][6];
                    return true;
                }
                if (squaresButton[i][7].Background == ColorInLine && squaresButton[i][5].Background == ColorInLine && squaresButton[i][8].Background == Brushes.Transparent)
                {
                    squaresButton[i][8].Background = Brushes.Black;
                    blackMoveButton = squaresButton[i][8];
                    return true;
                }

            }
            return false;
        }
    }
}
