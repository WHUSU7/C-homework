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
using System.Windows.Media.Effects;
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
            websocketpvp,
            home,
            set
        };
        public DropShadowEffect shadowEffect1 = new DropShadowEffect
        {
            Color = Colors.Gray,
            BlurRadius = 15,
            ShadowDepth = 5
        };
		public DropShadowEffect shadowEffect2 = new DropShadowEffect
		{
			Color = Colors.Gray,
			BlurRadius = 10,
			ShadowDepth = 1
		};
		Frame history = new Frame() { Content = new Pages.HistoryPage() };
        Frame local = new Frame() { Content = new Pages.Local() };
        Frame ai = new Frame() { Content = new Pages.AI() };
        Frame websocketpvp = new Frame() { Content = new Pages.WebsocketPvp() };
        Frame home = new Frame() { Content = new Pages.Home() };
        Frame set = new Frame() { Content = new Pages.Set() };
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
                case WindowsID.websocketpvp:
                    mainContent.Content = websocketpvp;
                    break;
                case WindowsID.home:
                    mainContent.Content = home;
                    break;
                case WindowsID.set:
                    mainContent.Content = set;
                    break;

            }
        }
        //end


    }
}
