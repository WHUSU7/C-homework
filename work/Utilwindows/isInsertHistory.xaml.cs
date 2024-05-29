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
using static work.Utilwindows.ChooseDifficultyWindow;

namespace work.Utilwindows
{
    /// <summary>
    /// isInsertHistory.xaml 的交互逻辑
    /// </summary>
    public partial class isInsertHistory : Window
    {
        
        public isInsertHistory()
        {
            InitializeComponent();
        }

        //confirm
        public void confirm(object sender, RoutedEventArgs e)
        {
            //调用APIService的insert函数来保存历史记录
            //是在这个window传还是点击确定后传一个确认信号等wzz完成后决定

            this.Close();
        }
        //cancel
        public void cancel(object sender, RoutedEventArgs e)
        {

           
            this.Close();

        }


    }
}
