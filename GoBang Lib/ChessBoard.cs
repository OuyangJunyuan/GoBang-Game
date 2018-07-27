using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoBang_Lib
{
    public class ChessBoard
    {
        public Stack<Chess> chesses_oder = new Stack<Chess>(225);

        public GoBang_Lib.Chess[,] Board =new GoBang_Lib.Chess[15, 15];

        public ChessBoard()
        {
            int i, j;
            for (i = 0; i < 15; i++)
            {
                for (j = 0; j < 15; j++)
                { Board[i, j] = new GoBang_Lib.Chess(GoBang_Lib.Type.Empety,0,false,i,j); }
            }
            if(chesses_oder!=null)chesses_oder.Clear();
        }
    }
}
