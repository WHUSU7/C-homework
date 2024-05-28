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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using work.Utilwindows;
using static work.Utilwindows.ChooseDifficultyWindow;

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
            App.AIInstance = this;
            this.Loaded += (s, e) =>
            {
                suggession();
            };
        }
        //难度
        public static int difficulty=-1;

        //落子计数器
        public int tie = 0;

        public int[,] board = Board.getBoardInstance();
        //决定现在是谁行动 1代表黄色，-1代表蓝色
        public int nowTurn = 1;
        private bool isAnimating = false;
        private bool AImove = false; //AI走
        //所有按钮的公共方法
        private void CommonBtnClickHandler(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
       
            //MessageBox.Show("sdfs");

        }
        double buttonWidthSize,buttonHeightSize;
        //点击棋盘canvas调用
        private async void myCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isAnimating) return;
            isAnimating = true;
            Point clickPoint = e.GetPosition(myCanvas);

            double canvasWidth = myCanvas.ActualWidth;
            double canvasHeight = myCanvas.ActualHeight;


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
                    Utils.end = true;
                    MessageBox.Show("YOU Win!");
                }
                
                //根据nowTurn显示当前按钮，后续添加逻辑时要注意何时将nowTurn取反              
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(@"..\..\Images\chess2.gif", UriKind.RelativeOrAbsolute);
                // Console.WriteLine("Image path: " + AppDomain.CurrentDomain.BaseDirectory + @"Images\OIP-C1.jpg");
                bitmap.EndInit();
                // 创建 ImageBrush 并设置其 ImageSource
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmap;
                btn.Background = imageBrush;
                await AnimationUtils.allAnimation(btn, x, canvasHeight, myCanvas);


                //选择难度级别（后续考虑动态选择困难难度）
                switch (AI.difficulty) {
                    case -1:
                        AImove = true; 
                       await SimpleAIPlay(x, canvasHeight);
                        AImove = false;
                        break;
                    case 1:
                        AImove = true;
                       await DifficultAIPlay(x, canvasHeight);
                        AImove = false;
                        break ;
                }

                
                
                tie += 1;
                if (tie == 21)
                {
                    Utils.end = true;
                    MessageBox.Show("平局");
                }

               
            }

            isAnimating = false;
        }

        
     
        //简单AI的封装函数
        private async Task SimpleAIPlay(int x, double canvasHeight)
    {
            if (!AImove) return;
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
                   
                  await  AnimationUtils.allAnimation(aiBtn, x, canvasHeight, myCanvas);
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
    private async Task DifficultAIPlay(int x, double canvasHeight)
        {
            if (!AImove) return;
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
                   await AnimationUtils.allAnimation(aiBtn, x, canvasHeight, myCanvas);
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


        //棋盘canvas尺寸变化时调用
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
            Board.resetBoard("MainPage");
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
                        App.AppCanvasShape.width = value;
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

		
        //位置提示
		public void suggession()
		{
            double canvasWidth = myCanvas.ActualWidth;
            double canvasHeight = myCanvas.ActualHeight;
            buttonWidthSize = canvasWidth * (0.142857);
            buttonHeightSize = canvasHeight * (0.166667);
            for (int i = 0; i < 7; i++)
			{
				
					for (int j = 5; j >= 0; j--)
					{
						if (board[j, i] == 0)
						{
							string targetBt = "Button" + j.ToString() + i.ToString();
							Button bt = (Button)FindName(targetBt);
							bt.Visibility = Visibility.Visible;
                        // 创建 ImageBrush 对象
                        ImageBrush imageBrush = new ImageBrush();

                        // 设置 ImageBrush 的 ImageSource 属性为 GIF 图像的路径
                        imageBrush.ImageSource = new BitmapImage(new Uri(@"..\..\Images\circle4.gif", UriKind.Relative));
                        // 创建 RotateTransform 对象
                        RotateTransform rotateTransform = new RotateTransform();
                
                        // 将 RotateTransform 设置为 ImageBrush 的 Transform 属性
                        imageBrush.Transform = rotateTransform;
                        rotateTransform.CenterX = buttonWidthSize/2;
                        rotateTransform.CenterY = buttonHeightSize/2;

                        // 创建动画，使 RotateTransform 持续旋转
                        DoubleAnimation rotateAnimation = new DoubleAnimation
                        {
                            
                            From = 0,
                            To = 360,
                            Duration = TimeSpan.FromSeconds(1), // 设置动画持续时间为1秒
                            RepeatBehavior = RepeatBehavior.Forever // 设置动画重复执行
                        };

                        // 将动画应用到 RotateTransform 的 Angle 属性
                        rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);

                        // 将 ImageBrush 设置为按钮的背景
                        bt.Background = imageBrush;

                        break;
						}
					}
				

			}
		}
		private void lastStep(object sender, RoutedEventArgs e)
		{

		}
		private void nextStep(object sender, RoutedEventArgs e)
		{

		}

      
    }
}

