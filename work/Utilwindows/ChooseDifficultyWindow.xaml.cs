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

namespace work.Utilwindows
{
    /// <summary>
    /// ChooseDifficultyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChooseDifficultyWindow : Window
    {
        //难度变化时调用的委托
        public delegate void DifficutyChangedEventHandler(object sender, DifficutyChangedEventArgs e);
        public event DifficutyChangedEventHandler DifficultyChanged;
        public ChooseDifficultyWindow()
        {
            InitializeComponent();


        }
        //简单难度
        public void easyLevel(object sender, RoutedEventArgs e)
        {

         DifficultyChanged?.Invoke(this, new DifficutyChangedEventArgs(-1));
            this.Close();

        }
        //简单难度
        public void difficultLevel(object sender, RoutedEventArgs e)
        {

            DifficultyChanged?.Invoke(this, new DifficutyChangedEventArgs(1));
            this.Close();

        }
        
        public class DifficutyChangedEventArgs:EventArgs
        {
            public int Difficuty { get; private set; }

            public DifficutyChangedEventArgs(int difficuty)
            {
                Difficuty = difficuty;
            }


        }

    }
}
