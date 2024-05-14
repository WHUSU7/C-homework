using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// AI.xaml 的交互逻辑
    /// </summary>
    public partial class AI : Page
    {
        MainDataModel mdm;
        public AI()
        {
            InitializeComponent();
            mdm = new MainDataModel();
            this.DataContext = mdm;
		      	suggession();
		}
        public int[,] board = Board.getBoardInstance();
        //决定现在是谁行动 1代表黄色，-1代表蓝色
        public int nowTurn = 1;
        //所有按钮的公共方法
        private void CommonBtnClickHandler(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            //MessageBox.Show("sdfs");

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
                board[x, y] = 1;
                if (Board.IsWin(x, y, 1))
                {
                    MessageBox.Show("YOU Win!");
                }

                AnimationUtils.allAnimation(btn, x, canvasHeight);
                //根据nowTurn显示当前按钮，后续添加逻辑时要注意何时将nowTurn取反              
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(@"..\..\Images\OIP-C1.jpg", UriKind.RelativeOrAbsolute);
                    // Console.WriteLine("Image path: " + AppDomain.CurrentDomain.BaseDirectory + @"Images\OIP-C1.jpg");
                    bitmap.EndInit();
                    // 创建 ImageBrush 并设置其 ImageSource
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = bitmap;
                    btn.Background = imageBrush;
                    

            }

            //简单AI
            //SimpleAIPlay(x, canvasHeight);
            //困难AI
            DifficultAIPlay(x, canvasHeight);
        }

        
     
        //简单AI的封装函数
        private void SimpleAIPlay(int x, double canvasHeight)
    {
        Tuple<int, int> aiMove = Board.NextMove(nowTurn);
        if (aiMove != null)
        {
            int aiX = aiMove.Item1;
            int aiY = aiMove.Item2;
            string aiTargetBtn = "Button" + aiX.ToString() + aiY.ToString();

            Button aiBtn = (Button)FindName(aiTargetBtn);
            if (aiBtn != null)
            {
                aiBtn.Visibility = Visibility.Visible;
                board[aiX, aiY] = -1;
                aiBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FBD26A"));
                AnimationUtils.allAnimation(aiBtn, x, canvasHeight);
                nowTurn = -1;

                if (Board.IsWin(aiX, aiY, -1))
                {
                    MessageBox.Show("AI Win!");
                }
            }
            else
            {
                MessageBox.Show("Error: aiBtn is null");
            }


        }
        suggession();
    }

    //困难AI的封装函数
    private void DifficultAIPlay(int x, double canvasHeight)
        {
            Tuple<int, int> aiMove = Board.showMove();
            if (aiMove != null)
            {
                int aiX = aiMove.Item1;
                int aiY = aiMove.Item2;
                string aiTargetBtn = "Button" + aiX.ToString() + aiY.ToString();

                Button aiBtn = (Button)FindName(aiTargetBtn);
                if (aiBtn != null)
                {
                    aiBtn.Visibility = Visibility.Visible;
                    board[aiX, aiY] = -1;
                    aiBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FBD26A"));
                    AnimationUtils.allAnimation(aiBtn, x, canvasHeight);
                    nowTurn = -1;

                    if (Board.IsWin(aiX, aiY, -1))
                    {
                        MessageBox.Show("AI Win!");
                    }
                }
                else
                {
                    MessageBox.Show("Error: aiBtn is null");
                }


            }
			suggession();

		}


        //棋盘canvas尺寸变化时调用（暂时无用，后续可能有用）
        private void myCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double canvasWidth = myCanvas.ActualWidth;
            double canvasHeight = myCanvas.ActualHeight;
            double myCanvasFatherGridHeight = myCanvasFatherGrid.ActualHeight;

            mdm.CanvasWidth = myCanvasFatherGridHeight * 1.166667;

        }
        //跳转到主页
        public void jumpBackToMain(object sender, RoutedEventArgs e)
        {
            MainWindow.window.jumpToTargetPage(MainWindow.WindowsID.main);
        }
        

        //Binding绑定的数据源
        public class MainDataModel : INotifyPropertyChanged
        {
            private double _canvasWidth;

            public double CanvasWidth
            {
                get { return _canvasWidth; }
                set
                {
                    if (_canvasWidth != value)
                    {
                        _canvasWidth = value;
                        OnPropertyChanged(nameof(CanvasWidth));
                    }
                }
            }

            //元素改变时候触发的委托（监听）
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

		private void Button50_Click(object sender, RoutedEventArgs e)
		{

		}
        //位置提示
		public void suggession()
		{
			for (int i = 0; i < 7; i++)
			{
				if (board[5, i] == 0)
				{
					string targetBt = "Button5" + i.ToString();
					Button bt = (Button)FindName(targetBt);
					bt.Visibility = Visibility.Visible;
				}
				else
				{
					for (int j = 4; j >= 0; j--)
					{
						if (board[j, i] == 0)
						{
							string targetBt = "Button" + j.ToString() + i.ToString();
							Button bt = (Button)FindName(targetBt);
							bt.Visibility = Visibility.Visible;
							break;
						}
					}
				}

			}
		}
	}
}

