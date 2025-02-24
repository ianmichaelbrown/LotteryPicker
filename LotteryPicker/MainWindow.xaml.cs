using System.Windows;

namespace LotteryPicker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var vm = new MainWindowViewModel(SynchronizationContext.Current);
            DataContext = vm;
        }
    }
}