using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using work.Utilwindows;
using static work.MainWindow;


namespace work
{
    
    public class Utils
    {
        //结束判断标志
        public static bool end = false;
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

        //判断该点击处是合法
        public static bool isClickValid(int x, int y, int[,] board)
        {
            if (end) { return false; }
            if (board[x, y] != 0) { return false; }
            if (x == 5) { return true; }
            else
            {
                if (board[x + 1, y] != 0) { return true; }
                else
                {
                    return false;
                }
            }

        }

        //输出board当前的值
        public static void PrintBoard(int[,] board)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < board.GetLength(0); i++)
            { 
                for (int j = 0; j < board.GetLength(1); j++)
                {  
                    str.Append(board[i, j]);
                    str.Append(" ");
                }
                str.Append("\r\n"); 
            }
            MessageBox.Show(str.ToString());
        }


        //显示是否要保存历史记录窗口
        public static void showIsInsertHistoryWindow() { 
        isInsertHistory isInsertHistory = new isInsertHistory();
          
                    isInsertHistory.Owner = App.mainWindow;
            isInsertHistory.WindowStartupLocation = WindowStartupLocation.CenterOwner;



            isInsertHistory.ShowDialog();
        
        }

        //接受record字符串分割为字符串数组用于历史记录页面布局
        public static string[] SplitStringIntoPairs(string input)
        {
            // 创建一个数组来存储每对字符
            string[] pairs = new string[input.Length / 2];

            // 循环遍历字符串，每次取两个字符
            for (int i = 0; i < input.Length / 2; i += 1)
            {
                pairs[i] = input[2 * i].ToString() + input[2 * i + 1].ToString();
            }

            return pairs;
        }



    }


    
}