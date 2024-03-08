using SubtitleGenerator.ViewModel;
using System.Linq;
using System.Windows;

namespace SubtitleGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = (MainViewModel)DataContext;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Entries.Add(_viewModel.CurrentEntry);

            var end = _viewModel.Entries.LastOrDefault()?.End;
            if (end.HasValue)
                _viewModel.CurrentEntry = new() { Start = end.Value };
            else
                _viewModel.CurrentEntry = new();
        }
    }
}