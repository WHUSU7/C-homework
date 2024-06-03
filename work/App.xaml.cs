using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using work.Pages;
using work.Models;
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
        public static HistoryRecords history = new HistoryRecords("");

        public static User user = new User(0,"-1","-1","-1");

        public static Msg AppMsg = new Msg("empty","0");

        public static CanvasShape AppCanvasShape= new CanvasShape(0,0);
	}
}
