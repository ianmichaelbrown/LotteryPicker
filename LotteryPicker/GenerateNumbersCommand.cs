using System.Windows.Input;

namespace LotteryPicker
{
    internal class GenerateNumbersCommand : ICommand
    {
        private const int NumberOfBalls = 6;
        private const int MaxBallNumber = 59;

        private readonly List<int> generatedNumbers = new();

        public event EventHandler? CanExecuteChanged;

        internal event EventHandler? Generating;
        internal delegate void BallPickedHandler(Ball ball);
        internal event BallPickedHandler? BallPicked;

        public bool CanExecute(object? parameter) => true;

        public async void Execute(object? parameter)
        {
            generatedNumbers.Clear();

            await GenerateNumbersAsync();
        }

        private async Task GenerateNumbersAsync()
        {
            Generating?.Invoke(this, new EventArgs());

            await Task.Run(() =>
            {
                // Pick main numbers
                for (int ballNumber = 1; ballNumber < NumberOfBalls; ballNumber++)
                {
                    generatedNumbers.Add(GenerateNumber());
                }

                // Arrange main numbers in ascending order
                generatedNumbers.Sort();

                // Pick bonus number
                generatedNumbers.Add(GenerateNumber());

                // Notify the UI
                generatedNumbers.ForEach(n => BallPicked?.Invoke(new Ball { Number = n, IsBonus = n == generatedNumbers.Last() }));
            });
        }

        private int GenerateNumber()
        {
            bool isUnique;
            int number;

            Random random = new();
            do
            {
                // Generate random number from 1 - MaxBallNumber (inclusive)
                number = random.Next(MaxBallNumber) + 1;

                isUnique = !generatedNumbers.Contains(number);
            } while (!isUnique);

            return number;
        }
    }
}
