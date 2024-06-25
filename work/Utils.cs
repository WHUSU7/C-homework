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
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Shapes;
using work.Utilwindows;
using static work.MainWindow;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
            if(x<0||x>5) { return false; }
            if (y<0||y>6) { return false; };
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
        //点击动画
        public static void ShowClickCircle(Canvas myCanvas,Button btn,double x,double y)
        {

            // 创建点击动画的圆
            var clickCircle = new Ellipse
            {
                Width = btn.ActualWidth,
                Height = btn.ActualWidth,
                Fill = new SolidColorBrush(Color.FromArgb(128, 255, 255, 255)), // 半透明白色
                Opacity = 0.5
            };

            // 将圆添加到 Canvas 上
            myCanvas.Children.Add(clickCircle);

            // 设置圆的初始位置为按钮位置
            Canvas.SetLeft(clickCircle, x - clickCircle.Width / 2);
            Canvas.SetTop(clickCircle, y - clickCircle.Height / 2);

            // 动态调整位置的方法
            void AdjustPosition(object sender, EventArgs e)
            {
                Canvas.SetLeft(clickCircle, x - clickCircle.Width / 2);
                Canvas.SetTop(clickCircle, y - clickCircle.Height / 2);
            }

            // 扩散动画
            DoubleAnimation expandAnim = new DoubleAnimation(clickCircle.Width, clickCircle.Width * 3, TimeSpan.FromSeconds(0.5));
            expandAnim.CurrentTimeInvalidated += (s, e) => AdjustPosition(s, e);
            expandAnim.Completed += (s, e) => myCanvas.Children.Remove(clickCircle); // 动画完成时移除圆

            // 不透明度动画
            DoubleAnimation opacityAnim = new DoubleAnimation(0.5, 0, TimeSpan.FromSeconds(0.5));

            // 开始动画
            clickCircle.BeginAnimation(Ellipse.WidthProperty, expandAnim);
            clickCircle.BeginAnimation(Ellipse.HeightProperty, expandAnim);
            clickCircle.BeginAnimation(Ellipse.OpacityProperty, opacityAnim);


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

          isInsertHistory.Left = App.AppMainWindowPosition.mainWindowLeft + App.AppMainWindowPosition.mainWindowWidth / 2 - isInsertHistory.Width/2; // 中心位置
          isInsertHistory.Top = App.AppMainWindowPosition.mainWindowTop + App.AppMainWindowPosition.mainWindowHeight / 2 -isInsertHistory.Height/2; // 中心位置
            isInsertHistory.ShowDialog();
        
        }
        //显示胜利窗口
        public static void showWinWindow()
        {
            showWin sw  = new showWin();

            sw.Left = App.AppMainWindowPosition.mainWindowLeft + App.AppMainWindowPosition.mainWindowWidth / 2 - sw.Width/2;  // 中心位置
            sw.Top = App.AppMainWindowPosition.mainWindowTop + App.AppMainWindowPosition.mainWindowHeight / 2 -sw.Height/2; // 中心位置
       
         sw.ShowDialog();

        }
        //显示失败窗口
        public static void showLoseWindow()
        {
            showLose sl = new showLose();

            sl.Left = App.AppMainWindowPosition.mainWindowLeft + App.AppMainWindowPosition.mainWindowWidth / 2 - sl.Width / 2;  // 中心位置
            sl.Top = App.AppMainWindowPosition.mainWindowTop + App.AppMainWindowPosition.mainWindowHeight / 2 - sl.Height / 2; // 中心位置

            sl.ShowDialog();

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

        public static string judgeIsWin(string content) {
   
                if (content[content.Length - 1] == 0)
                    return "败北";
                else
                    return "胜利";

    }

      




    }


    
}