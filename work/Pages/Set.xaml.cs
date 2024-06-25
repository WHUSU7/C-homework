using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        private APIService apiService = new APIService();
		public Set()
		{
			InitializeComponent();
		}
		//按钮阴影动效
		private void Border_MouseEnter(object sender, MouseEventArgs e)
		{
			if (sender is Border border)
			{

				border.Effect = mainpage.window.shadowEffect1;

			}
		}

		private void Border_MouseLeave(object sender, MouseEventArgs e)
		{
			if (sender is Border border)
			{

				border.Effect = mainpage.window.shadowEffect2;

			}

		}
		private void resetName(object sender, RoutedEventArgs e)
		{
            Utils.showWinWindow();
		}

		private void resetBackground(object sender, RoutedEventArgs e)
		{

		}

		private async void resetHeadImage(object sender, RoutedEventArgs e)
		{
            // 创建文件选择对话框
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            // 显示对话框并检查用户是否选择了文件
            if (openFileDialog.ShowDialog() == true)
            {
                // 获取用户选择的文件路径
                string selectedFileName = openFileDialog.FileName;
             
                //// 创建新的位图图像
                //BitmapImage bitmap = new BitmapImage();
                //bitmap.BeginInit();
                //bitmap.UriSource = new Uri(selectedFileName);
                //bitmap.CacheOption = BitmapCacheOption.OnLoad;
                //bitmap.EndInit();
                //ImageBrush image = (ImageBrush)App.HomeInstance.FindName("UserImageBrush");
                //// 将位图图像设置为 Ellipse 的填充
                //image.ImageSource = bitmap;

                // 保存图片到/bin/Image下
                string savedFilePath = SaveImageToDirectory(selectedFileName);
                //保存图片到数据库
               await  apiService.updateProfilePicture(selectedFileName);
                // 保存图片路径到/bin/Settings/config.txt
                SaveImagePathToConfig(savedFilePath);
            }
        }
        private const string SaveDirectory = "../Images";
        private const string ConfigFilePath = "../Settings/config.txt";
        private string SaveImageToDirectory(string selectedFilePath)
        {
            // 获取文件名
            string fileName = System.IO.Path.GetFileName(selectedFilePath);

            // 目标路径
            string targetPath = System.IO.Path.Combine(SaveDirectory, fileName);

            // 确保路径是绝对路径
            targetPath = System.IO.Path.GetFullPath(targetPath);
            // 复制文件到目标路径
            File.Copy(selectedFilePath, targetPath, true);

            return targetPath;
        }

        private void SaveImagePathToConfig(string imagePath)
        {
            File.WriteAllText(ConfigFilePath, imagePath);
        }
        public void jumpBackToMain(object sender, RoutedEventArgs e)
		{
			mainpage.window.jumpToTargetPage(WindowsID.home);
		}

        public void jumpToHistory(object sender, RoutedEventArgs e)
        {
            mainpage.window.jumpToTargetPage(mainpage.WindowsID.history);


        }


    }
}
