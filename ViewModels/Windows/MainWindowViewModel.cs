using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using TimeCraft.ViewModels.Windows;

namespace TimeCraft.ViewModels.Windows
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static Frame Frame;

        private object _currentPage;
        private DataBaseContent _;

        public object CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPage)));
            }
        }

        public ICommand NavigateCommand { get; private set; }

        public MainWindowViewModel(Frame frame)
        {
            Frame = frame;
            try
            {
                CategoryViewModel.CreateAllCategories();
            }
            catch { }
            CurrentPage = new AuthorizationPage();
        }
    }
}