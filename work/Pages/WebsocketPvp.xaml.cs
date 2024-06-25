using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static work.mainpage;

namespace work.Pages
{
	/// <summary>
	/// WebsocketPvp.xaml 的交互逻辑
	/// </summary>
	public partial class WebsocketPvp : Page
	{
		WebsocketService websocketService;

		private bool isAnimating = false;

		public int[,] board = Board.getBoardInstance();
		//决定现在是谁行动 1代表黄色，-1代表蓝色



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
						App.AppCanvasShape.width = value;
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
		private bool TimerFlag;
		private bool leftTimerFlag=false;
		private bool rightTimerFlag=false;
		public WebsocketPvp()
		{

			InitializeComponent();
			mdm = new MainDataModel();
			this.DataContext = mdm;
			App.WebsocketPVPInstance = this;
			timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += Timer_Tick;
			websocketService = new WebsocketService();
			TimerFlag = false;
		}

        // 刷新界面内容,目前会有bug，并不能实现全部刷新，后续完善
        private void WebsocketPvp_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            mdm = new MainDataModel();
            this.DataContext = mdm;
            App.WebsocketPVPInstance = this;
           // timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(1);
            //timer.Tick += Timer_Tick;
            websocketService = new WebsocketService();
            TimerFlag = false;
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

				border.Effect = mainpage.window.shadowEffect2;

			}

		}

		//通过websocket链接后端
		public async void connect()
		{
			Uri serverUri = new Uri($"ws://127.0.0.1:8000/fourchess/?param={App.AppPublicGroup.id}");
			await websocketService.ConnectAsync(serverUri);
			await websocketService.StartListeningAsync();


		}

		//发送信息
		private async void send(object json)
		{

			await websocketService.SendAsync(json);

		}



		//落子倒计时
		private void Timer_Tick(object sender, EventArgs e)
		{

			if (!TimerFlag) {
				return;
			}

			if (timeLeft > 0 && leftTimerFlag)
			{
				timeLeft--;
				mdm.OurSide = timeLeft.ToString();
			}
			else if(timeLeft > 0 && rightTimerFlag)
            {
                timeLeft--;
                mdm.OppSide = timeLeft.ToString();
            }
			else
			{
				timer.Stop();

			}
		}
		private void countDown1()
		{
			timeLeft = 9;
			mdm.OurSide = timeLeft.ToString();
			timer.Start();
		}
		private void countDown2()
		{
			timeLeft = 9;
			mdm.OppSide = timeLeft.ToString();
			timer.Start();
        }
		private void resetTime() {
            timeLeft = 9;
            mdm.OppSide = timeLeft.ToString();
            mdm.OurSide = timeLeft.ToString();
        }
		//要在接收到信息后才能调用，也就是WebsocketService里面调用
		public void setTimerAndClick()
		{

            //当前是我方落子，把倒计时设为对方并给我方加一层限制，不允许再次落子
            App.AppMsg.isDropChess = false;
            if (App.AppMsg.receiveTurn == App.AppMsg.turn)
			{
				//我方为左侧倒计时
				if (App.AppMsg.turn == "1") {
					resetTime();
					rightTimerFlag=true;
					leftTimerFlag = false;
					timer.Start();
				}
                //我方为右侧倒计时
               else if (App.AppMsg.turn == "-1")
                {
					resetTime();
                    rightTimerFlag = false;
                    leftTimerFlag = true;
                    timer.Start();
                }

            }
			//当前不是我方落子，倒计时设为我方，解除我方的落子限制
			else if (App.AppMsg.receiveTurn != App.AppMsg.turn)
			{
				App.AppMsg.isDropChess = true;
                //我方为左侧倒计时
                if (App.AppMsg.turn == "1")
                {
                    resetTime();
                    rightTimerFlag = false;
                    leftTimerFlag = true;
                    timer.Start();
                }
                //我方为右侧倒计时
                else if (App.AppMsg.turn == "-1")
                {
                    resetTime();
                    rightTimerFlag = true;
                    leftTimerFlag = false;
                    timer.Start();
                }
            }
		}
		public void jumpBackToMain(object sender, RoutedEventArgs e)

		{
			Board.resetBoard("PVP");
			mainpage.window.jumpToTargetPage(WindowsID.home);
		}
		//开始匹配，涉及网络通信，匹配成功后，按钮更改
		public void startToMatch(object sender, RoutedEventArgs e)
		{
			TimerFlag = true;
			mdm.MatchText = "匹配中";
			mdm.BackText = "终止对局并返回主页";
			//match();匹配函数
			mdm.MatchText = "正在对局";
			if (App.AppMsg.turn=="1") {
				leftTimerFlag = true;
			}
			else { 
				rightTimerFlag = true;
			}
			connect();


			countDown1();

			//交互函数
			countDown2();
		}


		//点击落子操作，与aixaml.cs逻辑类似
		private async void myCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (!App.AppMsg.isDropChess) {
				MessageBox.Show("当前不是你的回合哦，请耐心等待！");
				return;
			}
			if (isAnimating) return; isAnimating = true;
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
				
				//历史记录获取坐标
				//GameService.Instance.getPosition(x, y);

				btn.Visibility = Visibility.Visible;
				if (App.AppMsg.turn == "1") { board[x, y] = 1; } else { board[x, y] = -1; }
				if (Board.IsWin(x, y, int.Parse(App.AppMsg.turn)))
				{
					Utils.end = true;
					App.isPvpWin = true;

				}
				// AnimationUtils.ChessDropDownAnimation(btn,x,canvasHeight);
				//AnimationUtils.ChessRotateAnimation(btn);
				

				//根据nowTurn显示当前按钮，后续添加逻辑时要注意何时将nowTurn取反
				if (App.AppMsg.turn == "1")
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


				}
				else
				{

					btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FBD26A"));

				}
                await AnimationUtils.allAnimation(btn, x, canvasHeight, myCanvas);
                string mes = x.ToString() + y.ToString();
				var jsondata = new
				{
					Message = mes,
					Turn = App.AppMsg.turn,
				};
				send(jsondata);

			}
			isAnimating = false;


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


	}
}
