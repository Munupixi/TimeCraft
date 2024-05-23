using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCraft.ViewModels.UserControls
{
    internal class ForWeekEventUserControlViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Event _event;
        private DataBaseContent _context;

        public ForWeekEventUserControlViewModel(Event _event)
        {
            this._event = _event;
            _context = new DataBaseContent();
        }

        public int EventId
        {
            get { return _event.EventId; }
        }

        public string Title
        {
            get { return _event.Title; }
        }

        public string Description
        {
            get { return _event.Description; }
        }

        public string StartDate
        {
            get { return _event.StartDate.ToString(); }
        }

        public string EndDate
        {
            get { return _event.EndDate.ToString(); }
        }

        public string StartTime
        {
            get { return _event.StartTime.ToString(); }
        }

        public string EndTime
        {
            get { return _event.EndTime.ToString(); }
        }

        public string Location
        {
            get { return _event.Location; }
        }

        public string DressCode
        {
            get { return _event.DressCode.ToString(); }
        }

        public string Priority
        {
            get { return _event.Priority.ToString(); }
        }

        public string Category
        {
            get { return CategoryViewModel.Get(_event.CategoryId).Title; }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void ExecuteOpen()
        {
            if (_event.UserId != User.ActiveUser.UserId)
            {
                new CreateEditEventWindow(_event, true).Show();
                return;
            }
            new CreateEditEventWindow(_event).Show();
        }
    }
}
