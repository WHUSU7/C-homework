using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace work
{
    public static class Board
    {
        private static int[,] board = { 
            {0,0, 0, 0, 0, 0, 0, 0 },
            {0,0, 0, 0, 0, 0, 0, 0 },
            {0,0, 0, 0, 0, 0, 0, 0 },
            {0,0, 0, 0, 0, 0, 0, 0  },
            {0,0, 0, 0, 0, 0, 0, 0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 }, };
        

        //单例模式，确保只有一个board
        public static int[,] getBoardInstance() { 
            return board;
        }

    }
}
