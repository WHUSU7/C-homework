using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace work.Pages
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }
        public int[,] board = Board.getBoardInstance();
        //决定现在是谁行动 1代表黄色，-1代表蓝色
        public int nowTurn = 1;
       

        //所有按钮的公共方法
        private void CommonBtnClickHandler(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MessageBox.Show(btn.Name);

        }

        //点击棋盘canvas调用
        private void myCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPoint = e.GetPosition(myCanvas);

            double canvasWidth = myCanvas.ActualWidth;
            double canvasHeight = myCanvas.ActualHeight;

            double buttonWidthSize = canvasWidth * (0.142857);
            double buttonHeightSize = canvasHeight * (0.166667);

            //获取当前点击的区域，并转换成对应按钮实例和坐标
            int x = Utils.getIndex(buttonHeightSize, clickPoint.Y);
            int y = Utils.getIndex(buttonWidthSize, clickPoint.X);
            string targetBtn = "Button" + x.ToString() + y.ToString();
            Button btn = (Button)FindName(targetBtn);

            //判断该点击处是否合法，合法再执行下面动画和显示
            bool isClickValid = Utils.isClickValid(x, y, board);
            if (isClickValid)
            {
                btn.Visibility = Visibility.Visible;
                if (nowTurn == 1) { board[x, y] = 1; } else { board[x, y] = -1; }
                // AnimationUtils.ChessDropDownAnimation(btn,x,canvasHeight);
                //AnimationUtils.ChessRotateAnimation(btn);
                AnimationUtils.allAnimation(btn, x, canvasHeight);

                //根据nowTurn显示当前按钮，后续添加逻辑时要注意何时将nowTurn取反
                if (nowTurn == 1)
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(@"..\..\Images\OIP-C1.jpg", UriKind.RelativeOrAbsolute);
                    // Console.WriteLine("Image path: " + AppDomain.CurrentDomain.BaseDirectory + @"Images\OIP-C1.jpg");
                    bitmap.EndInit();
                    // 创建 ImageBrush 并设置其 ImageSource
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = bitmap;
                    btn.Background = imageBrush;
                    nowTurn = -1;
                }
                else
                {
                    btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FBD26A"));
                    nowTurn = 1;
                }

            }
            // MessageBox.Show($"(x,y):({clickPoint.X},{clickPoint.Y})");
            // MessageBox.Show($"width,height:({canvasWidth},{canvasHeight})");Bl
            //MessageBox.Show($"pos:({x},{y})");

        }




        //棋盘canvas尺寸变化时调用（暂时无用，后续可能有用）
        private void myCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double canvasWidth = myCanvas.ActualWidth;
            double canvasHeight = myCanvas.ActualHeight;

        }
        public void jumpToHistory(object sender, RoutedEventArgs e)
        {
            MainWindow.window.jumpToTargetPage(MainWindow.WindowsID.history);
        }


    }
}
