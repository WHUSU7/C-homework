﻿using System;
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
		public Local()
		{
			InitializeComponent();
			mdm = new MainDataModel();
			this.DataContext = mdm;
			App.LocalInstance = this;
			//suggession();

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

            double buttonWidthSize = canvasWidth * (0.142857);
            double buttonHeightSize = canvasHeight * (0.166667);
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
                await AnimationUtils.allAnimation(btn, x, canvasHeight, myCanvas);
            }

			isAnimating = false;
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

	}

}
