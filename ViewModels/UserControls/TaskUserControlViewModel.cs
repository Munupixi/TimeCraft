using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TimeCraft.ViewModels.UserControls
{
    internal class TaskUserControlViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Task _task;
        private DataBaseContent _context;

        public ICommand OpenCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public TaskUserControlViewModel(Task _task)
        {
            OpenCommand = new RelayCommand(OpenExecute);
            DeleteCommand = new RelayCommand(DeleteExecute);
            _context = new DataBaseContent();
            this._task = _task;
        }

        public string Title
        {
            get { return _task.Title; }
        }

        public string StartDate
        {
            get { return _task.StartDate.ToString().Substring(0, _task.StartDate.ToString().Length - 7); }
        }

        public string StartTime
        {
            get { return _task.StartTime.ToString(); }
        }

        public string Category
        {
            get { return CategoryViewModel.Get(_task.CategoryId).Title; }
        }
        public bool IsDone
        {
            get { return _task.IsDone; }
            set { _task.IsDone = value; }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void IsDoneChanged()
        {
            _task.IsDone = !_task.IsDone;
            new TaskViewModel(_task).Update();
        }

        internal void OpenExecute()
        {
            new CreateEditTaskWindow(_task).Show();
        }

        internal void DeleteExecute()
        {
            new TaskViewModel(_task).Delete();
        }
    }
}
