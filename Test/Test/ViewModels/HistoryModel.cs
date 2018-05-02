using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Test.ViewModels
{
    class HistoryModel : INotifyPropertyChanged
    {
        private ObservableCollection<HistoryItem> _items = new ObservableCollection<HistoryItem>();
        public ObservableCollection<HistoryItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var propertyChangedCallback = PropertyChanged;
            propertyChangedCallback?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class HistoryItem
        {
            public String Text { get; set; }
            public String Detail { get; set; }
        }
    }
}