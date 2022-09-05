using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperWA
{
    public class Field
    {

        public Field(bool Bomb)
        {
            HasBomb = Bomb;
            IsHidden = true;
            Value = 0;
        }

        public bool HasBomb
        {
            get;
            set;
        }

        public int Value
        {
            get;
            set;
        }

        public bool IsHidden
        {
            get;
            set;
        }





    }
}
