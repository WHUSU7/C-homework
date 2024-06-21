using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace work.Utilwindows
{
	/// <summary>
	/// isInsertHistory.xaml 的交互逻辑
	/// </summary>
	public partial class isInsertHistory : Window
	{

		public isInsertHistory()
		{
			InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            DynamicImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/user.png"));
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetImageAndShow();
        }

        public static readonly DependencyProperty DynamicImageSourceProperty =
           DependencyProperty.Register("DynamicImageSource", typeof(ImageSource), typeof(MainWindow), new PropertyMetadata(null));
        public ImageSource DynamicImageSource
        {
            get { return (ImageSource)GetValue(DynamicImageSourceProperty); }
            set { SetValue(DynamicImageSourceProperty, value); }
        }
        public void SetImageAndShow()
        {
			string imageUri;
			if (App.isWin == true) imageUri = "pack://application:,,,/Images/WIN.png";
			else imageUri = "pack://application:,,,/Images/LOSS.png";
            DynamicImageSource = new BitmapImage(new Uri(imageUri));
            this.Show();
        }
        //confirm
        public void confirm(object sender, RoutedEventArgs e)
		{
			//调用APIService的insert函数来保存历史记录
			//是在这个window传还是点击确定后传一个确认信号等wzz完成后决定

			this.Close();
		}
		//cancel
		public void cancel(object sender, RoutedEventArgs e)
		{


			this.Close();

		}


	}
}
