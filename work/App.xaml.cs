using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using work.Pages;

namespace work
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application

    {
        public static MainPage MainPageInstance { get; set; }
        public static AI AIInstance { get; set; }
        public static PVP PVPInstance { get; set; }
    }
}
