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
using static work.mainpage;

namespace work.Pages
{
	/// <summary>
	/// Set.xaml 的交互逻辑
	/// </summary>
	public partial class Set : Page
	{
		public Set()
		{
			InitializeComponent();
		}

		private void resetName(object sender, RoutedEventArgs e)
		{

		}

		private void resetBackground(object sender, RoutedEventArgs e)
		{

		}

		private void resetHeadImage(object sender, RoutedEventArgs e)
		{

		}
		public void jumpBackToMain(object sender, RoutedEventArgs e)
		{
			mainpage.window.jumpToTargetPage(WindowsID.home);
		}
	}
}
