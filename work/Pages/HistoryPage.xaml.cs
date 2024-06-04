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
using static work.mainpage;
using work;
using System.Collections;

namespace work.Pages
{
    /// <summary>
    /// HistoryPage.xaml 的交互逻辑
    /// </summary>
    public partial class HistoryPage : Page, INotifyPropertyChanged
    {

        public ObservableCollection<MoveRecord> MoveRecords { get; set; }
        public MoveRecord CurrentRecord { get; set; }
        private APIService apiService = new APIService();
        public ArrayList overallRecord = new ArrayList();
        private int index=0;
        public ICommand SelectCommand { get; private set; }
        public HistoryPage()
        {
            InitializeComponent();
            InitializeMoveRecords();
            InitializeChessboard();
            SelectCommand = new RelayCommand<MoveRecord>(OnMoveRecordSelected);
            this.DataContext =this; // 设置数据上下文为当前窗口实例
        }

        //初始化棋盘
        private void InitializeChessboard()
        {
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    Button button = new Button();
                    button.Name = $"Button{row}{col}";
                    button.Background = Brushes.Transparent;
                    //button.Click += Button_Click;
                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, col);
                    ChessboardGrid.Children.Add(button);
                    this.RegisterName(button.Name, button);
                }
            }
        }
        //棋盘点击方法
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                button.Background = Brushes.Black;
            }
        }


        //初始化总历史记录收集器
        private void InitializeMoveRecords()
        {
            MoveRecords = new ObservableCollection<MoveRecord>();

            overallRecord.Add("505152");
            overallRecord.Add("535455");
            overallRecord.Add("564636261606");
            if (overallRecord != null)
            {
                foreach (string move in overallRecord)
                {
                    MoveRecords.Add(new MoveRecord { MoveString = move });
                }
            }
        }

        //点击条目触发，锁定单条历史记录条目
        private void OnMoveRecordSelected(MoveRecord moveRecord)
        {
            //清空棋盘内容
            foreach (var child in ChessboardGrid.Children)
            {
                if (child is Button button)
                {
                    button.Background = Brushes.Transparent;
                }
            }
            if (moveRecord != null)
            {
                CurrentRecord = moveRecord;
                index = 0; // 重置步骤索引
            }
        }    

        // INotifyPropertyChanged 接口的实现
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //页面跳转
        public void jumpBackToMain(object sender, RoutedEventArgs e)
        {
            mainpage.window.jumpToTargetPage(WindowsID.home);
        }

        //获取单条历史记录
        public async void getHistoryById(object sender, RoutedEventArgs e)
        {
            var history =  await apiService.getSingleHistory(1);
            MessageBox.Show(history);
                
        }

        //获取所有历史记录
        public async void getHistories(object sender, RoutedEventArgs e)
        {
            var history = await apiService.getHistories(App.user.id);
            string str = "";
            foreach (string item in history) {
                overallRecord.Add(item);
            }
            MessageBox.Show(str);

        }

        
        //插入历史记录
        public async void insertHistory(object sender, RoutedEventArgs e) {
            //把这个替换成要插入的历史记录就行了
            string str = "1,";
            var isSuccess = await apiService.insertHistory(new History(-1,str),App.user.id);
            MessageBox.Show(isSuccess);
        }


        //渲染方法:下一步
        public void nextButton(object sender, RoutedEventArgs e)
        {
            // 读取当前索引处的数组元素值
            if (CurrentRecord != null)
            {
                if (index < CurrentRecord.Moves.Length)
                {

                    if (CurrentRecord.Moves[index] != null)
                    {
                        string buttonName = CurrentRecord.Moves[index];
                        Button targetButton = this.FindName("Button" + buttonName) as Button;
                        if (targetButton != null)
                        {
                            targetButton.Background = Brushes.Black;
                        }
                        index++;
                    }
                }
                else
                {
                    MessageBox.Show("已到最后一步");
                }
            }
                                    
        }
        //渲染方法:上一步                                        
        public void lastButton(object sender,RoutedEventArgs e)
        {
            if(CurrentRecord!=null)
            {
                if (index > 0)
                {
                    index--;
                    if (index < CurrentRecord.Moves.Length)
                    {
                        if (CurrentRecord.Moves[index] != null)
                        {
                            string buttonName = CurrentRecord.Moves[index];
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
        }
    }

    }

    public class GameService
    {
        private static GameService _instance;
        public ObservableCollection<string> Result { get; } = new ObservableCollection<string>();

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
        int turn = 0;
        public static string[] historyStep = new string[42];

        public void getPosition(int x, int y)
        {
            // ... 你的逻辑 ...
            historyStep[turn] = "{x},{y}";
            turn++;
            string moveDescription = (turn % 2 != 0)
                ? $"白子落子位置为{x},{y}"
                : $"黑子落子位置为{x},{y}";
            Result.Add(moveDescription);
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

//命令类
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


