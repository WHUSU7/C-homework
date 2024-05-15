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

namespace work.Pages
{
    /// <summary>
    /// PVP.xaml 的交互逻辑
    /// </summary>
    public partial class PVP : Page
    {
		MainDataModel mdm;
		public PVP()
        {
            InitializeComponent();
			mdm = new MainDataModel();
			this.DataContext = mdm;
			App.PVPInstance = this;
		}
        public void jumpBackToMain(object sender, RoutedEventArgs e)
        {

            MainWindow.window.jumpToTargetPage(MainWindow.WindowsID.main);
        }
		//开始匹配，涉及网络通信，匹配成功后，按钮更改
        public void startToMatch(object sender, RoutedEventArgs e)
        {

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
	}
}
