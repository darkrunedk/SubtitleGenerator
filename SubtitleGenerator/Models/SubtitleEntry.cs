using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SubtitleGenerator.Models
{
    public class SubtitleEntry : INotifyPropertyChanged
    {
        private string _subTitle;
        private TimeSpan _start;
        private TimeSpan _end;

        public string Subtitle
        {
            get { return _subTitle; }
            set
            {
                _subTitle = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan Start
        {
            get { return _start; }
            set
            {
                _start = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan End
        {
            get { return _end; }
            set
            {
                _end = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
