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
	/// Home.xaml 的交互逻辑
	/// </summary>
	public partial class Home : Page
	{
		public Home()
		{
			InitializeComponent();

		}
		private void CommonBtnClickHandler(object sender, RoutedEventArgs e)
		{
			Button btn = sender as Button;
			// MessageBox.Show(btn.Name);


		}
		//退出登录页面
		public void logout(object sender, RoutedEventArgs e)
		{			
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			mainpage.window.Close();
		}
		//跳转到设置页面
		public void setting(object sender, RoutedEventArgs e)
		{
			mainpage.window.jumpToTargetPage(mainpage.WindowsID.set);
		}
		
		
		//跳转到pvp页面
		public void jumpToPvp(object sender, RoutedEventArgs e)
		{

			mainpage.window.jumpToTargetPage(mainpage.WindowsID.pvp);
		}
		//跳转到local页面
		public void jumpToLocal(object sender, RoutedEventArgs e)
		{

			mainpage.window.jumpToTargetPage(mainpage.WindowsID.local);
		}
		//跳转到history页面
		public void jumpToHistory(object sender, RoutedEventArgs e)
		{

			mainpage.window.jumpToTargetPage(mainpage.WindowsID.history);
		}
		public void easyAi(object sender, RoutedEventArgs e)
		{
			AI.difficulty = -1;
			mainpage.window.jumpToTargetPage(mainpage.WindowsID.ai);
		}
		public void difficultAi(object sender, RoutedEventArgs e)
		{
			AI.difficulty = 1;
			mainpage.window.jumpToTargetPage(mainpage.WindowsID.ai);
		}
		//ai难度选择
		public void jumpToAI(object sender, RoutedEventArgs e)
		{
			AIBtn.Visibility = AIBtn.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			LocalBtn.Visibility = LocalBtn.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			PeopleBtn.Visibility = PeopleBtn.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			HistoryBtn.Visibility = HistoryBtn.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			Easy.Visibility = Easy.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			Difficult.Visibility = Difficult.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			Return.Visibility = Return.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
		}
		public void returnback(object sender, RoutedEventArgs e)
		{
			AIBtn.Visibility = AIBtn.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			LocalBtn.Visibility = LocalBtn.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			PeopleBtn.Visibility = PeopleBtn.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			HistoryBtn.Visibility = HistoryBtn.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			Easy.Visibility = Easy.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			Difficult.Visibility = Difficult.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			Return.Visibility = Return.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
		}
	}
}
