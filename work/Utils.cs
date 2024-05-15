using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using work.Pages;
using static work.MainWindow;


namespace work
{
    public class Utils
    {


        //获取当前坐标
        public static int getIndex(double inch, double pos)
        {

            int res = 1;
            while (inch * res <= pos)
            {
                res++;
            }
            return res - 1;
        }
        //结束判断标识符，在AI.xaml.cs中调用
        public static bool end=false;
        //判断该点击处是合法

        public static bool isClickValid(int x, int y, int[,] board)
        {
            if (end) return false;
            if (x==5 && board[x,y]==0)return true;
            if (board[x, y] != 0 || (board[x, y] == 0 && board[x+1,y]==0))
            {
                return false;
            }
            else return true;
            

        }



    }


    
}