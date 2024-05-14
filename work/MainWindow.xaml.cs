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

namespace work
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //跳转页面
        public static MainWindow window;
        public enum WindowsID { 
            main,
              history,
              ai
        };

        Frame history = new Frame() { Content = new Pages.HistoryPage() };
        Frame main = new Frame() { Content = new Pages.MainPage() };
        Frame ai= new Frame() { Content = new Pages.AI() };

        public MainWindow()
        {
            InitializeComponent();
            window = this;
        }

       

        //跳转到目标页面
        public void jumpToTargetPage(WindowsID winid)
        {
            
            

            switch (winid)
            {
                case WindowsID.main:
                    mainContent.Content = main;
                    break;
                case WindowsID.history:
                   mainContent.Content= history;
                    break;
                case WindowsID.ai:
                    mainContent.Content = ai;
                    break;


            }
        }

       public void  jumpToMain(object sender, RoutedEventArgs e) {
            MainWindow.window.jumpToTargetPage(MainWindow.WindowsID.main);
        }

       

    }
}
