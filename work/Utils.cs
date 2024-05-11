using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace work
{
    public  static class Utils
    {
        //获取当前坐标
        public static int getIndex(double inch,double pos) {
          
            int res = 1;
            while (inch*res <=pos) {
                res++;
            }
            return res-1;
        }

        //判断该点击处是合法
        public static bool isClickValid(int x,int y, int[,] board) {
            if (board[x, y] == 1) { return false; }
            if (x == 6) { return true; }
            else {
                if (board[x+1,y]==1) { return true; }
                else { 
                    return false;
                }
            }

        }




    }
}
