using CommunityToolkit.Mvvm.ComponentModel;

namespace MobileCarApp.ViewModels.Common;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    string title = string.Empty;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    public bool IsNotBusy => !isBusy;
}

//DOCUMENTATION
//Without ComunityToolkit MVVM
//public class BaseViewModel : INotifyPropertyChanged
//{
//    string _title;
//    bool _isBusy;

//    public bool IsBusy
//    {
//        get => _isBusy;
//        set
//        {
//            if (_isBusy == value) return;
//            _isBusy = value;
//            OnPropertyChanged();
//        }
//    }
//    public string Title
//    {
//        get => _title;
//        set
//        {
//            if (_title == value) return;
//            _title = value;
//            OnPropertyChanged();
//        }
//    }

//    public event PropertyChangedEventHandler? PropertyChanged;
//    //Méthode générique remplaçant le listener habituel
//    public void OnPropertyChanged([CallerMemberName] string name = null)
//    {
//        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
//    }
//}