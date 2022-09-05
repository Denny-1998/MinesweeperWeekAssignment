using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MinesweeperWA
{
    public class Field
    {

        public Field(bool Bomb, Button btn)
        {
            HasBomb = Bomb;
            IsHidden = true;
            Value = 0;
            this.btn = btn; 
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

        public Button btn
        { get; set; }


    }
}
