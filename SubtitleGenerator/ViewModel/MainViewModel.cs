using SubtitleGenerator.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SubtitleGenerator.ViewModel
{
    public class MainViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<SubtitleEntry> Entries { get; set; } = [];

        private SubtitleEntry _currentEntry = new();

        public SubtitleEntry CurrentEntry
        {
            get { return _currentEntry; }
            set
            {
                _currentEntry = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            
        }

        #region Property Changed
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
