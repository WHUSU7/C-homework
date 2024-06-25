using System.Collections.ObjectModel;
using System.ComponentModel;
using work.Models;

public class MyViewModel : INotifyPropertyChanged
{
    private ObservableCollection<History> _moveRecords;
    public static ObservableCollection<History> MyRecords { get; private set; }

    public MyViewModel()
    {
        MyRecords = new ObservableCollection<History>
        {
            //new History(1, "Content 1", "12:00", "Type 1", "WIN"),
            //new History(2, "Content 2", "13:00", "Type 2", "LOSS"),
            // new History(3, "Content 3", "14:00", "Type 3", "LOSS"),
            // Add more records as needed
        };
    }

    public static void ClearMoveRecords()
    {
        MyRecords.Clear();
    }
    public static void AddMoveRecord(int id, string content, string time, string type, string result)
    {
        var newRecord = new History(id, content, time, type, result);
        MyRecords.Add(newRecord);
    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
