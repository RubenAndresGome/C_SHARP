namespace Backend.Services
{
    public class RandomService : IRandomService
    {
        private readonly int _value;

        public int Value
        {
            get => _value;
        }

        public RandomService()
        {
            var random = new Random();
            _value = random.Next(1000);
        }

    }
}
