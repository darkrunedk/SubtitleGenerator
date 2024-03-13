using SubtitleGenerator.ViewModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace SubtitleGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        private readonly SolidColorBrush _statusDefaultColor;
        private readonly SolidColorBrush _error;

        private readonly Key[] _ignoreKeyPress;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = (MainViewModel)DataContext;

            _statusDefaultColor = (SolidColorBrush)Application.Current.FindResource("StatusDefaultColor");
            _error = (SolidColorBrush)Application.Current.FindResource("ErrorColor");

            _ignoreKeyPress =
            [
                Key.Home,
                Key.End,
                Key.Up,
                Key.Down,
                Key.Left,
                Key.Right,
                Key.Escape,
                Key.PageUp,
                Key.PageDown,
            ];
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBoxResult.No;
            if (_viewModel.CurrentEntry.Subtitle.Length >= 64)
                result = MessageBox.Show("Your subtitle text is longer than the recommended of 64 characters for a single subtitle block. Are you sure you want to proceed?", "Subtitle longer than recommended length", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
                return;

            _viewModel.Entries.Add(_viewModel.CurrentEntry);

            var end = _viewModel.Entries.LastOrDefault()?.End;
            if (end.HasValue)
                _viewModel.CurrentEntry = new() { Start = end.Value };
            else
                _viewModel.CurrentEntry = new();
        }

        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            string srtContent = ExportAsSrt();

            var dialog = new Microsoft.Win32.SaveFileDialog()
            {
                Filter = "SubRip Files (*.srt)|*.srt"
            };

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, srtContent, Encoding.UTF8);
            }
        }

        private string ExportAsSrt()
        {
            StringBuilder sb = new();

            for (int i = 0; i < _viewModel.Entries.Count; i++)
            {
                var entry = _viewModel.Entries[i];

                sb.AppendLine((i + 1).ToString());
                string timeLine = $"{entry.Start:hh\\:mm\\:ss\\,fff} --> {entry.End:hh\\:mm\\:ss\\,fff}";
                sb.AppendLine(timeLine);

                var words = entry.Subtitle.Split(" ", System.StringSplitOptions.TrimEntries);

                string captionText = "";
                foreach (var word in words)
                {
                    string newWord = word + " ";
                    if ((captionText.TrimEnd().Length + newWord.Length) <= 32)
                        captionText += word + " ";
                    else
                    {
                        sb.AppendLine(captionText.TrimEnd());
                        captionText = newWord;
                    }
                }

                if (!string.IsNullOrWhiteSpace(captionText))
                    sb.AppendLine(captionText);

                sb.AppendLine();
            }

            return sb.ToString();
        }

        private void SubtitleText_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;

            if (_ignoreKeyPress.Contains(e.Key))
                return;

            if (textBox.Text.Length > 64 && SubtitleStatus.Foreground == _error)
                return;

            if (textBox.Text.Length > 64)
            {
                SubtitleStatus.Foreground = _error;
                SubtitleStatus.Text = "Subtitle longer than recommended length!";
            }
            else
            {
                SubtitleStatus.Foreground = _statusDefaultColor;
                SubtitleStatus.Text = $"All good ({textBox.Text.Length}/64)";
            }
        }
    }
}