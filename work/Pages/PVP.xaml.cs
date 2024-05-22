using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
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
using System.Windows.Threading;

namespace work.Pages
{
    /// <summary>
    /// PVP.xaml 的交互逻辑
    /// </summary>
    public partial class PVP : Page
    {
		//数据绑定
		public class MainDataModel : INotifyPropertyChanged
		{
			private double _canvasWidth;
			private string _matchText;
			private string _backText;
			private string _ourSide;
			private string _oppSide;
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

			public string MatchText
			{
				get { return _matchText; }
				set
				{
					if (_matchText != value)
					{
						_matchText = value;
						OnPropertyChanged(nameof(MatchText));
					}
				}
			}

			public string BackText
			{
				get { return _backText; }
				set
				{
					if (_backText != value)
					{
						_backText = value;
						OnPropertyChanged(nameof(BackText));
					}
				}
			}
			public string OurSide
			{
				get { return _ourSide; }
				set
				{
					if (_ourSide != value)
					{
						_ourSide = value;
						OnPropertyChanged(nameof(OurSide));
					}
				}
			}
			public string OppSide
			{
				get { return _oppSide; }
				set
				{
					if (_oppSide != value)
					{
						_oppSide = value;
						OnPropertyChanged(nameof(OppSide));
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
		MainDataModel mdm;
		private DispatcherTimer timer;
		private int timeLeft;
		public PVP()
        {
            InitializeComponent();
			mdm = new MainDataModel();
			this.DataContext = mdm;
			App.PVPInstance = this;
			timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += Timer_Tick;
		}
		//落子倒计时
		private void Timer_Tick(object sender, EventArgs e)
		{
			if (timeLeft > 0)
			{
				timeLeft--;
				mdm.OurSide = timeLeft.ToString();
			}
			else
			{
				timer.Stop();
				
			}
		}
		private void countDown1()
		{
			timeLeft = 9;
			mdm.OurSide=timeLeft.ToString();
			timer.Start();
		}
		private void countDown2()
		{
			timeLeft = 9;
			mdm.OppSide = timeLeft.ToString();
			timer.Start();
		}
		public void jumpBackToMain(object sender, RoutedEventArgs e)
        {
            MainWindow.window.jumpToTargetPage(MainWindow.WindowsID.main);
        }
		//开始匹配，涉及网络通信，匹配成功后，按钮更改
        public void startToMatch(object sender, RoutedEventArgs e)
        {
			mdm.MatchText = "匹配中";
			mdm.BackText = "终止对局并返回主页";
			//match();匹配函数
			mdm.MatchText = "正在对局";
			countDown1();
			//交互函数
			countDown2();
		}
		

		//点击落子操作，与aixaml.cs逻辑类似
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

		}
		//外观适应，无需更改
		private void myCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			double canvasWidth = myCanvas.ActualWidth;
			double canvasHeight = myCanvas.ActualHeight;
			double myCanvasFatherGridHeight = myCanvasFatherGrid.ActualHeight;

			mdm.CanvasWidth = myCanvasFatherGridHeight * 1.166667;

		}
		private void CommonBtnClickHandler(object sender, RoutedEventArgs e)
		{
			Button btn = sender as Button;
		}
		//数据绑定
		
	}
}
