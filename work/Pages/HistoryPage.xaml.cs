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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using work.Models;
using System.Globalization;
using work.Utilwindows;
using work;
using System.Collections;
using System.Windows.Media.Animation;
using static work.mainpage;
using System.Runtime.Remoting.Channels;

namespace work.Pages
{
    /// HistoryPage.xaml 的交互逻辑
    public partial class HistoryPage : Page, INotifyPropertyChanged
    {

        //public ObservableCollection<MoveRecord> MoveRecords { get; set; }
        public MoveRecord CurrentRecord { get; set; }
        private APIService apiService = new APIService();
        public static int index = 0;
        // public ICommand SelectCommand { get;  set; }
        public CombinedViewModel combine;
        public HistoryPage()
        {
            InitializeComponent();
            
            combine = new CombinedViewModel();
            combine.HistoryViewModel.ClearChessboardAction = ClearChessboard;
            this.DataContext = combine;
            this.Loaded  += HistoryPage_Loaded;
            App.HistoryPageInstance = this;
        }
        private void HistoryPage_Loaded(object sender, RoutedEventArgs e)
        {
            getHistories();
            myLoad();
        }
            private void myLoad()
        {
            combine.HistoryViewModel.CurrentRecord.MoveString = App.TemphistoryFromMain;
        
        }
        //棋盘canvas尺寸变化时调用
        private void myCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double canvasWidth = myCanvas.ActualWidth;
            double canvasHeight = myCanvas.ActualHeight;
            double myCanvasFatherGridHeight = myCanvasFatherGrid.ActualHeight;
            combine.MainDataModel.CanvasWidth = myCanvasFatherGrid.ActualHeight * 1.166667;

        }


        // INotifyPropertyChanged 接口的实现
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //清空棋盘方法

        //页面跳转
        public void jumpBackToMain(object sender, RoutedEventArgs e)
        {
            ClearChessboard();
            index = 0;
            mainpage.window.jumpToTargetPage(WindowsID.home);
        }

        //获取单条历史记录
        public async void getHistoryById(object sender, RoutedEventArgs e)
        {
            var history = await apiService.getSingleHistory(1);
            MessageBox.Show(history);

        }

        //获取所有历史记录
        public async void getHistories()
        {
            var historyList = await apiService.getHistories(App.user.id);
 

            foreach (History item in historyList)
            {
                combine.HistoryViewModel.MoveRecords.Add(new MoveRecord { MoveString = item.content });
             

            }

        }


      

        //插入历史记录
        public  async void insertHistory()
        {
            // 获取当前时间
            DateTime currentTime = DateTime.Now;
         
            //把这个替换成要插入的历史记录就行了,需求参数为时间，对战类型，落子内容
            string content = GameService.Instance.singleHistory; //历史记录内容
            string matchTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss"); //历史记录时间
            string matchType = GameService.Instance.competeType;//历史记录类型
            string isWin = Utils.judgeIsWin(GameService.Instance.singleHistory);
            await apiService.insertHistory(new History(-1, content,matchTime,matchType,isWin), App.user.id);
            //每次进入都要清空
            GameService.Instance.clearData();
        }

        //清空棋盘棋子
        public void ClearChessboard()
        {
            foreach (var child in ChessboardGrid.Children)
            {
                if (child is Button button)
                {
                    button.Background = Brushes.Transparent;
                }
            }
        }
        private bool isAnimating = false;
        //渲染方法:下一步
        public async void nextButton(object sender, RoutedEventArgs e)
        {
            if (isAnimating) return; // 如果动画正在进行，则返回
            isAnimating = true;
            // 读取当前索引处的数组元素值
            
            if (combine.HistoryViewModel.CurrentRecord != null)
            {
                if (index < combine.HistoryViewModel.CurrentRecord.Moves.Length)
                {

                    if (combine.HistoryViewModel.CurrentRecord.Moves[index] != null)
                    {
                        string buttonName = combine.HistoryViewModel.CurrentRecord.Moves[index];
                        Button targetButton = this.FindName("Button" + buttonName) as Button;
                        //棋子渲染，两种类型的棋子
                        if (targetButton != null && (index % 2 == 0))
                        {
                            targetButton.Visibility = Visibility.Visible;
                            BitmapImage bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.UriSource = new Uri(@"..\..\Images\chess2.gif", UriKind.RelativeOrAbsolute);
                            // Console.WriteLine("Image path: " + AppDomain.CurrentDomain.BaseDirectory + @"Images\OIP-C1.jpg");
                            bitmap.EndInit();
                            // 创建 ImageBrush 并设置其 ImageSource
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = bitmap;
                            targetButton.Background = imageBrush;
                            char s = buttonName[0];
                            int x = s - '0';
                            double canvasHeight = myCanvas.ActualHeight;
                            await AnimationUtils.allAnimation(targetButton, x, canvasHeight, myCanvas);

                        }
                        else if (targetButton != null && index % 2 == 1)
                        {
                            targetButton.Visibility = Visibility.Visible;
                            targetButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FBD26A"));
                            char s = buttonName[0];
                            int x = s - '0';
                            double canvasHeight = myCanvas.ActualHeight;
                            await AnimationUtils.allAnimation(targetButton, x, canvasHeight, myCanvas);
                        }

                        index++;
                    }
                }
                else
                {
                    MessageBox.Show("已到最后一步");
                }
            }
            isAnimating = false;

        }
        //渲染方法:上一步                                        
        public void lastButton(object sender, RoutedEventArgs e)
        {
            if (isAnimating) return; // 如果动画正在进行，则返回
            isAnimating = true;
            if (combine.HistoryViewModel.CurrentRecord != null)
            {
                if (index > 0)
                {
                    index--;
                    if (index < combine.HistoryViewModel.CurrentRecord.Moves.Length)
                    {
                        if (combine.HistoryViewModel.CurrentRecord.Moves[index] != null)
                        {
                            string buttonName = combine.HistoryViewModel.CurrentRecord.Moves[index];
                            Button targetButton = this.FindName("Button" + buttonName) as Button;
                            if (targetButton != null)
                            {
                                targetButton.Background = Brushes.Transparent;
                            }
                        }
                        else
                        {
                            MessageBox.Show("已到第一步");
                        }
                    }

                }

                else
                    MessageBox.Show("已到第一步");
            }
            isAnimating = false;
        }

        private void ChessPieceButton_Click(object sender, RoutedEventArgs e)
        {
            // 切换按钮的可见性
            string targetBtn = "Button00";
            Button btn = (Button)FindName(targetBtn);
            btn.Visibility = Visibility.Visible;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(@"..\..\Images\chess2.gif", UriKind.RelativeOrAbsolute);
            // Console.WriteLine("Image path: " + AppDomain.CurrentDomain.BaseDirectory + @"Images\OIP-C1.jpg");
            bitmap.EndInit();
            // 创建 ImageBrush 并设置其 ImageSource
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = bitmap;
            btn.Background = imageBrush;

        }
        //Binding绑定的数据源
        public class MainDataModel : INotifyPropertyChanged
        {
            private double _canvasWidth;

            public double CanvasWidth
            {
                get { return _canvasWidth; }
                set
                {
                    if (_canvasWidth != value)
                    {
                        _canvasWidth = value;
                        App.AppCanvasShape.width = value;
                        OnPropertyChanged(nameof(CanvasWidth));
                    }
                }
            }

            //元素改变时候触发的委托（监听）
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public class HistoryViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MoveRecord> MoveRecords { get; set; }
            public MoveRecord CurrentRecord { get; set; }
            public ICommand SelectCommand { get; set; }
            public ArrayList overallRecord = new ArrayList();
            public Action ClearChessboardAction { get; set; }
            //命令类监听
            public class RelayCommand<T> : ICommand
            {
                private readonly Action<T> _execute;
                private readonly Predicate<T> _canExecute;

                public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
                {
                    _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                    _canExecute = canExecute;
                }

                public bool CanExecute(object parameter) => _canExecute == null || _canExecute((T)parameter);

                public event EventHandler CanExecuteChanged
                {
                    add { CommandManager.RequerySuggested += value; }
                    remove { CommandManager.RequerySuggested -= value; }
                }

                public void Execute(object parameter) => _execute((T)parameter);
            }

            public HistoryViewModel()
            {
                MoveRecords = new ObservableCollection<MoveRecord>();
                SelectCommand = new RelayCommand<MoveRecord>(OnMoveRecordSelected);
                CurrentRecord=new MoveRecord();
                InitializeMoveRecords();
            }

            public void InitializeMoveRecords()
            {
                // 初始化 MoveRecords 的逻辑
                
                MoveRecords.Add(new MoveRecord { MoveString = "505152" });

             
            }
            //清空棋盘操作

            // 调用 Action 的方法
            public void RequestClearChessboard()
            {
                ClearChessboardAction?.Invoke();
            }

            public void OnMoveRecordSelected(MoveRecord moveRecord)
            {
                MessageBox.Show("成功选中");
                if (moveRecord != null)
                {
                    
                    CurrentRecord = moveRecord;
                    App.TemphistoryFromMain = moveRecord.MoveString;
                    MessageBox.Show(CurrentRecord.MoveString);
                    index = 0; // 重置步骤索引
                    RequestClearChessboard();
                    mainpage.window.jumpToTargetPage(mainpage.WindowsID.history);//跳转
                }
                // 清空棋盘
            }

            // 事件，用于通知 View 清空棋盘
            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }




        public class CombinedViewModel : INotifyPropertyChanged
        {
            public MainDataModel MainDataModel { get; set; }
            public HistoryViewModel HistoryViewModel { get; set; }

            public CombinedViewModel()
            {
                MainDataModel = new MainDataModel();
                HistoryViewModel = new HistoryViewModel();
                SelectCommand = HistoryViewModel.SelectCommand;
            }
            public ICommand SelectCommand { get; }
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }




    }

}

public class GameService
{
    private static GameService _instance;
    public ObservableCollection<string> Result { get; } = new ObservableCollection<string>();
    public string singleHistory = null;//单条历史记录信息(最后一位0/1表示胜负，其余每两位表示坐标)
    public string competeType = null;//表示本局对局类型
    private GameService() { }

    public static GameService Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameService();
            }
            return _instance;
        }
    }

    public void getPosition(int x, int y)
    {
       
        singleHistory += x.ToString();
        singleHistory += y.ToString();
    }
    public void winOrfail(bool flag)
    {
        if (flag)
            singleHistory += '1';
        else
            singleHistory += '0';
    }
    public void getCompeteType(string type)
    {
        competeType = type;
    }
    public void clearData()
    {
        singleHistory = null;
        competeType = null;
    }
}

//该类用于获取后台传来的历史记录arraylist并将其拆解为可用于布局的各个元素
public class MoveRecord
{
    public string MoveString { get; set; }
    
   
    // 属性Moves将使用全局方法SplitIntoPairs来获取拆解后的坐标数组

    public string[] Moves
    {
        get
        {
            // 调用全局方法SplitIntoPairs来拆解字符串
            return Utils.SplitStringIntoPairs(MoveString);
        }
    }
}






