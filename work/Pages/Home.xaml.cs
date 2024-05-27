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
		//跳转到history页面
		public void jumpToHistory(object sender, RoutedEventArgs e)
		{

			mainpage.window.jumpToTargetPage(mainpage.WindowsID.history);
		}
		//跳转到ai页面
		public void jumpToAI(object sender, RoutedEventArgs e)
		{
			Board.resetBoard("AI");

			mainpage.window.jumpToTargetPage(mainpage.WindowsID.ai);
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
	}
}
