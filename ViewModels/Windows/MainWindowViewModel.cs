using System.ComponentModel;
using System.Windows.Input;

namespace TimeCraft.ViewModels.Windows
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private object _currentPage;
        private DataBaseContent appDBContent = new DataBaseContent();

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

        public MainWindowViewModel()
        {
            try
            {
                Category.CreateAllCategories();
            }
            catch { }
            CurrentPage = new AuthorizationPage();
        }
    }
}