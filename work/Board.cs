using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace work
{
    public static class Board
    {
        private static int[,] board = { 
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
        };
        

        //单例模式，确保只有一个board
        public static int[,] getBoardInstance() { 
            return board;
        }
        
        //落子逻辑  turn =0 为白子 用+1表示 ;turn = 1为黑子 用-1表示
        private static void SetBoard(int x,int y,bool turn)
        {
            if(turn == true)//黑子
            {
                board[x,y] = -1;
            }
            else
            {
                board[x, y] = 1;
            }
        }

        //判断胜利逻辑，看x,y附近是否四连
        //白字 turn =0 +1表示
        //黑子 turn =1 -1表示
        private static bool IsWin(int x,int y,bool turn)
        {
            if (turn == true)//黑子落下，看周围-1
            {
                int count = 0;
                // 1、纵向 只会在下方黑子
                int bottomcount = 0;
                int bx = x + 1;
                while (bx < 6 && board[bx, y] == -1)
                {
                    bottomcount++;
                    bx++;
                }
                count = bottomcount + 1;
                if (count >= 4)
                {
                    return true;
                }

                // 2、横向 
                int leftcount = 0;
                int rightcount = 0;
                int ly = y - 1;
                int ry = y + 1;
                while (ly >= 0 && board[x, ly] == -1)
                {
                    leftcount++;
                    ly--;
                }
                while (ry < 7 && board[x, ry] == -1)
                {
                    rightcount++;
                    ry++;
                }
                count = leftcount + rightcount + 1;
                if (count >= 4)
                {
                    return true;
                }

                // 3、左上到右下
                int ltcount = 0;
                int rbcount = 0;
                int ltx = x - 1;
                int lty = y - 1;
                int rbx = x + 1;
                int rby = y + 1;
                while (ltx >= 0 && lty >= 0 && board[ltx, lty] == -1)
                {
                    ltcount++;
                    ltx--;
                    lty--;
                }
                while (rbx < 6 && rby < 7 && board[rbx, rby] == -1)
                {
                    rbcount++;
                    rbx++;
                    rby++;
                }
                count = ltcount + rbcount + 1;
                if (count >= 4)
                {
                    return true;
                }

                // 4、左下到右上
                int lbcount = 0;
                int rtcount = 0;
                int lbx = x + 1;
                int lby = y - 1;
                int rtx = x - 1;
                int rty = y + 1;
                while (lbx < 6 && lby >= 0 && board[lbx, lby] == -1)
                {
                    lbcount++;
                    lbx++;
                    lby--;
                }
                while (rtx >= 0 && rty < 7 && board[rtx, rty] == -1)
                {
                    rtcount++;
                    rtx--;
                    rty++;
                }
                count = lbcount + rtcount + 1;
                if (count >= 4)
                {
                    return true;
                }

                return false;

            }
            else//白子落下，看周围+1
            {
                int count = 0;
                // 1、纵向 只会在下方白子
                int bottomcount = 0;
                int bx = x + 1;
                while (bx < 6 && board[bx, y] == 1)
                {
                    bottomcount++;
                    bx++;
                }
                count = bottomcount + 1;
                if (count >= 4)
                {
                    return true;
                }

                // 2、横向 
                int leftcount = 0;
                int rightcount = 0;
                int ly = y - 1;
                int ry = y + 1;
                while (ly >= 0 && board[x, ly] == 1)
                {
                    leftcount++;
                    ly--;
                }
                while (ry < 7 && board[x, ry] == 1)
                {
                    rightcount++;
                    ry++;
                }
                count = leftcount + rightcount + 1;
                if (count >= 4)
                {
                    return true;
                }

                // 3、左上到右下
                int ltcount = 0;
                int rbcount = 0;
                int ltx = x - 1;
                int lty = y - 1;
                int rbx = x + 1;
                int rby = y + 1;
                while (ltx >= 0 && lty >= 0 && board[ltx, lty] == 1)
                {
                    ltcount++;
                    ltx--;
                    lty--;
                }
                while (rbx < 6 && rby < 7 && board[rbx, rby] == 1)
                {
                    rbcount++;
                    rbx++;
                    rby++;
                }
                count = ltcount + rbcount + 1;
                if (count >= 4)
                {
                    return true;
                }

                // 4、左下到右上
                int lbcount = 0;
                int rtcount = 0;
                int lbx = x + 1;
                int lby = y - 1;
                int rtx = x - 1;
                int rty = y + 1;
                while (lbx < 6 && lby >= 0 && board[lbx, lby] == 1)
                {
                    lbcount++;
                    lbx++;
                    lby--;
                }
                while (rtx >= 0 && rty < 7 && board[rtx, rty] == 1)
                {
                    rtcount++;
                    rtx--;
                    rty++;
                }
                count = lbcount + rtcount + 1;
                if (count >= 4)
                {
                    return true;
                }

                return false;

            }

        }

    }
}
