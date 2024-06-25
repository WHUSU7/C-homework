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
using System.Windows.Shapes;
using work.Models;

namespace work.Utilwindows
{
    /// <summary>
    /// showWin.xaml 的交互逻辑
    /// </summary>
    public partial class showWin : Window
    {
        APIService apiService = new APIService();
        public showWin()
        {
            InitializeComponent();
        }

        public async void confirm(object sender, RoutedEventArgs e) {

            TextBox newName = (TextBox)this.FindName("nameInput");
            User user = new User(App.user.id,App.user.name,App.user.password,newName.Text);
           await apiService.modifyUserName(user,App.user.id);
            this.Close();
        }

    }
}
