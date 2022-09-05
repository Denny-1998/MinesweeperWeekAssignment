using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MinesweeperWA
{
    public class Game
    {
        public int square = 20;
        int bombCount = 40;

        Field[,] fields;
        Grid grid;





        public Game(Grid grid)
        {
            //import grid
            this.grid = grid;

            //create field array
            fields = new Field[square, square];

            //create values
            CreateField();
            CalcValues();

        }


        public Button CreateButton(int x, int y)
        {
           

            //new button
            Button btn = new Button();

            //button definitions
            
            btn.Content = "";
            btn.Background = new SolidColorBrush(Colors.Silver);
            btn.Name = "Button" + x + y;
            Grid.SetRow(btn, y);
            Grid.SetColumn(btn, x);
            grid.Children.Add(btn);

            //eventhandler (checks if there is a bomb or reveals the value)
            btn.Click += (source, e) =>
            {

                if (fields[x, y].HasBomb)
                {
                    btn.Background = new SolidColorBrush(Colors.Red);

                    MessageBox.Show("game over!\nStart new game");
                    MainWindow.newGame(grid);
                }
                else
                {

                    if (fields[x, y].Value == 0)
                    {
                        btn.Content = "";
                        btn.Background = new SolidColorBrush(Colors.White);
                        checkSpace(x,y);

                    }
                    else
                    {
                        btn.Content = fields[x, y].Value;
                    }
                }
            };



            //mark green when right clicked
            btn.MouseRightButtonDown += (sender, e) =>
            {

                //sets color to green
                if (((SolidColorBrush)btn.Background).Color == Colors.Silver)
                {
                    btn.Background = new SolidColorBrush(Colors.Green);
                }
                else
                {   
                    //or changes back to silver
                    btn.Background = new SolidColorBrush(Colors.Silver);
                }
                
            };


            return btn;
        }






        public void CreateField()
        {
            bool[] bombs = GenBombs(square * square, bombCount);
            int Counter = 0;



            for (int x = 0; x < square; x++)
            {
                for (int y = 0; y < square; y++)
                {
                    Button btn = CreateButton(x, y);


                    bool b = bombs[Counter];
                    Counter++;

                    Field field = new Field(b, btn);
                    fields[x,y] = field;
                }
            }
        }



        void freeSpace(int x, int y)
        {
            Button btn = fields[x, y].btn;

            if (fields[x, y].Value == 0)
            {
                btn.Content = "";
                btn.Background = new SolidColorBrush(Colors.White);


                //recursive method to free all empty space

                //guck mal später hier nach ob du das hinbekommst dass er immer wieder die darumliegenden felder checkt
                //
                //checkSpace(x, y);

            }
            else
            {
                btn.Content = fields[x, y].Value;
            }
        }


        //checks fields around if value == 0
        void checkSpace(int x, int y) { 

            //reveal row -1 
            if (y != 0)
            {
                if (x != 0)
                {
                    freeSpace(x-1, y-1);
                }

                freeSpace(x, y - 1);

                if (x < square)
                {
                    freeSpace(x + 1, y - 1);
                }
            }

            //reveal same row 
            if (x != 0)
            {
                freeSpace(x - 1, y);
            }

            if (x < square)
            {
                freeSpace(x + 1, y);
            }

            //reveal row + 1
            if (y < square)
            {
                if (x != 0)
                {
                    freeSpace(x - 1, y + 1);
                }

                freeSpace(x, y + 1);

                if (x < square)
                {
                    freeSpace(x + 1, y + 1);
                }
            }
            
        }





        void CalcValues()
        {
            for(int x = 0; x < square -1 ; x++)
            {
                for (int y = 0; y < square -1; y++)
                {

                    //check row -1 for bombs
                    if (y != 0)
                    {
                        if (x != 0)
                        {
                            if (fields[x - 1, y - 1].HasBomb)
                            {
                                fields[x, y].Value++;
                            }
                        }

                        if (fields[x, y - 1].HasBomb)
                        {
                            fields[x, y].Value++;
                        }

                        if (x < square) 
                        {
                            if (fields[x + 1, y - 1].HasBomb)
                            {
                                fields[x, y].Value++;
                            }
                        }
                    }

                    //check same row 
                    if (x != 0)
                    {
                        if (fields[x - 1, y].HasBomb)
                        {
                            fields[x, y].Value++;
                        }
                    }

                    if (x < square)
                    {
                        if (fields[x + 1, y].HasBomb)
                        {
                            fields[x, y].Value++;
                        }
                    }

                    //check row + 1
                    if (y < square)
                    {
                        if (x != 0)
                        {
                            if (fields[x - 1, y + 1].HasBomb)
                            {
                                fields[x, y].Value++;
                            }
                        }

                        if (fields[x, y + 1].HasBomb)
                        {
                            fields[x, y].Value++;
                        }

                        if (x < square)
                        {
                            if (fields[x + 1, y + 1].HasBomb)
                            {
                                fields[x, y].Value++;
                            }
                        }
                    }

                }
            }
        }






        bool[] GenBombs(int ArraySize, int NoOfBombs)
        {
            bool[] result = new bool[ArraySize];
            Random rand = new Random();
            for (int i = 0; i < NoOfBombs; ++i)
            {
                result[rand.Next(0,ArraySize)] = true;
            }
            return result;
        }
    }
}
