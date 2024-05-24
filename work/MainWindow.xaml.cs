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
    public partial class MainWindow : Window
    {
        public APIService apiService = new APIService();
        //跳转页面
        public static MainWindow window;
        public enum WindowsID { 
            main,
              history,
              ai,
              pvp
        };

        Frame history = new Frame() { Content = new Pages.HistoryPage() };
        Frame main = new Frame() { Content = new Pages.MainPage() };
        Frame ai= new Frame() { Content = new Pages.AI() };
        Frame pvp = new Frame { Content = new Pages.PVP() };
        public MainWindow()
        {
            InitializeComponent();
            window = this;
        }

        
        //登录
        public async void Login(object sender, RoutedEventArgs e)
        {
            if (nameInput.Text == ""|| passwordInput.Password=="") {
                MessageBox.Show("用户名或密码不能为空");
                return;
            }
            User u = new User(-1, nameInput.Text, passwordInput.Password);
            var result = await apiService.login(u);
            if (result >0)
            {
                MessageBox.Show("登录成功，id是：" + result.ToString());
                jumpToTargetPage(WindowsID.main);
              
            }
            else 
            {
                return;
            }
        }
        //注册
        public async void Register(object sender, RoutedEventArgs e) {
            if (nameInput.Text == "" || passwordInput.Password == "")
            {
                MessageBox.Show("用户名或密码不能为空");
                return;
            }
            User u = new User(-1, nameInput.Text, passwordInput.Password);
            var result = await apiService.register(u);
            if (result >0)
            {
                MessageBox.Show("注册成功，id是："+result.ToString());
                jumpToTargetPage(WindowsID.main);
            }
            else
            {
                return;
            }

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
                case WindowsID.pvp:
                    mainContent.Content = pvp;
                    break;
            }
        }

       public void  jumpToMain(object sender, RoutedEventArgs e) {
            MainWindow.window.jumpToTargetPage(MainWindow.WindowsID.main);
        }
        //窗口最小化和关闭
        public void WindowMinimized(object sender,RoutedEventArgs e)
        {
			this.WindowState = WindowState.Minimized;
		}
        public void WindowClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
		private void nameInput_TextChanged(object sender, TextChangedEventArgs e)
		{

        }

		private void login_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
