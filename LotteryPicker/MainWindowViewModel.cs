using System.Collections.ObjectModel;

namespace LotteryPicker
{
    internal class MainWindowViewModel
    {
        private readonly SynchronizationContext? _context;

        internal MainWindowViewModel(SynchronizationContext? synchronizationContext)
        {
            _context = synchronizationContext;

            SelectedNumbers = new();

            GenerateNumbersCommand = new();
            GenerateNumbersCommand.Generating += OnGenerating;
            GenerateNumbersCommand.BallPicked += OnBallPicked;
        }

        public ObservableCollection<Ball>? SelectedNumbers { get; private set; }

        public GenerateNumbersCommand GenerateNumbersCommand { get; private set; }

        private void OnGenerating(object? sender, EventArgs e)
        {
            SelectedNumbers!.Clear();
        }

        private void OnBallPicked(Ball ball)
        {
            _context!.Post((x) => SelectedNumbers!.Add(ball), null);
        }
    }
}
