using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MinesweeperWA
{
    public class Game
    {
        public int square = 20;
        int bombCount = 20;

        Field[,] fields;




        public Game()
        {
            fields = new Field[square, square];
            CreateField();
            CalcValues();
        }






        public void CreateField()
        {
            bool[] bombs = GenBombs(square * square, bombCount);
            int Counter = 0;



            for (int x = 0; x < square; x++)
            {
                for (int y = 0; y < square; y++)
                {
                    bool b = bombs[Counter];
                    Counter++;

                    Field field = new Field(b);
                    fields[x,y] = field;
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
                result[10 * i + rand.NextInt64(10)] = true;
            }
            return result;
        }
    }
}
