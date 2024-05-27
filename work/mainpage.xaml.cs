using System;
using System.Collections.Generic;
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
using work.Models;
using work.Pages;

namespace work
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class mainpage : Window
	{
		//跳转页面
		public static mainpage window;
		public enum WindowsID
		{
			local,
			history,
			ai,
			pvp,
			home
		};

		Frame history = new Frame() { Content = new Pages.HistoryPage() };
		Frame local = new Frame() { Content = new Pages.Local() };
		Frame ai = new Frame() { Content = new Pages.AI() };
		Frame pvp = new Frame() { Content = new Pages.PVP() };
		Frame home =new Frame() { Content=new Pages.Home() };
		public mainpage()
		{
			InitializeComponent();
			mainContent.Content = home;
			window = this;
		}


	
		//跳转到目标页面		
		// start
		public void jumpToTargetPage(WindowsID winid)
		{
			switch (winid)
			{
				case WindowsID.local:
					mainContent.Content = local;
					break;
				case WindowsID.history:
					mainContent.Content = history;
					break;
				case WindowsID.ai:
					mainContent.Content = ai;
					break;
				case WindowsID.pvp:
					mainContent.Content = pvp;
					break;
				case WindowsID.home:
					mainContent.Content = home;
					break;

			}
		}
		//end


	}
}
