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
		
		public MainWindow()
		{
			InitializeComponent();
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
			User u = new User(-1, nameInput.Text, passwordInput.Password, "");
			var result = await apiService.login(u);
			if (result > 0)
			{
				MessageBox.Show("登录成功，id是：" + result.ToString());
				mainpage mainpage = new mainpage();
				mainpage.Show();
				window.Close();

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
			if (passwordInput.Password != ensurepasswordInput.Password)
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
				mainpage mainpage = new mainpage();
				mainpage.Show();
				window.Close();
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
			
			Grid.SetRow(nameInputlabel, 4);
			Grid.SetRow(nameInput, 4);

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
			
			Grid.SetRow(nameInputlabel, 3);
			Grid.SetRow(nameInput, 3);

		}

		//跳转到目标页面
		

		public void jumpToMain(object sender, RoutedEventArgs e)
		{
			mainpage mainpage = new mainpage();
			mainpage.Show();
			window.Close();
			
		}
		
		//窗口最小化和关闭
		public void WindowMinimized(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}
		public void WindowClose(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			// 检查鼠标左键是否按下
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				// 开始拖动窗体
				this.DragMove();
			}
		}
		private void nameInput_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void login_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
