using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
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
using static work.mainpage;

namespace work.Pages
{
	/// <summary>
	/// MainPage.xaml 的交互逻辑
	/// </summary>
	public partial class Local : Page
	{
		MainDataModel mdm;
        List<Tuple<int, int>> Step = new List<Tuple<int, int>>();
        APIService apiService = new APIService();
        public Local()
		{
			InitializeComponent();
			mdm = new MainDataModel();
			this.DataContext = mdm;
			App.LocalInstance = this;
            this.Loaded += Local_Loaded;
        }

        public async void Local_Loaded(object sender, RoutedEventArgs e) {
            TextBlock userText = (TextBlock)this.FindName("userText");
            userText.Text = App.user.nickname;
            string path = await apiService.getProfilePicture();

            if (path != "empty")
            {
                // 创建新的位图图像
                BitmapImage bitmap = new BitmapImage();

                bitmap.BeginInit();
                bitmap.UriSource = new Uri(path);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();


                //var imageControl = App.HomeInstance.FindName("UserImageBrush") as Image;
                //// 将位图图像设置为 Ellipse 的填充
                //imageControl.Source = bitmap;
                UserImageBrush.Source = bitmap;

            }
        }

        //悔棋
        public void regret(object sender, RoutedEventArgs e)
        {
            if (Step.Count() > 0)
            {
                Tuple<int, int> lastElement = Step.Last();
                string targetBtn = "Button" + lastElement.Item1.ToString() + lastElement.Item2.ToString();
                Button btn = (Button)FindName(targetBtn);
                btn.Visibility = Visibility.Hidden;
                board[lastElement.Item1, lastElement.Item2] = 0;
                Step.RemoveAt(Step.Count - 1);
           //     suggession();
            }
        }
        //按钮阴影动效
        private void Border_MouseEnter(object sender, MouseEventArgs e)
		{
			if (sender is Border border)
			{

				border.Effect = mainpage.window.shadowEffect1;

			}
		}

		private void Border_MouseLeave(object sender, MouseEventArgs e)
		{
			if (sender is Border border)
			{

				border.Effect = null;

			}

		}
		public int[,] board = Board.getBoardInstance();
		//决定现在是谁行动 1代表黄色，-1代表蓝色
		public int nowTurn = 1;
		private bool isAnimating = false;


		//所有按钮的公共方法
		private void CommonBtnClickHandler(object sender, RoutedEventArgs e)
		{
			Button btn = sender as Button;
			// MessageBox.Show(btn.Name);


		}

		//点击棋盘canvas调用
		private async void myCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
            Point clickPoint = e.GetPosition(myCanvas);

            double canvasWidth = myCanvas.ActualWidth;
            double canvasHeight = myCanvas.ActualHeight;

             buttonWidthSize = canvasWidth * (0.142857);
            buttonHeightSize = canvasHeight * (0.166667);
            int x = Utils.getIndex(buttonHeightSize, clickPoint.Y);
            int y = Utils.getIndex(buttonWidthSize, clickPoint.X);

            string targetBtn = "Button" + x.ToString() + y.ToString();
            Button btn = (Button)FindName(targetBtn);
            int column = Grid.GetColumn(btn);
            int row = Grid.GetRow(btn);
			double mx = clickPoint.X;
            double my = clickPoint.Y;

            Utils.ShowClickCircle(myCanvas, btn, mx, my);
            if (isAnimating) return; // 如果动画正在进行，则返回
			isAnimating = true;

			//判断该点击处是否合法，合法再执行下面动画和显示
			bool isClickValid = Utils.isClickValid(x, y, board);

			if (isClickValid)
			{
				//历史记录获取坐标
				GameService.Instance.getPosition(x, y);


				btn.Visibility = Visibility.Visible;
				if (nowTurn == 1) { board[x, y] = 1; } else { board[x, y] = -1; }
                // AnimationUtils.ChessDropDownAnimation(btn,x,canvasHeight);
                //AnimationUtils.ChessRotateAnimation(btn);
                Step.Add(new Tuple<int, int>(x, y));

                //根据nowTurn显示当前按钮，后续添加逻辑时要注意何时将nowTurn取反
                if (nowTurn == 1)
				{
					BitmapImage bitmap = new BitmapImage();
					bitmap.BeginInit();
					bitmap.UriSource = new Uri(@"..\..\Images\black.png", UriKind.RelativeOrAbsolute);
					// Console.WriteLine("Image path: " + AppDomain.CurrentDomain.BaseDirectory + @"Images\OIP-C1.jpg");
					bitmap.EndInit();
					// 创建 ImageBrush 并设置其 ImageSource
					ImageBrush imageBrush = new ImageBrush();
					imageBrush.ImageSource = bitmap;
					btn.Background = imageBrush;
                    
				}
				else
				{
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(@"..\..\Images\white.png", UriKind.RelativeOrAbsolute);
                    // Console.WriteLine("Image path: " + AppDomain.CurrentDomain.BaseDirectory + @"Images\OIP-C1.jpg");
                    bitmap.EndInit();
                    // 创建 ImageBrush 并设置其 ImageSource
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = bitmap;
                    btn.Background = imageBrush;


                }
                await AnimationUtils.allAnimation(btn, x, canvasHeight, myCanvas);
				if (nowTurn == 1)
				{
                    if (Board.IsWin(x, y, 1))
                    {
                        MessageBox.Show("PLAYER1 WIN!");
                        Utils.showIsInsertHistoryWindow();
                        Board.resetBoard("Local");
                        isAnimating = false;
                        return;
                    }
                    nowTurn = -1;
                }
				else
				{
                    if (Board.IsWin(x, y, -1))
                    {
                        MessageBox.Show("PLAYER2 WIN");
                        Utils.showIsInsertHistoryWindow();
                        isAnimating = false;
                        nowTurn = 1;
                        Board.resetBoard("Local");
                        return;
                    }
                    nowTurn = 1;
                }
        
            }

			isAnimating = false;
         //   suggession();
        }

		public void jumpBackToMain(object sender, RoutedEventArgs e)
		{
			Board.resetBoard("Local");
			mainpage.window.jumpToTargetPage(WindowsID.home);
		}


		//棋盘canvas尺寸变化时调用（暂时无用，后续可能有用）
		private void myCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			double canvasWidth = myCanvas.ActualWidth;
			double canvasHeight = myCanvas.ActualHeight;
			double myCanvasFatherGridHeight = myCanvasFatherGrid.ActualHeight;

			mdm.CanvasWidth = myCanvasFatherGridHeight * 1.166667;

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
		private double buttonWidthSize;
		private double buttonHeightSize;
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
                        rotateTransform.CenterX = buttonWidthSize / 2;
                        rotateTransform.CenterY = buttonHeightSize / 2;

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

    }

}
