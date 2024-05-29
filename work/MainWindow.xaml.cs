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
              pvp,
              websocketPvp


        };

        Frame history = new Frame() { Content = new Pages.HistoryPage() };
        Frame main = new Frame() { Content = new Pages.MainPage() };
        Frame ai= new Frame() { Content = new Pages.AI() };
        Frame pvp = new Frame { Content = new Pages.PVP() };
        Frame websocketPvp = new Frame() { Content = new Pages.WebsocketPvp() };
        public MainWindow()
        {
            InitializeComponent();
            App.mainWindow = this;
            window = this;
        }

        
        //登录
        public async void Login(object sender, RoutedEventArgs e)
        {

            if (nameInput.Text == "" || passwordInput.Password == "")
            {
                MessageBox.Show("用户名或密码不能为空");
                return;
            }
            User u = new User(-1, nameInput.Text, passwordInput.Password,nicknameInput.Text);
            var result = await apiService.login(u);
            if (result > 0)
            {
                MessageBox.Show("登录成功，id是：" + result.ToString());
               // jumpToTargetPage(WindowsID.main);

            }
            else
            {
                return;
            }
        }
        //注册
        public async void Register(object sender, RoutedEventArgs e)
        {

            if (nameInput.Text == "" || passwordInput.Password == "")
            {
                MessageBox.Show("账号或密码不能为空");
                return;
            }
            if (nicknameInput.Text == "")
            {
                MessageBox.Show("用户名不能为空");
                return;
            }
            if(passwordInput.Password != ensurepasswordInput.Password)
            {
                passwordInput.Password = "";
                ensurepasswordInput.Password = "";
                MessageBox.Show("密码不匹配，请重新输入");
                return;
            }
            User u = new User(-1, nameInput.Text, passwordInput.Password, nicknameInput.Text);
            var result = await apiService.register(u);
            if (result > 0)
            {
                MessageBox.Show("注册成功，id是：" + result.ToString());
                jumpToTargetPage(WindowsID.main);
            }
            else
            {
                return;
            }
        }

            public void change2register(object sender, RoutedEventArgs e)
        {
            nameInput.Text = "";
            passwordInput.Password = "";
            nicknameInput.Text = "";
            ensurepasswordInput.Password = "";
            // 切换 ensurepasswordInput 和 nicknameInput 的可见性
            ensurepasswordInput.Visibility = ensurepasswordInput.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            ensurePasswordLabel.Visibility = ensurePasswordLabel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            nicknameInput.Visibility = nicknameInput.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            nicknameLabel.Visibility = nicknameLabel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            login.Visibility = login.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            register.Visibility = register.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            back.Visibility = back.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            confirm.Visibility = confirm.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            Grid.SetRow(passwordlabel, 2);
            Grid.SetRow(passwordInput, 2);
        }
        public void change2login(object sender, RoutedEventArgs e)
        {
            nameInput.Text = "";
            passwordInput.Password = "";
            // 切换 ensurepasswordInput 和 nicknameInput 的可见性
            ensurepasswordInput.Visibility = ensurepasswordInput.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            ensurePasswordLabel.Visibility = ensurePasswordLabel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            nicknameInput.Visibility = nicknameInput.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            nicknameLabel.Visibility = nicknameLabel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            login.Visibility = login.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            register.Visibility = register.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            back.Visibility = back.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            confirm.Visibility = confirm.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            Grid.SetRow(passwordlabel, 3);
            Grid.SetRow(passwordInput, 3);
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
                case WindowsID.websocketPvp:
                    mainContent.Content = websocketPvp;
                    break;
            }
        }

       public void  jumpToMain(object sender, RoutedEventArgs e) {
            MainWindow.window.jumpToTargetPage(MainWindow.WindowsID.main);
        }
        public void jumpToWebsocketPvp(object sender, RoutedEventArgs e)
        {
            MainWindow.window.jumpToTargetPage(MainWindow.WindowsID.websocketPvp);
        }


    }
}
